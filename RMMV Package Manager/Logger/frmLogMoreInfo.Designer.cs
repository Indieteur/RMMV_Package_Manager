namespace RMMV_PackMan
{
    partial class frmLogMoreInfo
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
            this.lblLogLevel = new System.Windows.Forms.Label();
            this.comboLogLevel = new System.Windows.Forms.ComboBox();
            this.lblTimeStamp = new System.Windows.Forms.Label();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.lblExceptionType = new System.Windows.Forms.Label();
            this.txtExceptionType = new System.Windows.Forms.TextBox();
            this.lblExceptionMessage = new System.Windows.Forms.Label();
            this.txtExceptionMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtExceptionFieldMessage = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblExceptionField = new System.Windows.Forms.Label();
            this.groupExceptionFields = new System.Windows.Forms.GroupBox();
            this.lblExceptionFieldMessage = new System.Windows.Forms.Label();
            this.comboExceptionFields = new System.Windows.Forms.ComboBox();
            this.groupAdditionalMessage = new System.Windows.Forms.GroupBox();
            this.lblAdditionalMessage = new System.Windows.Forms.Label();
            this.comboAdditionalMessagesIndex = new System.Windows.Forms.ComboBox();
            this.txtAdditionalMessages = new System.Windows.Forms.TextBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.txtTimeStamp = new System.Windows.Forms.TextBox();
            this.groupExceptionFields.SuspendLayout();
            this.groupAdditionalMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLogLevel
            // 
            this.lblLogLevel.AutoSize = true;
            this.lblLogLevel.Location = new System.Drawing.Point(12, 19);
            this.lblLogLevel.Name = "lblLogLevel";
            this.lblLogLevel.Size = new System.Drawing.Size(36, 13);
            this.lblLogLevel.TabIndex = 0;
            this.lblLogLevel.Text = "Level:";
            // 
            // comboLogLevel
            // 
            this.comboLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLogLevel.Enabled = false;
            this.comboLogLevel.FormattingEnabled = true;
            this.comboLogLevel.Items.AddRange(new object[] {
            "Information",
            "Warning",
            "Error",
            "Critical Error"});
            this.comboLogLevel.Location = new System.Drawing.Point(15, 35);
            this.comboLogLevel.Name = "comboLogLevel";
            this.comboLogLevel.Size = new System.Drawing.Size(330, 21);
            this.comboLogLevel.TabIndex = 1;
            // 
            // lblTimeStamp
            // 
            this.lblTimeStamp.AutoSize = true;
            this.lblTimeStamp.Enabled = false;
            this.lblTimeStamp.Location = new System.Drawing.Point(348, 19);
            this.lblTimeStamp.Name = "lblTimeStamp";
            this.lblTimeStamp.Size = new System.Drawing.Size(66, 13);
            this.lblTimeStamp.TabIndex = 2;
            this.lblTimeStamp.Text = "Time Stamp:";
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Enabled = false;
            this.lblNamespace.Location = new System.Drawing.Point(12, 63);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(67, 13);
            this.lblNamespace.TabIndex = 5;
            this.lblNamespace.Text = "Namespace:";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Enabled = false;
            this.txtNamespace.Location = new System.Drawing.Point(15, 81);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.ReadOnly = true;
            this.txtNamespace.Size = new System.Drawing.Size(330, 20);
            this.txtNamespace.TabIndex = 6;
            // 
            // lblExceptionType
            // 
            this.lblExceptionType.AutoSize = true;
            this.lblExceptionType.Enabled = false;
            this.lblExceptionType.Location = new System.Drawing.Point(348, 65);
            this.lblExceptionType.Name = "lblExceptionType";
            this.lblExceptionType.Size = new System.Drawing.Size(84, 13);
            this.lblExceptionType.TabIndex = 7;
            this.lblExceptionType.Text = "Exception Type:";
            // 
            // txtExceptionType
            // 
            this.txtExceptionType.Enabled = false;
            this.txtExceptionType.Location = new System.Drawing.Point(351, 81);
            this.txtExceptionType.Name = "txtExceptionType";
            this.txtExceptionType.ReadOnly = true;
            this.txtExceptionType.Size = new System.Drawing.Size(320, 20);
            this.txtExceptionType.TabIndex = 8;
            // 
            // lblExceptionMessage
            // 
            this.lblExceptionMessage.AutoSize = true;
            this.lblExceptionMessage.Enabled = false;
            this.lblExceptionMessage.Location = new System.Drawing.Point(348, 107);
            this.lblExceptionMessage.Name = "lblExceptionMessage";
            this.lblExceptionMessage.Size = new System.Drawing.Size(103, 13);
            this.lblExceptionMessage.TabIndex = 9;
            this.lblExceptionMessage.Text = "Exception Message:";
            // 
            // txtExceptionMessage
            // 
            this.txtExceptionMessage.Enabled = false;
            this.txtExceptionMessage.Location = new System.Drawing.Point(351, 123);
            this.txtExceptionMessage.Multiline = true;
            this.txtExceptionMessage.Name = "txtExceptionMessage";
            this.txtExceptionMessage.ReadOnly = true;
            this.txtExceptionMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExceptionMessage.Size = new System.Drawing.Size(320, 107);
            this.txtExceptionMessage.TabIndex = 10;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Enabled = false;
            this.lblMessage.Location = new System.Drawing.Point(12, 107);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(53, 13);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "Message:";
            // 
            // txtMessage
            // 
            this.txtMessage.Enabled = false;
            this.txtMessage.Location = new System.Drawing.Point(15, 123);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(330, 107);
            this.txtMessage.TabIndex = 12;
            // 
            // txtExceptionFieldMessage
            // 
            this.txtExceptionFieldMessage.Enabled = false;
            this.txtExceptionFieldMessage.Location = new System.Drawing.Point(9, 80);
            this.txtExceptionFieldMessage.Multiline = true;
            this.txtExceptionFieldMessage.Name = "txtExceptionFieldMessage";
            this.txtExceptionFieldMessage.ReadOnly = true;
            this.txtExceptionFieldMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExceptionFieldMessage.Size = new System.Drawing.Size(299, 105);
            this.txtExceptionFieldMessage.TabIndex = 14;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(565, 447);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblExceptionField
            // 
            this.lblExceptionField.AutoSize = true;
            this.lblExceptionField.Enabled = false;
            this.lblExceptionField.Location = new System.Drawing.Point(6, 20);
            this.lblExceptionField.Name = "lblExceptionField";
            this.lblExceptionField.Size = new System.Drawing.Size(32, 13);
            this.lblExceptionField.TabIndex = 15;
            this.lblExceptionField.Text = "Field:";
            // 
            // groupExceptionFields
            // 
            this.groupExceptionFields.Controls.Add(this.lblExceptionFieldMessage);
            this.groupExceptionFields.Controls.Add(this.comboExceptionFields);
            this.groupExceptionFields.Controls.Add(this.txtExceptionFieldMessage);
            this.groupExceptionFields.Controls.Add(this.lblExceptionField);
            this.groupExceptionFields.Enabled = false;
            this.groupExceptionFields.Location = new System.Drawing.Point(351, 236);
            this.groupExceptionFields.Name = "groupExceptionFields";
            this.groupExceptionFields.Size = new System.Drawing.Size(320, 205);
            this.groupExceptionFields.TabIndex = 16;
            this.groupExceptionFields.TabStop = false;
            this.groupExceptionFields.Text = "Exception Fields:";
            // 
            // lblExceptionFieldMessage
            // 
            this.lblExceptionFieldMessage.AutoSize = true;
            this.lblExceptionFieldMessage.Enabled = false;
            this.lblExceptionFieldMessage.Location = new System.Drawing.Point(6, 64);
            this.lblExceptionFieldMessage.Name = "lblExceptionFieldMessage";
            this.lblExceptionFieldMessage.Size = new System.Drawing.Size(53, 13);
            this.lblExceptionFieldMessage.TabIndex = 17;
            this.lblExceptionFieldMessage.Text = "Message:";
            // 
            // comboExceptionFields
            // 
            this.comboExceptionFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboExceptionFields.Enabled = false;
            this.comboExceptionFields.FormattingEnabled = true;
            this.comboExceptionFields.Location = new System.Drawing.Point(9, 36);
            this.comboExceptionFields.Name = "comboExceptionFields";
            this.comboExceptionFields.Size = new System.Drawing.Size(299, 21);
            this.comboExceptionFields.TabIndex = 16;
            this.comboExceptionFields.SelectedIndexChanged += new System.EventHandler(this.comboExceptionFields_SelectedIndexChanged);
            // 
            // groupAdditionalMessage
            // 
            this.groupAdditionalMessage.Controls.Add(this.lblAdditionalMessage);
            this.groupAdditionalMessage.Controls.Add(this.comboAdditionalMessagesIndex);
            this.groupAdditionalMessage.Controls.Add(this.txtAdditionalMessages);
            this.groupAdditionalMessage.Controls.Add(this.lblIndex);
            this.groupAdditionalMessage.Enabled = false;
            this.groupAdditionalMessage.Location = new System.Drawing.Point(15, 236);
            this.groupAdditionalMessage.Name = "groupAdditionalMessage";
            this.groupAdditionalMessage.Size = new System.Drawing.Size(330, 205);
            this.groupAdditionalMessage.TabIndex = 18;
            this.groupAdditionalMessage.TabStop = false;
            this.groupAdditionalMessage.Text = "Additional Messages:";
            // 
            // lblAdditionalMessage
            // 
            this.lblAdditionalMessage.AutoSize = true;
            this.lblAdditionalMessage.Enabled = false;
            this.lblAdditionalMessage.Location = new System.Drawing.Point(6, 64);
            this.lblAdditionalMessage.Name = "lblAdditionalMessage";
            this.lblAdditionalMessage.Size = new System.Drawing.Size(53, 13);
            this.lblAdditionalMessage.TabIndex = 17;
            this.lblAdditionalMessage.Text = "Message:";
            // 
            // comboAdditionalMessagesIndex
            // 
            this.comboAdditionalMessagesIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAdditionalMessagesIndex.Enabled = false;
            this.comboAdditionalMessagesIndex.FormattingEnabled = true;
            this.comboAdditionalMessagesIndex.Location = new System.Drawing.Point(9, 36);
            this.comboAdditionalMessagesIndex.Name = "comboAdditionalMessagesIndex";
            this.comboAdditionalMessagesIndex.Size = new System.Drawing.Size(315, 21);
            this.comboAdditionalMessagesIndex.TabIndex = 16;
            this.comboAdditionalMessagesIndex.SelectedIndexChanged += new System.EventHandler(this.comboAdditionalMessagesIndex_SelectedIndexChanged);
            // 
            // txtAdditionalMessages
            // 
            this.txtAdditionalMessages.Enabled = false;
            this.txtAdditionalMessages.Location = new System.Drawing.Point(9, 80);
            this.txtAdditionalMessages.Multiline = true;
            this.txtAdditionalMessages.Name = "txtAdditionalMessages";
            this.txtAdditionalMessages.ReadOnly = true;
            this.txtAdditionalMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAdditionalMessages.Size = new System.Drawing.Size(315, 105);
            this.txtAdditionalMessages.TabIndex = 14;
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Enabled = false;
            this.lblIndex.Location = new System.Drawing.Point(6, 20);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(36, 13);
            this.lblIndex.TabIndex = 15;
            this.lblIndex.Text = "Index:";
            // 
            // txtTimeStamp
            // 
            this.txtTimeStamp.Location = new System.Drawing.Point(351, 35);
            this.txtTimeStamp.Name = "txtTimeStamp";
            this.txtTimeStamp.ReadOnly = true;
            this.txtTimeStamp.Size = new System.Drawing.Size(320, 20);
            this.txtTimeStamp.TabIndex = 19;
            // 
            // frmLogMoreInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 475);
            this.Controls.Add(this.txtTimeStamp);
            this.Controls.Add(this.groupAdditionalMessage);
            this.Controls.Add(this.groupExceptionFields);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtExceptionMessage);
            this.Controls.Add(this.lblExceptionMessage);
            this.Controls.Add(this.txtExceptionType);
            this.Controls.Add(this.lblExceptionType);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.lblNamespace);
            this.Controls.Add(this.lblTimeStamp);
            this.Controls.Add(this.comboLogLevel);
            this.Controls.Add(this.lblLogLevel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogMoreInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logger";
            this.groupExceptionFields.ResumeLayout(false);
            this.groupExceptionFields.PerformLayout();
            this.groupAdditionalMessage.ResumeLayout(false);
            this.groupAdditionalMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLogLevel;
        private System.Windows.Forms.ComboBox comboLogLevel;
        private System.Windows.Forms.Label lblTimeStamp;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblExceptionType;
        private System.Windows.Forms.TextBox txtExceptionType;
        private System.Windows.Forms.Label lblExceptionMessage;
        private System.Windows.Forms.TextBox txtExceptionMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtExceptionFieldMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblExceptionField;
        private System.Windows.Forms.GroupBox groupExceptionFields;
        private System.Windows.Forms.ComboBox comboExceptionFields;
        private System.Windows.Forms.Label lblExceptionFieldMessage;
        private System.Windows.Forms.GroupBox groupAdditionalMessage;
        private System.Windows.Forms.Label lblAdditionalMessage;
        private System.Windows.Forms.ComboBox comboAdditionalMessagesIndex;
        private System.Windows.Forms.TextBox txtAdditionalMessages;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.TextBox txtTimeStamp;
    }
}