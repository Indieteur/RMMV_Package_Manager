using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public partial class frmOpenFile : Form
    {
        public string PathToFile { get; set; }
        public frmOpenFile()
        {
            InitializeComponent();
        }
        public frmOpenFile(string pathToFile)
        {
            InitializeComponent();
            PathToFile = pathToFile;
        }

        private void radioDefaultLaunch_CheckedChanged(object sender, EventArgs e)
        {
            groupLaunchOpt.Enabled = false;
        }

        private void radioCustomLaunch_CheckedChanged(object sender, EventArgs e)
        {
            groupLaunchOpt.Enabled = true;
        }

        private void cboxCustomArg_CheckedChanged(object sender, EventArgs e)
        {
            lblArg.Enabled = cboxCustomArg.Checked;
            txtArg.Enabled = cboxCustomArg.Checked;
        }

        private void btnBrowseApp_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            DialogResult dialogResult = openFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.Cancel)
                return;
            txtAppPath.Text = openFileDialog.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (string.IsNullOrWhiteSpace(PathToFile))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmOpenFile.NO_PATH_TO_FILE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            if (radioDefaultLaunch.Checked)
            {
                Logger.WriteInformationLog(LoggerMessages.GUI.frmOpenFile.OpenFileDefaultApp(PathToFile), _namespace);
                try
                {
                    Process.Start(PathToFile);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_VIEW_FILE + PathToFile + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmOpenFile.FAILED_DEFAULT_APP_LAUNCH, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Properties.Settings.Default.OpenFileDefault = true;
                Close();
            }
            else
            {
                if (!File.Exists(txtAppPath.Text))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmOpenFile.NO_PATH_TO_APP, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cboxCustomArg.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtArg.Text))
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmOpenFile.INVALID_ARG, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Logger.WriteInformationLog(LoggerMessages.GUI.frmOpenFile.OpenFileSelApp(PathToFile, txtAppPath.Text, txtArg.Text), _namespace);
                    try
                    {
                        Process.Start(txtAppPath.Text, txtArg.Text.Replace("%s", PathToFile));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.GUI.frmOpenFile.UnableLaunchApp(txtAppPath.Text, PathToFile, txtArg.Text), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmOpenFile.FAILED_APP_LAUNCH, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Properties.Settings.Default.OpenFileDefault = false;
                    Properties.Settings.Default.OpenFileAppPath = txtAppPath.Text;
                    Properties.Settings.Default.OpenFileArg = txtArg.Text;
                    Close();
                }
                else
                {
                    Logger.WriteInformationLog(LoggerMessages.GUI.frmOpenFile.OpenFileSelApp(PathToFile, txtAppPath.Text), _namespace);
                    try
                    {
                        Process.Start(txtAppPath.Text, "\"" + PathToFile + "\"");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.GUI.frmOpenFile.UnableLaunchApp(txtAppPath.Text, PathToFile), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmOpenFile.FAILED_APP_LAUNCH, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Properties.Settings.Default.OpenFileDefault = false;
                    Properties.Settings.Default.OpenFileAppPath = txtAppPath.Text;
                    Properties.Settings.Default.OpenFileArg = "";
                    Close();
                }
            }
            
        }

        private void frmOpenFile_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.OpenFileDefault)
                radioDefaultLaunch.Checked = true;
            else
                radioCustomLaunch.Checked = true;

            txtAppPath.Text = Properties.Settings.Default.OpenFileAppPath;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.OpenFileArg))
            {
                txtArg.Text = Properties.Settings.Default.OpenFileArg;
                cboxCustomArg.Checked = true;
            }

        }
    }
}
