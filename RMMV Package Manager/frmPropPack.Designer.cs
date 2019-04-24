namespace RMMV_PackMan
{
    partial class frmPropPack
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
            this.components = new System.ComponentModel.Container();
            this.groupInfo = new System.Windows.Forms.GroupBox();
            this.btnViewAssetDir = new System.Windows.Forms.Button();
            this.txtAssetDir = new System.Windows.Forms.TextBox();
            this.lblAssetDir = new System.Windows.Forms.Label();
            this.cboxImplicit = new System.Windows.Forms.CheckBox();
            this.btnEditAssets = new System.Windows.Forms.Button();
            this.btnWhatPackageURL = new System.Windows.Forms.Button();
            this.btnWhatAuthor = new System.Windows.Forms.Button();
            this.btnWhatVersion = new System.Windows.Forms.Button();
            this.btnWhatName = new System.Windows.Forms.Button();
            this.btnWhatNamespace = new System.Windows.Forms.Button();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveZIP = new System.Windows.Forms.Button();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.linkLic = new System.Windows.Forms.LinkLabel();
            this.lblLic = new System.Windows.Forms.Label();
            this.lblPackURL = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtDesc = new RMMV_PackMan.AdvanceTextBox();
            this.txtPackURL = new RMMV_PackMan.AdvanceTextBox();
            this.txtAuthor = new RMMV_PackMan.AdvanceTextBox();
            this.txtVersion = new RMMV_PackMan.AdvanceTextBox();
            this.txtName = new RMMV_PackMan.AdvanceTextBox();
            this.txtNamespace = new RMMV_PackMan.AdvanceTextBox();
            this.groupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPackURL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNamespace)).BeginInit();
            this.SuspendLayout();
            // 
            // groupInfo
            // 
            this.groupInfo.Controls.Add(this.btnViewAssetDir);
            this.groupInfo.Controls.Add(this.txtAssetDir);
            this.groupInfo.Controls.Add(this.lblAssetDir);
            this.groupInfo.Controls.Add(this.cboxImplicit);
            this.groupInfo.Controls.Add(this.btnEditAssets);
            this.groupInfo.Controls.Add(this.txtDesc);
            this.groupInfo.Controls.Add(this.txtPackURL);
            this.groupInfo.Controls.Add(this.txtAuthor);
            this.groupInfo.Controls.Add(this.txtVersion);
            this.groupInfo.Controls.Add(this.txtName);
            this.groupInfo.Controls.Add(this.txtNamespace);
            this.groupInfo.Controls.Add(this.btnWhatPackageURL);
            this.groupInfo.Controls.Add(this.btnWhatAuthor);
            this.groupInfo.Controls.Add(this.btnWhatVersion);
            this.groupInfo.Controls.Add(this.btnWhatName);
            this.groupInfo.Controls.Add(this.btnWhatNamespace);
            this.groupInfo.Controls.Add(this.btnSaveXML);
            this.groupInfo.Controls.Add(this.btnClose);
            this.groupInfo.Controls.Add(this.btnSaveZIP);
            this.groupInfo.Controls.Add(this.lblNamespace);
            this.groupInfo.Controls.Add(this.linkLic);
            this.groupInfo.Controls.Add(this.lblLic);
            this.groupInfo.Controls.Add(this.lblPackURL);
            this.groupInfo.Controls.Add(this.lblVersion);
            this.groupInfo.Controls.Add(this.lblDesc);
            this.groupInfo.Controls.Add(this.lblAuthor);
            this.groupInfo.Controls.Add(this.lblName);
            this.groupInfo.Location = new System.Drawing.Point(12, 12);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(404, 473);
            this.groupInfo.TabIndex = 40;
            this.groupInfo.TabStop = false;
            this.groupInfo.Text = "Package Properties";
            // 
            // btnViewAssetDir
            // 
            this.btnViewAssetDir.Enabled = false;
            this.btnViewAssetDir.Location = new System.Drawing.Point(321, 375);
            this.btnViewAssetDir.Name = "btnViewAssetDir";
            this.btnViewAssetDir.Size = new System.Drawing.Size(67, 20);
            this.btnViewAssetDir.TabIndex = 74;
            this.btnViewAssetDir.Text = "View...";
            this.btnViewAssetDir.UseVisualStyleBackColor = true;
            this.btnViewAssetDir.Click += new System.EventHandler(this.btnViewAssetDir_Click);
            // 
            // txtAssetDir
            // 
            this.txtAssetDir.Enabled = false;
            this.txtAssetDir.Location = new System.Drawing.Point(96, 375);
            this.txtAssetDir.Name = "txtAssetDir";
            this.txtAssetDir.ReadOnly = true;
            this.txtAssetDir.Size = new System.Drawing.Size(224, 20);
            this.txtAssetDir.TabIndex = 73;
            // 
            // lblAssetDir
            // 
            this.lblAssetDir.AutoSize = true;
            this.lblAssetDir.Enabled = false;
            this.lblAssetDir.Location = new System.Drawing.Point(16, 378);
            this.lblAssetDir.Name = "lblAssetDir";
            this.lblAssetDir.Size = new System.Drawing.Size(81, 13);
            this.lblAssetDir.TabIndex = 72;
            this.lblAssetDir.Text = "Asset Directory:";
            // 
            // cboxImplicit
            // 
            this.cboxImplicit.AutoSize = true;
            this.cboxImplicit.Location = new System.Drawing.Point(17, 414);
            this.cboxImplicit.Name = "cboxImplicit";
            this.cboxImplicit.Size = new System.Drawing.Size(137, 17);
            this.cboxImplicit.TabIndex = 71;
            this.cboxImplicit.Text = "&Auto Search For Assets";
            this.toolTip.SetToolTip(this.cboxImplicit, "Indicates whether the package manager automatically searches for RMMV compatible " +
        "assets in a specified directory. See help for more details.");
            this.cboxImplicit.UseVisualStyleBackColor = true;
            this.cboxImplicit.CheckedChanged += new System.EventHandler(this.cboxImplicit_CheckedChanged);
            // 
            // btnEditAssets
            // 
            this.btnEditAssets.Location = new System.Drawing.Point(208, 401);
            this.btnEditAssets.Name = "btnEditAssets";
            this.btnEditAssets.Size = new System.Drawing.Size(181, 30);
            this.btnEditAssets.TabIndex = 70;
            this.btnEditAssets.Text = "View/Edit Package Assets...";
            this.btnEditAssets.UseVisualStyleBackColor = true;
            this.btnEditAssets.Click += new System.EventHandler(this.btnEditAssets_Click);
            // 
            // btnWhatPackageURL
            // 
            this.btnWhatPackageURL.Location = new System.Drawing.Point(360, 132);
            this.btnWhatPackageURL.Name = "btnWhatPackageURL";
            this.btnWhatPackageURL.Size = new System.Drawing.Size(28, 20);
            this.btnWhatPackageURL.TabIndex = 61;
            this.btnWhatPackageURL.Text = "?";
            this.btnWhatPackageURL.UseVisualStyleBackColor = true;
            this.btnWhatPackageURL.Click += new System.EventHandler(this.btnWhatPackageURL_Click);
            // 
            // btnWhatAuthor
            // 
            this.btnWhatAuthor.Location = new System.Drawing.Point(360, 106);
            this.btnWhatAuthor.Name = "btnWhatAuthor";
            this.btnWhatAuthor.Size = new System.Drawing.Size(28, 20);
            this.btnWhatAuthor.TabIndex = 60;
            this.btnWhatAuthor.Text = "?";
            this.btnWhatAuthor.UseVisualStyleBackColor = true;
            this.btnWhatAuthor.Click += new System.EventHandler(this.btnWhatAuthor_Click);
            // 
            // btnWhatVersion
            // 
            this.btnWhatVersion.Location = new System.Drawing.Point(360, 80);
            this.btnWhatVersion.Name = "btnWhatVersion";
            this.btnWhatVersion.Size = new System.Drawing.Size(28, 20);
            this.btnWhatVersion.TabIndex = 59;
            this.btnWhatVersion.Text = "?";
            this.btnWhatVersion.UseVisualStyleBackColor = true;
            this.btnWhatVersion.Click += new System.EventHandler(this.btnWhatVersion_Click);
            // 
            // btnWhatName
            // 
            this.btnWhatName.Location = new System.Drawing.Point(360, 51);
            this.btnWhatName.Name = "btnWhatName";
            this.btnWhatName.Size = new System.Drawing.Size(28, 20);
            this.btnWhatName.TabIndex = 58;
            this.btnWhatName.Text = "?";
            this.btnWhatName.UseVisualStyleBackColor = true;
            this.btnWhatName.Click += new System.EventHandler(this.btnWhatName_Click);
            // 
            // btnWhatNamespace
            // 
            this.btnWhatNamespace.Location = new System.Drawing.Point(360, 25);
            this.btnWhatNamespace.Name = "btnWhatNamespace";
            this.btnWhatNamespace.Size = new System.Drawing.Size(28, 20);
            this.btnWhatNamespace.TabIndex = 57;
            this.btnWhatNamespace.Text = "?";
            this.btnWhatNamespace.UseVisualStyleBackColor = true;
            this.btnWhatNamespace.Click += new System.EventHandler(this.btnWhatNamespace_Click);
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(150, 437);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(128, 30);
            this.btnSaveXML.TabIndex = 56;
            this.btnSaveXML.Text = "Save as &XML Only";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(284, 437);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 30);
            this.btnClose.TabIndex = 53;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveZIP
            // 
            this.btnSaveZIP.Location = new System.Drawing.Point(17, 437);
            this.btnSaveZIP.Name = "btnSaveZIP";
            this.btnSaveZIP.Size = new System.Drawing.Size(128, 30);
            this.btnSaveZIP.TabIndex = 52;
            this.btnSaveZIP.Text = "&Save Package as ZIP";
            this.btnSaveZIP.UseVisualStyleBackColor = true;
            this.btnSaveZIP.Click += new System.EventHandler(this.btnSaveZIP_Click);
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamespace.Location = new System.Drawing.Point(13, 28);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(77, 13);
            this.lblNamespace.TabIndex = 52;
            this.lblNamespace.Text = "Namespace:";
            // 
            // linkLic
            // 
            this.linkLic.AutoSize = true;
            this.linkLic.Location = new System.Drawing.Point(93, 156);
            this.linkLic.Name = "linkLic";
            this.linkLic.Size = new System.Drawing.Size(66, 13);
            this.linkLic.TabIndex = 51;
            this.linkLic.TabStop = true;
            this.linkLic.Text = "View/Modify";
            this.linkLic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLic_LinkClicked);
            // 
            // lblLic
            // 
            this.lblLic.AutoSize = true;
            this.lblLic.Location = new System.Drawing.Point(13, 156);
            this.lblLic.Name = "lblLic";
            this.lblLic.Size = new System.Drawing.Size(47, 13);
            this.lblLic.TabIndex = 50;
            this.lblLic.Text = "License:";
            // 
            // lblPackURL
            // 
            this.lblPackURL.AutoSize = true;
            this.lblPackURL.Location = new System.Drawing.Point(12, 132);
            this.lblPackURL.Name = "lblPackURL";
            this.lblPackURL.Size = new System.Drawing.Size(78, 13);
            this.lblPackURL.TabIndex = 48;
            this.lblPackURL.Text = "Package URL:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 80);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 46;
            this.lblVersion.Text = "Version:";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 180);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(63, 13);
            this.lblDesc.TabIndex = 44;
            this.lblDesc.Text = "Description:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(13, 106);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblAuthor.TabIndex = 42;
            this.lblAuthor.Text = "Author:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 54);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 40;
            this.lblName.Text = "Name:";
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // txtDesc
            // 
            this.txtDesc.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtDesc.Location = new System.Drawing.Point(16, 196);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.PlaceholderForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtDesc.PlaceholderText = "(Optional)";
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(372, 170);
            this.txtDesc.TabIndex = 67;
            this.txtDesc.Text = "(Optional)";
            // 
            // txtPackURL
            // 
            this.txtPackURL.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtPackURL.Location = new System.Drawing.Point(96, 128);
            this.txtPackURL.Name = "txtPackURL";
            this.txtPackURL.PlaceholderForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtPackURL.PlaceholderText = "(Optional)";
            this.txtPackURL.Size = new System.Drawing.Size(258, 20);
            this.txtPackURL.TabIndex = 66;
            this.txtPackURL.Text = "(Optional)";
            this.txtPackURL.TextChanged += new System.EventHandler(this.txtPackURL_TextChanged);
            // 
            // txtAuthor
            // 
            this.txtAuthor.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtAuthor.Location = new System.Drawing.Point(96, 103);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.PlaceholderForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtAuthor.PlaceholderText = "(Optional)";
            this.txtAuthor.Size = new System.Drawing.Size(258, 20);
            this.txtAuthor.TabIndex = 65;
            this.txtAuthor.Text = "(Optional)";
            this.txtAuthor.TextChanged += new System.EventHandler(this.txtAuthor_TextChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtVersion.Location = new System.Drawing.Point(96, 77);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.PlaceholderForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtVersion.PlaceholderText = "(Optional)";
            this.txtVersion.Size = new System.Drawing.Size(258, 20);
            this.txtVersion.TabIndex = 64;
            this.txtVersion.Text = "(Optional)";
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtName.Location = new System.Drawing.Point(96, 51);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtName.PlaceholderText = "(Required)";
            this.txtName.Size = new System.Drawing.Size(258, 20);
            this.txtName.TabIndex = 63;
            this.txtName.Text = "(Required)";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtNamespace
            // 
            this.txtNamespace.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtNamespace.Location = new System.Drawing.Point(96, 26);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.PlaceholderForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtNamespace.PlaceholderText = "(Required)";
            this.txtNamespace.Size = new System.Drawing.Size(258, 20);
            this.txtNamespace.TabIndex = 62;
            this.txtNamespace.Text = "(Required)";
            this.txtNamespace.TextChanged += new System.EventHandler(this.txtNamespace_TextChanged);
            // 
            // frmPropPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(428, 495);
            this.ControlBox = false;
            this.Controls.Add(this.groupInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPropPack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form Name";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPropPack_FormClosing);
            this.Load += new System.EventHandler(this.frmPropPack_Load);
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPackURL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNamespace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupInfo;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.LinkLabel linkLic;
        private System.Windows.Forms.Label lblLic;
        private System.Windows.Forms.Label lblPackURL;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaveZIP;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Button btnWhatPackageURL;
        private System.Windows.Forms.Button btnWhatAuthor;
        private System.Windows.Forms.Button btnWhatVersion;
        private System.Windows.Forms.Button btnWhatName;
        private System.Windows.Forms.Button btnWhatNamespace;
        private AdvanceTextBox txtName;
        private AdvanceTextBox txtNamespace;
        private AdvanceTextBox txtPackURL;
        private AdvanceTextBox txtAuthor;
        private AdvanceTextBox txtVersion;
        private AdvanceTextBox txtDesc;
        private System.Windows.Forms.CheckBox cboxImplicit;
        private System.Windows.Forms.Button btnEditAssets;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtAssetDir;
        private System.Windows.Forms.Label lblAssetDir;
        private System.Windows.Forms.Button btnViewAssetDir;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}