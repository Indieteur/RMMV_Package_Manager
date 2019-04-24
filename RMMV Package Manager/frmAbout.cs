using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public partial class frmAbout : Form
    {
        const string VERSION = "Version ";
        const string COPYRIGHT_START = "Copyright © 2018-";
        public frmAbout()
        {
            InitializeComponent();
            txtVersion.Text = VERSION + Application.ProductVersion;
            lblCopyright.Text = COPYRIGHT_START + DateTime.Today.Year.ToString() + " " + Application.CompanyName;
            linkPatreon.Text = WebLinks.SUPPORT_PATREON;
            linkKoFi.Text = WebLinks.SUPPORT_KO_FI;
            linkBugReport.Text = WebLinks.BUG_REPORT;
            linkEULA.Text = WebLinks.LICENSE;
            linkGitHub.Text = WebLinks.GITHUB;
            linkHomepage.Text = WebLinks.HOMEPAGE;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void copyLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip.SourceControl != null && contextMenuStrip.SourceControl is LinkLabel linkLabel)
            {
                Clipboard.SetText(linkLabel.Text);
            }
        }

        void ViewLink(LinkLabel linkLabel)
        {
            string URL = linkLabel.Text;
            try
            {
                System.Diagnostics.Process.Start(URL);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + URL + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), ex, BasicDebugLogger.DebugErrorType.Error);
                Helper.ShowMessageBox(MessageBoxStrings.GUI.VIEW_LINK_ERROR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkBugReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLink((LinkLabel)sender);
        }

        private void linkPatreon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLink((LinkLabel)sender);
        }

        private void linkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLink((LinkLabel)sender);
        }

        private void linkEULA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLink((LinkLabel)sender);
        }

        private void linkKoFi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLink((LinkLabel)sender);
        }

        private void linkHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLink((LinkLabel)sender);
        }
    }
}
