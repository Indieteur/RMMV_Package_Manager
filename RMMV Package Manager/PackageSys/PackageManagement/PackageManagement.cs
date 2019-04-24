using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Indieteur.BasicLoggingSystem;
using MakeInstalledPackagesMessages = RMMV_PackMan.LoggerMessages.PackageManagement.MakeInstalledPackages;
using System.Threading;
namespace RMMV_PackMan
{

    static partial class PackageManagement
    {
        
        public delegate void PackageInstallDelegate(RMPackage packageage, bool globalPackage, string packagePath, ProjectPackMan project);
        public delegate void PackageUninstallBeginDelegate(RMPackage packageage, bool globalPackage, string installedDir, InstalledPackage installedPackage, ProjectPackMan project);
        public delegate void PackageUninstalledDelegate(RMPackage packageage, bool globalPackage, InstalledPackage uninstalledPackage, ProjectPackMan project);
        public delegate void PackageReinstallDelegate(InstalledPackage packageReinstall, bool globalPackage, ProjectPackMan project);
        public delegate void PackageUpdateDelegate(InstalledPackage packageUpdate, RMPackage packageage, bool globalPackage, string packagePath, ProjectPackMan project);


        static InstalledPackage _baseGen, _baseProj;

        public static List<InstalledPackage> GlobalPackages { get; private set; } = new List<InstalledPackage>();

       

        public static InstalledPackage BaseGeneratorPackage
        {
            get
            {
                if (_baseGen == null)
                    _baseGen = GlobalPackages.FindByUID(Vars.DEFAULT_GENERATOR_FILE_PACK_UID);
                return _baseGen;
            }
        }

        public static InstalledPackage DefaultProjectFilesPackage
        {
            get
            {
                if (_baseProj == null)
                    _baseProj = GlobalPackages.FindByUID(Vars.DEFAULT_PROJFILE_PACK_UID);
                return _baseProj;
            }
        }


        public static void InitializePackageManagement(this frmMain form)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            string[] directories = null;
            try
            {
                directories = Directory.GetDirectories(PMFileSystem.PackMan_ManDir);
            }
            catch (Exception ex)
            {
                Helper.ShowMessageBox(MessageBoxStrings.PackageManagement.INIT_GLOBAL_GET_DIRS_ERROR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.PackageManagement.GLOBAL_DIR_ERROR + PMFileSystem.PackMan_ManDir + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                return;
            }
            if (directories == null || directories.Length == 0)
                return;

            LogDataList log = new LogDataList();

            
                foreach (string directory in directories)
                {
                    LogDataList outLog = null;
                    try
                    {
                        GlobalPackages.AddSafely(new InstalledPackage(directory, _namespace, out outLog));
                    }
                    catch (Exception ex)
                    {
                        log.WriteWarningLog(LoggerMessages.PackageManagement.InstalledPackage.Error.ErrorTryLoad(directory), _namespace, ex);
                        if (ex is InvalidInstalledPackageFile castedEx)
                        {
                            log.WriteErrorLog(castedEx.Message, _namespace, castedEx.InnerException);
                        }
                        else
                            log.WriteErrorLog(ex.Message, _namespace, ex);

                    }
                    log.AppendLogs(outLog);
                }
            if (log.HasErrorsOrWarnings())
                Helper.ShowMessageBox(MessageBoxStrings.PackageManagement.INIT_ERROR_WARNS_FOUND, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.INIT_GLOBAL_DIR + PMFileSystem.PackMan_ManDir + ".", _namespace);
        }

        delegate bool makeInstPackageAnon();
        public static void MakeInstalledPackageFileForDefPackage(string XmlPath, string _namespace, out LogDataList log, string rootDirectory = null, bool ignoreClash = false)
        {
            log = new LogDataList();
            log.WriteInformationLog(MakeInstalledPackagesMessages.Info.CreateInstalledPack(XmlPath, rootDirectory), _namespace);
            LogDataList outLog = null;
            RMPackage newPack = null;
            Exception exResult;

            try
            {
                newPack = new RMPackage(XmlPath, _namespace, out outLog, rootDirectory);
            }
            catch (Exception ex)
            {
                log.AppendLogs(outLog);
                Logger.WriteErrorLog(MakeInstalledPackagesMessages.Error.InvalidXML(XmlPath, rootDirectory), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                throw;
            }
            log.AppendLogs(outLog);

            string origDir = newPack.XMLDirectory;

            InstalledPackage installedPackage = GlobalPackages.FindByUID(newPack.UniqueID);
            string newPackDir = PMFileSystem.PackMan_ManDir + "\\" + newPack.UniqueIDInMD5;
            if (installedPackage != null)
            {
                if (ignoreClash)
                {
                    Logger.WriteWarningLog(MakeInstalledPackagesMessages.Warning.InstalledPackExistsExit(newPack.Name), _namespace, null);
                    return;
                }
                else
                {
                    log.WriteWarningLog(MakeInstalledPackagesMessages.Warning.InstalledPackExistsReplace(newPack.Name), _namespace);
                    if (Helper.DeleteFolderSafely(newPackDir, _namespace, out exResult, new DeleteFolderLogMessages(deleteFailed: MakeInstalledPackagesMessages.Error.UnableToDeleteExistingFolder)) == DeleteFolderResult.UserCancelled)
                        throw exResult;
                }
            }

          

            string tempPackDir = PMFileSystem.PackMan_TempDir + "\\" + newPack.UniqueIDInMD5;

            if (Helper.DeleteFolderSafely(tempPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled)
                throw exResult;

            if (Helper.CreateFolderSafely(newPackDir, _namespace, out exResult, new CreateFolderLogMessages(createFailed: MakeInstalledPackagesMessages.Error.UnableCreateRootDir)) == CreateFolderResult.UserCancelled)
                throw exResult;
            
            if (Helper.CreateFolderSafely(tempPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
                throw exResult;

            string backupXMLInstallerPath = tempPackDir + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME;

          

            makeInstPackageAnon onErrorBackup = delegate ()
            {
                if (Helper.ShowMessageBox(MessageBoxStrings.PackageManagement.FailedMakeInstallPackageArch(newPack.Name), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    return true;

                Logger.WriteErrorLog(MakeInstalledPackagesMessages.Error.UserRequiredOnFailedBackUpNo(newPack.Name), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                Helper.DeleteFolderSafely(tempPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                Helper.DeleteFolderSafely(newPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_UNUSED_DIR_ARG);
                return false;
            };

         

            string rootPathOfFiles;
            if (rootDirectory == null)
                rootPathOfFiles = newPack.XMLDirectory;
            else
                rootPathOfFiles = rootDirectory;

            PackageUtil.ExplicitCopyAssetsAndLicFileTo(newPack, rootPathOfFiles, tempPackDir, _namespace);

            try
            {
                newPack.SaveToFile(backupXMLInstallerPath, _namespace, RMPackage.SaveToFileMode.ImplicitAssetInfo, 
                    logMessage: new WriteAllTextLogMessages(writeFailed: MakeInstalledPackagesMessages.Error.UnableCreateInstXML, logGroup: log));
            }
            catch (Exception ex)
            {
                if (onErrorBackup())
                    goto onerrorContinue;
                throw;

            }

            string archDest = newPackDir + "\\" + Vars.INSTALLED_ARCH_FILENAME;
            try
            {
                ArchiveManagement.CreateNewZip(tempPackDir, archDest,_namespace);
            }
            catch (Exception ex)
            {
                log.WriteWarningLog(MakeInstalledPackagesMessages.Error.UnableMakeBackup(newPack.Name, tempPackDir, archDest), _namespace, ex);
                if (onErrorBackup())
                    goto onerrorContinue;
                throw;
            }

            onerrorContinue:
            //newPack.Installed = true;
            //newPack.SetInstalledPropertyAll(RMPackObject.InstallStatus.Installed);
        
            if (newPack.License != null && newPack.License.LicenseSource == RMPackLic.Source.File && RMPackLic.IsAValidLicenseSourceFile(newPack.License.Data, origDir))
            {
                string licSourcePath = origDir + "\\" + newPack.License.Data;
                string fileName = Path.GetFileName(licSourcePath);
                string destPath = newPackDir + "\\" + fileName;
                CopyFileResult copyRes = Helper.CopyFileSafely(licSourcePath, destPath, true, _namespace, out exResult, new CopyFileLogMessages(copyFileFailed: MakeInstalledPackagesMessages.Error.UnableCopyLicenseFile));
                if (copyRes == CopyFileResult.UserCancelled || copyRes == CopyFileResult.SourceFileNotFound)
                {
                    Helper.DeleteFolderSafely(tempPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                    Helper.DeleteFolderSafely(newPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_UNUSED_DIR_ARG);
                    throw exResult;
                }
                newPack.License.Data = fileName;

            }
            string installedXMLDest = newPackDir + "\\" + Vars.INSTALLED_XML_FILENAME;
            newPack.Implicit = false;
            try
            {
                newPack.SaveToFile(installedXMLDest, _namespace,  RMPackage.SaveToFileMode.ExplicitAssetInfo, 
                    logMessage: new WriteAllTextLogMessages(writeFailed: MakeInstalledPackagesMessages.Error.UnableMakeMainXML));
            }
            catch (Exception ex)
            {
                Helper.DeleteFolderSafely(tempPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                Helper.DeleteFolderSafely(newPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_UNUSED_DIR_ARG);
                throw;
            }

            try
            {
                GlobalPackages.AddSafely(new InstalledPackage(newPackDir, _namespace, out outLog));
            }
            catch (Exception ex)
            {
                log.WriteWarningLog(LoggerMessages.PackageManagement.InstalledPackage.Error.ErrorTryLoad(newPackDir), _namespace, ex);
                if (ex is InvalidInstalledPackageFile castedEx)
                {
                    log.WriteErrorLog(castedEx.Message, _namespace, castedEx.InnerException);
                }
                else
                    log.WriteErrorLog(ex.Message, _namespace, ex);

            }
            log.AppendLogs(outLog);

            try
            {
                NewProject.CopyPackageInstallInfo(tempPackDir, _namespace, out outLog);
            }
            catch (Exception ex)
            {
                log.WriteWarningLog(MakeInstalledPackagesMessages.Warning.UnableCopyNewProj(newPack.Name, newPackDir), _namespace, ex);
            }
            log.AppendLogs(outLog);


            Helper.DeleteFolderSafely(tempPackDir, _namespace, out exResult, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
            log.WriteInformationLog(MakeInstalledPackagesMessages.Info.SUCCESS_CREATE + newPack.Name + ".", _namespace);
        }




        public class InstallFileNotFoundException : Exception
        {
            public string PackagePath;
            public InstallFileNotFoundException(string message, string packagePath) : base(message)
            {
                PackagePath = packagePath;
            }
        }


    }
}
