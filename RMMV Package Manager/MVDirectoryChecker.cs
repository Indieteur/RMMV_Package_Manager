using Indieteur.SAMAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    static class MVDirectoryChecker
    {
        public static void CheckAndOrRetrieveMVPath(this frmMain form)
        {
            Vars.FirstRun = Properties.Settings.Default.FirstLoad;
            if (Vars.FirstRun)
            {
                Properties.Settings.Default.FirstLoad = false;
                if(!RetrieveMVInstallDir())
                    NotFoundMVInstallFolder(form);
            }
            else
            {
                if (!ValidateMVInstallDir())
                    if (!RetrieveMVInstallDir())
                        MVInstallFolderInvalid(form);
                    else
                        Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.MV_DIR_CHANGED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (Vars.ApplicationMode == Vars.AppMode.Full)
                Logger.WriteInformationLog(LoggerMessages.AtTheStart.MV_DIR + PMFileSystem.MV_Installation_Directory + ".", _namespace, true);
            else
                Logger.WriteWarningLog(LoggerMessages.AtTheStart.MV_DIR_NOT_FOUND, _namespace, null, true);
        }

        static void MVInstallFolderInvalid(frmMain form)
        {


            Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.EXPIRED_MV_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            string newPath;
            if (!ShowLocateDirForMV(form, out newPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.MV_DIR_NOT_SPECIFIED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Vars.ApplicationMode = Vars.AppMode.MVNotInstalled;
                return;
            }

            bool validPath = File.Exists(newPath + "\\" + Vars.INSTALLTION_MV_EXEC);
            while (!validPath)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.INVALID_MV_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!ShowLocateDirForMV(form, out newPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.MV_DIR_NOT_SPECIFIED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Vars.ApplicationMode = Vars.AppMode.MVNotInstalled;
                    return;
                }
                validPath = File.Exists(newPath + "\\" + Vars.INSTALLTION_MV_EXEC);
            }
            PMFileSystem.MV_Installation_Directory = newPath;
            Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.MV_DIR_CHANGED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static void NotFoundMVInstallFolder(frmMain form)
        {
            Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.UNABLE_TO_LOCATE_MV_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            string newPath;
            if (!ShowLocateDirForMV(form, out newPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.MV_DIR_NOT_SPECIFIED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Vars.ApplicationMode = Vars.AppMode.MVNotInstalled;
                return;
            }

            bool validPath = File.Exists(newPath + "\\" + Vars.INSTALLTION_MV_EXEC);
            while (!validPath)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.INVALID_MV_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!ShowLocateDirForMV(form, out newPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.MV_DIR_NOT_SPECIFIED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Vars.ApplicationMode = Vars.AppMode.MVNotInstalled;
                    return;
                }
                validPath = File.Exists(newPath + "\\" + Vars.INSTALLTION_MV_EXEC);
            }
            PMFileSystem.MV_Installation_Directory = newPath;
        }


        
        static bool ShowLocateDirForMV(frmMain form, out string path)
        {
            path = null;
            FolderBrowserDialog folderBrowserDialog = form.folderBrowserDialog;
            folderBrowserDialog.SelectedPath = "";
            DialogResult dResult = folderBrowserDialog.ShowDialog();
            if (dResult == DialogResult.Cancel)
                return false;
            path = folderBrowserDialog.SelectedPath;
            return true;
        }

        

        static bool ValidateMVInstallDir()
        {
            bool dirExists = Directory.Exists(PMFileSystem.MV_Installation_Directory);
            if (dirExists)
                if (File.Exists(PMFileSystem.MV_Installation_Directory + "\\" + Vars.INSTALLTION_MV_EXEC))
                    return true;
            Logger.WriteWarningLog(LoggerMessages.AtTheStart.MV_DIR_NOT_VALID + PMFileSystem.MV_Installation_Directory + ".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), null);
            return false;

        }

        static bool RetrieveMVInstallDir()
        {
            try
            {
                SteamAppsManager sam = new SteamAppsManager();
                SteamApp sapp = sam.SteamApps.FindAppByID(Vars.STEAM_MV_APPID, false);
                if (sapp != null)
                    PMFileSystem.MV_Installation_Directory = sapp.InstallDir;
                else
                    return GetStandAloneInstallFolder();
                if (string.IsNullOrWhiteSpace(PMFileSystem.MV_Installation_Directory))
                    return GetStandAloneInstallFolder();
                else
                    return true;
            }
            catch (Exception ex)
            {
                Logger.CreateIssueLogForLowLevelException(ex, MethodBase.GetCurrentMethod().ToLogFormatFullName());
                return GetStandAloneInstallFolder();
            }
        }
        static bool GetStandAloneInstallFolder()
        {
            if (Directory.Exists(Vars.INSTALLATION_MV_PATH_x64) && File.Exists(Vars.INSTALLATION_MV_PATH_x64 + "\\" + Vars.INSTALLTION_MV_EXEC))
            {
                PMFileSystem.MV_Installation_Directory = Vars.INSTALLATION_MV_PATH_x64;
                return true;
            }
            else if (Directory.Exists(Vars.INSTALLATION_MV_PATH_x86) && File.Exists(Vars.INSTALLATION_MV_PATH_x86 + "\\" + Vars.INSTALLTION_MV_EXEC))
            {
                PMFileSystem.MV_Installation_Directory = Vars.INSTALLATION_MV_PATH_x86;
                return true;
            }
            return false;
        }
    }
}
