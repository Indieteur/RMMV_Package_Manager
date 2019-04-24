namespace RMMV_PackMan
{
    partial class frmAbout
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblHomePage = new System.Windows.Forms.Label();
            this.lblGitHub = new System.Windows.Forms.Label();
            this.lblPatreon = new System.Windows.Forms.Label();
            this.lblKoFi = new System.Windows.Forms.Label();
            this.lblEula = new System.Windows.Forms.Label();
            this.linkHomepage = new System.Windows.Forms.LinkLabel();
            this.linkEULA = new System.Windows.Forms.LinkLabel();
            this.linkGitHub = new System.Windows.Forms.LinkLabel();
            this.linkPatreon = new System.Windows.Forms.LinkLabel();
            this.linkKoFi = new System.Windows.Forms.LinkLabel();
            this.lblBugReport = new System.Windows.Forms.Label();
            this.linkBugReport = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(240, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(294, 24);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "RMMV Package Manager Tool";
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersion.Location = new System.Drawing.Point(16, 41);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(288, 15);
            this.txtVersion.TabIndex = 3;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCopyright.Location = new System.Drawing.Point(19, 203);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(0, 15);
            this.lblCopyright.TabIndex = 4;
            // 
            // lblHomePage
            // 
            this.lblHomePage.AutoSize = true;
            this.lblHomePage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomePage.Location = new System.Drawing.Point(13, 71);
            this.lblHomePage.Name = "lblHomePage";
            this.lblHomePage.Size = new System.Drawing.Size(84, 15);
            this.lblHomePage.TabIndex = 5;
            this.lblHomePage.Text = "• Homepage -";
            // 
            // lblGitHub
            // 
            this.lblGitHub.AutoSize = true;
            this.lblGitHub.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGitHub.Location = new System.Drawing.Point(13, 127);
            this.lblGitHub.Name = "lblGitHub";
            this.lblGitHub.Size = new System.Drawing.Size(60, 15);
            this.lblGitHub.TabIndex = 6;
            this.lblGitHub.Text = "• GitHub -";
            // 
            // lblPatreon
            // 
            this.lblPatreon.AutoSize = true;
            this.lblPatreon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatreon.Location = new System.Drawing.Point(13, 146);
            this.lblPatreon.Name = "lblPatreon";
            this.lblPatreon.Size = new System.Drawing.Size(133, 15);
            this.lblPatreon.TabIndex = 7;
            this.lblPatreon.Text = "• Developer\'s Patreon -";
            // 
            // lblKoFi
            // 
            this.lblKoFi.AutoSize = true;
            this.lblKoFi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKoFi.Location = new System.Drawing.Point(13, 166);
            this.lblKoFi.Name = "lblKoFi";
            this.lblKoFi.Size = new System.Drawing.Size(119, 15);
            this.lblKoFi.TabIndex = 8;
            this.lblKoFi.Text = "• Developer\'s Ko-Fi -";
            // 
            // lblEula
            // 
            this.lblEula.AutoSize = true;
            this.lblEula.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEula.Location = new System.Drawing.Point(13, 108);
            this.lblEula.Name = "lblEula";
            this.lblEula.Size = new System.Drawing.Size(65, 15);
            this.lblEula.TabIndex = 9;
            this.lblEula.Text = "• License -";
            // 
            // linkHomepage
            // 
            this.linkHomepage.AutoEllipsis = true;
            this.linkHomepage.ContextMenuStrip = this.contextMenuStrip;
            this.linkHomepage.Location = new System.Drawing.Point(94, 73);
            this.linkHomepage.Name = "linkHomepage";
            this.linkHomepage.Size = new System.Drawing.Size(221, 16);
            this.linkHomepage.TabIndex = 10;
            this.linkHomepage.TabStop = true;
            this.linkHomepage.Text = "a";
            this.linkHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHomepage_LinkClicked);
            // 
            // linkEULA
            // 
            this.linkEULA.AutoEllipsis = true;
            this.linkEULA.ContextMenuStrip = this.contextMenuStrip;
            this.linkEULA.Location = new System.Drawing.Point(75, 109);
            this.linkEULA.Name = "linkEULA";
            this.linkEULA.Size = new System.Drawing.Size(231, 13);
            this.linkEULA.TabIndex = 11;
            this.linkEULA.TabStop = true;
            this.linkEULA.Text = "a";
            this.linkEULA.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkEULA_LinkClicked);
            // 
            // linkGitHub
            // 
            this.linkGitHub.AutoEllipsis = true;
            this.linkGitHub.ContextMenuStrip = this.contextMenuStrip;
            this.linkGitHub.Location = new System.Drawing.Point(69, 127);
            this.linkGitHub.Name = "linkGitHub";
            this.linkGitHub.Size = new System.Drawing.Size(246, 13);
            this.linkGitHub.TabIndex = 12;
            this.linkGitHub.TabStop = true;
            this.linkGitHub.Text = "a";
            this.linkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGitHub_LinkClicked);
            // 
            // linkPatreon
            // 
            this.linkPatreon.AutoEllipsis = true;
            this.linkPatreon.ContextMenuStrip = this.contextMenuStrip;
            this.linkPatreon.Location = new System.Drawing.Point(143, 146);
            this.linkPatreon.Name = "linkPatreon";
            this.linkPatreon.Size = new System.Drawing.Size(172, 13);
            this.linkPatreon.TabIndex = 13;
            this.linkPatreon.TabStop = true;
            this.linkPatreon.Text = "a";
            this.linkPatreon.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPatreon_LinkClicked);
            // 
            // linkKoFi
            // 
            this.linkKoFi.AutoEllipsis = true;
            this.linkKoFi.ContextMenuStrip = this.contextMenuStrip;
            this.linkKoFi.Location = new System.Drawing.Point(129, 167);
            this.linkKoFi.Name = "linkKoFi";
            this.linkKoFi.Size = new System.Drawing.Size(186, 13);
            this.linkKoFi.TabIndex = 14;
            this.linkKoFi.TabStop = true;
            this.linkKoFi.Text = "a";
            this.linkKoFi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkKoFi_LinkClicked);
            // 
            // lblBugReport
            // 
            this.lblBugReport.AutoSize = true;
            this.lblBugReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBugReport.Location = new System.Drawing.Point(13, 89);
            this.lblBugReport.Name = "lblBugReport";
            this.lblBugReport.Size = new System.Drawing.Size(116, 15);
            this.lblBugReport.TabIndex = 15;
            this.lblBugReport.Text = "• Report Bug Form -";
            // 
            // linkBugReport
            // 
            this.linkBugReport.AutoEllipsis = true;
            this.linkBugReport.ContextMenuStrip = this.contextMenuStrip;
            this.linkBugReport.Location = new System.Drawing.Point(124, 89);
            this.linkBugReport.Name = "linkBugReport";
            this.linkBugReport.Size = new System.Drawing.Size(191, 13);
            this.linkBugReport.TabIndex = 16;
            this.linkBugReport.TabStop = true;
            this.linkBugReport.Text = "a";
            this.linkBugReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBugReport_LinkClicked);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyLinkToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(128, 26);
            // 
            // copyLinkToolStripMenuItem
            // 
            this.copyLinkToolStripMenuItem.Name = "copyLinkToolStripMenuItem";
            this.copyLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyLinkToolStripMenuItem.Text = "Copy Link";
            this.copyLinkToolStripMenuItem.Click += new System.EventHandler(this.copyLinkToolStripMenuItem_Click);
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(330, 231);
            this.Controls.Add(this.linkBugReport);
            this.Controls.Add(this.lblBugReport);
            this.Controls.Add(this.linkKoFi);
            this.Controls.Add(this.linkPatreon);
            this.Controls.Add(this.linkGitHub);
            this.Controls.Add(this.linkEULA);
            this.Controls.Add(this.linkHomepage);
            this.Controls.Add(this.lblEula);
            this.Controls.Add(this.lblKoFi);
            this.Controls.Add(this.lblPatreon);
            this.Controls.Add(this.lblGitHub);
            this.Controls.Add(this.lblHomePage);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblHomePage;
        private System.Windows.Forms.Label lblGitHub;
        private System.Windows.Forms.Label lblPatreon;
        private System.Windows.Forms.Label lblKoFi;
        private System.Windows.Forms.Label lblEula;
        private System.Windows.Forms.LinkLabel linkHomepage;
        private System.Windows.Forms.LinkLabel linkEULA;
        private System.Windows.Forms.LinkLabel linkGitHub;
        private System.Windows.Forms.LinkLabel linkPatreon;
        private System.Windows.Forms.LinkLabel linkKoFi;
        private System.Windows.Forms.Label lblBugReport;
        private System.Windows.Forms.LinkLabel linkBugReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyLinkToolStripMenuItem;
    }
}