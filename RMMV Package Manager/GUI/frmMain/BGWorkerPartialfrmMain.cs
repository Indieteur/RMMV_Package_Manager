using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMMV_PackMan.GUI;
using System.Windows.Forms;
using System.Reflection;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public partial class frmMain
    {
      
        void InitGUIRes()
        {
            SetSharedControls();
            PackageManagement.OnOpenProject += PackageManagement_OnOpenProject;
            PackageManagement.OnCloseProject += PackageManagement_OnCloseProject;
        }

        void SetSharedControls()
        {
            GlobalClass.GlobalPackControls = new GlobalOrLocalSharedControls();
            GlobalClass.GlobalPackControls.btnInstallNew = btnInstallPackGlobal;
            GlobalClass.GlobalPackControls.btnRemPack = btnRemovePackGlobal;
            GlobalClass.GlobalPackControls.btnSearch = btnSearchGlobal;
            GlobalClass.GlobalPackControls.btnUpdatePackage = btnUpdPackGlobal;
            GlobalClass.GlobalPackControls.groupInfo = groupInfoGlobal;
            GlobalClass.GlobalPackControls.lblAuthor = lblAuthorGlobal;
            GlobalClass.GlobalPackControls.lblCurInstalled = lblCurInstalledGlobal;
            GlobalClass.GlobalPackControls.lblDesc = lblDescGlobal;
            GlobalClass.GlobalPackControls.lblLic = lblLicGlobal;
            GlobalClass.GlobalPackControls.lblName = lblNameGlobal;
            GlobalClass.GlobalPackControls.lblPackURL = lblPackURLGlobal;
            GlobalClass.GlobalPackControls.lblVersion = lblVersionGlobal;
            GlobalClass.GlobalPackControls.linkLic = linkLicGlobal;
            GlobalClass.GlobalPackControls.linkPackURL = linkPackURLGlobal;
            GlobalClass.GlobalPackControls.listCurInstalled = listCurrentInstalledGlobal;
            GlobalClass.GlobalPackControls.txtAuthor = txtAuthorGlobal;
            GlobalClass.GlobalPackControls.txtDesc = txtDescGlobal;
            GlobalClass.GlobalPackControls.txtName = txtNameGlobal;
            GlobalClass.GlobalPackControls.txtSearch = txtSearchGlobal;
            GlobalClass.GlobalPackControls.txtVersion = txtVersionGlobal;
            GlobalClass.GlobalPackControls.btnReinstall = btnReinstallGlobal;
            GlobalClass.GlobalPackControls.toolTip = toolTip;
            GlobalClass.GlobalPackControls.btnViewAssets = btnGlobalViewAssets;
            GlobalClass.GlobalPackControls.btnShowAllPack = btnGlobalShowAllPack;


            GlobalClass.LocalPackControls = new GlobalOrLocalSharedControls();
            GlobalClass.LocalPackControls.btnInstallNew = btnInstallPackLocal;
            GlobalClass.LocalPackControls.btnRemPack = btnRemovePackLocal;
            GlobalClass.LocalPackControls.btnSearch = btnSearchLocal;
            GlobalClass.LocalPackControls.btnUpdatePackage = btnUpdPackLocal;
            GlobalClass.LocalPackControls.groupInfo = groupInfoLocal;
            GlobalClass.LocalPackControls.lblAuthor = lblAuthorLocal;
            GlobalClass.LocalPackControls.lblCurInstalled = lblCurInstalledLocal;
            GlobalClass.LocalPackControls.lblDesc = lblDescLocal;
            GlobalClass.LocalPackControls.lblLic = lblLicLocal;
            GlobalClass.LocalPackControls.lblName = lblNameLocal;
            GlobalClass.LocalPackControls.lblPackURL = lblPackURLlocal;
            GlobalClass.LocalPackControls.lblVersion = lblVersionLocal;
            GlobalClass.LocalPackControls.linkLic = linkLicLocal;
            GlobalClass.LocalPackControls.linkPackURL = linkPackURLLocal;
            GlobalClass.LocalPackControls.listCurInstalled = listCurInstalledLocal;
            GlobalClass.LocalPackControls.txtAuthor = txtAuthorLocal;
            GlobalClass.LocalPackControls.txtDesc = txtDescLocal;
            GlobalClass.LocalPackControls.txtName = txtNameLocal;
            GlobalClass.LocalPackControls.txtSearch = txtSearchLocal;
            GlobalClass.LocalPackControls.txtVersion = txtVersionLocal;
            GlobalClass.LocalPackControls.btnReinstall = btnReinstallLocal;
            GlobalClass.LocalPackControls.toolTip = toolTip;
            GlobalClass.LocalPackControls.btnViewAssets = btnLocalViewAssets;
            GlobalClass.LocalPackControls.btnShowAllPack = btnLocalShowAllPack;
        }

        private void PackageManagement_OnOpenProject(PackageManagement.ProjectPackMan whichProject)
        {
            GlobalClass.LocalPackControls.LoadPackages(whichProject.InstalledPackages, MethodBase.GetCurrentMethod().ToLogFormatFullName());
            SetEnableProjectControls(true);
            
        }

        private void PackageManagement_OnCloseProject(PackageManagement.ProjectPackMan whichProject)
        {
            SetEnableProjectControls(false);
        }

        void SetEnableProjectControls(bool enabled)
        {
            txtCurOpenProj.Enabled = enabled;
            listCurInstalledLocal.Enabled = enabled;
            lblCurInstalledLocal.Enabled = enabled;
            btnInstallPackLocal.Enabled = enabled;
            txtSearchLocal.Enabled = enabled;
            btnSearchLocal.Enabled = enabled;
            closeProjectToolStripMenuItem.Enabled = enabled;
            createProjectBackupToolStripMenuItem.Enabled = enabled;
            restoreProjectBackupToolStripMenuItem.Enabled = enabled;
            reloadProjectToolStripMenuItem.Enabled = enabled;
            if (!enabled)
            {
                txtSearchLocal.Text = string.Empty;
                listCurInstalledLocal.Items.Clear();
                txtCurOpenProj.Text = string.Empty;
            }
        }
     
    }
}
