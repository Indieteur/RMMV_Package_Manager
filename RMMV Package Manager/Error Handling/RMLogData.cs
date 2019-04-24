using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public class LogDataList
    {
        public List<LogMessage> Logs { get; private set; } = new List<LogMessage>();
        public bool HasErrors()
        {
            if (Logs.Count == 0)
                return false;
            foreach (LogMessage logData in Logs)
            {
                LogMessageWLevel logMessageWLevel = logData as LogMessageWLevel;
                if (logMessageWLevel != null && (logMessageWLevel.LogLevel == BasicLoggerLogLevel.Error  || logMessageWLevel.LogLevel == BasicLoggerLogLevel.CriticalError))
                    return true;
            }
            return false;
        }
        public bool HasWarnings()
        {
            if (Logs.Count == 0)
                return false;
            foreach (LogMessage logData in Logs)
            {
                LogMessageWLevel logMessageWLevel = logData as LogMessageWLevel;
                if (logMessageWLevel != null && logMessageWLevel.LogLevel == BasicLoggerLogLevel.Warning)
                    return true;
            }
            return false;
        }

        public bool HasErrorsOrWarnings()
        {
            return HasWarnings() || HasErrors();
        }

        public void WriteUserEventLog(string message, string _namespace, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            Logger.WriteUserEventLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteUserEventLog(string message, string _namespace, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            Logger.WriteUserEventLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteUserEventLog(string message, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            Logger.WriteInformationLog("[User Event] " + message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteUserEventLog(string message, string actionTaken, string _namespace, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            Logger.WriteUserEventLog(message, actionTaken, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteUserEventLog(string message, string actionTaken, string _namespace, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            Logger.WriteUserEventLog(message, actionTaken, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteUserEventLog(string message, string actionTaken, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            Logger.WriteUserEventLog(message, actionTaken, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteErrorLog(string message, string _namespace, Exception exception,  out DebugLogMessageLevel debugLog, BasicDebugLogger.DebugErrorType errorLevel = BasicDebugLogger.DebugErrorType.Error, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            Logger.WriteErrorLog(message, _namespace, exception, errorLevel, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }
        public void WriteErrorLog(string message, string _namespace, Exception exception, BasicDebugLogger.DebugErrorType errorLevel = BasicDebugLogger.DebugErrorType.Error, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            Logger.WriteErrorLog(message, _namespace, exception, errorLevel, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteErrorLog(string message, string _namespace, Exception exception, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, BasicDebugLogger.DebugErrorType errorLevel = BasicDebugLogger.DebugErrorType.Error, bool includeDateTime = true, params string[] additionalMessages)
        {
            Logger.WriteErrorLog(message, _namespace, exception, errorLevel, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteWarningLog(string message, string _namespace, out DebugLogMessageLevel debugLog, Exception exception = null, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            Logger.WriteWarningLog(message, _namespace, out userLog, out debugLog, exception, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteWarningLog(string message, string _namespace, Exception exception = null, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            Logger.WriteWarningLog(message, _namespace, out userLog, out debugLog, exception, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteWarningLog(string message, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, Exception exception = null, bool includeDateTime = true, params string[] additionalMessages)
        {
            Logger.WriteWarningLog(message, _namespace, out userLog, out debugLog, exception, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteInformationLog(string message, string _namespace, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            Logger.WriteInformationLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void WriteInformationLog(string message, string _namespace, bool includeDateTime = true, params string[] additionalMessages)
        {
            LogMessageWLevel userLog;
            DebugLogMessageLevel debugLog;
            Logger.WriteInformationLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }
        public void WriteInformationLog(string message, string _namespace, out LogMessageWLevel userLog, out DebugLogMessageLevel debugLog, bool includeDateTime = true, params string[] additionalMessages)
        {
            Logger.WriteInformationLog(message, _namespace, out userLog, out debugLog, includeDateTime, additionalMessages);
            if (Logs == null)
                Logs = new List<LogMessage>();
            if (debugLog != null)
                Logs.Add(debugLog);
        }

        public void AppendLogs (LogDataList logList)
        {
            if (logList != null && logList.Logs != null)
            {
                if (Logs == null)
                    Logs = new List<LogMessage>();
                Logs.AddRange(logList.Logs);
            }
        }
    }

   
}
