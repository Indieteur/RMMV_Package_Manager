using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;
using System.IO;
using System.Threading;

namespace RMMV_PackMan.GUI
{
    public class GlobalOrLocalSharedControls : PackageInfoControls
    {
        enum ThreadResult
        {
            Success,
            Reset,
            Cancel
        }
        public ListBox listCurInstalled;
        public Button btnInstallNew;
        public Button btnRemPack;
        public TextBox txtSearch;
        public Button btnSearch;
        public GroupBox groupInfo;
        public Button btnUpdatePackage;
        public Button btnReinstall;
        public Label lblCurInstalled;
        public Button btnViewAssets;
        public Button btnShowAllPack;

        List<InstalledPackage> allPackages = new List<InstalledPackage>();

        public void LoadPackages(List<InstalledPackage> listOfPackages, string _namespace)
        {
            LoadInfoInstalledPackage(null as RMPackage, _namespace);
            listCurInstalled.Items.Clear();
            allPackages.Clear();
            if (listOfPackages == null || listOfPackages.Count == 0)
                return;
            foreach (InstalledPackage pack in listOfPackages)
            {
                listCurInstalled.Items.Add(pack);
                allPackages.Add(pack);
            }
         
        }

        DialogResult AskUserBackup(Form parentForm, SaveFileDialog saveFileDialog, bool globalPackage, string _namespace)
        {
            Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.BACKUP_PROMPT, _namespace);
            DialogResult retVal = DialogResult.Yes;
            if (globalPackage)
            {
                retVal = Helper.ShowMessageBox(MessageBoxStrings.GUI.BACKUP_PROMPT_GLOBAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (retVal == DialogResult.Yes && frmMainBGWorker.MakeGlobalBackup(parentForm, saveFileDialog) == false)
                    return DialogResult.Cancel;

            }
            else
            {
                retVal = Helper.ShowMessageBox(MessageBoxStrings.GUI.BACKUP_PROMPT_LOCAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (retVal == DialogResult.Yes && frmMainBGWorker.MakeProjectBackup(parentForm, saveFileDialog) == false)
                    return DialogResult.Cancel;
            }
            if (retVal == DialogResult.No)
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.BACKUP_PROMPT_NO, _namespace);
            return retVal;
        }

        public void InstallPackage(Form parentForm, SaveFileDialog saveFileDialog, OpenFileDialog openFileDialog, bool isGlobalPackage, bool dontWriteInitEventLog = false)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();

            if (!isGlobalPackage && (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath)))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_PERFORM_ACTION_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.Global.Error.NoOpenProject(LoggerMessages.GUI.Global.Actions.INSTALL_PROJ_SPEC_PACK), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }


            if (!dontWriteInitEventLog)
            {
                string toWrite = (isGlobalPackage) ? LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_BEGIN : LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_BEGIN_SPEC_PROJ + PackageManagement.OpenedProject.DirectoryPath + ".";
                Logger.WriteUserEventLog(toWrite, LoggerMessages.GUI.ActionTaken.SHOW_OPEN_FILE_DLG, _namespace);

            }
            openFileDialog.ResetValues(FileDialogFilters.PACKAGE);
            if (openFileDialog.ShowDialog(parentForm) == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_CANCELLED, _namespace);
                return;
            }
            if (string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INSTALL_PACKAGE_SELECT_EMPTY, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                return;
            }
            Logger.WriteUserEventLog(openFileDialog.FileName + LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_SELECTED, LoggerMessages.GUI.ActionTaken.INSTALL_PACKAGE_SELECTED, _namespace);
            if (!File.Exists(openFileDialog.FileName))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INSTALL_PACKAGE_SELECT_NOT_FOUND + openFileDialog.FileName + ".", _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                return;
            }
            

            if (Path.GetExtension(openFileDialog.FileName).ToLower() == FileExtensions.ZIP)
            {
                ArchiveManagement.ChecksumStatus statusCheck =  ArchiveManagement.ChecksumStatus.ChecksumMatchFailed;
                try
                {
                    statusCheck = ArchiveManagement.PerformArchiveChecksumCheck(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UnableReadArchiveEnd(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                    return;
                }
                if (statusCheck == ArchiveManagement.ChecksumStatus.ChecksumMatchFailed)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_CORRUPTED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(openFileDialog.FileName + LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.ZIP_CHECKSUM_FAILED, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                    return;
                }
                else if (statusCheck == ArchiveManagement.ChecksumStatus.NoStoredChecksum)
                {
                    Logger.WriteWarningLog(openFileDialog.FileName + LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ZIP_CHECKSUM_NOT_FOUND, _namespace);
                    if (Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_NO_CHECKSUM, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_CANCELLED, _namespace);
                        return;
                    }
                }
            }

            string packagePath = null;
            LogDataList outLog = null;
            ThreadResult threadResult = ThreadResult.Success;

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.INIT_PACK_ASSETS_AND_INFO);
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    packagePath = PackageManagement.Installer.InitPackageInstaller(openFileDialog.FileName, isGlobalPackage, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.InstallPackage_ExtractCopyFailed(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadResult = ThreadResult.Reset;
                    loadingForm.SafeClose();
                    return;
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            if (threadResult == ThreadResult.Reset)
            {
                InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                return;
            }

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            RMPackage parsedPackage = null;
            loadingForm = new frmLoading(StringConst.frmLoading.PARSING_MANIFEST + packagePath + ".");
            thread = new Thread(delegate ()
           {
               try
               {
                   parsedPackage = new RMPackage(packagePath, _namespace, out outLog);
               }
               catch (Exception ex)
               {
                   Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                   Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INVALID_PACKAGE_XML + openFileDialog.FileName + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                   threadResult = ThreadResult.Reset;
                   loadingForm.SafeClose();
                   return;
               }
               threadResult = ThreadResult.Success;
               loadingForm.SafeClose();
           });
            thread.Start();
            loadingForm.ShowDialog();
            if (threadResult == ThreadResult.Reset)
            {
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                return;
            }

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            loadingForm = new frmLoading(StringConst.frmLoading.PERFORM_ASSETS_EXISTENCE_CHECK);
            thread = new Thread(delegate ()
            {
                try
                {
                    PackageManagement.Installer.PerformPackageFilesExistenceCheck(parsedPackage, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.FailedPreExistenceCheck(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadResult = ThreadResult.Reset;
                    loadingForm.SafeClose();
                    return;
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            if (threadResult == ThreadResult.Reset)
            {
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                return;
            }

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }


            bool isUpdatePackage = false;
            InstalledPackage packageFound = null;
            if (isGlobalPackage && PackageManagement.GlobalPackages != null && parsedPackage != null)
                packageFound = PackageManagement.GlobalPackages.FindByUID(parsedPackage.UniqueID);
            else if (!isGlobalPackage && PackageManagement.OpenedProject != null && PackageManagement.OpenedProject.InstalledPackages != null && parsedPackage != null)
                packageFound = PackageManagement.OpenedProject.InstalledPackages.FindByUID(parsedPackage.UniqueID);

            if (packageFound != null)
            {
                string oldVer = null;
                string newVer = null;

                if (!string.IsNullOrWhiteSpace(packageFound.Package.Version))
                    oldVer = packageFound.Package.Version;
                if (!string.IsNullOrWhiteSpace(parsedPackage.Version))
                    newVer = parsedPackage.Version;

                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.SimilarPackageFound(oldVer, newVer), _namespace);
                if (Helper.ShowMessageBox(MessageBoxStrings.GUI.InstallPackageSimilar(oldVer, newVer, packageFound.Name), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_CANCELLED, _namespace);
                    PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                    return;
                }
                isUpdatePackage = true;
            }

            DialogResult dResult = (new frmPackageInfo(parsedPackage, _namespace)).ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_CANCELLED, _namespace);
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                return;
            }

            if (AskUserBackup(parentForm, saveFileDialog, isGlobalPackage, _namespace) == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_CANCELLED, _namespace);
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                return;
            }

            loadingForm = new frmLoading(StringConst.frmLoading.INSTALLING_PACKAGE + packagePath  + ".");
            thread = new Thread(delegate ()
            {
                if (isUpdatePackage)
                {

                    if (isGlobalPackage)
                    {
                        try
                        {
                            PackageManagement.Updater.UpdateGlobalPackage(packagePath, _namespace, out outLog, true, true);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UPDATE_PACK_FAILED + parsedPackage.Name + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                            threadResult = ThreadResult.Cancel;
                            loadingForm.SafeClose();
                            return;
                        }

                    }
                    else
                    {
                        try
                        {
                            PackageManagement.Updater.UpdateLocalPackage(packagePath, PackageManagement.OpenedProject, true, _namespace, out outLog, true);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UPDATE_PACK_FAILED + parsedPackage.Name + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                            threadResult = ThreadResult.Cancel;
                            loadingForm.SafeClose();
                            return;
                        }

                    }
                }
                else
                {
                    if (isGlobalPackage)
                    {
                        try
                        {
                            PackageManagement.Installer.InstallGlobalPackage(packagePath, _namespace, out outLog, false, true, skipFileExistenceCheck: true);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INSTALL_PACK_FAILED + parsedPackage.Name + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                            threadResult = ThreadResult.Cancel;
                            loadingForm.SafeClose();
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            PackageManagement.Installer.InstallLocalPackage(packagePath, PackageManagement.OpenedProject, _namespace, out outLog, false, true, skipFileExistenceCheck: true);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INSTALL_PACK_FAILED + parsedPackage.Name + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                            threadResult = ThreadResult.Cancel;
                            loadingForm.SafeClose();
                            return;
                        }
                    }
                   
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            if (threadResult == ThreadResult.Cancel)
                return;

            SetEnableOnPackSelControls(false);
            if (isGlobalPackage)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_DONE + parsedPackage.Name + ".", _namespace);
                GlobalClass.GlobalPackControls.LoadPackages(PackageManagement.GlobalPackages, _namespace);
            }
            else
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.InstallPackageDoneSpecProj(parsedPackage.Name, PackageManagement.OpenedProject.DirectoryPath), _namespace);
                GlobalClass.LocalPackControls.LoadPackages(PackageManagement.OpenedProject.InstalledPackages, _namespace);
            }

            Helper.ShowMessageBox(MessageBoxStrings.GUI.InstallPackageDone(openFileDialog.FileName), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        public void UninstallPackage(Form parentForm, SaveFileDialog saveFileDialog, bool isGlobalPackage, InstalledPackage packageSel)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (isGlobalPackage)
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UNINSTALL_PACKAGE_GLOBAL, _namespace);
            else
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UNINSTALL_PACKAGE_LOCAL, _namespace);
                if (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_PERFORM_ACTION_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.Global.Error.NoOpenProject(LoggerMessages.GUI.Global.Actions.UNINSTALL_PROJ_SPEC_PACK), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
            }

            if (packageSel == null || packageSel.Package == null || string.IsNullOrWhiteSpace(packageSel.Namespace))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UNINSTALL_PACK_ERR_INVALID_SCRIPT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            string projectPath = (isGlobalPackage) ? null : PackageManagement.OpenedProject.DirectoryPath;
            if (Helper.ShowMessageBox(MessageBoxStrings.GUI.UninstallPackageProject(packageSel.Package.Name, projectPath), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UNINSTALL_PACKAGE_CANCELLED, _namespace);
                return;
            }
            if (AskUserBackup(parentForm, saveFileDialog, isGlobalPackage, _namespace) == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UNINSTALL_PACKAGE_CANCELLED, _namespace);
                return;
            }

            LogDataList outLog = null;

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.UNINSTALLING_PACKAGE + packageSel.Namespace + ".");
            ThreadResult threadResult = ThreadResult.Success;
            Thread thread = new Thread(delegate ()
            {
                if (isGlobalPackage)
                {
                    try
                    {
                        PackageManagement.Uninstaller.UninstallGlobalPackage(packageSel.Namespace, _namespace, out outLog);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.UNINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UNINSTALL_PACK_FAILED + packageSel.Namespace + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        threadResult = ThreadResult.Cancel;
                        loadingForm.SafeClose();
                        return;
                    }
                }
                else
                {
                    try
                    {
                        PackageManagement.Uninstaller.UninstallLocalPackage(PackageManagement.OpenedProject, packageSel.Namespace, _namespace, out outLog);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.UNINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UNINSTALL_PACK_FAILED + packageSel.Namespace + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        threadResult = ThreadResult.Cancel;
                        loadingForm.SafeClose();
                        return;
                    }
                    
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();


            if (threadResult == ThreadResult.Cancel)
                return;

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            SetEnableOnPackSelControls(false);
            if (isGlobalPackage)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UNINSTALL_PACKAGE_DONE + packageSel.Namespace + ".", _namespace);
                GlobalClass.GlobalPackControls.LoadPackages(PackageManagement.GlobalPackages, _namespace);
            }
            else
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UninstallPackageDoneProjSpec(packageSel.Namespace, PackageManagement.OpenedProject.DirectoryPath), _namespace);
                GlobalClass.LocalPackControls.LoadPackages(PackageManagement.OpenedProject.InstalledPackages, _namespace);
            }

            Helper.ShowMessageBox(MessageBoxStrings.GUI.UninstallPackageDone(packageSel.Namespace), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdatePackage(Form parentForm, SaveFileDialog saveFileDialog, OpenFileDialog openFileDialog, bool isGlobalPackage, InstalledPackage installedPackage, bool dontWriteInitEventLog = false)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (!isGlobalPackage && (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath)))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_PERFORM_ACTION_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.Global.Error.NoOpenProject(LoggerMessages.GUI.Global.Actions.UPDATE_PROJ_SPEC_PACK), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (installedPackage == null || installedPackage.Package == null || string.IsNullOrWhiteSpace(installedPackage.Namespace))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UPDATE_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UPDATE_PACK_ERR_INVALID_SCRIPT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (!dontWriteInitEventLog)
            {
                string toWrite = (isGlobalPackage) ? LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UPDATE_PACKAGE_BEGIN + installedPackage.Namespace + "." : LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UpdatePackageBeginSpecProj(installedPackage.Namespace, PackageManagement.OpenedProject.DirectoryPath);
                Logger.WriteUserEventLog(toWrite, LoggerMessages.GUI.ActionTaken.SHOW_OPEN_FILE_DLG, _namespace);

            }
            openFileDialog.ResetValues(FileDialogFilters.PACKAGE);
            if (openFileDialog.ShowDialog(parentForm) == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UPDATE_PACKAGE_CANCELLED, _namespace);
                return;
            }

            if (string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INSTALL_PACKAGE_SELECT_EMPTY, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                InstallPackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, true);
                return;
            }
            Logger.WriteUserEventLog(openFileDialog.FileName + LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.INSTALL_PACKAGE_SELECTED, LoggerMessages.GUI.ActionTaken.INSTALL_PACKAGE_SELECTED, _namespace);
            if (!File.Exists(openFileDialog.FileName))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INSTALL_PACKAGE_SELECT_NOT_FOUND + openFileDialog.FileName + ".", _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                return;
            }

            

            if (Path.GetExtension(openFileDialog.FileName).ToLower() == FileExtensions.ZIP)
            {
                ArchiveManagement.ChecksumStatus statusCheck = ArchiveManagement.ChecksumStatus.ChecksumMatchFailed;
                try
                {
                    statusCheck = ArchiveManagement.PerformArchiveChecksumCheck(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UnableReadArchiveEnd(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                    return;
                }
                if (statusCheck == ArchiveManagement.ChecksumStatus.ChecksumMatchFailed)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_CORRUPTED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(openFileDialog.FileName + LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.ZIP_CHECKSUM_FAILED, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                    return;
                }
                else if (statusCheck == ArchiveManagement.ChecksumStatus.NoStoredChecksum)
                {
                    Logger.WriteWarningLog(openFileDialog.FileName + LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ZIP_CHECKSUM_NOT_FOUND, _namespace);
                    if (Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_NO_CHECKSUM, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UPDATE_PACKAGE_CANCELLED, _namespace);
                        return;
                    }
                }
            }

            string packagePath = null;
            LogDataList outLog = null;

            ThreadResult threadResult = ThreadResult.Success;

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.INIT_PACK_ASSETS_AND_INFO);
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    packagePath = PackageManagement.Installer.InitPackageInstaller(openFileDialog.FileName, isGlobalPackage, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.InstallPackage_ExtractCopyFailed(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadResult = ThreadResult.Reset;
                    loadingForm.SafeClose();
                    return;
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            
            if (threadResult == ThreadResult.Reset)
            {
                UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                return;
            }

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }


            RMPackage parsedPackage = null;

            loadingForm = new frmLoading(StringConst.frmLoading.PARSING_MANIFEST + packagePath  + ".");
            thread = new Thread(delegate ()
            {
                try
                {
                    parsedPackage = new RMPackage(packagePath, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.INVALID_PACKAGE_XML + openFileDialog.FileName + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadResult = ThreadResult.Reset;
                    loadingForm.SafeClose();
                    return;
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            
            if (threadResult == ThreadResult.Reset)
            {
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                return;
            }

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            loadingForm = new frmLoading(StringConst.frmLoading.PERFORM_ASSETS_EXISTENCE_CHECK);
            thread = new Thread(delegate ()
            {
                try
                {
                    PackageManagement.Installer.PerformPackageFilesExistenceCheck(parsedPackage, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.INSTALL_PACKAGE_INVALID_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.FailedPreExistenceCheck(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadResult = ThreadResult.Reset;
                    loadingForm.SafeClose();
                    return;
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (threadResult == ThreadResult.Reset)
            {
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                return;
            }

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            if (parsedPackage.UniqueID.ToLower() != installedPackage.Namespace)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UPDATE_PACKAGE_NON_SIMILAR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UpdatePackNonSimilar(parsedPackage.UniqueID, installedPackage.Namespace), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                UpdatePackage(parentForm, saveFileDialog, openFileDialog, isGlobalPackage, installedPackage, true);
                return;
            }

            DialogResult dResult = (new frmPackageInfo(parsedPackage, _namespace)).ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UPDATE_PACKAGE_CANCELLED, _namespace);
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                return;
            }


            if (AskUserBackup(parentForm, saveFileDialog, isGlobalPackage, _namespace) == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UPDATE_PACKAGE_CANCELLED, _namespace);
                PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
                return;
            }


            loadingForm = new frmLoading(StringConst.frmLoading.UPDATING_PACK + packagePath + ".");
            thread = new Thread(delegate ()
            {
                if (isGlobalPackage)
                {
                    try
                    {
                        PackageManagement.Updater.UpdateGlobalPackage(packagePath, _namespace, out outLog, true, true);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.UPDATE_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UPDATE_PACK_FAILED + parsedPackage.Name + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                        threadResult = ThreadResult.Cancel;
                        loadingForm.SafeClose();
                        return;
                    }

                }
                else
                {
                    try
                    {
                        PackageManagement.Updater.UpdateLocalPackage(packagePath, PackageManagement.OpenedProject, true, _namespace, out outLog, true);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.UPDATE_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UPDATE_PACK_FAILED + parsedPackage.Name + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                        threadResult = ThreadResult.Cancel;
                        loadingForm.SafeClose();
                        return;
                    }
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
           
            PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
            if (threadResult == ThreadResult.Cancel)
                return;

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            SetEnableOnPackSelControls(false);
            if (isGlobalPackage)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UPDATE_PACKAGE_DONE + parsedPackage.Name + ".", _namespace);
                GlobalClass.GlobalPackControls.LoadPackages(PackageManagement.GlobalPackages, _namespace);
            }
            else
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.UpdatePackageDoneSpecProj(parsedPackage.Name, PackageManagement.OpenedProject.DirectoryPath), _namespace);
                GlobalClass.LocalPackControls.LoadPackages(PackageManagement.OpenedProject.InstalledPackages, _namespace);
            }

            Helper.ShowMessageBox(MessageBoxStrings.GUI.UpdatePackageDone(parsedPackage.Name), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ReinstallPackage(Form parentForm, SaveFileDialog saveFileDialog, bool isGlobalPackage, InstalledPackage installedPackage)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();

            if (isGlobalPackage)
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.REINSTALL_PACKAGE_GLOBAL, _namespace);
            else
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.REINSTALL_PACKAGE_LOCAL, _namespace);

            if (!isGlobalPackage && (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath)))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_PERFORM_ACTION_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.Global.Error.NoOpenProject(LoggerMessages.GUI.Global.Actions.REINSTALL_PROJ_SPEC_PACK), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (installedPackage == null || installedPackage.Package == null || string.IsNullOrWhiteSpace(installedPackage.Namespace))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.REINSTALL_PACK_ERR_INVALID_SCRIPT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (installedPackage.PackageInstallArchive == null || string.IsNullOrWhiteSpace(installedPackage.ArchivePath) || !File.Exists(installedPackage.ArchivePath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.REINSTALL_PACK_NO_ARCH, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            try
            {
                if (ArchiveManagement.PerformArchiveChecksumCheck(installedPackage.ArchivePath) != ArchiveManagement.ChecksumStatus.MatchingChecksum)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.REINSTALL_PACK_CHECKSUM_FAILED, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.UnableReadArchiveEnd(installedPackage.ArchivePath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_PROMPT, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.REINSTALL_PACKAGE_CANCELLED, _namespace);
                return;
            }

            if (AskUserBackup(parentForm, saveFileDialog, isGlobalPackage, _namespace) == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.REINSTALL_PACKAGE_CANCELLED, _namespace);
                return;
            }

            LogDataList outLog = null;

            ThreadResult threadResult = ThreadResult.Success;
            frmLoading loadingForm = new frmLoading("Reinstalling package...");
            Thread thread = new Thread(delegate ()
            {
                if (isGlobalPackage)
                {
                    try
                    {
                        PackageManagement.Reinstaller.ReinstallGlobalPackage(installedPackage, _namespace, out outLog);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.REINSTALL_PACK_FAILED + installedPackage.Namespace + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                        threadResult = ThreadResult.Cancel;
                        loadingForm.SafeClose();
                        return;
                    }
                }
                else
                {
                    try
                    {
                        PackageManagement.Reinstaller.ReinstallLocalPackage(installedPackage, PackageManagement.OpenedProject, _namespace, out outLog);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_PACKAGE_FAILED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.REINSTALL_PACK_FAILED + installedPackage.Namespace + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                        threadResult = ThreadResult.Cancel;
                        loadingForm.SafeClose();
                        return;
                    }
                }
                threadResult = ThreadResult.Success;
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            PackageManagement.Installer.CleanupTempInstallDir(_namespace, false);
            if (threadResult == ThreadResult.Cancel)
                return;

            if (outLog != null && outLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: outLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            SetEnableOnPackSelControls(false);
            if (isGlobalPackage)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.REINSTALL_PACKAGE_DONE + installedPackage.Namespace + ".", _namespace);
                GlobalClass.GlobalPackControls.LoadPackages(PackageManagement.GlobalPackages, _namespace);
            }
            else
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.ReinstallPackageDoneSpecProj(installedPackage.Namespace, PackageManagement.OpenedProject.DirectoryPath), _namespace);
                GlobalClass.LocalPackControls.LoadPackages(PackageManagement.OpenedProject.InstalledPackages, _namespace);
            }

            Helper.ShowMessageBox(MessageBoxStrings.GUI.ReinstallPackageDone(installedPackage.Namespace), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ViewLicense()
        {
            if (listCurInstalled.SelectedItem != null)
            {
                InstalledPackage instPackage = listCurInstalled.SelectedItem as InstalledPackage;

                if (instPackage != null && instPackage.Package != null)
                    frmMainBGWorker.ViewLicense(linkLic, instPackage.Package);
            }
        }

        public override void LoadInfoInstalledPackage(InstalledPackage package, string _namespace)
        {
            string packNameOnErr = string.Empty;
            base.LoadInfoInstalledPackage(package, _namespace, out packNameOnErr);
            try
            {
                if (package.PackageInstallArchive == null || !File.Exists(package.ArchivePath))
                    btnReinstall.Enabled = false;
                else
                    btnReinstall.Enabled = true;
            }
            catch (Exception ex)
            {
                btnReinstall.Enabled = false;
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveArch(packNameOnErr), _namespace, ex);
            }
        }

        public override void SetEnableOnPackSelControls(bool enabled)
        {
            base.SetEnableOnPackSelControls(enabled);
            btnUpdatePackage.Enabled = enabled;
            btnRemPack.Enabled = enabled;
            btnReinstall.Enabled = enabled;
            groupInfo.Enabled = enabled;
            btnViewAssets.Enabled = enabled;
        }

        public void SetEnableSearchControls (bool enabled)
        {
            btnShowAllPack.Enabled = enabled;
            btnSearch.Enabled = enabled;
            txtSearch.Enabled = enabled;
        }

        public void Search(string searchTerm, string _namespace)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ShowAllPackages(_namespace);
                return;
            }
            LoadInfoInstalledPackage(null as RMPackage, _namespace);
            listCurInstalled.Items.Clear();
            if (allPackages == null || allPackages.Count == 0)
                return;
            searchTerm = searchTerm.ToLower();
            foreach (InstalledPackage installedPackage in allPackages)
            {
                if (installedPackage.Package == null || string.IsNullOrWhiteSpace(installedPackage.Name))
                    continue;
                if (installedPackage.ToString().ToLower().Contains(searchTerm))
                    listCurInstalled.Items.Add(installedPackage);
            }
        }

        public void ShowAllPackages(string _namespace)
        {
            LoadInfoInstalledPackage(null as RMPackage, _namespace);
            listCurInstalled.Items.Clear();
            if (allPackages == null || allPackages.Count == 0)
                return;
            foreach (InstalledPackage pack in allPackages)
            {
                listCurInstalled.Items.Add(pack);
            }
        }

        public void ViewAssets(Form parentForm, bool globalPackage, InstalledPackage packageSel)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (globalPackage)
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.VIEW_PACKAGE_GLOBAL, _namespace);
            else
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Information.VIEW_PACKAGE_LOCAL, _namespace);
                if (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_PERFORM_ACTION_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.Global.Error.NoOpenProject(LoggerMessages.GUI.Global.Actions.VIEW_ASSETS_PROJ_SPEC_PACK), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
            }

            if (packageSel == null || packageSel.Package == null || string.IsNullOrWhiteSpace(packageSel.Namespace))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.VIEW_PACKAGE_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Error.VIEW_PACK_ERR_INVALID_SCRIPT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (globalPackage)
            {
                RMPackage packToCheck = (RMPackage)packageSel.Package.Clone();
                List<RMPackFile> packFiles= packToCheck.RetrieveAllFiles();
                if (packFiles != null)
                {
                    foreach (RMPackFile packFile in packFiles)
                    {
                        if (packFile is RMGenFile)
                            packFile.Path = PMFileSystem.MV_Installation_Directory + "\\" + packFile.Path;
                        else
                            packFile.Path = PMFileSystem.MV_NewData_Dir + "\\" + packFile.Path;
                    }
                }
                frmPackageAssets packageAssets = new frmPackageAssets(packToCheck, true);
                packageAssets.ShowDialog(parentForm);
            }
            else
            {
                frmPackageAssets packageAssets = new frmPackageAssets(packageSel.Package, true);
                packageAssets.RootDirectory = PackageManagement.OpenedProject.DirectoryPath;
                packageAssets.ShowDialog(parentForm);
            }
        }

        public void CloseProject(string _namespace)
        {
            string projectPath = GlobalClass.MainForm.txtCurOpenProj.Text;
            Logger.WriteInformationLog(LoggerMessages.GUI.BGWorker.Misc.Information.CLOSE_PROJ + PackageManagement.OpenedProject.DirectoryPath + ".", MethodBase.GetCurrentMethod().ToLogFormatFullName());
            PackageManagement.CloseProject();
            LoadInfoInstalledPackage(null as RMPackage, _namespace);
            allPackages.Clear();
            Helper.ShowMessageBox(MessageBoxStrings.GUI.CloseProjectSuccess(projectPath), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
