using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RMMV_PackMan.GUI;
namespace RMMV_PackMan
{
    public partial class frmPackageInfo : Form
    {
        PackageInfoControls Controller;
        RMPackage Package;
        public frmPackageInfo()
        {
            InitializeComponent();
        }

        public frmPackageInfo(RMPackage package, string _namespace)
        {
            Package = package;
            InitializeComponent();
            Controller = new PackageInfoControls();
            Controller.lblAuthor = lblAuthor;
            Controller.lblDesc = lblDesc;
            Controller.lblLic = lblLic;
            Controller.lblName = lblName;
            Controller.lblPackURL = lblPackURL;
            Controller.lblVersion = lblVersion;
            Controller.linkLic = linkLic;
            Controller.linkPackURL = linkPackURL;
            Controller.toolTip = toolTip;
            Controller.txtAuthor = txtAuthor;
            Controller.txtDesc = txtDesc;
            Controller.txtName = txtName;
            Controller.txtVersion = txtVersion;
            Controller.LoadInfoInstalledPackage(package, _namespace);
            Text = txtName.Text + " (" + ((!string.IsNullOrWhiteSpace( package.UniqueID)) ? package.UniqueID : "Not Specified") + ")";
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnInstallPackage_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void linkPackURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMainBGWorker.LaunchProjectURL(linkPackURL);
        }

        private void linkLic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Package != null)
             frmMainBGWorker.ViewLicense(linkLic, Package);
        }

        private void btnViewAssets_Click(object sender, EventArgs e)
        {
            frmPackageAssets frmAssets = new frmPackageAssets(Package, true);
            frmAssets.NonVisibleRootDir = Package.XMLDirectory;
            frmAssets.ShowDialog(this);
        }
    }
}
