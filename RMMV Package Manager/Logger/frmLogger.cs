using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public partial class frmLogger : Form
    {
        const int LOC_X_PADDING = 10;
        Form frmParent;
        LogDataList logList;

        public frmLogger(Form form = null, LogDataList _logList = null)
        {
            InitializeComponent();
            logList = _logList;
            frmParent = form;
            InitializeLogger();
            InitLogMessages();
            
        }

        void InitializeLogger()
        {
           
            imageList.Images.Add(new Icon(System.Drawing.SystemIcons.Information, new Size(16, 16)));
            imageList.Images.Add(new Icon(System.Drawing.SystemIcons.Warning, new Size(16, 16)));
            imageList.Images.Add(new Icon(System.Drawing.SystemIcons.Error, new Size(16, 16)));
            columnDesc.Width = -1;
            columnType.Width = -1;
            columnTimeStamp.Width = -1;
            if (logList == null)
            {
                Logger.OnLogMessageCreated += Logger_OnLogMessageCreated;
                if (frmParent != null)
                {
                    StartPosition = FormStartPosition.Manual;
                    Location = new Point(LOC_X_PADDING + frmParent.Location.X + Size.Width, frmParent.Location.Y);
                    TopMost = true;
                }
            }
        }

        private void Logger_OnLogMessageCreated(LogMessage logMessage)
        {
            if (logMessage != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    LoadLogMessage(logMessage);
                });
            }
        }

        void InitLogMessages()
        {
            LogDataList _logList = (logList == null) ? Logger.logList : logList;

            if (_logList == null || _logList.Logs.Count == 0)
            {
                Close();
                return;
            }

            lView.BeginUpdate();
            foreach (LogMessage logMessage in _logList.Logs)
                LoadLogMessage(logMessage);
            lView.EndUpdate();
           
        }

        void LoadLogMessage(LogMessage logMessage)
        {
            ListViewItem lViewItem = null;
            if (logMessage is LogMessageWLevel logMessageWLevel)
                lViewItem = new ListViewItem(logMessageWLevel.LogLevel.ToLogStringFormat(), LogLevelToImageIndex(logMessageWLevel.LogLevel));
            else
                lViewItem = new ListViewItem("Information", 0);
            lViewItem.Tag = logMessage;
            lViewItem.SubItems.Add(new ListViewItem.ListViewSubItem(lViewItem, logMessage.TimeStamp.ToLogStringFormat()));
            if (!string.IsNullOrWhiteSpace(logMessage.Message))
                lViewItem.SubItems.Add(logMessage.Message);
            lView.Items.Add(lViewItem);
            lViewItem.EnsureVisible();
        }

        int LogLevelToImageIndex(BasicLoggerLogLevel logLevel)
        {
            switch (logLevel)
            {
                case BasicLoggerLogLevel.CriticalError:
                    return 2;
                case BasicLoggerLogLevel.Error:
                    return 2;
                case BasicLoggerLogLevel.Information:
                    return 0;
                default:
                    return 1;
            }
        }

        private void lView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lView.SelectedItems.Count == 0)
            {
                btnMoreInfo.Enabled = false;
                return;
            }
            btnMoreInfo.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogger_Load(object sender, EventArgs e)
        {
            if (logList == null)
                Properties.Settings.Default.FormLoggerShown = true;
            if (lView.Items != null && lView.Items.Count > 0)
             lView.Items[lView.Items.Count - 1].EnsureVisible();
        }

        private void btnMoreInfo_Click(object sender, EventArgs e)
        {
            frmLogMoreInfo logMoreInfoForm = new frmLogMoreInfo(lView.SelectedItems[0].Tag as LogMessage);
            logMoreInfoForm.ShowDialog(this);
        }

        private void lView_DoubleClick(object sender, EventArgs e)
        {
            if (lView.SelectedItems.Count > 0)
            {
                frmLogMoreInfo logMoreInfoForm = new frmLogMoreInfo(lView.SelectedItems[0].Tag as LogMessage);
                logMoreInfoForm.ShowDialog(this);
            }
        }

        private void frmLogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logList == null)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                    Properties.Settings.Default.FormLoggerShown = false;
                Logger.OnLogMessageCreated -= Logger_OnLogMessageCreated;
                GlobalClass.LoggerForm = null;
            }
        }
    }
}
