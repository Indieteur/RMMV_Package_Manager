using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public delegate void OnLogMessageCreate(LogMessage logMessage);
    public static partial class Logger
    {
        public static LogDataList logList { get; private set; } = new LogDataList();
        public static BasicLogCombo LogWriter { get; private set; }
        public static event OnLogMessageCreate OnLogMessageCreated;

        public static void InitializeLogger()
        {
            Exception ex;
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (!Directory.Exists(PMFileSystem.LogDebug_Dir))
            {
                if (Helper.CreateFolderSafely(PMFileSystem.LogDebug_Dir, _namespace, out ex) == CreateFolderResult.UserCancelled)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.Logger.DEBUG_DIR_NOT_ABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!Directory.Exists(PMFileSystem.LogUser_Dir))
            {
                if (Helper.CreateFolderSafely(PMFileSystem.LogUser_Dir, _namespace, out ex) == CreateFolderResult.UserCancelled) // null arg
                {
                    Helper.ShowMessageBox(MessageBoxStrings.Logger.USER_DIR_NOT_ABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Exception exCatched = null;
            try
            {
                BasicLoggingTools.LimitLogsInFolder(PMFileSystem.LogDebug_Dir, Properties.Settings.Default.LogLimit, Properties.Settings.Default.LogLimitType.ToBasicLogLimit());
                BasicLoggingTools.LimitLogsInFolder(PMFileSystem.LogUser_Dir, Properties.Settings.Default.LogLimit, Properties.Settings.Default.LogLimitType.ToBasicLogLimit());
            }
            catch (Exception exCaught)
            {
                exCatched = exCaught;
            }

            try
            {
                LogWriter = new BasicLogCombo(PMFileSystem.LogUser_Dir, PMFileSystem.LogDebug_Dir);
            }
            catch 
            {
                Helper.ShowMessageBox(MessageBoxStrings.Logger.LOG_NOT_ABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter = null;
                
            }
            if (exCatched != null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.Logger.LIMIT_NOT_ABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteErrorLog(exCatched.Message, MethodBase.GetCurrentMethod().ToLogFormatFullName(), exCatched, BasicDebugLogger.DebugErrorType.Error);
            }
        }

        public static void WriteUserEventLog(string message, string _namespace, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            WriteUserEventLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteUserEventLog(string message, string _namespace, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            WriteUserEventLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteUserEventLog(string message, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            WriteInformationLog("[User Event] " + message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteUserEventLog(string message, string actionTaken, string _namespace, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            WriteUserEventLog(message, actionTaken, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteUserEventLog(string message, string actionTaken, string _namespace, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            WriteUserEventLog(message, actionTaken, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteUserEventLog(string message, string actionTaken, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            userLog = null;
            debugLog = null;
            if (additionalMessages == null || additionalMessages.Length == 0)
                WriteInformationLog("[User Event] " + message, _namespace, out userLog, out debugLog, includeDateTime, "Action Taken - " + actionTaken);
            else
            {
                List<string> additionalMessagesList = new List<string>(additionalMessages.Length + 1);
                additionalMessagesList.Add("Action Taken - " + actionTaken);
                additionalMessagesList.AddRange(additionalMessages);
                WriteInformationLog("[User Event] " + message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessagesList.ToArray());
            }
          
        }

        public static void WriteErrorLog(string message, string _namespace, Exception exception, BasicDebugLogger.DebugErrorType errorLevel, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            WriteErrorLog(message, _namespace, exception, errorLevel, out userLog, out debugLog, includeDateTime, additionalMessages);
        }
        public static void WriteErrorLog(string message, string _namespace, Exception exception, BasicDebugLogger.DebugErrorType errorLevel, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            WriteErrorLog(message, _namespace, exception, errorLevel, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteErrorLog(string message, string _namespace, Exception exception, BasicDebugLogger.DebugErrorType errorLevel, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            userLog = null;
            debugLog = null;
            if (LogWriter != null)
            {
                try
                {
                    LogWriter.WriteErrorLog(message, _namespace, exception, errorLevel, out userLog, out debugLog, includeDateTime, additionalMessages);
                }
                catch
                {
                    if (Helper.ShowMessageBox(MessageBoxStrings.Logger.LOG_NOT_ABLE_CHOICE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        LogWriter = null;
                }
            }
            if (debugLog != null)
            {
                logList.Logs.Add(debugLog);
                if (OnLogMessageCreated != null)
                    OnLogMessageCreated(debugLog);
            }
        }

        public static void WriteWarningLog(string message, string _namespace, out DebugLogMessageLevel debugLog, Exception exception = null, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            WriteWarningLog(message, _namespace, out userLog, out debugLog, exception, includeDateTime, additionalMessages);
        }

        public static void WriteWarningLog(string message, string _namespace, Exception exception = null, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            WriteWarningLog(message, _namespace, out userLog, out debugLog, exception, includeDateTime, additionalMessages);
        }

        public static void WriteWarningLog(string message, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, Exception exception = null, bool includeDateTime = true, params string[] additionalMessages)
        {
            userLog = null;
            debugLog = null;
            if (LogWriter != null)
            {
                try
                {
                    LogWriter.WriteWarningLog(message, _namespace, exception, out userLog, out debugLog, includeDateTime, additionalMessages);
                }
                catch
                {
                    if (Helper.ShowMessageBox(MessageBoxStrings.Logger.LOG_NOT_ABLE_CHOICE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        LogWriter = null;
                }
            }
            if (debugLog != null)
            {
                logList.Logs.Add(debugLog);
                if (OnLogMessageCreated != null)
                    OnLogMessageCreated(debugLog);
            }
        }

        public static void WriteInformationLog(string message, string _namespace, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            WriteInformationLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }

        public static void WriteInformationLog(string message, string _namespace, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            WriteInformationLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
        }
        public static void WriteInformationLog(string message, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            userLog = null;
            debugLog = null;
            if (LogWriter != null)
            {
                try
                {
                    LogWriter.WriteInformationLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
                }
                catch
                {
                    if (Helper.ShowMessageBox(MessageBoxStrings.Logger.LOG_NOT_ABLE_CHOICE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        LogWriter = null;
                }
            }
            if (debugLog != null)
            {
                logList.Logs.Add(debugLog);
                if (OnLogMessageCreated != null)
                    OnLogMessageCreated(debugLog);
            }
        }
        

        public static void CloseLog()
        {
            if (LogWriter != null)
            {
                LogWriter.Dispose();
                LogWriter = null;
            }
        }
    }
}
