namespace RMMV_PackMan
{
    partial class frmPackageAssets
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblListAssets = new System.Windows.Forms.Label();
            this.groupFileInfo = new System.Windows.Forms.GroupBox();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.comboFileType4 = new System.Windows.Forms.ComboBox();
            this.lblFileType4 = new System.Windows.Forms.Label();
            this.comboFileType3 = new System.Windows.Forms.ComboBox();
            this.lblFileType3 = new System.Windows.Forms.Label();
            this.comboFileType2 = new System.Windows.Forms.ComboBox();
            this.lblFileType2 = new System.Windows.Forms.Label();
            this.comboFileType1 = new System.Windows.Forms.ComboBox();
            this.lblFileType1 = new System.Windows.Forms.Label();
            this.btnFileSave = new System.Windows.Forms.Button();
            this.btnFileRevert = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnRemoveFile = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.comboGroupType = new System.Windows.Forms.ComboBox();
            this.groupGroupInfo = new System.Windows.Forms.GroupBox();
            this.comboGroupGender = new System.Windows.Forms.ComboBox();
            this.lblGroupGender = new System.Windows.Forms.Label();
            this.btnGroupSave = new System.Windows.Forms.Button();
            this.btnGroupRevert = new System.Windows.Forms.Button();
            this.lblGroupType = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.tViewAssets = new System.Windows.Forms.TreeView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupFileInfo.SuspendLayout();
            this.groupGroupInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(713, 455);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(632, 455);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblListAssets
            // 
            this.lblListAssets.AutoSize = true;
            this.lblListAssets.Location = new System.Drawing.Point(6, 9);
            this.lblListAssets.Name = "lblListAssets";
            this.lblListAssets.Size = new System.Drawing.Size(72, 13);
            this.lblListAssets.TabIndex = 5;
            this.lblListAssets.Text = "List of Assets:";
            // 
            // groupFileInfo
            // 
            this.groupFileInfo.Controls.Add(this.btnFileOpen);
            this.groupFileInfo.Controls.Add(this.comboFileType4);
            this.groupFileInfo.Controls.Add(this.lblFileType4);
            this.groupFileInfo.Controls.Add(this.comboFileType3);
            this.groupFileInfo.Controls.Add(this.lblFileType3);
            this.groupFileInfo.Controls.Add(this.comboFileType2);
            this.groupFileInfo.Controls.Add(this.lblFileType2);
            this.groupFileInfo.Controls.Add(this.comboFileType1);
            this.groupFileInfo.Controls.Add(this.lblFileType1);
            this.groupFileInfo.Controls.Add(this.btnFileSave);
            this.groupFileInfo.Controls.Add(this.btnFileRevert);
            this.groupFileInfo.Controls.Add(this.txtFilePath);
            this.groupFileInfo.Controls.Add(this.lblFilePath);
            this.groupFileInfo.Location = new System.Drawing.Point(389, 201);
            this.groupFileInfo.Name = "groupFileInfo";
            this.groupFileInfo.Size = new System.Drawing.Size(399, 248);
            this.groupFileInfo.TabIndex = 6;
            this.groupFileInfo.TabStop = false;
            this.groupFileInfo.Text = "File Information";
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Enabled = false;
            this.btnFileOpen.Location = new System.Drawing.Point(313, 19);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(68, 22);
            this.btnFileOpen.TabIndex = 13;
            this.btnFileOpen.Text = "&Open...";
            this.btnFileOpen.UseVisualStyleBackColor = true;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // comboFileType4
            // 
            this.comboFileType4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileType4.Enabled = false;
            this.comboFileType4.FormattingEnabled = true;
            this.comboFileType4.Location = new System.Drawing.Point(14, 188);
            this.comboFileType4.Name = "comboFileType4";
            this.comboFileType4.Size = new System.Drawing.Size(367, 21);
            this.comboFileType4.TabIndex = 20;
            this.comboFileType4.SelectedIndexChanged += new System.EventHandler(this.comboFileType4_SelectedIndexChanged);
            this.comboFileType4.TextChanged += new System.EventHandler(this.comboFileType4_TextChanged);
            // 
            // lblFileType4
            // 
            this.lblFileType4.AutoSize = true;
            this.lblFileType4.Location = new System.Drawing.Point(11, 172);
            this.lblFileType4.Name = "lblFileType4";
            this.lblFileType4.Size = new System.Drawing.Size(43, 13);
            this.lblFileType4.TabIndex = 21;
            this.lblFileType4.Text = "Type 4:";
            // 
            // comboFileType3
            // 
            this.comboFileType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileType3.Enabled = false;
            this.comboFileType3.FormattingEnabled = true;
            this.comboFileType3.Location = new System.Drawing.Point(14, 144);
            this.comboFileType3.Name = "comboFileType3";
            this.comboFileType3.Size = new System.Drawing.Size(367, 21);
            this.comboFileType3.TabIndex = 18;
            this.comboFileType3.SelectedIndexChanged += new System.EventHandler(this.comboFileType3_SelectedIndexChanged);
            this.comboFileType3.TextChanged += new System.EventHandler(this.comboFileType3_TextChanged);
            // 
            // lblFileType3
            // 
            this.lblFileType3.AutoSize = true;
            this.lblFileType3.Location = new System.Drawing.Point(11, 128);
            this.lblFileType3.Name = "lblFileType3";
            this.lblFileType3.Size = new System.Drawing.Size(43, 13);
            this.lblFileType3.TabIndex = 19;
            this.lblFileType3.Text = "Type 3:";
            // 
            // comboFileType2
            // 
            this.comboFileType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileType2.Enabled = false;
            this.comboFileType2.FormattingEnabled = true;
            this.comboFileType2.Location = new System.Drawing.Point(14, 103);
            this.comboFileType2.Name = "comboFileType2";
            this.comboFileType2.Size = new System.Drawing.Size(366, 21);
            this.comboFileType2.TabIndex = 16;
            this.comboFileType2.SelectedIndexChanged += new System.EventHandler(this.comboFileType2_SelectedIndexChanged);
            this.comboFileType2.TextChanged += new System.EventHandler(this.comboFileType2_TextChanged);
            // 
            // lblFileType2
            // 
            this.lblFileType2.AutoSize = true;
            this.lblFileType2.Location = new System.Drawing.Point(11, 87);
            this.lblFileType2.Name = "lblFileType2";
            this.lblFileType2.Size = new System.Drawing.Size(43, 13);
            this.lblFileType2.TabIndex = 17;
            this.lblFileType2.Text = "Type 2:";
            // 
            // comboFileType1
            // 
            this.comboFileType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileType1.Enabled = false;
            this.comboFileType1.FormattingEnabled = true;
            this.comboFileType1.Location = new System.Drawing.Point(15, 62);
            this.comboFileType1.Name = "comboFileType1";
            this.comboFileType1.Size = new System.Drawing.Size(365, 21);
            this.comboFileType1.TabIndex = 14;
            this.comboFileType1.SelectedIndexChanged += new System.EventHandler(this.comboFileType1_SelectedIndexChanged);
            this.comboFileType1.TextChanged += new System.EventHandler(this.comboFileType1_TextChanged);
            // 
            // lblFileType1
            // 
            this.lblFileType1.AutoSize = true;
            this.lblFileType1.Location = new System.Drawing.Point(12, 46);
            this.lblFileType1.Name = "lblFileType1";
            this.lblFileType1.Size = new System.Drawing.Size(43, 13);
            this.lblFileType1.TabIndex = 15;
            this.lblFileType1.Text = "Type 1:";
            // 
            // btnFileSave
            // 
            this.btnFileSave.Enabled = false;
            this.btnFileSave.Location = new System.Drawing.Point(181, 219);
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(97, 23);
            this.btnFileSave.TabIndex = 13;
            this.btnFileSave.Text = "Save";
            this.btnFileSave.UseVisualStyleBackColor = true;
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            // 
            // btnFileRevert
            // 
            this.btnFileRevert.Enabled = false;
            this.btnFileRevert.Location = new System.Drawing.Point(284, 219);
            this.btnFileRevert.Name = "btnFileRevert";
            this.btnFileRevert.Size = new System.Drawing.Size(97, 23);
            this.btnFileRevert.TabIndex = 12;
            this.btnFileRevert.Text = "Revert";
            this.btnFileRevert.UseVisualStyleBackColor = true;
            this.btnFileRevert.Click += new System.EventHandler(this.btnFileRevert_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(55, 20);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(257, 20);
            this.txtFilePath.TabIndex = 9;
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 23);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(32, 13);
            this.lblFilePath.TabIndex = 8;
            this.lblFilePath.Text = "Path:";
            // 
            // btnAddFile
            // 
            this.btnAddFile.Enabled = false;
            this.btnAddFile.Location = new System.Drawing.Point(9, 455);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(183, 23);
            this.btnAddFile.TabIndex = 6;
            this.btnAddFile.Text = "Add &File(s)";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Enabled = false;
            this.btnRemoveFile.Location = new System.Drawing.Point(198, 455);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(182, 23);
            this.btnRemoveFile.TabIndex = 8;
            this.btnRemoveFile.Text = "Remove File";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Enabled = false;
            this.btnAddGroup.Location = new System.Drawing.Point(9, 426);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(183, 23);
            this.btnAddGroup.TabIndex = 9;
            this.btnAddGroup.Text = "Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Enabled = false;
            this.btnRemoveGroup.Location = new System.Drawing.Point(198, 426);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(182, 23);
            this.btnRemoveGroup.TabIndex = 10;
            this.btnRemoveGroup.Text = "Remove Group";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // comboGroupType
            // 
            this.comboGroupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGroupType.Enabled = false;
            this.comboGroupType.FormattingEnabled = true;
            this.comboGroupType.Location = new System.Drawing.Point(14, 65);
            this.comboGroupType.Name = "comboGroupType";
            this.comboGroupType.Size = new System.Drawing.Size(366, 21);
            this.comboGroupType.TabIndex = 7;
            this.comboGroupType.SelectedIndexChanged += new System.EventHandler(this.comboGroupType_SelectedIndexChanged);
            // 
            // groupGroupInfo
            // 
            this.groupGroupInfo.Controls.Add(this.comboGroupGender);
            this.groupGroupInfo.Controls.Add(this.lblGroupGender);
            this.groupGroupInfo.Controls.Add(this.btnGroupSave);
            this.groupGroupInfo.Controls.Add(this.btnGroupRevert);
            this.groupGroupInfo.Controls.Add(this.comboGroupType);
            this.groupGroupInfo.Controls.Add(this.lblGroupType);
            this.groupGroupInfo.Controls.Add(this.txtGroupName);
            this.groupGroupInfo.Controls.Add(this.lblGroupName);
            this.groupGroupInfo.Location = new System.Drawing.Point(389, 25);
            this.groupGroupInfo.Name = "groupGroupInfo";
            this.groupGroupInfo.Size = new System.Drawing.Size(399, 171);
            this.groupGroupInfo.TabIndex = 11;
            this.groupGroupInfo.TabStop = false;
            this.groupGroupInfo.Text = "Group Information";
            // 
            // comboGroupGender
            // 
            this.comboGroupGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGroupGender.Enabled = false;
            this.comboGroupGender.FormattingEnabled = true;
            this.comboGroupGender.Location = new System.Drawing.Point(15, 106);
            this.comboGroupGender.Name = "comboGroupGender";
            this.comboGroupGender.Size = new System.Drawing.Size(365, 21);
            this.comboGroupGender.TabIndex = 12;
            this.comboGroupGender.SelectedIndexChanged += new System.EventHandler(this.comboGroupGender_SelectedIndexChanged);
            // 
            // lblGroupGender
            // 
            this.lblGroupGender.AutoSize = true;
            this.lblGroupGender.Location = new System.Drawing.Point(12, 90);
            this.lblGroupGender.Name = "lblGroupGender";
            this.lblGroupGender.Size = new System.Drawing.Size(45, 13);
            this.lblGroupGender.TabIndex = 13;
            this.lblGroupGender.Text = "Gender:";
            // 
            // btnGroupSave
            // 
            this.btnGroupSave.Enabled = false;
            this.btnGroupSave.Location = new System.Drawing.Point(181, 133);
            this.btnGroupSave.Name = "btnGroupSave";
            this.btnGroupSave.Size = new System.Drawing.Size(97, 23);
            this.btnGroupSave.TabIndex = 11;
            this.btnGroupSave.Text = "Save";
            this.btnGroupSave.UseVisualStyleBackColor = true;
            this.btnGroupSave.Click += new System.EventHandler(this.btnGroupSave_Click);
            // 
            // btnGroupRevert
            // 
            this.btnGroupRevert.Enabled = false;
            this.btnGroupRevert.Location = new System.Drawing.Point(284, 133);
            this.btnGroupRevert.Name = "btnGroupRevert";
            this.btnGroupRevert.Size = new System.Drawing.Size(97, 23);
            this.btnGroupRevert.TabIndex = 10;
            this.btnGroupRevert.Text = "Revert";
            this.btnGroupRevert.UseVisualStyleBackColor = true;
            this.btnGroupRevert.Click += new System.EventHandler(this.btnGroupRevert_Click);
            // 
            // lblGroupType
            // 
            this.lblGroupType.AutoSize = true;
            this.lblGroupType.Location = new System.Drawing.Point(11, 49);
            this.lblGroupType.Name = "lblGroupType";
            this.lblGroupType.Size = new System.Drawing.Size(83, 13);
            this.lblGroupType.TabIndex = 9;
            this.lblGroupType.Text = "Collection Type:";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(55, 22);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.ReadOnly = true;
            this.txtGroupName.Size = new System.Drawing.Size(325, 20);
            this.txtGroupName.TabIndex = 8;
            this.txtGroupName.TextChanged += new System.EventHandler(this.txtGroupName_TextChanged);
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(11, 25);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(38, 13);
            this.lblGroupName.TabIndex = 7;
            this.lblGroupName.Text = "Name:";
            // 
            // tViewAssets
            // 
            this.tViewAssets.HideSelection = false;
            this.tViewAssets.Location = new System.Drawing.Point(9, 25);
            this.tViewAssets.Name = "tViewAssets";
            this.tViewAssets.Size = new System.Drawing.Size(371, 395);
            this.tViewAssets.TabIndex = 12;
            this.tViewAssets.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tViewAssets_AfterSelect);
            this.tViewAssets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tViewAssets_KeyDown);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // frmPackageAssets
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.ControlBox = false;
            this.Controls.Add(this.tViewAssets);
            this.Controls.Add(this.groupGroupInfo);
            this.Controls.Add(this.btnRemoveGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemoveFile);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.groupFileInfo);
            this.Controls.Add(this.lblListAssets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPackageAssets";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Package Assets";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPackageAssets_FormClosing);
            this.Load += new System.EventHandler(this.frmPackageAssets_Load);
            this.groupFileInfo.ResumeLayout(false);
            this.groupFileInfo.PerformLayout();
            this.groupGroupInfo.ResumeLayout(false);
            this.groupGroupInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblListAssets;
        private System.Windows.Forms.GroupBox groupFileInfo;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.ComboBox comboGroupType;
        private System.Windows.Forms.GroupBox groupGroupInfo;
        private System.Windows.Forms.Button btnGroupSave;
        private System.Windows.Forms.Button btnGroupRevert;
        private System.Windows.Forms.Label lblGroupType;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.ComboBox comboGroupGender;
        private System.Windows.Forms.Label lblGroupGender;
        private System.Windows.Forms.Button btnFileSave;
        private System.Windows.Forms.Button btnFileRevert;
        private System.Windows.Forms.ComboBox comboFileType4;
        private System.Windows.Forms.Label lblFileType4;
        private System.Windows.Forms.ComboBox comboFileType3;
        private System.Windows.Forms.Label lblFileType3;
        private System.Windows.Forms.ComboBox comboFileType2;
        private System.Windows.Forms.Label lblFileType2;
        private System.Windows.Forms.ComboBox comboFileType1;
        private System.Windows.Forms.Label lblFileType1;
        private System.Windows.Forms.TreeView tViewAssets;
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}