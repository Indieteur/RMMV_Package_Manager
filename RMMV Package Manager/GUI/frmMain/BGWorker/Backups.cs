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
        

        public static bool MakeGlobalBackup(Form parentForm, SaveFileDialog saveFileDialog)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.MAKE_GLOBAL_BACKUP_BEGIN, LoggerMessages.GUI.ActionTaken.SHOW_SAVE_FILE_DLG, _namespace);
            saveFileDialog.ResetValues(FileDialogFilters.BACKUP);
            saveFileDialog.FileName = BackupManager.GetDefaultBackupName();
            DialogResult dResult = saveFileDialog.ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.MAKE_GLOBAL_BACKUP_CANCELLED, _namespace);
                return false;
            }
            LogDataList log = null;

            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.BACKUP_SELECTED_PATH + saveFileDialog.FileName + ".", _namespace);

            bool threadRetVal = true;
            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.CREATE_GLOBAL_BACKUP);
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    BackupManager.CreateGlobalBackup(PMFileSystem.MV_Installation_Directory, saveFileDialog.FileName, _namespace, out log);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_MAKE_BACKUP_GLOBAL + saveFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Backup.Error.BackupCreateFailedGlobal(saveFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadRetVal = false;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (!threadRetVal)
                return false;
        
            Helper.ShowMessageBox(MessageBoxStrings.GUI.MAKE_BACKUP_GLOBAL_SUCCESS + saveFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Logger.WriteInformationLog(LoggerMessages.GUI.BGWorker.Backup.Information.MAKE_GLOBAL_BACKUP_SUCCESS + saveFileDialog.FileName + ".", _namespace);
            return true;
        }

        public static void RestoreGlobalBackup(Form parentForm, OpenFileDialog openFileDialog)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.RESTORE_GLOBAL_BACKUP_BEGIN, LoggerMessages.GUI.ActionTaken.SHOW_OPEN_FILE_DLG, _namespace);
            openFileDialog.ResetValues(FileDialogFilters.BACKUP);
            DialogResult dResult = openFileDialog.ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.RESTORE_GLOBAL_BACKUP_CANCELLED, _namespace);
                return;
            }

            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.BACKUP_RESTORE_SELECTED_PATH + openFileDialog.FileName + ".", _namespace);

            bool threadRetVal = true;
            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.RESTORE_GLOBAL_BACKUP); 
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    BackupManager.RestoreGlobalBackup(openFileDialog.FileName, PMFileSystem.MV_Installation_Directory, _namespace);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_RESTORE_BACKUP_GLOBAL + openFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Backup.Error.BackupRestoreFailedGlobal(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadRetVal = false;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (!threadRetVal)
                return;


            Helper.ShowMessageBox(MessageBoxStrings.GUI.RestoreGlobalBackupSuccess(openFileDialog.FileName), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);



            Logger.WriteInformationLog(LoggerMessages.GUI.BGWorker.Backup.Information.RestoreGlobalBackupSuccess(openFileDialog.FileName), _namespace);
            Program.RestartApp = true;
            Application.Exit();
        }

        public static bool MakeProjectBackup(Form parentForm, SaveFileDialog saveFileDialog)
        {
            if (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_MAKE_LOCAL_BACKUP_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.MAKE_LOCAL_BACKUP_BEGIN, LoggerMessages.GUI.ActionTaken.SHOW_SAVE_FILE_DLG, _namespace);
            saveFileDialog.ResetValues(FileDialogFilters.BACKUP);
            saveFileDialog.FileName = BackupManager.GetDefaultBackupName();
            DialogResult dResult = saveFileDialog.ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.MAKE_LOCAL_BACKUP_CANCELLED, _namespace);
                return false;
            }

            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.BACKUP_SELECTED_PATH + saveFileDialog.FileName + ".", _namespace);

            bool threadRetVal = true;
            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.CREATE_PROJ_BACKUP);
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    BackupManager.CreateProjectBackup(PackageManagement.OpenedProject.DirectoryPath, saveFileDialog.FileName, _namespace);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_MAKE_BACKUP_LOCAL + saveFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Backup.Error.BackupCreateFailedLocal(saveFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadRetVal = false;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            if (!threadRetVal)
                return false;

            Helper.ShowMessageBox(MessageBoxStrings.GUI.MAKE_BACKUP_LOCAL_SUCCESS + saveFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Logger.WriteInformationLog(LoggerMessages.GUI.BGWorker.Backup.Information.MAKE_LOCAL_BACKUP_SUCCESS + saveFileDialog.FileName + ".", _namespace);
            return true;
        }

        public static void RestoreProjectBackup(Form parentForm, OpenFileDialog openFileDialog)
        {
            if (PackageManagement.OpenedProject == null || string.IsNullOrWhiteSpace(PackageManagement.OpenedProject.DirectoryPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_RESTORE_LOCAL_BACKUP_NO_OPEN_PROJ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.RESTORE_PROJECT_BACKUP_BEGIN, LoggerMessages.GUI.ActionTaken.SHOW_OPEN_FILE_DLG, _namespace);
            openFileDialog.ResetValues(FileDialogFilters.BACKUP);
            DialogResult dResult = openFileDialog.ShowDialog(parentForm);
            if (dResult == DialogResult.Cancel)
            {
                Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.RESTORE_PROJECT_BACKUP_CANCELLED, _namespace);
                return;
            }

            Logger.WriteUserEventLog(LoggerMessages.GUI.BGWorker.Backup.Information.BACKUP_RESTORE_SELECTED_PATH + openFileDialog.FileName + ".", _namespace);

            bool threadRetVal = true;
            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.RESTORE_PROJ_BACKUP);
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    BackupManager.RestoreLocalBackup(openFileDialog.FileName, PackageManagement.OpenedProject.DirectoryPath, _namespace);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.UNABLE_RESTORE_LOCAL_BACKUP + openFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.BGWorker.Backup.Error.BackupRestoreFailedLocal(openFileDialog.FileName), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    threadRetVal = false;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            if (!threadRetVal)
                return;

            Helper.ShowMessageBox(MessageBoxStrings.GUI.RestoredLocalBackupSuccess(openFileDialog.FileName), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);



            Logger.WriteInformationLog(LoggerMessages.GUI.BGWorker.Backup.Information.RestoreProjBackupSuccess(openFileDialog.FileName), _namespace);
            ReloadProject(_namespace);
        }
    }

    
}
