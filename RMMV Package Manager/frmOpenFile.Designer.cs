namespace RMMV_PackMan
{
    partial class frmOpenFile
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
            this.groupLaunchOpt = new System.Windows.Forms.GroupBox();
            this.lblAppPath = new System.Windows.Forms.Label();
            this.txtAppPath = new System.Windows.Forms.TextBox();
            this.btnBrowseApp = new System.Windows.Forms.Button();
            this.cboxCustomArg = new System.Windows.Forms.CheckBox();
            this.lblArg = new System.Windows.Forms.Label();
            this.txtArg = new System.Windows.Forms.TextBox();
            this.groupOpenWith = new System.Windows.Forms.GroupBox();
            this.radioDefaultLaunch = new System.Windows.Forms.RadioButton();
            this.radioCustomLaunch = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupLaunchOpt.SuspendLayout();
            this.groupOpenWith.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(403, 214);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(322, 213);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupLaunchOpt
            // 
            this.groupLaunchOpt.Controls.Add(this.txtArg);
            this.groupLaunchOpt.Controls.Add(this.lblArg);
            this.groupLaunchOpt.Controls.Add(this.cboxCustomArg);
            this.groupLaunchOpt.Controls.Add(this.btnBrowseApp);
            this.groupLaunchOpt.Controls.Add(this.txtAppPath);
            this.groupLaunchOpt.Controls.Add(this.lblAppPath);
            this.groupLaunchOpt.Enabled = false;
            this.groupLaunchOpt.Location = new System.Drawing.Point(12, 88);
            this.groupLaunchOpt.Name = "groupLaunchOpt";
            this.groupLaunchOpt.Size = new System.Drawing.Size(461, 119);
            this.groupLaunchOpt.TabIndex = 6;
            this.groupLaunchOpt.TabStop = false;
            this.groupLaunchOpt.Text = "Launch Options";
            // 
            // lblAppPath
            // 
            this.lblAppPath.AutoSize = true;
            this.lblAppPath.Enabled = false;
            this.lblAppPath.Location = new System.Drawing.Point(9, 29);
            this.lblAppPath.Name = "lblAppPath";
            this.lblAppPath.Size = new System.Drawing.Size(54, 13);
            this.lblAppPath.TabIndex = 2;
            this.lblAppPath.Text = "App Path:";
            // 
            // txtAppPath
            // 
            this.txtAppPath.Location = new System.Drawing.Point(72, 26);
            this.txtAppPath.Name = "txtAppPath";
            this.txtAppPath.ReadOnly = true;
            this.txtAppPath.Size = new System.Drawing.Size(283, 20);
            this.txtAppPath.TabIndex = 3;
            // 
            // btnBrowseApp
            // 
            this.btnBrowseApp.Location = new System.Drawing.Point(361, 26);
            this.btnBrowseApp.Name = "btnBrowseApp";
            this.btnBrowseApp.Size = new System.Drawing.Size(75, 21);
            this.btnBrowseApp.TabIndex = 5;
            this.btnBrowseApp.Text = "&Browse";
            this.btnBrowseApp.UseVisualStyleBackColor = true;
            this.btnBrowseApp.Click += new System.EventHandler(this.btnBrowseApp_Click);
            // 
            // cboxCustomArg
            // 
            this.cboxCustomArg.AutoSize = true;
            this.cboxCustomArg.Location = new System.Drawing.Point(12, 59);
            this.cboxCustomArg.Name = "cboxCustomArg";
            this.cboxCustomArg.Size = new System.Drawing.Size(131, 17);
            this.cboxCustomArg.TabIndex = 6;
            this.cboxCustomArg.Text = "Use &Custom Argument";
            this.cboxCustomArg.UseVisualStyleBackColor = true;
            this.cboxCustomArg.CheckedChanged += new System.EventHandler(this.cboxCustomArg_CheckedChanged);
            // 
            // lblArg
            // 
            this.lblArg.AutoSize = true;
            this.lblArg.Enabled = false;
            this.lblArg.Location = new System.Drawing.Point(9, 86);
            this.lblArg.Name = "lblArg";
            this.lblArg.Size = new System.Drawing.Size(143, 13);
            this.lblArg.TabIndex = 7;
            this.lblArg.Text = "Argument (%s - Path To File):\r\n";
            // 
            // txtArg
            // 
            this.txtArg.Enabled = false;
            this.txtArg.Location = new System.Drawing.Point(154, 83);
            this.txtArg.Name = "txtArg";
            this.txtArg.Size = new System.Drawing.Size(282, 20);
            this.txtArg.TabIndex = 8;
            this.txtArg.Text = "\"%s\"";
            // 
            // groupOpenWith
            // 
            this.groupOpenWith.Controls.Add(this.radioDefaultLaunch);
            this.groupOpenWith.Controls.Add(this.radioCustomLaunch);
            this.groupOpenWith.Location = new System.Drawing.Point(12, 8);
            this.groupOpenWith.Name = "groupOpenWith";
            this.groupOpenWith.Size = new System.Drawing.Size(461, 74);
            this.groupOpenWith.TabIndex = 8;
            this.groupOpenWith.TabStop = false;
            this.groupOpenWith.Text = "Open With";
            // 
            // radioDefaultLaunch
            // 
            this.radioDefaultLaunch.AutoSize = true;
            this.radioDefaultLaunch.Checked = true;
            this.radioDefaultLaunch.Location = new System.Drawing.Point(12, 25);
            this.radioDefaultLaunch.Name = "radioDefaultLaunch";
            this.radioDefaultLaunch.Size = new System.Drawing.Size(173, 17);
            this.radioDefaultLaunch.TabIndex = 9;
            this.radioDefaultLaunch.TabStop = true;
            this.radioDefaultLaunch.Text = "Open Using &Default Application";
            this.radioDefaultLaunch.UseVisualStyleBackColor = true;
            this.radioDefaultLaunch.CheckedChanged += new System.EventHandler(this.radioDefaultLaunch_CheckedChanged);
            // 
            // radioCustomLaunch
            // 
            this.radioCustomLaunch.AutoSize = true;
            this.radioCustomLaunch.Location = new System.Drawing.Point(12, 48);
            this.radioCustomLaunch.Name = "radioCustomLaunch";
            this.radioCustomLaunch.Size = new System.Drawing.Size(176, 17);
            this.radioCustomLaunch.TabIndex = 8;
            this.radioCustomLaunch.Text = "Open Using &Another Application";
            this.radioCustomLaunch.UseVisualStyleBackColor = true;
            this.radioCustomLaunch.CheckedChanged += new System.EventHandler(this.radioCustomLaunch_CheckedChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Executable File (*.exe)|*.exe";
            // 
            // frmOpenFile
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(485, 248);
            this.ControlBox = false;
            this.Controls.Add(this.groupOpenWith);
            this.Controls.Add(this.groupLaunchOpt);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpenFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open File";
            this.Load += new System.EventHandler(this.frmOpenFile_Load);
            this.groupLaunchOpt.ResumeLayout(false);
            this.groupLaunchOpt.PerformLayout();
            this.groupOpenWith.ResumeLayout(false);
            this.groupOpenWith.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupLaunchOpt;
        private System.Windows.Forms.TextBox txtArg;
        private System.Windows.Forms.Label lblArg;
        private System.Windows.Forms.CheckBox cboxCustomArg;
        private System.Windows.Forms.Button btnBrowseApp;
        private System.Windows.Forms.TextBox txtAppPath;
        private System.Windows.Forms.Label lblAppPath;
        private System.Windows.Forms.GroupBox groupOpenWith;
        private System.Windows.Forms.RadioButton radioDefaultLaunch;
        private System.Windows.Forms.RadioButton radioCustomLaunch;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}