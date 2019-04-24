using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static class Updater
        {
            public static event PackageUpdateDelegate OnPackageUpdateBegin;
            public static event PackageUpdateDelegate OnPackageUpdateDone;


            public enum UpdaterResult
            {
                Successful,
                PackageNotInstalled,
                ProjectNotFoundOrNull
            }

            public enum VersionCompareResult
            {
                NewerVersion,
                SameVersion,
                OlderVersion,
                Unknown,
                PackageNotInstalled,
                InvalidProjectProvided
            }

            public static InstalledPackage UpdateLocalPackage(string pathToInstaller, string pathToProject, bool packageAlreadyCopiedToTem, string _namespace, out LogDataList log, bool skipInstallFileExistenceCheck = false)
            {
                if (string.IsNullOrWhiteSpace(pathToProject))
                {
                    try
                    {
                        throw new ArgumentNullException(ExceptionMessages.General.PROJ_PATH_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Uninstaller.Error.UNABLE_OPEN_LOCAL_PACKAGE_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                log = new LogDataList();
                LogDataList outLog = null;
                ProjectPackMan openedProject = null;
                try
                {
                    openedProject = new ProjectPackMan(pathToProject, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Uninstaller.Error.UNABLE_OPEN_LOCAL_PACKAGE + pathToProject + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage retVal = UpdateLocalPackage(pathToInstaller, openedProject, packageAlreadyCopiedToTem, _namespace, out outLog, skipInstallFileExistenceCheck);
                log.AppendLogs(outLog);
                return retVal;
            }

            public static InstalledPackage UpdateLocalPackage(string pathToInstaller, ProjectPackMan openProject, bool packageAlreadyCopiedToTemp, string _namespace, out LogDataList log, bool skipInstallFileExistenceCheck = false)
            {
                log = new LogDataList();
                if (openProject == null)
                {
                    try
                    {
                        throw new NullProjectException(ExceptionMessages.General.OPEN_PROJ_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.NO_OPEN_PROJ, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                if (string.IsNullOrWhiteSpace(openProject.DirectoryPath))
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.General.OPEN_PROJ_DIR_PATH_ARG_NULL, openProject.DirectoryPath);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.INVALID_OPEN_PROJ_DIR, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string origPath = pathToInstaller;
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Updater.Information.UpdatePackageStart(openProject.DirectoryPath, pathToInstaller), _namespace);
                LogDataList outLog = null;
                try
                {
                    if (!packageAlreadyCopiedToTemp)
                        pathToInstaller = Installer.InitPackageInstaller(pathToInstaller, false, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.UNABLE_EXTRACT_LOCAL + pathToInstaller + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);


                RMPackage package = null;
                try
                {
                    package = new RMPackage(pathToInstaller, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.AppendLogs(outLog);
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.UNABLE_PARSE_XML_LOCAL + pathToInstaller + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                if (openProject.InstalledPackages == null)
                {
                    try
                    {
                        throw new PackageNotFoundException(false, ExceptionMessages.General.PROJ_NO_PACKAGES);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.OPEN_PROJ_NOT_ABLE_FIND_PACK, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                InstalledPackage packageInstalled = openProject.InstalledPackages.FindByUID(package.UniqueID);
                if (packageInstalled == null)
                {
                    try
                    {
                        throw new PackageNotFoundException(false, ExceptionMessages.General.PackWIDNotFound(package.UniqueID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.OPEN_PROJ_NOT_ABLE_FIND_PACK, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (OnPackageUpdateBegin != null)
                    OnPackageUpdateBegin.Invoke(packageInstalled, package, false, origPath, openProject);

                
                try
                { 
                    Uninstaller.UninstallLocalPackage(openProject, package.UniqueID, _namespace, out outLog, procEvents: false);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.FAILED_UNINSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }
                log.AppendLogs(outLog);


                InstalledPackage newlyInstalledPackage = null;
                try
                {
                    newlyInstalledPackage = Installer.InstallLocalPackage(pathToInstaller, openProject, _namespace, out outLog, true, true, false, skipInstallFileExistenceCheck);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.FAILED_INSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }
                log.AppendLogs(outLog);

                if (OnPackageUpdateDone != null)
                    OnPackageUpdateDone.Invoke(newlyInstalledPackage, newlyInstalledPackage.Package, false, origPath, openProject);

                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Updater.Information.UpdatePackageDone(origPath), _namespace);
                return newlyInstalledPackage;
            }

            public static InstalledPackage UpdateGlobalPackage(string pathToInstaller, string _namespace, out LogDataList log, bool alreadyCopiedToTemp = false, bool skipInstallFileExistenceCheck = false)
            {
                log = new LogDataList();
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Updater.Information.UPDATE_PACKAGE_START_G + pathToInstaller + ".", _namespace);
                string origPath = pathToInstaller;
                LogDataList outLog = null;

                try
                {
                    if (!alreadyCopiedToTemp)
                        pathToInstaller = Installer.InitPackageInstaller(pathToInstaller, true, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.UNABLE_EXTRACT_GLOBAL + pathToInstaller + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                RMPackage package = null;

                try
                {
                    package = new RMPackage(pathToInstaller, _namespace, out outLog);
                }
                catch  (Exception ex)
                {
                    log.AppendLogs(outLog);
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.XML_INVALID + pathToInstaller + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                if (GlobalPackages == null)
                {
                    try
                    {
                        throw new NullGlobalPackagesException();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.NO_GLOBAL_PACKAGES, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                InstalledPackage packageInstalled = GlobalPackages.FindByUID(package.UniqueID);
                if (packageInstalled == null)
                {
                    try
                    {
                        throw new PackageNotFoundException(true);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteWarningLog(LoggerMessages.PackageManagement.Updater.Warning.PACKAGE_TO_BE_UPDATED_NOT_FOUND, _namespace, ex);
                        throw;
                    }
                }

                

                if (OnPackageUpdateBegin != null)
                    OnPackageUpdateBegin.Invoke(packageInstalled, package, true, origPath, null);

               
                try
                {
                    Uninstaller.UninstallGlobalPackage(package.UniqueID, _namespace, out outLog, renumberParts: false, procEvents: false);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.FAILED_UNINSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage newlyInstalledPackage = null;
                try
                {
                    newlyInstalledPackage = Installer.InstallGlobalPackage(pathToInstaller, _namespace, out outLog, true, true, false, skipInstallFileExistenceCheck);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Updater.Error.FAILED_INSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }
                log.AppendLogs(outLog);


                if (OnPackageUpdateDone != null)
                    OnPackageUpdateDone.Invoke(newlyInstalledPackage, newlyInstalledPackage.Package, true, origPath, null);

                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Updater.Information.UPDATE_PACKAGE_DONE_G, _namespace);
                return newlyInstalledPackage;
            }

            #region Version Checking method. Might add in a future version of the app.
            //public static VersionCompareResult CheckIfLocalPackageIsNewer(string pathToPackage, string pathToProject)
            //{
            //    if (!Directory.Exists(pathToProject))
            //        return VersionCompareResult.InvalidProjectProvided;
            //    ProjectPackMan openedProject = new ProjectPackMan(pathToProject);
            //    return CheckIfLocalPackageIsNewer(pathToPackage, openedProject);
            //}

            //public static VersionCompareResult CheckIfLocalPackageIsNewer(string pathToPackage, ProjectPackMan openProject)
            //{
            //    string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            //    pathToPackage = Installer.InitPackageInstaller(pathToPackage, _namespace);
            //    RMLogDataset tempLog;
            //    RMPackage package = new RMPackage(pathToPackage, _namespace, out tempLog);
            //    if (openProject == null)
            //        return VersionCompareResult.InvalidProjectProvided;
            //    if (openProject.InstalledPackages == null)
            //        return VersionCompareResult.PackageNotInstalled;
            //    InstalledPackage packageInstalled = openProject.InstalledPackages.FindByUID(package.UniqueID);
            //    return VersionComparePackageAndInstalledVersion(packageInstalled, package);
            //}

            //public static VersionCompareResult CheckIfGlobalPackageIsNewer(string pathToPackage)
            //{
            //    string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            //    pathToPackage = Installer.InitPackageInstaller(pathToPackage, _namespace);
            //    RMLogDataset tempLog;
            //    RMPackage package = new RMPackage(pathToPackage, _namespace, out tempLog);
            //    if (GlobalPackages == null)
            //        return VersionCompareResult.PackageNotInstalled;
            //    InstalledPackage packageInstalled = GlobalPackages.FindByUID(package.UniqueID);
            //    return VersionComparePackageAndInstalledVersion(packageInstalled, package);
            //}

            //static VersionCompareResult VersionComparePackageAndInstalledVersion(InstalledPackage packageInstalled, RMPackage package)
            //{
            //    if (packageInstalled == null)
            //        return VersionCompareResult.PackageNotInstalled;
            //    if (string.IsNullOrWhiteSpace(package.Version) || packageInstalled.Package == null || string.IsNullOrWhiteSpace(packageInstalled.Package.Version))
            //        return VersionCompareResult.Unknown;
            //    if (packageInstalled.Package.Version.ToLower() == package.Version.ToLower())
            //        return VersionCompareResult.SameVersion;

            //    float installedPackageVersionFloat, packageCheckingVersionFloat;
            //    if (!float.TryParse(packageInstalled.Package.Version, out installedPackageVersionFloat))
            //        return VersionCompareResult.Unknown;
            //    if (!float.TryParse(package.Version, out packageCheckingVersionFloat))
            //        return VersionCompareResult.Unknown;

            //    if (packageCheckingVersionFloat == installedPackageVersionFloat)
            //        return VersionCompareResult.SameVersion;
            //    else if (packageCheckingVersionFloat > installedPackageVersionFloat)
            //        return VersionCompareResult.NewerVersion;
            //    else
            //        return VersionCompareResult.OlderVersion;
            //}
            #endregion
        }
    }
}
