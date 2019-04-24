using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RMMV_PackMan.GUI;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public partial class frmPackageAssets : Form
    {
        bool changesMade
        {
            get
            {
                return changesMadeGroup || changesMadeFile;
            }
        }

        bool changesMadeGroup = false;
        bool changesMadeFile = false;

        bool skipSelected = false;
        TreeNode prevSelectedNode;
        bool _viewOnlyMode;
        public bool ViewOnlyMode
        {
            get
            {
                return _viewOnlyMode;
            }
            set
            {
                _viewOnlyMode = value;
                SetViewMode(!value);
            }
        }

        public string RootDirectory { get; set; } = string.Empty;
        public string NonVisibleRootDir { get; set; } = string.Empty;
        

        public RMPackage PackageOfCollections { get; private set; }

        const string EDIT_MODE = "Package Assets";
        const string VIEW_MODE = "Package Assets [Read-Only Mode]";


        public frmPackageAssets()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            PackageOfCollections = new RMPackage();
            ViewOnlyMode = false;
        }

        public frmPackageAssets(RMPackage existingPackage, bool viewOnlyMode)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            ViewOnlyMode = viewOnlyMode;
            if (viewOnlyMode)
                PackageOfCollections = existingPackage;
            else
                PackageOfCollections = existingPackage.Clone() as RMPackage;
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageNullCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_ADD_GROUP_NO_PACK))
                return;
            frmAddAssetGroup addAssetGroupForm = new frmAddAssetGroup(RetrieveDefaultAddGroupSelection(tViewAssets.SelectedNode));
            if (addAssetGroupForm.ShowDialog(this) == DialogResult.Cancel)
                return;
            TreeNode tNode = AddAssetGroup(addAssetGroupForm);
            if (tNode != null)
                tViewAssets.SelectedNode = tNode;
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SetViewMode(bool Editable)
        {
            if (Editable)
                Text = EDIT_MODE;
            else
                Text = VIEW_MODE;

            btnAddGroup.Enabled = Editable;
        }

        private void frmPackageAssets_Load(object sender, EventArgs e)
        {
            tViewAssets.LoadWithPackageCollection(PackageOfCollections, RootDirectory);
        }

        private void tViewAssets_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (skipSelected)
            {
                skipSelected = false;
                return;
            }

            if (changesMade && Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CHANGES_MADE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                skipSelected = true;
                tViewAssets.SelectedNode = prevSelectedNode;
                return;
            }
            if (e.Node != null)
            {
                frmPackAssetTNodeTag tag = e.Node.Tag as frmPackAssetTNodeTag;
                SetupControls(tag);
            }
            else
                ResetSelection();
            prevSelectedNode = e.Node;

        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FileToOpenPath) || !File.Exists(FileToOpenPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.NO_FILE_TO_OPEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmOpenFile fOpen = new frmOpenFile(FileToOpenPath);
            fOpen.ShowDialog(this); 
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageNullCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_REMOVE_GROUP_NO_PACK))
                return;
            if (tViewSelectionCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_REMOVE_GROUP_NULL_NODE))
                return;

            if (Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_SURE_GROUP, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            RemoveAssetGroup(tViewAssets.SelectedNode);

            if (tViewAssets.SelectedNode == null)
                ResetSelection();

        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageNullCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_ADD_FILE_NO_PACK))
                return;
            if (tViewSelectionCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_ADD_FILE_NULL_NODE))
                return;

            openFileDialog.FileName = string.Empty;
            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.Cancel)
                return;

            TreeNode tNode = AddFiles(openFileDialog.FileNames, _namespace);
            if (tNode != null)
                tViewAssets.SelectedNode = tNode;
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageNullCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_REMOVE_FILE_NO_PACK))
                return;
            if (tViewSelectionCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_REMOVE_FILE_NULL_NODE))
                return;
            if (Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_SURE_ASSET, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            RemoveAsset(tViewAssets.SelectedNode);

            if (tViewAssets.SelectedNode == null)
                ResetSelection();
        }

        private void btnGroupRevert_Click(object sender, EventArgs e)
        {
            if (tViewAssets.SelectedNode != null)
            {
                frmPackAssetTNodeTag tag = tViewAssets.SelectedNode.Tag as frmPackAssetTNodeTag;
                SetupControls(tag);
            }
            else
                ResetSelection();
        }

        private void btnFileRevert_Click(object sender, EventArgs e)
        {
            if (tViewAssets.SelectedNode != null)
            {
                frmPackAssetTNodeTag tag = tViewAssets.SelectedNode.Tag as frmPackAssetTNodeTag;
                SetupControls(tag);
            }
            else
                ResetSelection();
        }

        private void btnGroupSave_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageNullCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_PACK))
                return;
            if (tViewSelectionCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_SELECT))
                return;

            SaveGroupInfo(tViewAssets.SelectedNode);
        }
        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            if (tViewAssets.SelectedNode == null)
                return;
            frmPackAssetTNodeTag tag = tViewAssets.SelectedNode.Tag as frmPackAssetTNodeTag;
            if (tag == null)
                return;
            if (tag.Name == txtGroupName.Text)
                return;
            changesMadeGroup = true;
        }

        private void comboGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesMadeGroup = true;
        }

        private void comboGroupGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesMadeGroup = true;
        }

        private void comboFileType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType3_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType4_SelectedIndexChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType4_TextChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType3_TextChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType2_TextChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void comboFileType1_TextChanged(object sender, EventArgs e)
        {
            changesMadeFile = true;
        }

        private void btnFileSave_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageNullCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_PACK))
                return;
            if (tViewSelectionCheck(_namespace, LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_SELECT))
                return;

            SaveFileInfo(tViewAssets.SelectedNode);
        }

        private void tViewAssets_KeyDown(object sender, KeyEventArgs e)
        {
            if (tViewAssets.SelectedNode == null || e.KeyCode != Keys.Delete)
                return;

            if (btnRemoveFile.Enabled)
            {
                btnRemoveFile_Click(sender, null);
            }
            else if (btnRemoveGroup.Enabled)
            {
                btnRemoveGroup_Click(sender, null);
            }
        }

        private void frmPackageAssets_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ViewOnlyMode && DialogResult == DialogResult.Cancel && Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.EXIT_PROMPT, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
