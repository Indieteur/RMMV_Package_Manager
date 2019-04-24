namespace RMMV_PackMan
{
    partial class frmLogger
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
            this.lView = new System.Windows.Forms.ListView();
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.lblLogger = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnMoreInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lView
            // 
            this.lView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnType,
            this.columnTimeStamp,
            this.columnDesc});
            this.lView.FullRowSelect = true;
            this.lView.GridLines = true;
            this.lView.HideSelection = false;
            this.lView.LargeImageList = this.imageList;
            this.lView.Location = new System.Drawing.Point(15, 26);
            this.lView.MultiSelect = false;
            this.lView.Name = "lView";
            this.lView.Size = new System.Drawing.Size(776, 393);
            this.lView.SmallImageList = this.imageList;
            this.lView.TabIndex = 0;
            this.lView.UseCompatibleStateImageBehavior = false;
            this.lView.View = System.Windows.Forms.View.Details;
            this.lView.SelectedIndexChanged += new System.EventHandler(this.lView_SelectedIndexChanged);
            this.lView.DoubleClick += new System.EventHandler(this.lView_DoubleClick);
            // 
            // columnType
            // 
            this.columnType.Text = "Log Type";
            this.columnType.Width = 92;
            // 
            // columnTimeStamp
            // 
            this.columnTimeStamp.Text = "Time Stamp";
            this.columnTimeStamp.Width = 165;
            // 
            // columnDesc
            // 
            this.columnDesc.Text = "Message";
            this.columnDesc.Width = 579;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblLogger
            // 
            this.lblLogger.AutoSize = true;
            this.lblLogger.Location = new System.Drawing.Point(12, 10);
            this.lblLogger.Name = "lblLogger";
            this.lblLogger.Size = new System.Drawing.Size(43, 13);
            this.lblLogger.TabIndex = 1;
            this.lblLogger.Text = "Logger:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(677, 425);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(111, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnMoreInfo
            // 
            this.btnMoreInfo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMoreInfo.Enabled = false;
            this.btnMoreInfo.Location = new System.Drawing.Point(12, 425);
            this.btnMoreInfo.Name = "btnMoreInfo";
            this.btnMoreInfo.Size = new System.Drawing.Size(105, 23);
            this.btnMoreInfo.TabIndex = 3;
            this.btnMoreInfo.Text = "More Info...";
            this.btnMoreInfo.UseVisualStyleBackColor = true;
            this.btnMoreInfo.Click += new System.EventHandler(this.btnMoreInfo_Click);
            // 
            // frmLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(800, 453);
            this.Controls.Add(this.btnMoreInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLogger);
            this.Controls.Add(this.lView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogger";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = " Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogger_FormClosing);
            this.Load += new System.EventHandler(this.frmLogger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lView;
        private System.Windows.Forms.Label lblLogger;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnDesc;
        private System.Windows.Forms.ColumnHeader columnTimeStamp;
        private System.Windows.Forms.Button btnMoreInfo;
    }
}