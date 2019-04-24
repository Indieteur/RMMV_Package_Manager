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

namespace RMMV_PackMan
{
    public partial class frmLicenseEdit : Form
    {
        public RMPackLic PackageLicense { get; private set; }

        const int MIN_FORM_SIZE_Y = 177;
        const int MIN_BTN_OK_CANCEL_LOC_Y = 103;

        const int MAX_FORM_SIZE_Y = 231;
        const int MAX_BTN_OK_CANCEL_LOC_Y = 157;
        const int MAX_TXT_VAL_HEIGHT = 84;

        string[] prevVals = new string[3];

        int prevIndex = 0;

        void RequiredInit()
        {
            InitializeComponent();
            openFileDialog.Filter = FileDialogFilters.LICENSE_FILE;
        }

        public frmLicenseEdit()
        {
            RequiredInit();
        }


        public frmLicenseEdit(RMPackLic license)
        {
            RequiredInit();
            PackageLicense = license;
           
        }

        RMPackLic.Source ToLicenseSource(int index)
        {
            switch (index)
            {
                case 0:
                    return RMPackLic.Source.URL;
                case 1:
                    return RMPackLic.Source.File;
                case 2:
                    return RMPackLic.Source.Text;
                default:
                    return RMPackLic.Source.None;
            }
        }

        int ToIndex(RMPackLic.Source source)
        {
            switch (source)
            {
                case RMPackLic.Source.URL:
                    return 0;
                case RMPackLic.Source.File:
                    return 1;
                case RMPackLic.Source.Text:
                    return 2;
                default:
                    return 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RMPackLic.Source licSource = ToLicenseSource(comboType.SelectedIndex);
            if (ValidateLicenseValue(txtValue.Text, licSource))
            {
                DialogResult = DialogResult.OK;
                PackageLicense = new RMPackLic(licSource, txtValue.Text);
                Close();
            }
            else
            {
                switch (licSource)
                {
                    case RMPackLic.Source.File:
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmLicenseEdit.FileNotFound(txtValue.Text), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case RMPackLic.Source.Text:
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmLicenseEdit.TEXT_EMPTY, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case RMPackLic.Source.URL:
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmLicenseEdit.INVALID_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }             
            }
           
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            prevVals[prevIndex] = txtValue.Text;
            if (comboType.SelectedIndex == 2)
            {
                txtValue.Multiline = true;
                Size = new Size(Size.Width, MAX_FORM_SIZE_Y);
                btnOK.Location = new Point(btnOK.Location.X, MAX_BTN_OK_CANCEL_LOC_Y);
                btnCancel.Location = new Point(btnCancel.Location.X, MAX_BTN_OK_CANCEL_LOC_Y);
                txtValue.Size = new Size(txtValue.Size.Width, MAX_TXT_VAL_HEIGHT);
            }
            else
            {
               
                txtValue.Multiline = false;
                Size = new Size(Size.Width, MIN_FORM_SIZE_Y);
                btnOK.Location = new Point(btnOK.Location.X, MIN_BTN_OK_CANCEL_LOC_Y);
                btnCancel.Location = new Point(btnCancel.Location.X, MIN_BTN_OK_CANCEL_LOC_Y);
            }

            if (comboType.SelectedIndex == 1)
            {
                txtValue.ReadOnly = true;
                btnBrowse.Enabled = true;
            }
            else
            {
                txtValue.ReadOnly = false;
                btnBrowse.Enabled = false;
            }

            if (!string.IsNullOrWhiteSpace(prevVals[comboType.SelectedIndex]))
                txtValue.Text = prevVals[comboType.SelectedIndex];
            else
                txtValue.Text = string.Empty;

            prevIndex = comboType.SelectedIndex;
        }

        private void frmLicenseEdit_Load(object sender, EventArgs e)
        {
            if (PackageLicense != null)
            {
                comboType.SelectedIndex = ToIndex(PackageLicense.LicenseSource);
                if (!string.IsNullOrWhiteSpace(PackageLicense.Data))
                    txtValue.Text = PackageLicense.Data;
            }
            else
              comboType.SelectedIndex = 0;
        }

        bool ValidateLicenseValue(string value, RMPackLic.Source type)
        {
            if (type == RMPackLic.Source.File)
            {
                if (File.Exists(value) && RMPackLic.HasAValidLicFileExtension(Path.GetExtension(value)))
                    return true;
                return false;
            }
            else if (type == RMPackLic.Source.Text)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return false;
                return true;
            }
            else
            {
                if (value.IsAValidURL())
                    return true;
                return false;  
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = string.Empty;
            DialogResult dr = openFileDialog.ShowDialog(this);
            if (dr == DialogResult.Cancel)
                return;
            txtValue.Text = openFileDialog.FileName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
