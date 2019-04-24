using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;
using System.Configuration;


namespace RMMV_PackMan
{
   
    static class Program
    {
        public static bool RestartApp = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!SingleInstanceProcessCheck())
                return;

            if (!Debugger.IsAttached)
                RegisterUnhandledExceptionHandling();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger.InitializeLogger();
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            Logger.WriteInformationLog(LoggerMessages.AtTheStart.APP_STARTED + Application.StartupPath + ".", _namespace);
            Exception ex;
            if (Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempDir, _namespace, out ex, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.TEMP_DIR_NOT_ABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempDir, _namespace, out ex, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.TEMP_DIR_NOT_ABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            try
            {
                GUI.frmPackAssetBGWorker.PreloadAssetFormData();
                Application.Run(new frmMain());

            }
            catch (Exception exCaught)
            {
                if (exCaught is ConfigurationErrorsException configurationException)
                    Logger.CreateMissingConfigLog(configurationException);
                else
                    Logger.CreateIssueLogForUnhandledEx(exCaught);
            }
            Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempDir, _namespace, out ex, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
            Logger.CloseLog();
            if (RestartApp)
                Application.Restart();
           
        }

        static void RegisterUnhandledExceptionHandling() //https://stackoverflow.com/questions/5762526/how-can-i-make-something-that-catches-all-unhandled-exceptions-in-a-winforms-a
        {
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new
          ThreadExceptionEventHandler(UnhandledGUIException);

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new
            UnhandledExceptionEventHandler(UnhandledException);
        }

        private static void UnhandledGUIException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is ConfigurationErrorsException configurationException)
                Logger.CreateMissingConfigLog(configurationException);
            else
                Logger.CreateIssueLogForUnhandledEx(e.Exception);
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is ConfigurationErrorsException configurationException)
                Logger.CreateMissingConfigLog(configurationException);
            else
                Logger.CreateIssueLogForUnhandledEx(e.ExceptionObject);
        }

        static bool SingleInstanceProcessCheck()
        {
            string exePath = Application.ExecutablePath.ToLower();
            Process[] processesRunning;
            try
            {
                processesRunning = Process.GetProcesses(); 
            }
            catch (Exception ex)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.SI_PROC_RETRIEVE_FAIL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (processesRunning == null || processesRunning.Length == 0)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.SI_PROC_RETRIEVE_FAIL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int countRunningApp = 0;
            foreach (Process proc in processesRunning)
            {
                try
                {
                    string procPath = proc.MainModule.FileName.ToLower();
                    if (procPath == exePath)
                        ++countRunningApp;
                    if (countRunningApp > 1)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.SI_CHECK_FAIL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch
                {
                    continue;
                }
            }
            if (countRunningApp > 1)
            {
                Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.SI_CHECK_FAIL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
