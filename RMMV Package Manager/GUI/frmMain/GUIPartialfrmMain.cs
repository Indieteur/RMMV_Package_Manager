using RMMV_PackMan.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;
using System.IO;

namespace RMMV_PackMan
{
    public partial class frmMain
    {
        private void listCurrentInstalledGlobal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (listCurrentInstalledGlobal.SelectedItem != null)
                GlobalClass.GlobalPackControls.LoadInfoInstalledPackage(listCurrentInstalledGlobal.SelectedItem as InstalledPackage, _namespace);
            else
                GlobalClass.GlobalPackControls.LoadInfoInstalledPackage(null as RMPackage, _namespace);
        }

        private void listCurInstalledLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (listCurInstalledLocal.SelectedItem != null)
                GlobalClass.LocalPackControls.LoadInfoInstalledPackage(listCurInstalledLocal.SelectedItem as InstalledPackage, _namespace);
            else
                GlobalClass.LocalPackControls.LoadInfoInstalledPackage(null as RMPackage, _namespace);
        }

        private void linkPackURLGlobal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLbl = (LinkLabel)sender;
            frmMainBGWorker.LaunchProjectURL(linkLbl);
        }
        private void linkPackURLLocal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLbl = (LinkLabel)sender;
            frmMainBGWorker.LaunchProjectURL(linkLbl);
        }
        private void linkLicGlobal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalClass.GlobalPackControls.ViewLicense();
        }

        private void linkLicLocal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalClass.LocalPackControls.ViewLicense();
        }

        private void btnInstallPackGlobal_Click(object sender, EventArgs e)
        {
            GlobalClass.GlobalPackControls.InstallPackage(this, saveFileDialog, openFileDialog, true);
        }

        private void btnInstallPackLocal_Click(object sender, EventArgs e)
        {
            GlobalClass.LocalPackControls.InstallPackage(this, saveFileDialog, openFileDialog, false);
        }

        private void createGlobalBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMainBGWorker.MakeGlobalBackup(this, saveFileDialog);
        }

        private void restoreGlobalBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmMainBGWorker.RestoreGlobalBackup(this, openFileDialog);
        }

        private void createProjectBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMainBGWorker.MakeProjectBackup(this, saveFileDialog);
        }

        private void restoreProjectBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMainBGWorker.RestoreProjectBackup(this, openFileDialog);
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMainBGWorker.OpenProject(this, openFileDialog);
        }

        private void btnBrowseProj_Click(object sender, EventArgs e)
        {
            frmMainBGWorker.OpenProject(this, openFileDialog);
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalClass.LocalPackControls.CloseProject(MethodBase.GetCurrentMethod().ToLogFormatFullName());
            GlobalClass.LocalPackControls.SetEnableSearchControls(false);
          
        }
       

        private void reloadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMainBGWorker.ReloadProject(MethodBase.GetCurrentMethod().ToLogFormatFullName());
        }

        private void btnRemovePackGlobal_Click(object sender, EventArgs e)
        {
            if (listCurrentInstalledGlobal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNINSTALL_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.GlobalPackControls.UninstallPackage(this, saveFileDialog, true, listCurrentInstalledGlobal.SelectedItem as InstalledPackage);
        }

        private void btnRemovePackLocal_Click(object sender, EventArgs e)
        {
            if (listCurInstalledLocal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UNINSTALL_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.LocalPackControls.UninstallPackage(this, saveFileDialog, false, listCurInstalledLocal.SelectedItem as InstalledPackage);
        }

        private void btnUpdPackLocal_Click(object sender, EventArgs e)
        {
            if (listCurInstalledLocal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UPDATE_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.LocalPackControls.UpdatePackage(this, saveFileDialog, openFileDialog, false, listCurInstalledLocal.SelectedItem as InstalledPackage);
        }

        private void btnUpdPackGlobal_Click(object sender, EventArgs e)
        {
            if (listCurrentInstalledGlobal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.UPDATE_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.GlobalPackControls.UpdatePackage(this, saveFileDialog, openFileDialog, true, listCurrentInstalledGlobal.SelectedItem as InstalledPackage);
        }

        private void btnReinstallGlobal_Click(object sender, EventArgs e)
        {
            if (listCurrentInstalledGlobal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.GlobalPackControls.ReinstallPackage(this, saveFileDialog, true, listCurrentInstalledGlobal.SelectedItem as InstalledPackage);
        }

        private void btnReinstallLocal_Click(object sender, EventArgs e)
        {
            if (listCurInstalledLocal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.REINSTALL_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.LocalPackControls.ReinstallPackage(this, saveFileDialog, false, listCurInstalledLocal.SelectedItem as InstalledPackage);

        }

        private void btnGlobalViewAssets_Click(object sender, EventArgs e)
        {
            if (listCurrentInstalledGlobal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.VIEW_ASSETS_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.GlobalPackControls.ViewAssets(this, true, listCurrentInstalledGlobal.SelectedItem as InstalledPackage);
        }

        private void btnLocalViewAssets_Click(object sender, EventArgs e)
        {
            if (listCurInstalledLocal.SelectedItem == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.VIEW_ASSETS_SEL_PACKAGE_FIRST, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalClass.LocalPackControls.ViewAssets(this, false, listCurInstalledLocal.SelectedItem as InstalledPackage);
        }

        private void createNewPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.WriteUserEventLog(LoggerMessages.GUI.frmMain.CREATE_PACKAGE, MethodBase.GetCurrentMethod().ToLogFormatFullName());
            frmPropPack createNewPackDlg = new frmPropPack();
            createNewPackDlg.Text = Vars.FRMPROPPACK_CREATENEW_TITLE;
            createNewPackDlg.ShowDialog(this);

           
        }

        private void modifyPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteUserEventLog(LoggerMessages.GUI.frmMain.MODIFY_PACKAGE, _namespace);
            openFileDialog.ResetValues(FileDialogFilters.PACKAGE);
            if (openFileDialog.ShowDialog(this) == DialogResult.Cancel)
                return;

            Logger.WriteInformationLog(LoggerMessages.GUI.frmMain.MODIFY_PACKAGE_SEL + openFileDialog.FileName + ".", _namespace);
            if (Path.GetExtension(openFileDialog.FileName) == FileExtensions.XML)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.MODIFY_PACK_SELECT_XML_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderBrowserDialog.SelectedPath = Path.GetDirectoryName(openFileDialog.FileName);
                if (folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel)
                    return;
                frmMainBGWorker.ModifyPackageLoadXML(this, openFileDialog.FileName, folderBrowserDialog.SelectedPath);
            }
            else
                frmMainBGWorker.ModifyPackageLoadZIP(this, openFileDialog.FileName);
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalClass.LoggerForm == null)
            {
                GlobalClass.LoggerForm = new frmLogger(this);
                GlobalClass.LoggerForm.Show(this);
            }
            else
            {
                GlobalClass.LoggerForm.Activate();
            }
        }

        private void btnSearchGlobal_Click(object sender, EventArgs e)
        {
            GlobalClass.GlobalPackControls.Search(txtSearchGlobal.Text, MethodBase.GetCurrentMethod().ToLogFormatFullName());
        }

        private void btnSearchLocal_Click(object sender, EventArgs e)
        {
            GlobalClass.LocalPackControls.Search(txtSearchLocal.Text, MethodBase.GetCurrentMethod().ToLogFormatFullName());
        }

        private void btnLocalShowAllPack_Click(object sender, EventArgs e)
        {
            GlobalClass.LocalPackControls.ShowAllPackages(MethodBase.GetCurrentMethod().ToLogFormatFullName());
        }

        private void btnGlobalShowAllPack_Click(object sender, EventArgs e)
        {
            GlobalClass.GlobalPackControls.ShowAllPackages(MethodBase.GetCurrentMethod().ToLogFormatFullName());
        }

        private void btnDonateKoFi_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(WebLinks.SUPPORT_KO_FI);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + WebLinks.SUPPORT_KO_FI + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.SUPPORT_KO_FI_ERROR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupportPatreon_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(WebLinks.SUPPORT_PATREON);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + WebLinks.SUPPORT_PATREON + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.SUPPORT_PATREON_ERROR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout formAbout = new frmAbout();
            formAbout.StartPosition = FormStartPosition.CenterParent;
            formAbout.ShowDialog();
        }

        private void browsePreMadePackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(WebLinks.PRE_MADE_PACKS);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + WebLinks.PRE_MADE_PACKS + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.VIEW_PREMADE_PACK_ERROR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.ShowMessageBox(MessageBoxStrings.GUI.CHECK_FOR_UPDATES_CUR_VER + Application.ProductVersion + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                System.Diagnostics.Process.Start(WebLinks.UPDATES);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + WebLinks.UPDATES + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.CHECK_FOR_UPDATES_ERROR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchGlobal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchGlobal_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void txtSearchLocal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchLocal_Click(sender, e);
                e.SuppressKeyPress = true;
            }
               
        }
    }
}
