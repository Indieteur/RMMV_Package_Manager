using Indieteur.BasicLoggingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstallerMessages = RMMV_PackMan.LoggerMessages.PackageManagement.Installer;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static partial class Installer
        {
            public static event PackageInstallDelegate OnPackageInstallBegin;
            public static event PackageInstallDelegate OnPackageInstalled;

            public static InstalledPackage InstallLocalPackage(string packagePath, string projectPath, string _namespace, out LogDataList log, bool ignoreClash = false, bool alreadyCopiedToTemp = false, bool procEvents = true)
            {
                if (string.IsNullOrWhiteSpace(projectPath))
                {
                    try
                    {
                        throw new ArgumentNullException(ExceptionMessages.General.PROJ_PATH_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_OPEN_LOCAL_PACKAGE_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                log = new LogDataList();
                ProjectPackMan openedProject = null;
                LogDataList outLog = null;
                try
                {
                    openedProject = new ProjectPackMan(projectPath, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_OPEN_LOCAL_PACKAGE + projectPath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage retVal = InstallLocalPackage(packagePath, openedProject, _namespace, out outLog, ignoreClash, alreadyCopiedToTemp, procEvents);
                log.AppendLogs(outLog);
                return retVal;
            }

            public static InstalledPackage InstallLocalPackage(string packagePath, ProjectPackMan openProject, string _namespace, out LogDataList log, bool ignoreClash = false, bool alreadyCopiedToTemp = false, bool procEvents = true, bool skipFileExistenceCheck = false)
            {

                log = new LogDataList();

                if (string.IsNullOrWhiteSpace(packagePath))
                {
                    try
                    {
                        throw new ArgumentNullException(ExceptionMessages.General.PACK_PATH_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(InstallerMessages.Error.PACKAGE_PATH_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (openProject == null || string.IsNullOrWhiteSpace(openProject.DirectoryPath))
                {
                    try
                    {
                        throw new ArgumentException(ExceptionMessages.General.OPEN_PROJ_ARG_OR_DIRPATH_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(InstallerMessages.Error.NO_OPEN_PROJECT, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                Logger.WriteInformationLog(InstallerMessages.Information.PACKAGE_INSTALL_START_L + packagePath + ".", _namespace);
                Exception outEx;
                LogDataList outLog = null;
                string oldPackagePath = packagePath;

                try
                {
                    if (!alreadyCopiedToTemp)
                        packagePath = InitPackageInstaller(packagePath, false, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.INIT_ERROR + packagePath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }
                log.AppendLogs(outLog);

                RMPackage package = null;
                try
                {
                    package = new RMPackage(packagePath, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.AppendLogs(outLog);
                    Logger.WriteErrorLog(InstallerMessages.Error.XML_READ_ERROR + packagePath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }
                log.AppendLogs(outLog);

                if (!skipFileExistenceCheck)
                {
                    PerformPackageFilesExistenceCheck(package, _namespace, out outLog);
                    if (outLog != null)
                        log.Logs.AddRange(outLog.Logs);
                }

                if (!ignoreClash && openProject.InstalledPackages.FindByUID(package.UniqueID) != null)
                {
                    try
                    {
                        throw new PackageAlreadyExistsException(ExceptionMessages.General.PackWIDExists(package.UniqueID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(InstallerMessages.Error.PACKAGE_ALREADY_EXISTS, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        if (procEvents)
                            CleanupTempInstallDir(_namespace, false);
                        throw;
                    }
                }

                if (procEvents && OnPackageInstallBegin != null)
                    OnPackageInstallBegin.Invoke(package, false, oldPackagePath, openProject);

                try
                {
                    PerformInstallLocal(package, openProject.DirectoryPath, _namespace);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.INSTALL_PACK_FAILED, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }

                string packManStorePath = openProject.DirectoryPath + "\\" + Vars.PACKAGE_MANAGER_DIRECTORY;

                if (!Directory.Exists(packManStorePath) && Helper.CreateFolderSafely(packManStorePath, _namespace, out outEx, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                    throw outEx;
                InstalledPackage newInstall = null;
                try
                {
                    newInstall = CreateLocalInstalledFile(package, Path.GetDirectoryName(packagePath), packManStorePath, _namespace, out outLog);
                    openProject.InstalledPackages.AddSafely(newInstall);
                }
                catch
                {
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }
                if (outLog != null)
                    log.Logs.AddRange(outLog.Logs);

                if (!alreadyCopiedToTemp)
                    CleanupTempInstallDir(_namespace, false);

                if (procEvents && OnPackageInstalled != null)
                    OnPackageInstalled.Invoke(package, false, oldPackagePath, openProject);

                Logger.WriteInformationLog(InstallerMessages.Information.PACKAGE_INSTALL_DONE_L, _namespace);
                return newInstall;
            }

            static void PerformInstallLocal(RMPackage package, string installPath, string _namespace)
            {
    
                if (package.Collections != null)
                    foreach (RMCollection collection in package.Collections)
                        try
                        {
                            ProcessRMCollectionInstall(collection, installPath, package.XMLDirectory, _namespace);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_INSTALL_COLLECTION, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                //package.Installed = true;

            }
            static InstalledPackage CreateLocalInstalledFile(RMPackage package, string sourceFolder, string packManStorePath, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                string newInstalledDir = packManStorePath + "\\" + package.UniqueIDInMD5;
                LogDataList outLog = null;
                try
                {
                    CreateInstalledFile(package, sourceFolder, newInstalledDir, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_CREATE_INSTALLED_FILES, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage retVal = null;
                try
                {
                    retVal = new InstalledPackage(newInstalledDir, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.WriteWarningLog(InstallerMessages.Warning.UNABLE_LOAD_PACK, _namespace, ex);
                }
                log.AppendLogs(outLog);
                return retVal;
            }

            // -------- Global Packages -------- //
            public static InstalledPackage InstallGlobalPackage(string packagePath, string _namespace, out LogDataList log, bool ignoreClash = false, bool alreadyCopiedToTemp = false, bool procEvents = true, bool skipFileExistenceCheck = false)
            {
                Logger.WriteInformationLog(InstallerMessages.Information.PACKAGE_INSTALL_START_G + packagePath + ".", _namespace);
                log = new LogDataList();
                string oldPackagePath = packagePath;
                LogDataList outLog = null;

                try
                {
                    if (!alreadyCopiedToTemp)
                        packagePath = packagePath = InitPackageInstaller(packagePath, true, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.INIT_ERROR + packagePath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }
                log.AppendLogs(outLog);

                RMPackage package = null;
                try
                {
                    package = new RMPackage(packagePath, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.AppendLogs(outLog);
                    Logger.WriteErrorLog(InstallerMessages.Error.XML_READ_ERROR + packagePath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }
                log.AppendLogs(outLog);

                if (!skipFileExistenceCheck)
                {
                    PerformPackageFilesExistenceCheck(package, _namespace, out outLog);
                    log.AppendLogs(outLog);
                }

                if (!ignoreClash && GlobalPackages.FindByUID(package.UniqueID) != null)
                {
                    try
                    {
                        throw new PackageAlreadyExistsException(ExceptionMessages.General.PackWIDExists(package.UniqueID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(InstallerMessages.Error.PACKAGE_ALREADY_EXISTS, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        if (procEvents)
                            CleanupTempInstallDir(_namespace, false);
                        throw;
                    }
                }

                if (procEvents && OnPackageInstallBegin != null)
                    OnPackageInstallBegin.Invoke(package, true, oldPackagePath, null);

                try
                {
                    PerformInstallGlobal(package, _namespace);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.INSTALL_PACK_FAILED, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }

                InstalledPackage newInstall = null;
                try
                {
                    newInstall = CreateGlobalInstalledFile(package, Path.GetDirectoryName(packagePath), _namespace, out outLog);
                    GlobalPackages.AddSafely(newInstall);
                }
                catch
                {
                    if (procEvents)
                        CleanupTempInstallDir(_namespace, false);
                    throw;
                }
                log.AppendLogs(outLog);

                if (!alreadyCopiedToTemp)
                    CleanupTempInstallDir(_namespace, false);

                if (OnPackageInstalled != null)
                    OnPackageInstalled.Invoke(package, true, oldPackagePath, null);
                Logger.WriteInformationLog(InstallerMessages.Information.PACKAGE_INSTALL_DONE_G, _namespace);
                return newInstall;
            }

            static void PerformInstallGlobal(RMPackage package, string _namespace)
            {
                if (package.Collections != null)
                    foreach (RMCollection collection in package.Collections)
                        if (collection is RMGeneratorCollection)
                        {
                            try
                            {
                                Generator.InstallGeneratorParts(PMFileSystem.MV_Installation_Directory, package.XMLDirectory, collection as RMGeneratorCollection, _namespace);
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_INSTALL_GEN, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                throw;
                            }
                        }
                        else
                        {
                            try
                            {
                                ProcessRMCollectionInstall(collection, PMFileSystem.MV_NewData_Dir, package.XMLDirectory, _namespace);
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_INSTALL_COLLECTION, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                throw;
                            }
                        }
               // package.Installed = true;

            }

            static InstalledPackage CreateGlobalInstalledFile(RMPackage package, string sourceFolder, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                string newInstalledDir = PMFileSystem.PackMan_ManDir + "\\" + package.UniqueIDInMD5;

                LogDataList outLog = null;
                try
                {
                    CreateInstalledFile(package, sourceFolder, newInstalledDir, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(InstallerMessages.Error.UNABLE_CREATE_INSTALLED_FILES, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage retVal = null;
                try
                {
                    retVal = new InstalledPackage(newInstalledDir, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.WriteWarningLog(InstallerMessages.Warning.UNABLE_LOAD_PACK, _namespace, ex);
                }
                if (outLog != null)
                    log.Logs.AddRange(outLog.Logs);

                try
                {
                    NewProject.CopyPackageInstallInfo(sourceFolder, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.WriteWarningLog(InstallerMessages.Warning.UNABLE_COPY_NEWDATA_DIR, _namespace, ex);
                }
                if (outLog != null)
                    log.Logs.AddRange(outLog.Logs);
                return retVal;
            }
            // ---- Helper Methods ---- //
            static void CreateInstalledFile(RMPackage package, string sourceFolder, string newInstalledDir, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                log.WriteInformationLog(InstallerMessages.Information.CREATE_INSTALLED_FILE_START, _namespace);
                Exception outEx;
                if (Helper.CreateFolderSafely(newInstalledDir, _namespace, out outEx, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                    throw outEx;

                try
                {
                    Helper.DeleteEmptySubDir(sourceFolder, _namespace);
                }
                catch (Exception ex)
                {
                    log.WriteWarningLog(InstallerMessages.Warning.DELETE_SUB_DIR + sourceFolder + ".", _namespace, ex);
                }

                string destArch = newInstalledDir + "\\" + Vars.INSTALLED_ARCH_FILENAME;

                try
                {
                    ArchiveManagement.CreateNewZip(sourceFolder, destArch, _namespace);
                }
                catch (Exception ex)
                {
                    log.WriteWarningLog(InstallerMessages.Warning.UnableMakeZip(destArch), _namespace, ex);
                }

                if (package.License == null)
                    log.WriteWarningLog(InstallerMessages.Warning.LIC_SOURCE_NULL, _namespace);
                else
                {
                    if (package.License.LicenseSource == RMPackLic.Source.File)
                    {
                        if (RMPackLic.IsAValidLicenseSourceFile(package.License.Data, package.XMLDirectory))
                        {
                            string licSourcePath = package.XMLDirectory + "\\" + package.License.Data;
                            string fileName = null;
                            try
                            {
                                fileName = Path.GetFileName(licSourcePath);
                            }
                            catch (Exception ex)
                            {
                                package.License = null;
                                log.WriteWarningLog(InstallerMessages.Warning.INVALID_FILE_NAME_LIC, _namespace, ex);
                                goto skipLic;
                            }
                            string destPath = newInstalledDir + "\\" + fileName;

                            if (Helper.CopyFileSafely(licSourcePath, destPath, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: InstallerMessages.Warning.UnableCopyLicFile, logGroup: log)) != CopyFileResult.Success)
                            {
                                package.License = null;
                                goto skipLic;
                            }
                            package.License.Data = fileName;
                        }
                        else
                        {
                            package.License = null;
                            log.WriteWarningLog(InstallerMessages.Warning.INVALID_LIC, _namespace);
                        }
                    }
                }
                skipLic:
                string saveToXMLPath = newInstalledDir + "\\" + Vars.INSTALLED_XML_FILENAME;
                package.Implicit = false;
                package.SaveToFile(saveToXMLPath, _namespace, RMPackage.SaveToFileMode.ExplicitAssetInfo, 
                    logMessage: new WriteAllTextLogMessages(writeFailed: InstallerMessages.Error.UnableSaveInstalledXml));
                log.WriteInformationLog(InstallerMessages.Information.CREATE_INSTALLED_FILE_DONE, _namespace);

            }

            public static void PerformPackageFilesExistenceCheck(RMPackage package, string _namespace, out LogDataList outLog)
            {
                outLog = null;
                if (!package.Implicit)
                {
                    List<BoolAndRMFile> fileExistenceCheckRes = package.CheckFileExistences(package.XMLDirectory);
                    if (fileExistenceCheckRes.HasNonExistingFile())
                    {
                        try
                        {
                            throw new FileNotFoundException(ExceptionMessages.PackageManagement.Installer.ASSETS_NOT_FOUND);
                        }
                        catch
                        {
                            outLog = fileExistenceCheckRes.MakeLogSet(InstallerMessages.Error.MISS_FILE_PREFIX, _namespace);
                            throw;
                        }
                    }
                }
            }

            public static string InitPackageInstaller(string packagePath, bool globalPackageInstall, string _namespace, out LogDataList log, bool performChecksumMatching = false)
            {
                log = null;
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.InitPackageInstaller.Info.INIT_START, _namespace);
                Exception outEx;

                if (Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled) // Null arg
                    throw outEx;

                if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)  // Null arg
                    throw outEx;

                string origPackPath = packagePath;

                if (!File.Exists(packagePath))
                {
                    try
                    {
                        throw new InstallFileNotFoundException(ExceptionMessages.PackageManagement.Installer.INSTALLER_FILE_NOT_FOUND + packagePath + ".", packagePath);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.InitPackageInstaller.Error.MissInstallFile(packagePath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (Path.GetExtension(packagePath).ToLower() == "." + RMPConstants.MiscFileExtensions.ZIP)
                {
                    try
                    {
                        ArchiveManagement.ExtractZip(packagePath, PMFileSystem.PackMan_TempInstall, _namespace, performChecksumMatching);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.InitPackageInstaller.Error.ExtractZipFailed(packagePath, PMFileSystem.PackMan_TempInstall), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                    packagePath = PMFileSystem.PackMan_TempInstall + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME;

                    if (!globalPackageInstall)
                    {
                        RMPackage packageParsed = null;

                        try
                        {
                            packageParsed = new RMPackage(packagePath, _namespace, out log);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(InstallerMessages.Error.FAIL_PACKAGE_PARSE + origPackPath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }

                        if (packageParsed.Collections == null || packageParsed.Collections.Count == 0 || packageParsed.ContentsSummary == RMCollectionType.Generator)
                        {
                            try
                            {
                                throw new EmptyPackageException();
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(InstallerMessages.Error.EMPTY_PACKAGE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                throw;
                            }
                        }

                        if (packageParsed.ContentsSummary.HasFlag(RMCollectionType.Generator))
                        {
                            PackageUtil.RemoveGeneratorFilesAndCollection(packageParsed, PMFileSystem.PackMan_TempInstall, _namespace);
                             packageParsed.SaveToFile(packagePath, _namespace, logMessage: new WriteAllTextLogMessages(writeFailed: InstallerMessages.Error.SaveCopyXMLFailed));
                        }
                    }
                  
                }
                else
                {
                    string packageDir = Path.GetDirectoryName(packagePath);

                    RMPackage packageParsed = null;

                    try
                    {
                        packageParsed = new RMPackage(packagePath, _namespace, out log, null, false, !globalPackageInstall);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(InstallerMessages.Error.FAIL_PACKAGE_PARSE + origPackPath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }

                   
                    try
                    {
                        PackageUtil.ExplicitCopyAssetsAndLicFileTo(packageParsed, packageDir, PMFileSystem.PackMan_TempInstall, _namespace);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.InitPackageInstaller.Error.CopyPackageFilesFailed(packageDir, PMFileSystem.PackMan_TempInstall), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }

                    if (packageParsed.Collections == null || packageParsed.Collections.Count == 0)
                    {
                        try
                        {
                            throw new EmptyPackageException();
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(InstallerMessages.Error.EMPTY_PACKAGE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }

                    packagePath = PMFileSystem.PackMan_TempInstall + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME;
                    packageParsed.SaveToFile(packagePath, _namespace, logMessage: new WriteAllTextLogMessages(writeFailed: InstallerMessages.Error.SaveCopyXMLFailed));
                }
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.InitPackageInstaller.Info.INIT_DONE, _namespace);
                return packagePath;
            }

            static void ProcessRMCollectionInstall(RMCollection collection, string installPath, string sourcePath, string _namespace)
            {
                if (collection is RMAudioCollection)
                    Audio.InstallAudioCollection(installPath, sourcePath, collection as RMAudioCollection, _namespace);
                else if (collection is RMDataCollection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Data, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMAnimationCollection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Animation, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMBattleBacks1_Collection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.BattleBacks_1, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMBattleBacks2_Collection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.BattleBacks_2, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMParallaxCollection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Parallax, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMPictureCollection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Pictures, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMSysImageCollection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.System, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMTitles1_Collection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Titles_1, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMTitles2_Collection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Titles_2, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMPluginsCollection)
                    SingleFile.InstallSingleFileCollection(installPath, sourcePath, RMSingleFileCollection.CollectionType.Plugins, collection as RMSingleFileCollection, _namespace);
                else if (collection is RMCharImageCollection)
                    Characters.InstallCharacters(installPath, sourcePath, collection as RMCharImageCollection, _namespace);
                else if (collection is RMTilesetCollection)
                    Tilesets.InstallTileset(installPath, sourcePath, collection as RMTilesetCollection, _namespace);
                else if (collection is RMMovieCollection)
                    Movies.InstallMovie(installPath, sourcePath, collection as RMMovieCollection, _namespace);
            }
            public static void CleanupTempInstallDir(string _namespace, bool throwErrorOnException = true)
            {
                if (!Directory.Exists(PMFileSystem.PackMan_TempInstall))
                    return;
                Exception outEx;
                if (Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled && throwErrorOnException)
                    throw outEx;
            }



        }
    }
}
