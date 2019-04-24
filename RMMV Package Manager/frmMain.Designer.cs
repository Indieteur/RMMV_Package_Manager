namespace RMMV_PackMan
{
    partial class frmMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.createGlobalBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreGlobalBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.createProjectBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreProjectBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browsePreMadePackagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.createNewPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGlobal = new System.Windows.Forms.TabPage();
            this.btnGlobalShowAllPack = new System.Windows.Forms.Button();
            this.btnRemovePackGlobal = new System.Windows.Forms.Button();
            this.btnInstallPackGlobal = new System.Windows.Forms.Button();
            this.groupInfoGlobal = new System.Windows.Forms.GroupBox();
            this.btnGlobalViewAssets = new System.Windows.Forms.Button();
            this.btnReinstallGlobal = new System.Windows.Forms.Button();
            this.btnUpdPackGlobal = new System.Windows.Forms.Button();
            this.linkLicGlobal = new System.Windows.Forms.LinkLabel();
            this.lblLicGlobal = new System.Windows.Forms.Label();
            this.linkPackURLGlobal = new System.Windows.Forms.LinkLabel();
            this.lblPackURLGlobal = new System.Windows.Forms.Label();
            this.txtVersionGlobal = new System.Windows.Forms.TextBox();
            this.lblVersionGlobal = new System.Windows.Forms.Label();
            this.txtDescGlobal = new System.Windows.Forms.TextBox();
            this.lblDescGlobal = new System.Windows.Forms.Label();
            this.txtAuthorGlobal = new System.Windows.Forms.TextBox();
            this.lblAuthorGlobal = new System.Windows.Forms.Label();
            this.txtNameGlobal = new System.Windows.Forms.TextBox();
            this.lblNameGlobal = new System.Windows.Forms.Label();
            this.btnSearchGlobal = new System.Windows.Forms.Button();
            this.txtSearchGlobal = new System.Windows.Forms.TextBox();
            this.lblCurInstalledGlobal = new System.Windows.Forms.Label();
            this.listCurrentInstalledGlobal = new System.Windows.Forms.ListBox();
            this.tabPageLocal = new System.Windows.Forms.TabPage();
            this.btnLocalShowAllPack = new System.Windows.Forms.Button();
            this.btnBrowseProj = new System.Windows.Forms.Button();
            this.txtCurOpenProj = new System.Windows.Forms.TextBox();
            this.lblCurOpenProj = new System.Windows.Forms.Label();
            this.btnRemovePackLocal = new System.Windows.Forms.Button();
            this.btnInstallPackLocal = new System.Windows.Forms.Button();
            this.groupInfoLocal = new System.Windows.Forms.GroupBox();
            this.btnLocalViewAssets = new System.Windows.Forms.Button();
            this.btnReinstallLocal = new System.Windows.Forms.Button();
            this.btnUpdPackLocal = new System.Windows.Forms.Button();
            this.linkLicLocal = new System.Windows.Forms.LinkLabel();
            this.lblLicLocal = new System.Windows.Forms.Label();
            this.linkPackURLLocal = new System.Windows.Forms.LinkLabel();
            this.lblPackURLlocal = new System.Windows.Forms.Label();
            this.txtVersionLocal = new System.Windows.Forms.TextBox();
            this.lblVersionLocal = new System.Windows.Forms.Label();
            this.txtDescLocal = new System.Windows.Forms.TextBox();
            this.lblDescLocal = new System.Windows.Forms.Label();
            this.txtAuthorLocal = new System.Windows.Forms.TextBox();
            this.lblAuthorLocal = new System.Windows.Forms.Label();
            this.txtNameLocal = new System.Windows.Forms.TextBox();
            this.lblNameLocal = new System.Windows.Forms.Label();
            this.btnSearchLocal = new System.Windows.Forms.Button();
            this.txtSearchLocal = new System.Windows.Forms.TextBox();
            this.lblCurInstalledLocal = new System.Windows.Forms.Label();
            this.listCurInstalledLocal = new System.Windows.Forms.ListBox();
            this.btnSupportPatreon = new System.Windows.Forms.Button();
            this.btnDonateKoFi = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageGlobal.SuspendLayout();
            this.groupInfoGlobal.SuspendLayout();
            this.tabPageLocal.SuspendLayout();
            this.groupInfoLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.packagesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(820, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectToolStripMenuItem,
            this.reloadProjectToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator3,
            this.createGlobalBackupToolStripMenuItem,
            this.restoreGlobalBackupToolStripMenuItem,
            this.toolStripSeparator4,
            this.createProjectBackupToolStripMenuItem,
            this.restoreProjectBackupToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openProjectToolStripMenuItem.Text = "&Open Project...";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // reloadProjectToolStripMenuItem
            // 
            this.reloadProjectToolStripMenuItem.Enabled = false;
            this.reloadProjectToolStripMenuItem.Name = "reloadProjectToolStripMenuItem";
            this.reloadProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.reloadProjectToolStripMenuItem.Text = "Reload Pro&ject";
            this.reloadProjectToolStripMenuItem.Click += new System.EventHandler(this.reloadProjectToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Enabled = false;
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.closeProjectToolStripMenuItem.Text = "&Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
            // 
            // createGlobalBackupToolStripMenuItem
            // 
            this.createGlobalBackupToolStripMenuItem.Name = "createGlobalBackupToolStripMenuItem";
            this.createGlobalBackupToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.createGlobalBackupToolStripMenuItem.Text = "Create G&lobal Backup";
            this.createGlobalBackupToolStripMenuItem.Click += new System.EventHandler(this.createGlobalBackupToolStripMenuItem_Click);
            // 
            // restoreGlobalBackupToolStripMenuItem
            // 
            this.restoreGlobalBackupToolStripMenuItem.Name = "restoreGlobalBackupToolStripMenuItem";
            this.restoreGlobalBackupToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.restoreGlobalBackupToolStripMenuItem.Text = "Restore &Global Backup";
            this.restoreGlobalBackupToolStripMenuItem.Click += new System.EventHandler(this.restoreGlobalBackupToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(192, 6);
            // 
            // createProjectBackupToolStripMenuItem
            // 
            this.createProjectBackupToolStripMenuItem.Enabled = false;
            this.createProjectBackupToolStripMenuItem.Name = "createProjectBackupToolStripMenuItem";
            this.createProjectBackupToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.createProjectBackupToolStripMenuItem.Text = "Create P&roject Backup";
            this.createProjectBackupToolStripMenuItem.Click += new System.EventHandler(this.createProjectBackupToolStripMenuItem_Click);
            // 
            // restoreProjectBackupToolStripMenuItem
            // 
            this.restoreProjectBackupToolStripMenuItem.Enabled = false;
            this.restoreProjectBackupToolStripMenuItem.Name = "restoreProjectBackupToolStripMenuItem";
            this.restoreProjectBackupToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.restoreProjectBackupToolStripMenuItem.Text = "Restore &Project Backup";
            this.restoreProjectBackupToolStripMenuItem.Click += new System.EventHandler(this.restoreProjectBackupToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt + F4";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // packagesToolStripMenuItem
            // 
            this.packagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browsePreMadePackagesToolStripMenuItem,
            this.toolStripSeparator5,
            this.createNewPackageToolStripMenuItem,
            this.modifyPackageToolStripMenuItem});
            this.packagesToolStripMenuItem.Name = "packagesToolStripMenuItem";
            this.packagesToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.packagesToolStripMenuItem.Text = "&Packages";
            // 
            // browsePreMadePackagesToolStripMenuItem
            // 
            this.browsePreMadePackagesToolStripMenuItem.Name = "browsePreMadePackagesToolStripMenuItem";
            this.browsePreMadePackagesToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.browsePreMadePackagesToolStripMenuItem.Text = "Browse Pre-Made Packages";
            this.browsePreMadePackagesToolStripMenuItem.Click += new System.EventHandler(this.browsePreMadePackagesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(231, 6);
            // 
            // createNewPackageToolStripMenuItem
            // 
            this.createNewPackageToolStripMenuItem.Name = "createNewPackageToolStripMenuItem";
            this.createNewPackageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createNewPackageToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.createNewPackageToolStripMenuItem.Text = "Create &New Package...";
            this.createNewPackageToolStripMenuItem.Click += new System.EventHandler(this.createNewPackageToolStripMenuItem_Click);
            // 
            // modifyPackageToolStripMenuItem
            // 
            this.modifyPackageToolStripMenuItem.Name = "modifyPackageToolStripMenuItem";
            this.modifyPackageToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.modifyPackageToolStripMenuItem.Text = "&Modify a Package...";
            this.modifyPackageToolStripMenuItem.Click += new System.EventHandler(this.modifyPackageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewLogToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // viewLogToolStripMenuItem
            // 
            this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewLogToolStripMenuItem.Text = "View Log...";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "&Check for Updates...";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGlobal);
            this.tabControl.Controls.Add(this.tabPageLocal);
            this.tabControl.Location = new System.Drawing.Point(12, 56);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(796, 422);
            this.tabControl.TabIndex = 2;
            // 
            // tabPageGlobal
            // 
            this.tabPageGlobal.Controls.Add(this.btnGlobalShowAllPack);
            this.tabPageGlobal.Controls.Add(this.btnRemovePackGlobal);
            this.tabPageGlobal.Controls.Add(this.btnInstallPackGlobal);
            this.tabPageGlobal.Controls.Add(this.groupInfoGlobal);
            this.tabPageGlobal.Controls.Add(this.btnSearchGlobal);
            this.tabPageGlobal.Controls.Add(this.txtSearchGlobal);
            this.tabPageGlobal.Controls.Add(this.lblCurInstalledGlobal);
            this.tabPageGlobal.Controls.Add(this.listCurrentInstalledGlobal);
            this.tabPageGlobal.Location = new System.Drawing.Point(4, 22);
            this.tabPageGlobal.Name = "tabPageGlobal";
            this.tabPageGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGlobal.Size = new System.Drawing.Size(788, 396);
            this.tabPageGlobal.TabIndex = 0;
            this.tabPageGlobal.Text = "Global Packages";
            this.tabPageGlobal.UseVisualStyleBackColor = true;
            // 
            // btnGlobalShowAllPack
            // 
            this.btnGlobalShowAllPack.Location = new System.Drawing.Point(262, 343);
            this.btnGlobalShowAllPack.Name = "btnGlobalShowAllPack";
            this.btnGlobalShowAllPack.Size = new System.Drawing.Size(109, 23);
            this.btnGlobalShowAllPack.TabIndex = 7;
            this.btnGlobalShowAllPack.Text = "&Show All Packages";
            this.btnGlobalShowAllPack.UseVisualStyleBackColor = true;
            this.btnGlobalShowAllPack.Click += new System.EventHandler(this.btnGlobalShowAllPack_Click);
            // 
            // btnRemovePackGlobal
            // 
            this.btnRemovePackGlobal.Enabled = false;
            this.btnRemovePackGlobal.Location = new System.Drawing.Point(142, 343);
            this.btnRemovePackGlobal.Name = "btnRemovePackGlobal";
            this.btnRemovePackGlobal.Size = new System.Drawing.Size(114, 23);
            this.btnRemovePackGlobal.TabIndex = 6;
            this.btnRemovePackGlobal.Text = "Remo&ve Package";
            this.btnRemovePackGlobal.UseVisualStyleBackColor = true;
            this.btnRemovePackGlobal.Click += new System.EventHandler(this.btnRemovePackGlobal_Click);
            // 
            // btnInstallPackGlobal
            // 
            this.btnInstallPackGlobal.Location = new System.Drawing.Point(17, 343);
            this.btnInstallPackGlobal.Name = "btnInstallPackGlobal";
            this.btnInstallPackGlobal.Size = new System.Drawing.Size(119, 23);
            this.btnInstallPackGlobal.TabIndex = 5;
            this.btnInstallPackGlobal.Text = "&Install New Package";
            this.btnInstallPackGlobal.UseVisualStyleBackColor = true;
            this.btnInstallPackGlobal.Click += new System.EventHandler(this.btnInstallPackGlobal_Click);
            // 
            // groupInfoGlobal
            // 
            this.groupInfoGlobal.Controls.Add(this.btnGlobalViewAssets);
            this.groupInfoGlobal.Controls.Add(this.btnReinstallGlobal);
            this.groupInfoGlobal.Controls.Add(this.btnUpdPackGlobal);
            this.groupInfoGlobal.Controls.Add(this.linkLicGlobal);
            this.groupInfoGlobal.Controls.Add(this.lblLicGlobal);
            this.groupInfoGlobal.Controls.Add(this.linkPackURLGlobal);
            this.groupInfoGlobal.Controls.Add(this.lblPackURLGlobal);
            this.groupInfoGlobal.Controls.Add(this.txtVersionGlobal);
            this.groupInfoGlobal.Controls.Add(this.lblVersionGlobal);
            this.groupInfoGlobal.Controls.Add(this.txtDescGlobal);
            this.groupInfoGlobal.Controls.Add(this.lblDescGlobal);
            this.groupInfoGlobal.Controls.Add(this.txtAuthorGlobal);
            this.groupInfoGlobal.Controls.Add(this.lblAuthorGlobal);
            this.groupInfoGlobal.Controls.Add(this.txtNameGlobal);
            this.groupInfoGlobal.Controls.Add(this.lblNameGlobal);
            this.groupInfoGlobal.Enabled = false;
            this.groupInfoGlobal.Location = new System.Drawing.Point(377, 23);
            this.groupInfoGlobal.Name = "groupInfoGlobal";
            this.groupInfoGlobal.Size = new System.Drawing.Size(405, 365);
            this.groupInfoGlobal.TabIndex = 4;
            this.groupInfoGlobal.TabStop = false;
            this.groupInfoGlobal.Text = "Information";
            // 
            // btnGlobalViewAssets
            // 
            this.btnGlobalViewAssets.Enabled = false;
            this.btnGlobalViewAssets.Location = new System.Drawing.Point(18, 330);
            this.btnGlobalViewAssets.Name = "btnGlobalViewAssets";
            this.btnGlobalViewAssets.Size = new System.Drawing.Size(131, 31);
            this.btnGlobalViewAssets.TabIndex = 14;
            this.btnGlobalViewAssets.Text = "&View Assets";
            this.btnGlobalViewAssets.UseVisualStyleBackColor = true;
            this.btnGlobalViewAssets.Click += new System.EventHandler(this.btnGlobalViewAssets_Click);
            // 
            // btnReinstallGlobal
            // 
            this.btnReinstallGlobal.Enabled = false;
            this.btnReinstallGlobal.Location = new System.Drawing.Point(155, 330);
            this.btnReinstallGlobal.Name = "btnReinstallGlobal";
            this.btnReinstallGlobal.Size = new System.Drawing.Size(107, 31);
            this.btnReinstallGlobal.TabIndex = 13;
            this.btnReinstallGlobal.Text = "&Reinstall";
            this.btnReinstallGlobal.UseVisualStyleBackColor = true;
            this.btnReinstallGlobal.Click += new System.EventHandler(this.btnReinstallGlobal_Click);
            // 
            // btnUpdPackGlobal
            // 
            this.btnUpdPackGlobal.Enabled = false;
            this.btnUpdPackGlobal.Location = new System.Drawing.Point(268, 330);
            this.btnUpdPackGlobal.Name = "btnUpdPackGlobal";
            this.btnUpdPackGlobal.Size = new System.Drawing.Size(131, 31);
            this.btnUpdPackGlobal.TabIndex = 12;
            this.btnUpdPackGlobal.Text = "&Update Package";
            this.btnUpdPackGlobal.UseVisualStyleBackColor = true;
            this.btnUpdPackGlobal.Click += new System.EventHandler(this.btnUpdPackGlobal_Click);
            // 
            // linkLicGlobal
            // 
            this.linkLicGlobal.AutoSize = true;
            this.linkLicGlobal.Enabled = false;
            this.linkLicGlobal.Location = new System.Drawing.Point(104, 107);
            this.linkLicGlobal.Name = "linkLicGlobal";
            this.linkLicGlobal.Size = new System.Drawing.Size(27, 13);
            this.linkLicGlobal.TabIndex = 11;
            this.linkLicGlobal.TabStop = true;
            this.linkLicGlobal.Text = "N/A";
            this.linkLicGlobal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLicGlobal_LinkClicked);
            // 
            // lblLicGlobal
            // 
            this.lblLicGlobal.AutoSize = true;
            this.lblLicGlobal.Location = new System.Drawing.Point(15, 107);
            this.lblLicGlobal.Name = "lblLicGlobal";
            this.lblLicGlobal.Size = new System.Drawing.Size(47, 13);
            this.lblLicGlobal.TabIndex = 10;
            this.lblLicGlobal.Text = "License:";
            // 
            // linkPackURLGlobal
            // 
            this.linkPackURLGlobal.AutoSize = true;
            this.linkPackURLGlobal.Enabled = false;
            this.linkPackURLGlobal.Location = new System.Drawing.Point(104, 132);
            this.linkPackURLGlobal.Name = "linkPackURLGlobal";
            this.linkPackURLGlobal.Size = new System.Drawing.Size(27, 13);
            this.linkPackURLGlobal.TabIndex = 9;
            this.linkPackURLGlobal.TabStop = true;
            this.linkPackURLGlobal.Text = "N/A";
            this.linkPackURLGlobal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPackURLGlobal_LinkClicked);
            // 
            // lblPackURLGlobal
            // 
            this.lblPackURLGlobal.AutoSize = true;
            this.lblPackURLGlobal.Location = new System.Drawing.Point(15, 132);
            this.lblPackURLGlobal.Name = "lblPackURLGlobal";
            this.lblPackURLGlobal.Size = new System.Drawing.Size(78, 13);
            this.lblPackURLGlobal.TabIndex = 8;
            this.lblPackURLGlobal.Text = "Package URL:";
            // 
            // txtVersionGlobal
            // 
            this.txtVersionGlobal.Location = new System.Drawing.Point(107, 49);
            this.txtVersionGlobal.Name = "txtVersionGlobal";
            this.txtVersionGlobal.ReadOnly = true;
            this.txtVersionGlobal.Size = new System.Drawing.Size(292, 20);
            this.txtVersionGlobal.TabIndex = 7;
            // 
            // lblVersionGlobal
            // 
            this.lblVersionGlobal.AutoSize = true;
            this.lblVersionGlobal.Location = new System.Drawing.Point(15, 52);
            this.lblVersionGlobal.Name = "lblVersionGlobal";
            this.lblVersionGlobal.Size = new System.Drawing.Size(45, 13);
            this.lblVersionGlobal.TabIndex = 6;
            this.lblVersionGlobal.Text = "Version:";
            // 
            // txtDescGlobal
            // 
            this.txtDescGlobal.Location = new System.Drawing.Point(18, 171);
            this.txtDescGlobal.Multiline = true;
            this.txtDescGlobal.Name = "txtDescGlobal";
            this.txtDescGlobal.ReadOnly = true;
            this.txtDescGlobal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescGlobal.Size = new System.Drawing.Size(381, 151);
            this.txtDescGlobal.TabIndex = 5;
            // 
            // lblDescGlobal
            // 
            this.lblDescGlobal.AutoSize = true;
            this.lblDescGlobal.Location = new System.Drawing.Point(15, 155);
            this.lblDescGlobal.Name = "lblDescGlobal";
            this.lblDescGlobal.Size = new System.Drawing.Size(63, 13);
            this.lblDescGlobal.TabIndex = 4;
            this.lblDescGlobal.Text = "Description:";
            // 
            // txtAuthorGlobal
            // 
            this.txtAuthorGlobal.Location = new System.Drawing.Point(107, 75);
            this.txtAuthorGlobal.Name = "txtAuthorGlobal";
            this.txtAuthorGlobal.ReadOnly = true;
            this.txtAuthorGlobal.Size = new System.Drawing.Size(292, 20);
            this.txtAuthorGlobal.TabIndex = 3;
            // 
            // lblAuthorGlobal
            // 
            this.lblAuthorGlobal.AutoSize = true;
            this.lblAuthorGlobal.Location = new System.Drawing.Point(15, 75);
            this.lblAuthorGlobal.Name = "lblAuthorGlobal";
            this.lblAuthorGlobal.Size = new System.Drawing.Size(41, 13);
            this.lblAuthorGlobal.TabIndex = 2;
            this.lblAuthorGlobal.Text = "Author:";
            // 
            // txtNameGlobal
            // 
            this.txtNameGlobal.Location = new System.Drawing.Point(107, 23);
            this.txtNameGlobal.Name = "txtNameGlobal";
            this.txtNameGlobal.ReadOnly = true;
            this.txtNameGlobal.Size = new System.Drawing.Size(292, 20);
            this.txtNameGlobal.TabIndex = 1;
            // 
            // lblNameGlobal
            // 
            this.lblNameGlobal.AutoSize = true;
            this.lblNameGlobal.Location = new System.Drawing.Point(15, 30);
            this.lblNameGlobal.Name = "lblNameGlobal";
            this.lblNameGlobal.Size = new System.Drawing.Size(38, 13);
            this.lblNameGlobal.TabIndex = 0;
            this.lblNameGlobal.Text = "Name:";
            // 
            // btnSearchGlobal
            // 
            this.btnSearchGlobal.Location = new System.Drawing.Point(262, 371);
            this.btnSearchGlobal.Name = "btnSearchGlobal";
            this.btnSearchGlobal.Size = new System.Drawing.Size(109, 21);
            this.btnSearchGlobal.TabIndex = 3;
            this.btnSearchGlobal.Text = "&Search";
            this.btnSearchGlobal.UseVisualStyleBackColor = true;
            this.btnSearchGlobal.Click += new System.EventHandler(this.btnSearchGlobal_Click);
            // 
            // txtSearchGlobal
            // 
            this.txtSearchGlobal.Location = new System.Drawing.Point(17, 371);
            this.txtSearchGlobal.Name = "txtSearchGlobal";
            this.txtSearchGlobal.Size = new System.Drawing.Size(239, 20);
            this.txtSearchGlobal.TabIndex = 2;
            this.txtSearchGlobal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchGlobal_KeyDown);
            // 
            // lblCurInstalledGlobal
            // 
            this.lblCurInstalledGlobal.AutoSize = true;
            this.lblCurInstalledGlobal.Location = new System.Drawing.Point(14, 7);
            this.lblCurInstalledGlobal.Name = "lblCurInstalledGlobal";
            this.lblCurInstalledGlobal.Size = new System.Drawing.Size(93, 13);
            this.lblCurInstalledGlobal.TabIndex = 1;
            this.lblCurInstalledGlobal.Text = "Currently Installed:";
            // 
            // listCurrentInstalledGlobal
            // 
            this.listCurrentInstalledGlobal.FormattingEnabled = true;
            this.listCurrentInstalledGlobal.Location = new System.Drawing.Point(17, 23);
            this.listCurrentInstalledGlobal.Name = "listCurrentInstalledGlobal";
            this.listCurrentInstalledGlobal.Size = new System.Drawing.Size(354, 316);
            this.listCurrentInstalledGlobal.TabIndex = 0;
            this.listCurrentInstalledGlobal.SelectedIndexChanged += new System.EventHandler(this.listCurrentInstalledGlobal_SelectedIndexChanged);
            // 
            // tabPageLocal
            // 
            this.tabPageLocal.Controls.Add(this.btnLocalShowAllPack);
            this.tabPageLocal.Controls.Add(this.btnBrowseProj);
            this.tabPageLocal.Controls.Add(this.txtCurOpenProj);
            this.tabPageLocal.Controls.Add(this.lblCurOpenProj);
            this.tabPageLocal.Controls.Add(this.btnRemovePackLocal);
            this.tabPageLocal.Controls.Add(this.btnInstallPackLocal);
            this.tabPageLocal.Controls.Add(this.groupInfoLocal);
            this.tabPageLocal.Controls.Add(this.btnSearchLocal);
            this.tabPageLocal.Controls.Add(this.txtSearchLocal);
            this.tabPageLocal.Controls.Add(this.lblCurInstalledLocal);
            this.tabPageLocal.Controls.Add(this.listCurInstalledLocal);
            this.tabPageLocal.Location = new System.Drawing.Point(4, 22);
            this.tabPageLocal.Name = "tabPageLocal";
            this.tabPageLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLocal.Size = new System.Drawing.Size(788, 396);
            this.tabPageLocal.TabIndex = 1;
            this.tabPageLocal.Text = "Project Specific Packages";
            this.tabPageLocal.UseVisualStyleBackColor = true;
            // 
            // btnLocalShowAllPack
            // 
            this.btnLocalShowAllPack.Enabled = false;
            this.btnLocalShowAllPack.Location = new System.Drawing.Point(262, 343);
            this.btnLocalShowAllPack.Name = "btnLocalShowAllPack";
            this.btnLocalShowAllPack.Size = new System.Drawing.Size(109, 23);
            this.btnLocalShowAllPack.TabIndex = 15;
            this.btnLocalShowAllPack.Text = "&Show All Packages";
            this.btnLocalShowAllPack.UseVisualStyleBackColor = true;
            this.btnLocalShowAllPack.Click += new System.EventHandler(this.btnLocalShowAllPack_Click);
            // 
            // btnBrowseProj
            // 
            this.btnBrowseProj.Location = new System.Drawing.Point(690, 8);
            this.btnBrowseProj.Name = "btnBrowseProj";
            this.btnBrowseProj.Size = new System.Drawing.Size(92, 26);
            this.btnBrowseProj.TabIndex = 13;
            this.btnBrowseProj.Text = "&Browse...";
            this.btnBrowseProj.UseVisualStyleBackColor = true;
            this.btnBrowseProj.Click += new System.EventHandler(this.btnBrowseProj_Click);
            // 
            // txtCurOpenProj
            // 
            this.txtCurOpenProj.Enabled = false;
            this.txtCurOpenProj.Location = new System.Drawing.Point(178, 12);
            this.txtCurOpenProj.Name = "txtCurOpenProj";
            this.txtCurOpenProj.ReadOnly = true;
            this.txtCurOpenProj.Size = new System.Drawing.Size(506, 20);
            this.txtCurOpenProj.TabIndex = 13;
            // 
            // lblCurOpenProj
            // 
            this.lblCurOpenProj.AutoSize = true;
            this.lblCurOpenProj.Location = new System.Drawing.Point(14, 15);
            this.lblCurOpenProj.Name = "lblCurOpenProj";
            this.lblCurOpenProj.Size = new System.Drawing.Size(153, 13);
            this.lblCurOpenProj.TabIndex = 14;
            this.lblCurOpenProj.Text = "Currently Opened Project Path:";
            // 
            // btnRemovePackLocal
            // 
            this.btnRemovePackLocal.Enabled = false;
            this.btnRemovePackLocal.Location = new System.Drawing.Point(142, 343);
            this.btnRemovePackLocal.Name = "btnRemovePackLocal";
            this.btnRemovePackLocal.Size = new System.Drawing.Size(114, 23);
            this.btnRemovePackLocal.TabIndex = 13;
            this.btnRemovePackLocal.Text = "Remo&ve Package";
            this.btnRemovePackLocal.UseVisualStyleBackColor = true;
            this.btnRemovePackLocal.Click += new System.EventHandler(this.btnRemovePackLocal_Click);
            // 
            // btnInstallPackLocal
            // 
            this.btnInstallPackLocal.Enabled = false;
            this.btnInstallPackLocal.Location = new System.Drawing.Point(17, 343);
            this.btnInstallPackLocal.Name = "btnInstallPackLocal";
            this.btnInstallPackLocal.Size = new System.Drawing.Size(119, 23);
            this.btnInstallPackLocal.TabIndex = 12;
            this.btnInstallPackLocal.Text = "&Install New Package";
            this.btnInstallPackLocal.UseVisualStyleBackColor = true;
            this.btnInstallPackLocal.Click += new System.EventHandler(this.btnInstallPackLocal_Click);
            // 
            // groupInfoLocal
            // 
            this.groupInfoLocal.Controls.Add(this.btnLocalViewAssets);
            this.groupInfoLocal.Controls.Add(this.btnReinstallLocal);
            this.groupInfoLocal.Controls.Add(this.btnUpdPackLocal);
            this.groupInfoLocal.Controls.Add(this.linkLicLocal);
            this.groupInfoLocal.Controls.Add(this.lblLicLocal);
            this.groupInfoLocal.Controls.Add(this.linkPackURLLocal);
            this.groupInfoLocal.Controls.Add(this.lblPackURLlocal);
            this.groupInfoLocal.Controls.Add(this.txtVersionLocal);
            this.groupInfoLocal.Controls.Add(this.lblVersionLocal);
            this.groupInfoLocal.Controls.Add(this.txtDescLocal);
            this.groupInfoLocal.Controls.Add(this.lblDescLocal);
            this.groupInfoLocal.Controls.Add(this.txtAuthorLocal);
            this.groupInfoLocal.Controls.Add(this.lblAuthorLocal);
            this.groupInfoLocal.Controls.Add(this.txtNameLocal);
            this.groupInfoLocal.Controls.Add(this.lblNameLocal);
            this.groupInfoLocal.Enabled = false;
            this.groupInfoLocal.Location = new System.Drawing.Point(377, 49);
            this.groupInfoLocal.Name = "groupInfoLocal";
            this.groupInfoLocal.Size = new System.Drawing.Size(405, 341);
            this.groupInfoLocal.TabIndex = 11;
            this.groupInfoLocal.TabStop = false;
            this.groupInfoLocal.Text = "Information";
            // 
            // btnLocalViewAssets
            // 
            this.btnLocalViewAssets.Enabled = false;
            this.btnLocalViewAssets.Location = new System.Drawing.Point(18, 304);
            this.btnLocalViewAssets.Name = "btnLocalViewAssets";
            this.btnLocalViewAssets.Size = new System.Drawing.Size(131, 31);
            this.btnLocalViewAssets.TabIndex = 15;
            this.btnLocalViewAssets.Text = "&View Assets";
            this.btnLocalViewAssets.UseVisualStyleBackColor = true;
            this.btnLocalViewAssets.Click += new System.EventHandler(this.btnLocalViewAssets_Click);
            // 
            // btnReinstallLocal
            // 
            this.btnReinstallLocal.Enabled = false;
            this.btnReinstallLocal.Location = new System.Drawing.Point(155, 304);
            this.btnReinstallLocal.Name = "btnReinstallLocal";
            this.btnReinstallLocal.Size = new System.Drawing.Size(107, 31);
            this.btnReinstallLocal.TabIndex = 14;
            this.btnReinstallLocal.Text = "&Reinstall";
            this.btnReinstallLocal.UseVisualStyleBackColor = true;
            this.btnReinstallLocal.Click += new System.EventHandler(this.btnReinstallLocal_Click);
            // 
            // btnUpdPackLocal
            // 
            this.btnUpdPackLocal.Enabled = false;
            this.btnUpdPackLocal.Location = new System.Drawing.Point(268, 304);
            this.btnUpdPackLocal.Name = "btnUpdPackLocal";
            this.btnUpdPackLocal.Size = new System.Drawing.Size(131, 31);
            this.btnUpdPackLocal.TabIndex = 12;
            this.btnUpdPackLocal.Text = "&Update Package";
            this.btnUpdPackLocal.UseVisualStyleBackColor = true;
            this.btnUpdPackLocal.Click += new System.EventHandler(this.btnUpdPackLocal_Click);
            // 
            // linkLicLocal
            // 
            this.linkLicLocal.AutoSize = true;
            this.linkLicLocal.Enabled = false;
            this.linkLicLocal.Location = new System.Drawing.Point(104, 98);
            this.linkLicLocal.Name = "linkLicLocal";
            this.linkLicLocal.Size = new System.Drawing.Size(27, 13);
            this.linkLicLocal.TabIndex = 11;
            this.linkLicLocal.TabStop = true;
            this.linkLicLocal.Text = "N/A";
            this.linkLicLocal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLicLocal_LinkClicked);
            // 
            // lblLicLocal
            // 
            this.lblLicLocal.AutoSize = true;
            this.lblLicLocal.Location = new System.Drawing.Point(15, 98);
            this.lblLicLocal.Name = "lblLicLocal";
            this.lblLicLocal.Size = new System.Drawing.Size(47, 13);
            this.lblLicLocal.TabIndex = 10;
            this.lblLicLocal.Text = "License:";
            // 
            // linkPackURLLocal
            // 
            this.linkPackURLLocal.AutoSize = true;
            this.linkPackURLLocal.Enabled = false;
            this.linkPackURLLocal.Location = new System.Drawing.Point(104, 121);
            this.linkPackURLLocal.Name = "linkPackURLLocal";
            this.linkPackURLLocal.Size = new System.Drawing.Size(27, 13);
            this.linkPackURLLocal.TabIndex = 9;
            this.linkPackURLLocal.TabStop = true;
            this.linkPackURLLocal.Text = "N/A";
            this.linkPackURLLocal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPackURLLocal_LinkClicked);
            // 
            // lblPackURLlocal
            // 
            this.lblPackURLlocal.AutoSize = true;
            this.lblPackURLlocal.Location = new System.Drawing.Point(15, 121);
            this.lblPackURLlocal.Name = "lblPackURLlocal";
            this.lblPackURLlocal.Size = new System.Drawing.Size(78, 13);
            this.lblPackURLlocal.TabIndex = 8;
            this.lblPackURLlocal.Text = "Package URL:";
            // 
            // txtVersionLocal
            // 
            this.txtVersionLocal.Location = new System.Drawing.Point(107, 40);
            this.txtVersionLocal.Name = "txtVersionLocal";
            this.txtVersionLocal.ReadOnly = true;
            this.txtVersionLocal.Size = new System.Drawing.Size(292, 20);
            this.txtVersionLocal.TabIndex = 7;
            // 
            // lblVersionLocal
            // 
            this.lblVersionLocal.AutoSize = true;
            this.lblVersionLocal.Location = new System.Drawing.Point(15, 43);
            this.lblVersionLocal.Name = "lblVersionLocal";
            this.lblVersionLocal.Size = new System.Drawing.Size(45, 13);
            this.lblVersionLocal.TabIndex = 6;
            this.lblVersionLocal.Text = "Version:";
            // 
            // txtDescLocal
            // 
            this.txtDescLocal.Location = new System.Drawing.Point(18, 162);
            this.txtDescLocal.Multiline = true;
            this.txtDescLocal.Name = "txtDescLocal";
            this.txtDescLocal.ReadOnly = true;
            this.txtDescLocal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescLocal.Size = new System.Drawing.Size(381, 136);
            this.txtDescLocal.TabIndex = 5;
            // 
            // lblDescLocal
            // 
            this.lblDescLocal.AutoSize = true;
            this.lblDescLocal.Location = new System.Drawing.Point(15, 146);
            this.lblDescLocal.Name = "lblDescLocal";
            this.lblDescLocal.Size = new System.Drawing.Size(63, 13);
            this.lblDescLocal.TabIndex = 4;
            this.lblDescLocal.Text = "Description:";
            // 
            // txtAuthorLocal
            // 
            this.txtAuthorLocal.Location = new System.Drawing.Point(107, 66);
            this.txtAuthorLocal.Name = "txtAuthorLocal";
            this.txtAuthorLocal.ReadOnly = true;
            this.txtAuthorLocal.Size = new System.Drawing.Size(292, 20);
            this.txtAuthorLocal.TabIndex = 3;
            // 
            // lblAuthorLocal
            // 
            this.lblAuthorLocal.AutoSize = true;
            this.lblAuthorLocal.Location = new System.Drawing.Point(15, 66);
            this.lblAuthorLocal.Name = "lblAuthorLocal";
            this.lblAuthorLocal.Size = new System.Drawing.Size(41, 13);
            this.lblAuthorLocal.TabIndex = 2;
            this.lblAuthorLocal.Text = "Author:";
            // 
            // txtNameLocal
            // 
            this.txtNameLocal.Location = new System.Drawing.Point(107, 14);
            this.txtNameLocal.Name = "txtNameLocal";
            this.txtNameLocal.ReadOnly = true;
            this.txtNameLocal.Size = new System.Drawing.Size(292, 20);
            this.txtNameLocal.TabIndex = 1;
            // 
            // lblNameLocal
            // 
            this.lblNameLocal.AutoSize = true;
            this.lblNameLocal.Location = new System.Drawing.Point(15, 21);
            this.lblNameLocal.Name = "lblNameLocal";
            this.lblNameLocal.Size = new System.Drawing.Size(38, 13);
            this.lblNameLocal.TabIndex = 0;
            this.lblNameLocal.Text = "Name:";
            // 
            // btnSearchLocal
            // 
            this.btnSearchLocal.Enabled = false;
            this.btnSearchLocal.Location = new System.Drawing.Point(262, 371);
            this.btnSearchLocal.Name = "btnSearchLocal";
            this.btnSearchLocal.Size = new System.Drawing.Size(109, 21);
            this.btnSearchLocal.TabIndex = 10;
            this.btnSearchLocal.Text = "&Search";
            this.btnSearchLocal.UseVisualStyleBackColor = true;
            this.btnSearchLocal.Click += new System.EventHandler(this.btnSearchLocal_Click);
            // 
            // txtSearchLocal
            // 
            this.txtSearchLocal.Enabled = false;
            this.txtSearchLocal.Location = new System.Drawing.Point(17, 371);
            this.txtSearchLocal.Name = "txtSearchLocal";
            this.txtSearchLocal.Size = new System.Drawing.Size(239, 20);
            this.txtSearchLocal.TabIndex = 9;
            this.txtSearchLocal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchLocal_KeyDown);
            // 
            // lblCurInstalledLocal
            // 
            this.lblCurInstalledLocal.AutoSize = true;
            this.lblCurInstalledLocal.Enabled = false;
            this.lblCurInstalledLocal.Location = new System.Drawing.Point(14, 33);
            this.lblCurInstalledLocal.Name = "lblCurInstalledLocal";
            this.lblCurInstalledLocal.Size = new System.Drawing.Size(93, 13);
            this.lblCurInstalledLocal.TabIndex = 8;
            this.lblCurInstalledLocal.Text = "Currently Installed:";
            // 
            // listCurInstalledLocal
            // 
            this.listCurInstalledLocal.Enabled = false;
            this.listCurInstalledLocal.FormattingEnabled = true;
            this.listCurInstalledLocal.Location = new System.Drawing.Point(17, 49);
            this.listCurInstalledLocal.Name = "listCurInstalledLocal";
            this.listCurInstalledLocal.Size = new System.Drawing.Size(354, 290);
            this.listCurInstalledLocal.TabIndex = 7;
            this.listCurInstalledLocal.SelectedIndexChanged += new System.EventHandler(this.listCurInstalledLocal_SelectedIndexChanged);
            // 
            // btnSupportPatreon
            // 
            this.btnSupportPatreon.Location = new System.Drawing.Point(688, 27);
            this.btnSupportPatreon.Name = "btnSupportPatreon";
            this.btnSupportPatreon.Size = new System.Drawing.Size(120, 45);
            this.btnSupportPatreon.TabIndex = 3;
            this.btnSupportPatreon.Text = "Support developer on Patreon";
            this.btnSupportPatreon.UseVisualStyleBackColor = true;
            this.btnSupportPatreon.Click += new System.EventHandler(this.btnSupportPatreon_Click);
            // 
            // btnDonateKoFi
            // 
            this.btnDonateKoFi.Location = new System.Drawing.Point(563, 27);
            this.btnDonateKoFi.Name = "btnDonateKoFi";
            this.btnDonateKoFi.Size = new System.Drawing.Size(119, 45);
            this.btnDonateKoFi.TabIndex = 4;
            this.btnDonateKoFi.Text = "Donate to developer on Ko-fi";
            this.btnDonateKoFi.UseVisualStyleBackColor = true;
            this.btnDonateKoFi.Click += new System.EventHandler(this.btnDonateKoFi_Click);
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 482);
            this.Controls.Add(this.btnDonateKoFi);
            this.Controls.Add(this.btnSupportPatreon);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "RMMV Package Manager";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageGlobal.ResumeLayout(false);
            this.tabPageGlobal.PerformLayout();
            this.groupInfoGlobal.ResumeLayout(false);
            this.groupInfoGlobal.PerformLayout();
            this.tabPageLocal.ResumeLayout(false);
            this.tabPageLocal.PerformLayout();
            this.groupInfoLocal.ResumeLayout(false);
            this.groupInfoLocal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        internal System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGlobal;
        private System.Windows.Forms.Button btnRemovePackGlobal;
        private System.Windows.Forms.Button btnInstallPackGlobal;
        private System.Windows.Forms.GroupBox groupInfoGlobal;
        private System.Windows.Forms.Button btnSearchGlobal;
        private System.Windows.Forms.TextBox txtSearchGlobal;
        private System.Windows.Forms.Label lblCurInstalledGlobal;
        private System.Windows.Forms.ListBox listCurrentInstalledGlobal;
        private System.Windows.Forms.TabPage tabPageLocal;
        private System.Windows.Forms.Label lblNameGlobal;
        private System.Windows.Forms.Button btnUpdPackGlobal;
        private System.Windows.Forms.LinkLabel linkLicGlobal;
        private System.Windows.Forms.Label lblLicGlobal;
        private System.Windows.Forms.LinkLabel linkPackURLGlobal;
        private System.Windows.Forms.Label lblPackURLGlobal;
        private System.Windows.Forms.TextBox txtVersionGlobal;
        private System.Windows.Forms.Label lblVersionGlobal;
        private System.Windows.Forms.TextBox txtDescGlobal;
        private System.Windows.Forms.Label lblDescGlobal;
        private System.Windows.Forms.TextBox txtAuthorGlobal;
        private System.Windows.Forms.Label lblAuthorGlobal;
        private System.Windows.Forms.TextBox txtNameGlobal;
        private System.Windows.Forms.Button btnBrowseProj;
        private System.Windows.Forms.Label lblCurOpenProj;
        private System.Windows.Forms.Button btnRemovePackLocal;
        private System.Windows.Forms.Button btnInstallPackLocal;
        private System.Windows.Forms.GroupBox groupInfoLocal;
        private System.Windows.Forms.Button btnUpdPackLocal;
        private System.Windows.Forms.LinkLabel linkLicLocal;
        private System.Windows.Forms.Label lblLicLocal;
        private System.Windows.Forms.LinkLabel linkPackURLLocal;
        private System.Windows.Forms.Label lblPackURLlocal;
        private System.Windows.Forms.TextBox txtVersionLocal;
        private System.Windows.Forms.Label lblVersionLocal;
        private System.Windows.Forms.TextBox txtDescLocal;
        private System.Windows.Forms.Label lblDescLocal;
        private System.Windows.Forms.TextBox txtAuthorLocal;
        private System.Windows.Forms.Label lblAuthorLocal;
        private System.Windows.Forms.TextBox txtNameLocal;
        private System.Windows.Forms.Label lblNameLocal;
        private System.Windows.Forms.Button btnSearchLocal;
        private System.Windows.Forms.TextBox txtSearchLocal;
        private System.Windows.Forms.Label lblCurInstalledLocal;
        private System.Windows.Forms.ListBox listCurInstalledLocal;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button btnSupportPatreon;
        private System.Windows.Forms.Button btnDonateKoFi;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnReinstallGlobal;
        private System.Windows.Forms.Button btnReinstallLocal;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem restoreGlobalBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreProjectBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem createGlobalBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createProjectBackupToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.TextBox txtCurOpenProj;
        private System.Windows.Forms.ToolStripMenuItem reloadProjectToolStripMenuItem;
        private System.Windows.Forms.Button btnGlobalViewAssets;
        private System.Windows.Forms.Button btnLocalViewAssets;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
        private System.Windows.Forms.Button btnGlobalShowAllPack;
        private System.Windows.Forms.Button btnLocalShowAllPack;
        private System.Windows.Forms.ToolStripMenuItem browsePreMadePackagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

