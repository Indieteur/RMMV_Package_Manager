namespace RMMV_PackMan
{
    partial class frmPackageInfo
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
            this.linkLic = new System.Windows.Forms.LinkLabel();
            this.lblLic = new System.Windows.Forms.Label();
            this.linkPackURL = new System.Windows.Forms.LinkLabel();
            this.lblPackURL = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnInstallPackage = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnViewAssets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLic
            // 
            this.linkLic.AutoSize = true;
            this.linkLic.Enabled = false;
            this.linkLic.Location = new System.Drawing.Point(102, 96);
            this.linkLic.Name = "linkLic";
            this.linkLic.Size = new System.Drawing.Size(27, 13);
            this.linkLic.TabIndex = 23;
            this.linkLic.TabStop = true;
            this.linkLic.Text = "N/A";
            this.linkLic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLic_LinkClicked);
            // 
            // lblLic
            // 
            this.lblLic.AutoSize = true;
            this.lblLic.Location = new System.Drawing.Point(13, 96);
            this.lblLic.Name = "lblLic";
            this.lblLic.Size = new System.Drawing.Size(47, 13);
            this.lblLic.TabIndex = 22;
            this.lblLic.Text = "License:";
            // 
            // linkPackURL
            // 
            this.linkPackURL.AutoSize = true;
            this.linkPackURL.Enabled = false;
            this.linkPackURL.Location = new System.Drawing.Point(102, 121);
            this.linkPackURL.Name = "linkPackURL";
            this.linkPackURL.Size = new System.Drawing.Size(27, 13);
            this.linkPackURL.TabIndex = 21;
            this.linkPackURL.TabStop = true;
            this.linkPackURL.Text = "N/A";
            this.linkPackURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPackURL_LinkClicked);
            // 
            // lblPackURL
            // 
            this.lblPackURL.AutoSize = true;
            this.lblPackURL.Location = new System.Drawing.Point(13, 121);
            this.lblPackURL.Name = "lblPackURL";
            this.lblPackURL.Size = new System.Drawing.Size(78, 13);
            this.lblPackURL.TabIndex = 20;
            this.lblPackURL.Text = "Package URL:";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(105, 38);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(292, 20);
            this.txtVersion.TabIndex = 19;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 41);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 18;
            this.lblVersion.Text = "Version:";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(16, 160);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(381, 128);
            this.txtDesc.TabIndex = 17;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(13, 144);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(63, 13);
            this.lblDesc.TabIndex = 16;
            this.lblDesc.Text = "Description:";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(105, 64);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(292, 20);
            this.txtAuthor.TabIndex = 15;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(13, 64);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblAuthor.TabIndex = 14;
            this.lblAuthor.Text = "Author:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(105, 12);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(292, 20);
            this.txtName.TabIndex = 13;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Name:";
            // 
            // btnInstallPackage
            // 
            this.btnInstallPackage.Location = new System.Drawing.Point(292, 295);
            this.btnInstallPackage.Name = "btnInstallPackage";
            this.btnInstallPackage.Size = new System.Drawing.Size(105, 30);
            this.btnInstallPackage.TabIndex = 24;
            this.btnInstallPackage.Text = "&Install Package";
            this.btnInstallPackage.UseVisualStyleBackColor = true;
            this.btnInstallPackage.Click += new System.EventHandler(this.btnInstallPackage_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(181, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // btnViewAssets
            // 
            this.btnViewAssets.Location = new System.Drawing.Point(16, 295);
            this.btnViewAssets.Name = "btnViewAssets";
            this.btnViewAssets.Size = new System.Drawing.Size(105, 30);
            this.btnViewAssets.TabIndex = 26;
            this.btnViewAssets.Text = "&View Assets";
            this.btnViewAssets.UseVisualStyleBackColor = true;
            this.btnViewAssets.Click += new System.EventHandler(this.btnViewAssets_Click);
            // 
            // frmPackageInfo
            // 
            this.AcceptButton = this.btnInstallPackage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(404, 332);
            this.ControlBox = false;
            this.Controls.Add(this.btnViewAssets);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInstallPackage);
            this.Controls.Add(this.linkLic);
            this.Controls.Add(this.lblLic);
            this.Controls.Add(this.linkPackURL);
            this.Controls.Add(this.lblPackURL);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmPackageInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "packageName (UID)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLic;
        private System.Windows.Forms.Label lblLic;
        private System.Windows.Forms.LinkLabel linkPackURL;
        private System.Windows.Forms.Label lblPackURL;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnInstallPackage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnViewAssets;
    }
}