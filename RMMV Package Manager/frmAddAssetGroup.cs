using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMMV_PackMan
{
    public partial class frmAddAssetGroup : Form
    {
        public RMCollectionType TypeToAdd { get; private set; }
        public RMGenPart.GenPartType GeneratorType { get; private set; }
        public RMGenPart.eGender GeneratorGender { get; private set; }
        public string TypeName { get; private set; }
        public frmAddAssetGroup(RMCollectionType typeSelected)
        {
            InitializeComponent();
            comboType.SelectedIndex = 0;
            comboGender.SelectedIndex = 0;

            int comboSelIndex = CollTypeToIndex(typeSelected);
            if (comboSelIndex > -1)
                comboType.SelectedIndex = comboSelIndex;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text.HasInvalidFileNameCharacters())
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmAddAssetGroup.NULL_NAME, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            TypeToAdd = IndexToType(comboType.SelectedIndex);
            GeneratorType = IndexToPartType(comboType.SelectedIndex);
            GeneratorGender = IndexToGender(comboGender.SelectedIndex);
            TypeName = txtName.Text;
            Close();
        }

        RMCollectionType IndexToType(int index)
        {
            switch (index)
            {
                case 0:
                    return RMCollectionType.BGM;
                case 1:
                    return RMCollectionType.BGS;
                case 2:
                    return RMCollectionType.ME;
                case 3:
                    return RMCollectionType.SE;
                case 4:
                    return RMCollectionType.Characters;
                case 5:
                    return RMCollectionType.Tilesets;
                case 6:
                    return RMCollectionType.Movies;
                default:
                    return RMCollectionType.Generator;
            }
        }

        int CollTypeToIndex(RMCollectionType type)
        {
            switch (type)
            {
                case RMCollectionType.BGM:
                    return 0;
                case RMCollectionType.BGS:
                    return 1;
                case RMCollectionType.ME:
                    return 2;
                case RMCollectionType.SE:
                    return 3;
                case RMCollectionType.Characters:
                    return 4;
                case RMCollectionType.Tilesets:
                    return 5;
                case RMCollectionType.Movies:
                    return 6;
                case RMCollectionType.Generator:
                    return 7;
                default:
                    return -1;
            }
        }

        RMGenPart.GenPartType IndexToPartType(int index)
        {
            switch (index)
            {
                case 7:
                    return RMGenPart.GenPartType.Accessory_A;
                case 8:
                    return RMGenPart.GenPartType.Accessory_B;
                case 9:
                    return RMGenPart.GenPartType.Beast_Ears;
                case 10:
                    return RMGenPart.GenPartType.Beard;
                case 11:
                    return RMGenPart.GenPartType.Body;
                case 12:
                    return RMGenPart.GenPartType.Cloak;
                case 13:
                    return RMGenPart.GenPartType.Clothing;
                case 14:
                    return RMGenPart.GenPartType.Ears;
                case 15:
                    return RMGenPart.GenPartType.Eyebrows;
                case 16:
                    return RMGenPart.GenPartType.Eyes;
                case 17:
                    return RMGenPart.GenPartType.Face;
                case 18:
                    return RMGenPart.GenPartType.Facial_Mark;
                case 19:
                    return RMGenPart.GenPartType.Front_Hair;
                case 20:
                    return RMGenPart.GenPartType.Glasses;
                case 21:
                    return RMGenPart.GenPartType.Mouth;
                case 22:
                    return RMGenPart.GenPartType.Nose;
                case 23:
                    return RMGenPart.GenPartType.Rear_Hair;
                case 24:
                    return RMGenPart.GenPartType.Tail;
                case 25:
                    return RMGenPart.GenPartType.Wing;
                default:
                    return RMGenPart.GenPartType.None;
            }
        }

        RMGenPart.eGender IndexToGender(int index)
        {
            switch (index)
            {
                case 0:
                    return RMGenPart.eGender.Male;
                case 1:
                    return RMGenPart.eGender.Female;
                case 2:
                    return RMGenPart.eGender.Kid;
                default:
                    return RMGenPart.eGender.None;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboType.SelectedIndex > 6)
            {
                lblGender.Enabled = true;
                comboGender.Enabled = true;
            }
            else
            {
                lblGender.Enabled = false;
                comboGender.Enabled = false;
            }
        }

        private void frmAddAssetGroup_Load(object sender, EventArgs e)
        {
            ActiveControl = txtName;
        }
    }
}
