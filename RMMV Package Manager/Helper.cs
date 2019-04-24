using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public enum RMRootType
    {
        RMMV_INSTALLATION_DIR,
        PROJECT_DIR,
        XML_DIR
    }

    [Flags]
    public enum MoveFileResult
    {
        Success = 1,
        ErrorBeforeSuccess = 2,
        SourceFileNotFound = 4,
        InvalidSourcePathProvided = 8,
        InvalidDestinationPathProvided = 16,
        UserCancelled = 32,
        FileCopiedButOrigNotDeleted = 64
    }

    public enum CopyFileResult
    {
        Success,
        ErrorBeforeSuccess,
        SourceFileNotFound,
        InvalidSourcePathProvided,
        InvalidDestinationPathProvided,
        UserCancelled
    }

    public enum WriteFileResult
    {
        Success,
        ErrorBeforeSuccess,
        InvalidPathProvided,
        UserCancelled,
        DataToWriteIsNull
    }

    public enum DeleteFileResult
    {
        Success,
        ErrorBeforeSuccess,
        FileNotFound,
        InvalidPathProvided,
        UserCancelled
    }

    public enum DeleteFolderResult
    {
        Success,
        ErrorBeforeSuccess,
        DirectoryNotFound,
        InvalidPathProvided,
        UserCancelled
    }
    public enum CreateFolderResult
    {
        Success,
        ErrorBeforeSuccess,
        UserCancelled,
        DirectoryAlreadyExists,
        InvalidPathProvided

    }


    static class Helper
    {

       
        public static int TryToInt(this string str, int minVal)
        {
            int tInt;
            if (int.TryParse(str, out tInt) && tInt >= minVal)
                return tInt;
            return -1;
        }


        public static string GetRelativePath(string path, string rootPath)
        {
            if (rootPath.EndsWith("\\"))
                return path.Remove(0, rootPath.Length);
            else
             return path.Remove(0, rootPath.Length + 1);
        }

        public static void MakeDirIfNotExistInstall(string path, string _namespace)
        {
            Exception ex;
            if (!Directory.Exists(path) && Helper.CreateFolderSafely(path, _namespace, out ex, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                throw ex;
        }

        public static void MakeDirIfNotExistCopy(string path, string _namespace)
        {
            Exception ex;
            if (!Directory.Exists(path) && Helper.CreateFolderSafely(path, _namespace, out ex, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                throw ex;
        }

        public static string CreateMD5FromString(string input) //https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite, string _namespace)
        {
            LogDataList log;
            DirectoryCopy(sourceDirName, destDirName, copySubDirs, overwrite, _namespace, false, out log);
            
        }

       


        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite, string _namespace, bool ignoreErrors, out LogDataList log) //https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        {
            log = new LogDataList();
            log.WriteInformationLog(LoggerMessages.Helper.CopyDir(sourceDirName, destDirName), _namespace);

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = null;
            try
            {
                dir = new DirectoryInfo(sourceDirName);
            }catch (Exception ex)
            {
                if (!ignoreErrors)
                    throw;
                log.WriteErrorLog(LoggerMessages.Helper.DIR_COPY_ERR_GEN + sourceDirName + ".", _namespace, ex);
                return;
            }
            Exception exOut;
            if (!dir.Exists)
            {
                try
                {
                    throw new DirectoryNotFoundException(sourceDirName + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                }
                catch (Exception ex2)
                {
                    if (!ignoreErrors)
                        throw;

                    log.WriteErrorLog(LoggerMessages.Helper.DIR_COPY_ERR_GEN + sourceDirName + ".", _namespace, ex2);
                    return;
                }
            }
            DirectoryInfo[] dirs = null;
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (Exception ex)
            {
                if (!ignoreErrors)
                    throw;
                log.WriteErrorLog(LoggerMessages.Helper.DIR_COPY_ERR_GEN + sourceDirName + ".", _namespace, ex);
                return;
            }
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {

                if (CreateFolderSafely(destDirName, _namespace, out exOut) == CreateFolderResult.UserCancelled) // null arg
                {
                    if (!ignoreErrors)
                        throw exOut;
                    return;
                }
            }

            // Get the files in the directory and copy them to the new location.


            FileInfo[] files = null;
            try
            {
                files = dir.GetFiles();
            }
            catch (Exception ex)
            {
                if (!ignoreErrors)
                    throw;
                log.WriteErrorLog(LoggerMessages.Helper.DIR_COPY_ERR_GEN + sourceDirName + ".", _namespace, ex);
                return;
            }

            foreach (FileInfo file in files)
            {

                string temppath = Path.Combine(destDirName, file.Name);
                CopyFileResult copyRes = CopyFileSafely(file.FullName, temppath, overwrite, _namespace, out exOut); // null arg
                if (copyRes == CopyFileResult.UserCancelled || copyRes == CopyFileResult.SourceFileNotFound)
                {
                    if (!ignoreErrors)
                        throw exOut;
                    continue;
                }

            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    LogDataList outLog = null;
                    try
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs, overwrite, _namespace, ignoreErrors, out outLog);
                    }
                    catch (Exception ex)
                    {
                        if (!ignoreErrors)
                            throw;
                        log.WriteErrorLog(LoggerMessages.Helper.DIR_COPY_ERR_GEN + subdir.FullName + ".", _namespace, ex);
                        continue;
                    }
                    log.AppendLogs(outLog);
                }
            }
        }

        /// <summary>
        /// Depth-first recursive delete, with handling for descendant 
        /// directories open in Windows Explorer.
        /// </summary>
        public static void DeleteDirectory(string path, bool recursive = true) //https://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory, recursive);
            }

            try
            {
                Directory.Delete(path, recursive);
            }
            catch (IOException)
            {
                Directory.Delete(path, recursive);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, recursive);
            }
        }

        public static void DeleteEmptySubDir(string path, string _namespace, bool showGUI = true)
        {
            Logger.WriteInformationLog(LoggerMessages.Helper.DELETE_EMPTY_SUB_DIR + path + ".", _namespace);
            string[] subDirs = Directory.GetDirectories(path);
            if (subDirs == null || subDirs.Length == 0)
                return;

            Action actionToPerform = new Action(delegate ()
            {
                foreach (string directory in subDirs)
                {
                    if (Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Length == 0)
                        DeleteDirectory(directory, false);
                    else
                        DeleteEmptySubDir(directory, _namespace, false);
                }
            });
            if (showGUI)
            {

            }
            else
                actionToPerform();
          
        }

        public static string GetCommonPath(bool GetDirectoryName, IEnumerable<string> listOfPath)
        {
            if (listOfPath == null || listOfPath.Count() == 0)
                return null;
            string[] paths = listOfPath.ToArray();
            for (int i = 0; i < paths.Length; ++i)
            {
                if (!string.IsNullOrWhiteSpace(paths[i]))
                 paths[i] = paths[i].ToLower();
            }


            string[] commonPathList = new string[paths.Length];
            for (int i = 0; i < paths.Length; ++i)
            {
                IntAndString result = GetCommonPathListCount(paths[i], paths, GetDirectoryName, false).GetItemWithMaxIntAndOrStrLen();
                if (result != null)
                    commonPathList[i] = result.String;
                else
                    commonPathList[i] = null;
            }


            List<IntAndString> finalCommonPathList = new List<IntAndString>(commonPathList.Length);
            for (int i = 0; i < commonPathList.Length; ++i)
            {
                if (string.IsNullOrWhiteSpace(commonPathList[i]))
                    continue;
                int counter = 0;
                for (int i2 = 0; i2 < commonPathList.Length; ++i2)
                {
                    if (i == i2 || string.IsNullOrWhiteSpace(commonPathList[i2]))
                        continue;
                    if (commonPathList[i] == commonPathList[i2])
                    {
                        ++counter;
                        commonPathList[i2] = null;
                    }
                }
                if (counter > 0)
                 finalCommonPathList.Add(new IntAndString(counter, commonPathList[i]));
            }

            IntAndString res = finalCommonPathList.GetItemWithMaxIntAndOrStrLen();
            if (res == null)
                return null;

            return res.String;

        }

       


        public static List<IntAndString> GetCommonPathListCount(string pathToCheck, IEnumerable<string> listOfPath, bool GetDirectoryName = true, bool performLowerCase = true)
        {
            if (listOfPath == null || listOfPath.Count() == 0)
                return null;
            if (string.IsNullOrWhiteSpace(pathToCheck))
                return null;
            string[] strToArray = listOfPath.ToArray();

            if (GetDirectoryName)
            {
                if (performLowerCase)
                    pathToCheck = Path.GetDirectoryName(pathToCheck).ToLower();
                else
                    pathToCheck = Path.GetDirectoryName(pathToCheck);
                for (int i = 0; i < strToArray.Length; ++i)
                {

                    if (performLowerCase)
                        strToArray[i] = Path.GetDirectoryName(strToArray[i]).ToLower();
                    else
                        strToArray[i] = Path.GetDirectoryName(strToArray[i]);
                }
            }
            else if (performLowerCase)
            {
                pathToCheck = pathToCheck.ToLower();
                for (int i = 0; i < strToArray.Length; ++i)
                {
                    strToArray[i] = strToArray[i].ToLower();
                }
            }



            List<IntAndString> listOfIntAndStr = new List<IntAndString>();

            string subStr = GetSubPathInPath(pathToCheck, 1);
            int i3 = 1;
            while (subStr != null)
            {
                int counter = 0;
                for (int i2 = 0; i2 < strToArray.Length; ++i2)
                {
                    if (strToArray[i2].StartsWith(subStr))
                        ++counter;
                }
                if (counter == 0)
                    break;
                listOfIntAndStr.Add(new IntAndString(counter, subStr));
                ++i3;
                subStr = GetSubPathInPath(pathToCheck, i3);

            }

            if (GetDirectoryName)
            {
                int counter = 0;
                for (int i2 = 0; i2 < strToArray.Length; ++i2)
                {
                    if (strToArray[i2].StartsWith(pathToCheck))
                        ++counter;
                }
                if (counter > 0)
                    listOfIntAndStr.Add(new IntAndString(counter, pathToCheck));
            }
            return listOfIntAndStr;
        }

        static string GetSubPathInPath(string path, int ascLevel)
        {
            if (string.IsNullOrWhiteSpace(path) || ascLevel == 0)
                return null;
            int fSlashCount = 0;
            char[] charArray = path.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charArray.Length; ++i)
            {
                sb.Append(charArray[i]);
                if (charArray[i] == '\\')
                {
                    ++fSlashCount;
                    if (fSlashCount == ascLevel)
                        return sb.ToString();
                }
                
            }
            return null;
        }

        public static MoveFileResult MoveFileSafely(string sourceFile, string destination, bool overwrite, string _namespaceOfCallee, out Exception ex, MoveFileLogMessages logMessages = null)
        {
            string logMessage;
            DebugLogMessageLevel debugLog;
            if (string.IsNullOrWhiteSpace(sourceFile))
            {
                try
                {
                    throw new InvalidPathException(sourceFile);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && !string.IsNullOrWhiteSpace(logMessages.NullSourceFile)) ? logMessages.NullSourceFile : LoggerMessages.FileSystem.Error.MOVE_FILE_FAILED_NULL_SOURCE;
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return MoveFileResult.InvalidSourcePathProvided;
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                try
                {
                    throw new InvalidPathException(destination);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.NullDestinationFile != null) ? logMessages.NullDestinationFile.Invoke(sourceFile) : LoggerMessages.FileSystem.Error.MoveFileFailedNullDest(sourceFile);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return MoveFileResult.InvalidDestinationPathProvided;
            }

            if (!File.Exists(sourceFile))
            {
                try
                {
                    throw new FileNotFoundException(sourceFile + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.SourceFileNotFound != null) ? logMessages.SourceFileNotFound.Invoke(sourceFile) : LoggerMessages.FileSystem.Error.MoveFileFailedNotExistSource(sourceFile);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return MoveFileResult.SourceFileNotFound;
            }

            MoveFileResult retVal = MoveFileResult.Success;
            ex = null;
            CopyFileResult copyResult = CopyFileSafely(sourceFile, destination, overwrite, _namespaceOfCallee, out ex, null);

            if (copyResult == CopyFileResult.UserCancelled)
            {
                logMessage = (logMessages != null && logMessages.MoveFileFailed != null) ? logMessages.MoveFileFailed.Invoke(sourceFile, destination) : LoggerMessages.FileSystem.Error.MoveFileFailed(sourceFile, destination);
                Logger.WriteErrorLog(logMessage, _namespaceOfCallee, ex, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                    logMessages.LogGroup.Logs.Add(debugLog);
                return MoveFileResult.UserCancelled;
            }

            if (copyResult == CopyFileResult.ErrorBeforeSuccess)
                retVal = MoveFileResult.ErrorBeforeSuccess;

            if (copyResult == CopyFileResult.InvalidDestinationPathProvided)
                return MoveFileResult.InvalidDestinationPathProvided;

            if (copyResult == CopyFileResult.InvalidSourcePathProvided)
                return MoveFileResult.InvalidSourcePathProvided;

            if (copyResult == CopyFileResult.SourceFileNotFound)
                return MoveFileResult.SourceFileNotFound;


            DeleteFileResult delResult = DeleteFileSafely(sourceFile, _namespaceOfCallee, out ex, null);

            if (delResult == DeleteFileResult.UserCancelled)
                retVal = MoveFileResult.FileCopiedButOrigNotDeleted | MoveFileResult.UserCancelled;

            if (delResult == DeleteFileResult.ErrorBeforeSuccess)
                retVal = MoveFileResult.ErrorBeforeSuccess;

            if (delResult == DeleteFileResult.FileNotFound)
                retVal = MoveFileResult.FileCopiedButOrigNotDeleted | MoveFileResult.SourceFileNotFound;

            if (delResult == DeleteFileResult.InvalidPathProvided)
                retVal = MoveFileResult.FileCopiedButOrigNotDeleted | MoveFileResult.InvalidSourcePathProvided;

            if (retVal == MoveFileResult.ErrorBeforeSuccess || retVal == MoveFileResult.Success)
            {
                logMessage = (logMessages != null && logMessages.MoveFileSuccess != null) ? logMessages.MoveFileSuccess.Invoke(sourceFile, destination) : LoggerMessages.FileSystem.MoveFile(sourceFile, destination);
                Logger.WriteInformationLog(logMessage, _namespaceOfCallee, out debugLog);
                if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                    logMessages.LogGroup.Logs.Add(debugLog);
            }
            else
            {
                logMessage = (logMessages != null && logMessages.SourceFileNotDeleted != null) ? logMessages.SourceFileNotDeleted.Invoke(sourceFile, destination) : LoggerMessages.FileSystem.MoveFilePartial(sourceFile, destination);
                Logger.WriteWarningLog(logMessage, _namespaceOfCallee, out debugLog, ex);
                if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                    logMessages.LogGroup.Logs.Add(debugLog);
            }

            return retVal;
        }

        public static CopyFileResult CopyFileSafely(string sourceFile, string destination, bool overwrite, string _namespaceOfCallee, out Exception ex, CopyFileLogMessages logMessages = null)
        {
            ex = null;
            CopyFileResult retVal = CopyFileResult.Success;
            string logMessage;
            DebugLogMessageLevel debugLog;
            if (string.IsNullOrWhiteSpace(sourceFile))
            {
                try
                {
                    throw new InvalidPathException(sourceFile);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && !string.IsNullOrWhiteSpace(logMessages.NullSourceFile)) ? logMessages.NullSourceFile : LoggerMessages.FileSystem.Error.COPY_FILE_FAILED_NULL_SOURCE;
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return CopyFileResult.InvalidSourcePathProvided;
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                try
                {
                    throw new InvalidPathException(destination);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.NullDestinationFile != null) ? logMessages.NullDestinationFile.Invoke(sourceFile) : LoggerMessages.FileSystem.Error.CopyFileFailedNullDest(sourceFile);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return CopyFileResult.InvalidDestinationPathProvided;
            }

            if (!File.Exists(sourceFile))
            {
                try
                {
                    throw new FileNotFoundException(sourceFile + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.SourceFileNotFound != null) ? logMessages.SourceFileNotFound.Invoke(sourceFile) : LoggerMessages.FileSystem.Error.CopyFileFailedNotExistSource(sourceFile);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return CopyFileResult.SourceFileNotFound;
            }


            try
            {
                File.Copy(sourceFile, destination, overwrite);
            }
            catch (Exception exCaught)
            {
                ex = exCaught;
                retVal = CopyFileResult.ErrorBeforeSuccess;
                if (Helper.ShowMessageBox(MessageBoxStrings.Helper.CopyFileFailed(sourceFile, destination), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    logMessage = (logMessages != null && logMessages.CopyFileFailed != null) ? logMessages.CopyFileFailed.Invoke(sourceFile, destination) : LoggerMessages.FileSystem.Error.CopyFileFailed(sourceFile, destination);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, ex, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    return CopyFileResult.UserCancelled;

                }
                Exception exTemp;
                CopyFileResult retVal2 = CopyFileSafely(sourceFile, destination, overwrite, _namespaceOfCallee, out exTemp, logMessages);
                if (ex == null)
                    ex = exTemp;
                if (retVal2 == CopyFileResult.Success && retVal == CopyFileResult.ErrorBeforeSuccess)
                    return CopyFileResult.ErrorBeforeSuccess;
                else
                    return retVal2;
            }
            logMessage = (logMessages != null && logMessages.CopyFileSuccess != null) ? logMessages.CopyFileSuccess.Invoke(sourceFile, destination) : LoggerMessages.FileSystem.CopyFile(sourceFile, destination);
            Logger.WriteInformationLog(logMessage, _namespaceOfCallee, out debugLog);
            if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                logMessages.LogGroup.Logs.Add(debugLog);
            return retVal;
        }

        public static WriteFileResult WriteAllTextSafely(string path, string dataToWrite, string _namespaceOfCallee, out Exception ex, WriteAllTextLogMessages logMessages = null)
        {
            ex = null;
            WriteFileResult retVal = WriteFileResult.Success;
            string logMessage;
            DebugLogMessageLevel debugLog;
            if (string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    throw new InvalidPathException(path);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && !string.IsNullOrWhiteSpace(logMessages.NullPathToFile)) ? logMessages.NullPathToFile : LoggerMessages.FileSystem.Error.WRITE_FILE_FAILED_NULL_DEST;
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return WriteFileResult.InvalidPathProvided;
            }

            if (dataToWrite == null)
            {
                try
                {
                    throw new ArgumentNullException(ExceptionMessages.Helper.DATA_TO_WRITE_ARG_NULL);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.NullDataToWrite != null) ? logMessages.NullDataToWrite.Invoke(path) : LoggerMessages.FileSystem.Error.WriteToFileFailedNullText(path);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, ex, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return WriteFileResult.DataToWriteIsNull;
            }

            try
            {
                File.WriteAllText(path, dataToWrite);
            }
            catch (Exception exCaught)
            {
                ex = exCaught;
                retVal = WriteFileResult.ErrorBeforeSuccess;
                if (Helper.ShowMessageBox(MessageBoxStrings.Helper.WriteFailed(path), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    logMessage = (logMessages != null && logMessages.WriteFailed != null) ? logMessages.WriteFailed.Invoke(path) : LoggerMessages.FileSystem.Error.WriteToFileFailed(path);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, ex, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    return WriteFileResult.UserCancelled;
                }
                Exception exTemp;
                WriteFileResult retVal2 = WriteAllTextSafely(path, dataToWrite, _namespaceOfCallee, out exTemp, logMessages);
                if (ex == null)
                    ex = exTemp;
                if (retVal2 == WriteFileResult.Success && retVal == WriteFileResult.ErrorBeforeSuccess)
                    return WriteFileResult.ErrorBeforeSuccess;
                else
                    return retVal2;

            }
            logMessage = (logMessages != null && logMessages.WriteSuccess != null) ? logMessages.WriteSuccess.Invoke(path) : LoggerMessages.FileSystem.WRITE_FILE + path + ".";
            Logger.WriteInformationLog(logMessage, _namespaceOfCallee, out debugLog);
            if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                logMessages.LogGroup.Logs.Add(debugLog);
            return retVal;
        }

        public static DeleteFileResult DeleteFileSafely(string path, string _namespaceOfCallee, out Exception ex, DeleteFileLogMessages logMessages = null)
        {
            ex = null;
            DeleteFileResult retVal = DeleteFileResult.Success;
            string logMessage;
            DebugLogMessageLevel debugLog;
            if (string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    throw new InvalidPathException(path);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && !string.IsNullOrWhiteSpace(logMessages.NullPathToFile)) ? logMessages.NullPathToFile : LoggerMessages.FileSystem.Error.DELETE_FILE_FAILED_NULL_PATH;
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return DeleteFileResult.InvalidPathProvided;
            }
            if (!File.Exists(path))
            {
                try
                {
                    throw new FileNotFoundException(path + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.FileNotFound != null) ? logMessages.FileNotFound.Invoke(path) : LoggerMessages.FileSystem.Warning.DeleteFileFailedNotExist(path);
                    Logger.WriteWarningLog(logMessage, _namespaceOfCallee, out debugLog, caughtEx);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return DeleteFileResult.FileNotFound;
            }


            try
            {
                File.Delete(path);
            }
            catch (Exception exCaught)
            {
                ex = exCaught;
                retVal = DeleteFileResult.ErrorBeforeSuccess;
                if (Helper.ShowMessageBox(MessageBoxStrings.Helper.DelFileFailed(path), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    logMessage = (logMessages != null && logMessages.DeleteFailed != null) ? logMessages.DeleteFailed.Invoke(path) : LoggerMessages.FileSystem.Error.DeleteFileFailed(path);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, ex, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    return DeleteFileResult.UserCancelled;
                }
                Exception exTemp;
                DeleteFileResult retVal2 = DeleteFileSafely(path, _namespaceOfCallee, out exTemp, logMessages);
                if (ex == null)
                    ex = exTemp;
                if (retVal2 == DeleteFileResult.Success && retVal == DeleteFileResult.ErrorBeforeSuccess)
                    return DeleteFileResult.ErrorBeforeSuccess;
                else
                    return retVal2;
            }

            logMessage = (logMessages != null && logMessages.DeleteSuccess != null) ? logMessages.DeleteSuccess.Invoke(path) : LoggerMessages.FileSystem.DELETED_FILE + path + " successfully.";
            Logger.WriteInformationLog(logMessage, _namespaceOfCallee, out debugLog);
            if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                logMessages.LogGroup.Logs.Add(debugLog);
            return retVal;
        }

       


        public static DeleteFolderResult DeleteFolderSafely(string path, string _namespaceOfCallee, out Exception ex, DeleteFolderLogMessages logMessages = null, bool recursive = true, bool showLoadingForm = true)
        {
            ex = null;
            DeleteFolderResult retVal = DeleteFolderResult.Success;
            string logMessage;
            DebugLogMessageLevel debugLog;
            if (string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    throw new InvalidPathException(path);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && !string.IsNullOrWhiteSpace(logMessages.NullPathToDir)) ? logMessages.NullPathToDir : LoggerMessages.FileSystem.Error.DELETE_FOLDER_FAILED_NULL_PATH;
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return DeleteFolderResult.InvalidPathProvided;
            }
            if (!Directory.Exists(path))
            {
                try
                {
                    throw new DirectoryNotFoundException(path + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.DirNotFound != null) ? logMessages.DirNotFound.Invoke(path) : LoggerMessages.FileSystem.Warning.DeleteFolderFailedNotExist(path);
                    Logger.WriteWarningLog(logMessage, _namespaceOfCallee, out debugLog, caughtEx);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return DeleteFolderResult.DirectoryNotFound;
            }
            Exception exDelegOut = null;
            frmLoading loadingForm = null;
            Action action = delegate ()
            {
                try
                {
                    DeleteDirectory(path, recursive);
                }
                catch (Exception exCaught)
                {
                    exDelegOut = exCaught;
                    retVal = DeleteFolderResult.ErrorBeforeSuccess;
                    if (Helper.ShowMessageBox(MessageBoxStrings.Helper.DelDirFailed(path), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        logMessage = (logMessages != null && logMessages.DeleteFailed != null) ? logMessages.DeleteFailed.Invoke(path) : LoggerMessages.FileSystem.Error.DeleteDirFailed(path);
                        Logger.WriteErrorLog(logMessage, _namespaceOfCallee, exDelegOut, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                        if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                            logMessages.LogGroup.Logs.Add(debugLog);
                        retVal = DeleteFolderResult.UserCancelled;
                        if (showLoadingForm)
                            loadingForm.SafeClose();
                        return;
                    }
                    Exception exTemp;
                    DeleteFolderResult retVal2 = DeleteFolderSafely(path, _namespaceOfCallee, out exTemp, logMessages, recursive, false);
                    if (exDelegOut == null)
                        exDelegOut = exTemp;
                    if (retVal2 == DeleteFolderResult.Success && retVal == DeleteFolderResult.ErrorBeforeSuccess)
                    {
                        retVal = DeleteFolderResult.ErrorBeforeSuccess;
                        if (showLoadingForm)
                            loadingForm.SafeClose();
                        return;
                    }
                    else
                    {
                        retVal = retVal2;
                        if (showLoadingForm)
                            loadingForm.SafeClose();
                        return;
                    }

                }
                logMessage = (logMessages != null && logMessages.DeleteSuccess != null) ? logMessages.DeleteSuccess.Invoke(path) : LoggerMessages.FileSystem.DELETED_DIR + path + " successfully.";
                Logger.WriteInformationLog(logMessage, _namespaceOfCallee, out debugLog);
                if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                    logMessages.LogGroup.Logs.Add(debugLog);
                if (showLoadingForm)
                    loadingForm.SafeClose();
            };
            if (showLoadingForm)
            {
                Thread thread = new Thread(delegate () { action(); });
              
               
                if (GlobalClass.MainForm != null && GlobalClass.MainForm.Visible && !GlobalClass.MainForm.Disposing && !GlobalClass.MainForm.IsDisposed)
                    GlobalClass.MainForm.Invoke((MethodInvoker)delegate {
                        loadingForm = new frmLoading(StringConst.frmLoading.DEL_DIR + path + ".");
                        thread.Start();
                        loadingForm.ShowDialog();
                    });
                else
                {
                    loadingForm = new frmLoading(StringConst.frmLoading.DEL_DIR + path + "."); 
                    thread.Start();
                    loadingForm.ShowDialog();
                }


                ex = exDelegOut;
            }
            else
                action();
            return retVal;
        }
      

        public static CreateFolderResult CreateFolderSafely(string path, string _namespaceOfCallee, out Exception ex, CreateFolderLogMessages logMessages = null)
        {
            ex = null;
            string logMessage;
            CreateFolderResult retVal = CreateFolderResult.Success;
            DebugLogMessageLevel debugLog;
            if (string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    throw new InvalidPathException(path);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && !string.IsNullOrWhiteSpace(logMessages.NullPathToDir)) ? logMessages.NullPathToDir : LoggerMessages.FileSystem.Error.CREATE_FOLDER_FAILED_NULL_PATH;
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, caughtEx, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return CreateFolderResult.InvalidPathProvided;
            }
            if (Directory.Exists(path))
            {
                try
                {
                    throw new DirectoryExistsException(path + ExceptionMessages.General.ALREADY_EXISTS, path);
                }
                catch (Exception caughtEx)
                {
                    logMessage = (logMessages != null && logMessages.DirAlreadyExists != null) ? logMessages.DirAlreadyExists.Invoke(path) : LoggerMessages.FileSystem.Warning.CreateFolderFailedAlreadyExists(path);
                    Logger.WriteWarningLog(logMessage, _namespaceOfCallee, out debugLog, caughtEx);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    ex = caughtEx;
                }
                return CreateFolderResult.DirectoryAlreadyExists;
            }
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception exCaught)
            {
                ex = exCaught;
                retVal = CreateFolderResult.ErrorBeforeSuccess;
                if (Helper.ShowMessageBox(MessageBoxStrings.Helper.CreateDirTreeFailed(path), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    logMessage = (logMessages != null && logMessages.CreateFailed != null) ? logMessages.CreateFailed.Invoke(path) : LoggerMessages.FileSystem.Error.CreateDirFailed(path);
                    Logger.WriteErrorLog(logMessage, _namespaceOfCallee, ex, BasicDebugLogger.DebugErrorType.Error, out debugLog);
                    if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                        logMessages.LogGroup.Logs.Add(debugLog);
                    return CreateFolderResult.UserCancelled;
                }
                Exception exTemp;
                CreateFolderResult retVal2 = CreateFolderSafely(path, _namespaceOfCallee, out exTemp, logMessages);
                if (ex == null)
                    ex = exTemp;
                if (retVal2 == CreateFolderResult.Success && retVal == CreateFolderResult.ErrorBeforeSuccess)
                    return CreateFolderResult.ErrorBeforeSuccess;
                else
                    return retVal2;

            }
            logMessage = (logMessages != null && logMessages.CreateSuccess != null) ? logMessages.CreateSuccess.Invoke(path) : LoggerMessages.FileSystem.CREATED_DIR + path + " successfully.";
            Logger.WriteInformationLog(logMessage, _namespaceOfCallee, out debugLog);
            if (logMessages != null && logMessages.LogGroup != null && debugLog != null)
                logMessages.LogGroup.Logs.Add(debugLog);
            return retVal;
        }

        public static DialogResult ShowMessageBox(string message, string messageBoxName, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            bool invokeDone = false;
            DialogResult retVal = DialogResult.OK;
            if (GlobalClass.MainForm != null && GlobalClass.MainForm.Visible && !GlobalClass.MainForm.Disposing && !GlobalClass.MainForm.IsDisposed && GlobalClass.MainForm.InvokeRequired)
                GlobalClass.MainForm.Invoke((MethodInvoker)delegate
                {
                    retVal = MessageBox.Show(message, messageBoxName, messageBoxButtons, messageBoxIcon);
                    invokeDone = true;
                });
            else
            {
                retVal = MessageBox.Show(message, messageBoxName, messageBoxButtons, messageBoxIcon);
                invokeDone = true;
            }
            while (!invokeDone) ;
            return retVal;
        }

    }
    public class IntAndString
    {
        public int Integer { get; private set; }
        public string String { get; private set; }

        public IntAndString(int integer, string _string)
        {
            Integer = integer;
            String = _string;
        }
    }

    public delegate string StringMessageSingleArg(string arg);
    public delegate string StringMessageDualArg(string arg1, string arg2);

    public class MoveFileLogMessages
    {
        public string NullSourceFile;
        public StringMessageSingleArg NullDestinationFile;
        public StringMessageSingleArg SourceFileNotFound;
        public StringMessageDualArg MoveFileFailed;
        public StringMessageDualArg SourceFileNotDeleted;
        public StringMessageDualArg MoveFileSuccess;
        public LogDataList LogGroup;

        public MoveFileLogMessages(string nullSourceFile = null, StringMessageSingleArg nullDestinationFile = null, StringMessageSingleArg sourceFileNotFound = null, StringMessageDualArg moveFileFailed = null, StringMessageDualArg sourceFileNotDeleted = null, StringMessageDualArg moveFileSuccess = null, LogDataList logGroup = null)
        {
            NullSourceFile = nullSourceFile;
            NullDestinationFile = nullDestinationFile;
            SourceFileNotDeleted = sourceFileNotDeleted;
            SourceFileNotFound = sourceFileNotFound;
            MoveFileFailed = moveFileFailed;
            MoveFileSuccess = moveFileSuccess;
            LogGroup = logGroup;
        }
    }

    public class CopyFileLogMessages
    {
        public string NullSourceFile;
        public StringMessageSingleArg NullDestinationFile;
        public StringMessageSingleArg SourceFileNotFound;
        public StringMessageDualArg CopyFileSuccess;
        public StringMessageDualArg CopyFileFailed;
        public LogDataList LogGroup;

        public CopyFileLogMessages(string nullSourceFile = null, StringMessageSingleArg nullDestinationFile = null, StringMessageSingleArg sourceFileNotFound = null, StringMessageDualArg copyFileSuccess = null, StringMessageDualArg copyFileFailed = null, LogDataList logGroup = null)
        {
            NullSourceFile = nullSourceFile;
            NullDestinationFile = nullDestinationFile;
            SourceFileNotFound = sourceFileNotFound;
            CopyFileSuccess = copyFileSuccess;
            CopyFileFailed = copyFileFailed;
            LogGroup = logGroup;
        }
    }

    public class WriteAllTextLogMessages
    {
        public string NullPathToFile;
        public StringMessageSingleArg NullDataToWrite;
        public StringMessageSingleArg WriteFailed;
        public StringMessageSingleArg WriteSuccess;
        public LogDataList LogGroup;

        public WriteAllTextLogMessages(string nullPathToFile = null, StringMessageSingleArg nullDataToWrite = null, StringMessageSingleArg writeFailed = null, StringMessageSingleArg writeSuccess = null, LogDataList logGroup = null)
        {
            NullPathToFile = nullPathToFile;
            NullDataToWrite = nullDataToWrite;
            WriteFailed = writeFailed;
            WriteSuccess = writeSuccess;
            LogGroup = logGroup;
        }
    }

    public class DeleteFileLogMessages
    {
        public string NullPathToFile;
        public StringMessageSingleArg FileNotFound;
        public StringMessageSingleArg DeleteFailed;
        public StringMessageSingleArg DeleteSuccess;
        public LogDataList LogGroup;

        public DeleteFileLogMessages(string nullPathToFile = null, StringMessageSingleArg fileNotFound = null, StringMessageSingleArg deleteFailed = null, StringMessageSingleArg deleteSuccess = null, LogDataList logGroup = null)
        {
            NullPathToFile = nullPathToFile;
            FileNotFound = fileNotFound;
            DeleteFailed = deleteFailed;
            DeleteSuccess = deleteSuccess;
            LogGroup = logGroup;
        }
    }

    public class DeleteFolderLogMessages
    {
        public string NullPathToDir;
        public StringMessageSingleArg DirNotFound;
        public StringMessageSingleArg DeleteFailed;
        public StringMessageSingleArg DeleteSuccess;
        public LogDataList LogGroup;

        public DeleteFolderLogMessages(string nullPathToDir = null, StringMessageSingleArg dirNotFound = null, StringMessageSingleArg deleteFailed = null, StringMessageSingleArg deleteSuccess = null, LogDataList logGroup = null)
        {
            NullPathToDir = nullPathToDir;
            DirNotFound = dirNotFound;
            DeleteFailed = deleteFailed;
            DeleteSuccess = deleteSuccess;
            LogGroup = logGroup;
        }
    }

    public class CreateFolderLogMessages
    {
        public string NullPathToDir;
        public StringMessageSingleArg DirAlreadyExists;
        public StringMessageSingleArg CreateFailed;
        public StringMessageSingleArg CreateSuccess;
        public LogDataList LogGroup;

        public CreateFolderLogMessages(string nullPathToDir = null, StringMessageSingleArg dirAlreadyExists = null, StringMessageSingleArg createFailed = null, StringMessageSingleArg createSuccess = null, LogDataList logGroup = null)
        {
            NullPathToDir = nullPathToDir;
            DirAlreadyExists = dirAlreadyExists;
            CreateFailed = createFailed;
            CreateSuccess = createSuccess;
            LogGroup = logGroup;
        }
    }
}
