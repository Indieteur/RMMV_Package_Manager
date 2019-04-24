using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Indieteur.SimpleDatabaseFormat;
using System.Reflection;
using Indieteur.BasicLoggingSystem;
using RMMV_PackMan.GUI;
using Indieteur.ChecksumZIP;
using System.Threading;

namespace RMMV_PackMan
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteInformationLog(LoggerMessages.GUI.frmMain.INIT_START, _namespace);
            GlobalClass.MainForm = this;
            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.INIT_REQ_RES);
            Thread thread = new Thread(delegate ()
            {
                this.CheckAndOrRetrieveMVPath();
                this.CheckBaseFiles();
                this.InitializePackageManagement();
                this.CheckDefaultPackages();
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();
            InitGUIRes();
            GlobalClass.GlobalPackControls.LoadPackages(PackageManagement.GlobalPackages, _namespace);
            Logger.WriteInformationLog(LoggerMessages.GUI.frmMain.INIT_DONE, _namespace);
           
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Vars.MustExitAtStart)
            {
                Application.Exit();
                return;
            }
            ProcessShowLogger();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void ProcessShowLogger()
        {
            if (Properties.Settings.Default.FormLoggerShown)
            {
                GlobalClass.LoggerForm = new frmLogger(this);
                GlobalClass.LoggerForm.Show(this);
            }
        }

       
    }
}
