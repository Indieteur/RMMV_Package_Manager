using Indieteur.BasicLoggingSystem;
using Indieteur.ChecksumZIP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace RMMV_PackMan.GUI
{
    public static partial class frmMainBGWorker
    {
        public static void ResetValues(this FileDialog fileDialog, string filter)
        {
            fileDialog.FileName = string.Empty;
            fileDialog.Filter = filter;
        }
       

        public static void LaunchProjectURL(LinkLabel linkLabel)
        {
            string tagAsString = linkLabel.Tag as string;
            if (tagAsString.IsAValidURL())
            {
                try
                {
                    System.Diagnostics.Process.Start(tagAsString);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + tagAsString + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_VIEW_PROJ_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                Helper.ShowMessageBox(MessageBoxStrings.GUI.INVALID_PROJ_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ViewLicense(LinkLabel linkLabel, RMPackage package)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            RMPackLic license = linkLabel.Tag as RMPackLic;
            if (license != null && !string.IsNullOrEmpty(license.Data))
            {
                if (license.LicenseSource == RMPackLic.Source.File && !string.IsNullOrWhiteSpace(package.XMLDirectory))
                {
                    string path = package.XMLDirectory + "\\" + license.Data;
                    if (File.Exists(path))
                    {
                        string ext = Path.GetExtension(path);
                        if (RMPackLic.HasAValidLicFileExtension(ext))
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(path);
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_VIEW_FILE + path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_VIEW_LIC_FILE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return;
                        }
                    }
                }
                else if (license.LicenseSource == RMPackLic.Source.URL && license.Data.IsAValidURL())
                {
                    try
                    {
                        System.Diagnostics.Process.Start(license.Data);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + license.Data + "\".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_VIEW_LIC_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                else if (license.LicenseSource == RMPackLic.Source.Text && !string.IsNullOrWhiteSpace(license.Data))
                {
                    Helper.ShowMessageBox(license.Data, MessageBoxStrings.MESSAGEBOX_NAME_LIC, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            Helper.ShowMessageBox(MessageBoxStrings.GUI.INVALID_LICENSE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public static void OpenProject(Form parentForm, OpenFileDialog openFileDialog)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Misc.Information.OPEN_PROJ_BEGIN, LoggerMessages.GUI.ActionTaken.SHOW_OPEN_FILE_DLG, _namespace);
            openFileDialog.ResetValues(FileDialogFilters.MV_PROJ_FILE);

            DialogResult dResult = openFileDialog.ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Misc.Information.OPEN_PROJ_CANCELLED, _namespace);
                return;
            }

            LogDataList log = null;
            try
            {
                PackageManagement.OpenProject(openFileDialog.FileName, _namespace, out log);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.OpenProjFailed(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.OPEN_PROJECT_ERROR + openFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (log != null && log.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: log);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Misc.Information.OpenProjSuccess(openFileDialog.FileName), _namespace);
            Helper.ShowMessageBox(MessageBoxStrings.GUI.OpenProjectSuccess(openFileDialog.FileName), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            GlobalClass.MainForm.txtCurOpenProj.Text = openFileDialog.FileName;
            GlobalClass.LocalPackControls.SetEnableSearchControls(true);
        }

        public static void ReloadProject(string _namespace)
        {
            string projectPath = GlobalClass.MainForm.txtCurOpenProj.Text;
            Logger.WriteInformationLog(LoggerMessages.GUI.BGWorker.Misc.Information.RELOAD_PROJ + PackageManagement.OpenedProject.DirectoryPath + ".", MethodBase.GetCurrentMethod().ToLogFormatFullName());
            GlobalClass.LocalPackControls.LoadInfoInstalledPackage(null as RMPackage, _namespace);
            LogDataList log = null;
            try
            {
                PackageManagement.OpenProject(projectPath, MethodBase.GetCurrentMethod().ToLogFormatFullName(), out log);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.OpenProjFailed(projectPath), MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.RELOADING_PROJECT_ERROR + projectPath + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                GlobalClass.LocalPackControls.CloseProject(_namespace);
                GlobalClass.LocalPackControls.SetEnableSearchControls(false);
                return;
            }

            if (log != null && log.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: log);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            GlobalClass.MainForm.txtCurOpenProj.Text = projectPath;
            Helper.ShowMessageBox(MessageBoxStrings.GUI.RestoreProjectSuccess(projectPath), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       

        public static void ModifyPackageLoadXML(Form parent, string xmlPath, string dirPath)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            RMPackage package = null;
            LogDataList log = null;

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.PARSING_MANIFEST + xmlPath + ".");  
            bool error = false;
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    package = new RMPackage(xmlPath, _namespace, out log, NoAssetProbing: true);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_XML_READ_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.ModifyPackXMLReadError(xmlPath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    error = true;
                    loadingForm.SafeClose();
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (error)
                return;

            if (log != null && log.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: log);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            frmPropPack loadPackDlg = new frmPropPack(package, dirPath, false);
            loadPackDlg.Text = Vars.FRMPROPPACK_MODFY_TITLE;
            loadPackDlg.ShowDialog(parent);
        }

        public static void ModifyPackageLoadZIP(Form parent, string zipPath)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Exception outEx;
            if (Directory.Exists(PMFileSystem.PackMan_TempMakeDir) && Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_TEMP_DIR_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_TEMP_DIR_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArchiveManagement.ChecksumStatus archCheckStat = ArchiveManagement.ChecksumStatus.NoStoredChecksum;
            try
            {
                archCheckStat = ArchiveManagement.PerformArchiveChecksumCheck(zipPath);
            }
            catch (Exception ex)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.UnableReadArchStatus(zipPath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (archCheckStat == ArchiveManagement.ChecksumStatus.ChecksumMatchFailed)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.FailedChecksumZip(zipPath), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }
            else if (archCheckStat == ArchiveManagement.ChecksumStatus.NoStoredChecksum)
            {
                if (Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_ARCH_NO_CHECK, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                 return;
            }

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.EXTRACTING_ARCH + zipPath  + "."); 
            bool error = false;
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    ArchiveManagement.ExtractZip(zipPath, PMFileSystem.PackMan_TempMakeDir, _namespace, false);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.UnableExtractZip(zipPath, PMFileSystem.PackMan_TempMakeDir), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    error = true;
                    loadingForm.SafeClose();
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (error)
            {
                Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                return;
            }

            string xmlPath = PMFileSystem.PackMan_TempMakeDir + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME;
            RMPackage package = null;
            LogDataList log = null;

            if (!File.Exists(xmlPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.ZipExtractInstallXmlNull(zipPath), _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                return;
            }


            loadingForm = new frmLoading(StringConst.frmLoading.PARSING_MANIFEST + xmlPath + ".");
            thread = new Thread(delegate ()
            {
                try
                {
                    package = new RMPackage(xmlPath, _namespace, out log, NoAssetProbing: true);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.ZipExtractInstallXMLErr(zipPath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    error = true;
                    loadingForm.SafeClose();
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (error)
            {
                Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                return;
            }


            package.Implicit = false;

            LogDataList outLog = null;

            loadingForm = new frmLoading(StringConst.frmLoading.RETRIEVING_ASSETS_DIR + PMFileSystem.PackMan_TempMakeDir + "."); 
            thread = new Thread(delegate ()
            {
                try
                {
                    RMImplicit.RetrievePackFromDir(PMFileSystem.PackMan_TempMakeDir, _namespace, false, out outLog, ref package);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_ZIP_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Misc.Error.ZipExtractUnableRetrieveAsset(zipPath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    error = true;
                    loadingForm.SafeClose();
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (error)
            {
                Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
                return;
            }

            if (outLog != null)
                log.Logs.AddRange(outLog.Logs);

            if (log != null && log.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: log);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            frmPropPack loadPackDlg = new frmPropPack(package, PMFileSystem.PackMan_TempMakeDir, true);
            loadPackDlg.Text = Vars.FRMPROPPACK_MODFY_TITLE;
            loadPackDlg.ShowDialog(parent);
        }

    }

    
}
