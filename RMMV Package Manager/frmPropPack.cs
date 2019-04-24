using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Indieteur.BasicLoggingSystem;
using System.Threading;

namespace RMMV_PackMan
{
    public partial class frmPropPack : Form
    {
        const string MANUAL_ASSETS_EDIT_BTN = "View/Edit Package Assets...";
        const string AUTO_ASSETS_EDIT_BTN = "Browse Directory To Search...";

        public RMPackage SelectedPackage { get; private set; }
        RMPackage CustomAssetPack, FormattedCustomAssetPack;
        RMPackLic License;

        string CustomAssetsCommonPath;
        string LicenseFileRelativePath;

        bool MadeChanges = false;
        bool StartupDone = false;


        public frmPropPack()
        {
            InitializeComponent();
        }

        public frmPropPack(RMPackage package, string directory, bool zipFile)
        {
            InitializeComponent();
            SelectedPackage = package;
            if (zipFile)
                LoadPackage(directory, true);
            else
                LoadPackage(directory, false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            frmLicenseEdit licEdit = null;
            if (License == null)
                licEdit = new frmLicenseEdit();
            else
                licEdit = new frmLicenseEdit(License);

            if (licEdit.ShowDialog(this) == DialogResult.Cancel)
                return;

            License = licEdit.PackageLicense;
            if (StartupDone)
                MadeChanges = true;

            if (cboxImplicit.Checked)
                ProcessLicenseAndImplicitDirCommonPath();
            else
                ProcessCustomAssetsCommonPath(_namespace);
        }

        private void cboxImplicit_CheckedChanged(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (cboxImplicit.Checked)
            {
                txtAssetDir.Enabled = true;
                lblAssetDir.Enabled = true;
                btnEditAssets.Text = AUTO_ASSETS_EDIT_BTN;
                ProcessLicenseAndImplicitDirCommonPath();
                if (!string.IsNullOrWhiteSpace(txtAssetDir.Text))
                    btnViewAssetDir.Enabled = true;
            }
            else
            {
                txtAssetDir.Enabled = false;
                lblAssetDir.Enabled = false;
                btnEditAssets.Text = MANUAL_ASSETS_EDIT_BTN;
                ProcessCustomAssetsCommonPath(_namespace);
                btnViewAssetDir.Enabled = false;
            }
            if (StartupDone)
                MadeChanges = true;
        }

        private void btnEditAssets_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (cboxImplicit.Checked)
            {
                folderBrowserDialog.SelectedPath = string.Empty;
                DialogResult dResult = folderBrowserDialog.ShowDialog(this);
                if (dResult == DialogResult.Cancel)
                    return;
                if (!Directory.Exists(folderBrowserDialog.SelectedPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.INVALID_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ViewImplicitDir(folderBrowserDialog.SelectedPath) == DialogResult.Cancel)
                    return;

                txtAssetDir.Text = folderBrowserDialog.SelectedPath;
                btnViewAssetDir.Enabled = true;
                if (StartupDone)
                    MadeChanges = true;
                ProcessLicenseAndImplicitDirCommonPath();
            }
            else
            {
                if (CustomAssetPack == null)
                    CustomAssetPack = new RMPackage();
                frmPackageAssets packageAssetsForm = new frmPackageAssets(CustomAssetPack, false);
                
                DialogResult dResult = packageAssetsForm.ShowDialog(this);

                if (dResult == DialogResult.Cancel)
                    return;
                CustomAssetPack = packageAssetsForm.PackageOfCollections;
                if (StartupDone)
                    MadeChanges = true;
                ProcessCustomAssetsCommonPath(_namespace);
            }
        }

        DialogResult ViewImplicitDir(string path)
        {
            RMPackage temppackage = new RMPackage();
            temppackage.Name = "Package Manager Probe";
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            LogDataList log = null;

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.RETRIEVING_ASSETS_DIR + path + "."); 
            bool cancel = false;
            Thread thread = new Thread(delegate ()
            {
                try
                {
                    RMImplicit.RetrievePackFromDir(path, _namespace, false, out log, ref temppackage);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.UNABLE_RETRIEVE_PACK, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPropPack.Error.UnableRetrievePack(path), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    cancel = true;
                    loadingForm.SafeClose();
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (cancel)
                return DialogResult.Cancel;

            if (log != null && log.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: log);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            frmPackageAssets packageAssetsForm = new frmPackageAssets(temppackage, true);
            return packageAssetsForm.ShowDialog(this);
            
        }

        private void btnViewAssetDir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAssetDir.Text) || !Directory.Exists(txtAssetDir.Text))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_IMPLICIT_DIR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ViewImplicitDir(txtAssetDir.Text);
        }

        private void btnSaveZIP_Click(object sender, EventArgs e)
        {
            if (!CheckPackageInfo())
                return;
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (cboxImplicit.Checked)
                CreateZIPFile(_namespace, true);
            else
                CreateZIPFile(_namespace, false);
        }

      

        private void frmPropPack_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MadeChanges && Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.EXIT_PROMPT, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            CleanupMakeTemp(MethodBase.GetCurrentMethod().ToLogFormatFullName(), false);
        }

      

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            if (!CheckPackageInfo())
                return;

            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (cboxImplicit.Checked)
                CreateXMLFile(_namespace, true);
            else
                CreateXMLFile(_namespace, false);
        }

        private void btnWhatNamespace_Click(object sender, EventArgs e)
        {
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.WHAT_NAMESPACE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnWhatName_Click(object sender, EventArgs e)
        {
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.WHAT_NAME, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnWhatVersion_Click(object sender, EventArgs e)
        {
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.WHAT_VERSION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnWhatAuthor_Click(object sender, EventArgs e)
        {
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.WHAT_AUTHOR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmPropPack_Load(object sender, EventArgs e)
        {
            StartupDone = true;
        }

        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {
            if (txtNamespace.SelectedText == txtNamespace.Text && txtNamespace.Text != "") // https://stackoverflow.com/questions/12354753/textbox-textchanged-and-ctrl-a
                return;
            if (StartupDone)
                MadeChanges = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.SelectedText == txtName.Text && txtName.Text != "") // https://stackoverflow.com/questions/12354753/textbox-textchanged-and-ctrl-a
                return;
            if (StartupDone)
                MadeChanges = true;
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            if (txtVersion.SelectedText == txtVersion.Text && txtVersion.Text != "") // https://stackoverflow.com/questions/12354753/textbox-textchanged-and-ctrl-a
                return;
            if (StartupDone)
                MadeChanges = true;
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
            if (txtAuthor.SelectedText == txtAuthor.Text && txtAuthor.Text != "") // https://stackoverflow.com/questions/12354753/textbox-textchanged-and-ctrl-a
                return;
            if (StartupDone)
                MadeChanges = true;
        }

        private void txtPackURL_TextChanged(object sender, EventArgs e)
        {
            if (txtPackURL.SelectedText == txtPackURL.Text && txtPackURL.Text != "") // https://stackoverflow.com/questions/12354753/textbox-textchanged-and-ctrl-a
                return;
            if (StartupDone)
                MadeChanges = true;
        }

        private void btnWhatPackageURL_Click(object sender, EventArgs e)
        {
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.WHAT_PACKAGE_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
