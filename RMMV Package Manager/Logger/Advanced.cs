using Indieteur.BasicLoggingSystem;
using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;
namespace RMMV_PackMan
{
    public static partial class Logger
    {
        public enum ErrorActionTaken
        {
            None,
            ApplicationExited,
            OperationAborted,
            MVDirNotFound
        }
   

      

        public static void CreateMissingConfigLog(ConfigurationErrorsException exception)
        {
            Helper.ShowMessageBox(MessageBoxStrings.AtTheStart.CONFIG_FILE_NOT_FOUND, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            string _namespace = "Startup";
            if (exception.TargetSite != null)
                _namespace = exception.TargetSite.ToLogFormatFullName();
            WriteErrorLog(LoggerMessages.MISSING_CFG_FILE, _namespace, exception, BasicDebugLogger.DebugErrorType.CriticalError, true);
            Vars.MustExitAtStart = true;
            Application.Exit();
        }

        public static void CreateIssueLogForUnhandledEx(object ex)
        {
            DialogResult res = Helper.ShowMessageBox(MessageBoxStrings.UNHANDLED_EXCEPTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            

            Exception exception = ex as Exception;
            if (exception != null)
                CreateIssueLogForUnhandledException(exception, res);
            else
            {
                exception = new UnwrappedException();
                WriteErrorLog(LoggerMessages.UNHANDLED_EXCEPTION , "UNKNOWN", exception, BasicDebugLogger.DebugErrorType.CriticalError, true);
            }

            if (Helper.ShowMessageBox(MessageBoxStrings.REPORT_BUG, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                try
                {
                    System.Diagnostics.Process.Start(WebLinks.BUG_REPORT);
                }
                catch (Exception exOut)
                {
                    Logger.WriteErrorLog(LoggerMessages.GeneralError.UNABLE_LAUNCH_URL + WebLinks.BUG_REPORT + "\".", MethodBase.GetCurrentMethod().ToLogFormatFullName(), exOut, BasicDebugLogger.DebugErrorType.Error);
                    Helper.ShowMessageBox(MessageBoxStrings.Logger.UNABLE_VIEW_BUG_REPORT_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            if (res == DialogResult.Yes)
            {
                Vars.MustExitAtStart = true;
                Application.Exit();
                return;
            }
        }

        public static void CreateIssueLogForLowLevelException(Exception ex, string fallbackNamespace, BasicLoggerLogLevel logLevel = BasicLoggerLogLevel.Warning)
        {
            if (ex.TargetSite != null)
                fallbackNamespace = ex.TargetSite.ToLogFormatFullName();
            if (logLevel == BasicLoggerLogLevel.Warning)
                WriteWarningLog(ex.Message, fallbackNamespace, ex, true);
            else
                WriteErrorLog(ex.Message, fallbackNamespace, ex, logLevel.ToDebugErrorType(), true);
        }

        static void CreateIssueLogForUnhandledException(Exception ex, DialogResult res)
        {

            
            if (LogWriter != null)
            {
                string _namespace = "UNKNOWN";
                if (ex.TargetSite != null)
                    _namespace = ex.TargetSite.ToLogFormatFullName();
                if (res == DialogResult.Yes)

                    WriteErrorLog(ex.Message, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError, true);
                else
                    WriteErrorLog(ex.Message, _namespace, ex, BasicDebugLogger.DebugErrorType.Error, true);

            }


        }

        

    }
}
