using Indieteur.BasicLoggingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UninstallerMessages = RMMV_PackMan.LoggerMessages.PackageManagement.Uninstaller;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static partial class Uninstaller
        {
            public static event PackageUninstallBeginDelegate OnPackageUninstallBegin;
            public static event PackageUninstalledDelegate OnPackageUninstalled;
            public enum UninstallResult
            {
                normal,
                genPartsRemoved,
                XMLFileOrDirectoryNotFound,
                UIDNotFoundOrHasInvalidPackage
            }
            public enum UninstallArgType
            {
                XMLPath,
                DirectoryPath,
                UID
            }
            public static void UninstallLocalPackage(string projectPath, string arg, string _namespace, out LogDataList log, UninstallArgType argType = UninstallArgType.UID, bool deleteInstallFiles = true, bool procEvents = true)
            {

                if (string.IsNullOrWhiteSpace(projectPath))
                {
                    try
                    {
                        throw new ArgumentNullException(ExceptionMessages.General.PROJ_PATH_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.UNABLE_OPEN_LOCAL_PACKAGE_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                log = new LogDataList();
                LogDataList outLog = null;
                ProjectPackMan openedProject = null;
                try
                {
                    openedProject = new ProjectPackMan(projectPath, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(UninstallerMessages.Error.UNABLE_OPEN_LOCAL_PACKAGE + projectPath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                try
                {
                    UninstallLocalPackage(openedProject, arg, _namespace, out outLog, argType, deleteInstallFiles, procEvents);
                }
                catch
                {
                    throw;
                }
                log.AppendLogs(outLog);

            }
            public static void UninstallLocalPackage(ProjectPackMan openProject, string arg, string _namespace, out LogDataList log, UninstallArgType argType = UninstallArgType.UID, bool deleteInstallFiles = true, bool procEvents = true)
            {
                log = new LogDataList();
                Logger.WriteInformationLog(UninstallerMessages.Information.UNINSTALL_PACKAGE_START_L, _namespace);
                Exception outEx;

                if (string.IsNullOrWhiteSpace(arg))
                {
                    try
                    {
                        throw new ArgumentNullException(ExceptionMessages.General.ARG_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.ARG_INVALID, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (argType == UninstallArgType.DirectoryPath)
                    arg += "\\" + Vars.INSTALLED_XML_FILENAME;

                if ((argType == UninstallArgType.XMLPath || argType == UninstallArgType.DirectoryPath) && !File.Exists(arg))
                {
                    try
                    {
                        throw new FileNotFoundException(ExceptionMessages.PackageManagement.Uninstaller.INSTALL_SCRIPT_FILE_NOT_FOUND + arg + ".");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.XML_FILE_NOT_FOUND + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                LogDataList outLog;
                if (openProject.InstalledPackages != null)
                {
                    InstalledPackage installedPackage;
                    if (argType == UninstallArgType.XMLPath || argType == UninstallArgType.DirectoryPath)
                        installedPackage = openProject.InstalledPackages.FindByPath(arg);
                    else
                        installedPackage = openProject.InstalledPackages.FindByUID(arg);

                    if (installedPackage != null)
                    {
                     
                        if (installedPackage.Package != null)
                        {
                            if (procEvents && OnPackageUninstallBegin != null)
                                OnPackageUninstallBegin.Invoke(installedPackage.Package, false, installedPackage.Directory, installedPackage, openProject);

                            UninstallPackage(installedPackage.Package, _namespace, out outLog, false, openProject.DirectoryPath);
                            if (outLog != null)
                                log.Logs.AddRange(outLog.Logs);

                            if (procEvents && OnPackageUninstalled != null)
                                OnPackageUninstalled.Invoke(installedPackage.Package, false, installedPackage, openProject);
                        }
                        else
                        {
                            ProcDelNonReadXMLPackage(arg, installedPackage.Directory, installedPackage, openProject, _namespace, out outLog, procEvents, argType, false, openProject.DirectoryPath);
                            if (outLog != null)
                                log.Logs.AddRange(outLog.Logs);
                        }
                        if (deleteInstallFiles)
                        {
                            if (Directory.Exists(installedPackage.Directory))
                                Helper.DeleteFolderSafely(installedPackage.Directory, _namespace, out outEx, new DeleteFolderLogMessages(deleteFailed: UninstallerMessages.Warning.DirectoryUnableDelete, logGroup: log));
                            openProject.InstalledPackages.Remove(installedPackage);
                        }
                        //else
                        //{
                        //    if (installedPackage.Package != null)
                        //    {
                        //        installedPackage.Installed = false;
                        //        try
                        //        {
                        //            installedPackage.Package.SaveToFile(installedPackage.XMLPath, _namespace, RMPackage.SaveToFileMode.ExplicitAssetInfo);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            log.DataList.Add(new RMLogDataWarning(UninstallerMessages.Warning.InstalledXMLUpdateFailedInsStatus(installedPackage.XMLPath), _namespace, ex));
                        //            throw;
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (argType == UninstallArgType.UID)
                        {
                            try
                            {
                                throw new PackageNotFoundException(true, ExceptionMessages.General.PackWIDNotFound(arg));
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(UninstallerMessages.Error.PACKAGE_UID_NOT_FOUND + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                throw;
                            }
                        }
                        string installedDir = null;
                        try
                        {
                            installedDir = Path.GetDirectoryName(arg);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(UninstallerMessages.Error.INVALID_XML_PATH + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                         ProcDelNonReadXMLPackage(arg, installedDir, null, openProject, _namespace, out outLog, procEvents, argType, false, openProject.DirectoryPath);
                         
                    }
                }
                else
                {
                    if (argType == UninstallArgType.UID)
                    {
                        try
                        {
                            throw new PackageNotFoundException(true, ExceptionMessages.General.PackWIDNotFound(arg));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(UninstallerMessages.Error.PACKAGE_UID_NOT_FOUND + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }
                    string installedDir = null;
                    try
                    {
                        installedDir = Path.GetDirectoryName(arg);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.INVALID_XML_PATH + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                    ProcDelNonReadXMLPackage(arg, installedDir, null, openProject, _namespace, out outLog, procEvents, argType, false, openProject.DirectoryPath);
                }
                Logger.WriteInformationLog(UninstallerMessages.Information.UNINSTALL_PACKAGE_DONE_L, _namespace);
            }

            public static void UninstallGlobalPackage (string arg, string _namespace, out LogDataList log, UninstallArgType argType = UninstallArgType.UID, bool renumberParts = true, bool deleteInstallFiles = true, bool procEvents = true)
            {
                log = new LogDataList();
                Logger.WriteInformationLog(UninstallerMessages.Information.UNINSTALL_PACKAGE_START_G, _namespace);


                if (string.IsNullOrWhiteSpace(arg))
                {
                    try
                    {
                        throw new ArgumentNullException(ExceptionMessages.General.ARG_ARG_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.ARG_INVALID, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (argType == UninstallArgType.DirectoryPath)
                    arg += "\\" + Vars.INSTALLED_XML_FILENAME;

                if ((argType == UninstallArgType.XMLPath || argType == UninstallArgType.DirectoryPath) && !File.Exists(arg))
                {
                    try
                    {
                        throw new FileNotFoundExceptionWPath(arg);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.XML_FILE_NOT_FOUND, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                Exception outEx;
                LogDataList outLog = null;
                UninstallResult result = UninstallResult.normal;
                if (GlobalPackages != null)
                {
                    InstalledPackage installedPackage;
                    if (argType == UninstallArgType.XMLPath || argType == UninstallArgType.DirectoryPath)
                        installedPackage = GlobalPackages.FindByPath(arg);
                    else
                        installedPackage = GlobalPackages.FindByUID(arg);

                    if (installedPackage != null)
                    {
                        if (installedPackage.Package != null)
                        {
                            if ( procEvents && OnPackageUninstallBegin != null)
                                OnPackageUninstallBegin.Invoke(installedPackage.Package, true, installedPackage.Directory, installedPackage, null);
                            
                             result = UninstallPackage(installedPackage.Package, _namespace, out outLog);
                            log.AppendLogs(outLog);

                            if (procEvents && OnPackageUninstalled != null)
                                OnPackageUninstalled.Invoke(installedPackage.Package, true, installedPackage, null);
                        }
                        else
                        {
                            result = ProcDelNonReadXMLPackage(arg, installedPackage.Directory, installedPackage, null, _namespace, out outLog, procEvents, argType);
                            log.AppendLogs(outLog);
                        }
                        if (deleteInstallFiles)
                        {

                            if (Directory.Exists(installedPackage.Directory))
                            {
                                string MD5INUID = new DirectoryInfo(installedPackage.Directory).Name;
                                Helper.DeleteFolderSafely(installedPackage.Directory, _namespace, out outEx, new DeleteFolderLogMessages(deleteFailed: UninstallerMessages.Warning.DirectoryUnableDelete, logGroup: log));
                                NewProject.DeletePackageInstallInfo(MD5INUID, _namespace);
                            }

                            GlobalPackages.Remove(installedPackage);
                        }
                        //else
                        //{
                        //    if (installedPackage.Package != null)
                        //    {
                        //        installedPackage.Installed = false;
                        //        bool successfulCopy = true;
                        //        try
                        //        {
                        //            installedPackage.Package.SaveToFile(installedPackage.XMLPath, _namespace, RMPackage.SaveToFileMode.ExplicitAssetInfo);
                                    
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            log.DataList.Add(new RMLogDataWarning(UninstallerMessages.Warning.InstalledXMLUpdateFailedInsStatus(installedPackage.XMLPath), _namespace, ex));
                        //            successfulCopy = false;
                        //        }
                        //        if (successfulCopy)
                        //            NewProject.CopyPackageInstallXML(installedPackage.XMLPath, _namespace, installedPackage.Package);
                        //    }
                        //}

                    }
                    else
                    {
                        if (argType == UninstallArgType.UID)
                        {
                            try
                            {
                                throw new PackageNotFoundException(true, ExceptionMessages.General.PackWIDNotFound(arg));
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(UninstallerMessages.Error.PACKAGE_UID_NOT_FOUND + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                throw;
                            }
                        }
                        string installedDir = null;
                        try
                        {
                             installedDir = Path.GetDirectoryName(arg);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(UninstallerMessages.Error.INVALID_XML_PATH + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                        result = ProcDelNonReadXMLPackage(arg, installedDir, null, null, _namespace, out outLog, procEvents, argType);
                     
                    }

                }
                else
                {
                    if (argType == UninstallArgType.UID)
                    {
                        try
                        {
                            throw new PackageNotFoundException(true, ExceptionMessages.General.PackWIDNotFound(arg));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(UninstallerMessages.Error.PACKAGE_UID_NOT_FOUND + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }
                    string installedDir = null;
                    try
                    {
                         installedDir = Path.GetDirectoryName(arg);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.INVALID_XML_PATH + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                    result = ProcDelNonReadXMLPackage(arg, installedDir, null, null, _namespace, out outLog, procEvents, argType);

                }
                outLog = null;

                try
                {
                    if (renumberParts && result == UninstallResult.genPartsRemoved)
                        GeneratorPartsManager.RenumberParts(_namespace, out outLog, true);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(UninstallerMessages.Error.GEN_PART_RENUMBER_FAILED, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }
                log.AppendLogs(outLog);
                Logger.WriteInformationLog(UninstallerMessages.Information.UNINSTALL_PACKAGE_DONE_G, _namespace);
            }


            static UninstallResult ProcDelNonReadXMLPackage(string arg, string installedDir, InstalledPackage installedPackage, ProjectPackMan openedProject, string _namespace, out LogDataList log, bool procEvents = true, UninstallArgType argType = UninstallArgType.UID, bool globalPackage = true, string whereToRemove = null)
            {
                log = new LogDataList();

                if (argType == UninstallArgType.DirectoryPath || argType == UninstallArgType.XMLPath)
                {
                    LogDataList outLog = null;
                    RMPackage package = null;
                    if (procEvents && OnPackageUninstallBegin != null)
                        OnPackageUninstallBegin.Invoke(package, globalPackage, installedDir, installedPackage, openedProject);

                    RMPackage parsedPackage = null;

                    try
                    {
                        parsedPackage = new RMPackage(arg, _namespace, out outLog);
                    }
                    catch (Exception ex)
                    {
                        log.AppendLogs(outLog);
                        Logger.WriteErrorLog(UninstallerMessages.Error.XML_FILE_INVALID + arg + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                    log.AppendLogs(outLog);

                    UninstallResult res = UninstallPackage(parsedPackage, _namespace, out outLog, globalPackage, whereToRemove);

                    log.AppendLogs(outLog);

                    if (procEvents && OnPackageUninstalled != null)
                        OnPackageUninstalled.Invoke(package, globalPackage, installedPackage, openedProject);

                    return res;
                }
                else
                {
                    try
                    {
                        throw new InvalidArgumentException(argType.GetType(), "argType");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(UninstallerMessages.Error.INVALID_PROCDELNONREADXMLPACKAGE_ARG, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
            }



            static UninstallResult UninstallPackage (RMPackage package, string _namespace, out LogDataList log, bool globalPackage = true, string whereToRemove = null)
            {
                UninstallResult retVal = UninstallResult.normal;
                log = new LogDataList();
                if (package.Collections != null)
                {
                    foreach (RMCollection collection in package.Collections)
                    {
                        RMGeneratorCollection genCollection = collection as RMGeneratorCollection;
                        if (genCollection != null)
                        {
                            if (globalPackage)
                            {
                                whereToRemove = PMFileSystem.MV_Installation_Directory;
                                retVal = Generator.UninstallGeneratorParts(whereToRemove, _namespace ,genCollection);
                                genCollection = null;
                            }
                            //ignore this collection if not a global package
                        }
                        else
                        {
                            if (globalPackage)
                                whereToRemove = PMFileSystem.MV_NewData_Dir;
                            List<RMPackFile> files = collection.RetrieveAllFiles();
                            if (files != null)
                            {
                                foreach (RMPackFile file in files)
                                {
                                    if (string.IsNullOrWhiteSpace(file.Path))
                                        continue;
                                    string concatPath = whereToRemove + "\\" + file.Path;
                                    if (File.Exists(concatPath))
                                    {
                                        Exception ex = null;
                                        if (Helper.DeleteFileSafely(concatPath, _namespace, out ex, new DeleteFileLogMessages(deleteFailed: UninstallerMessages.Warning.FileDeleteFailed, logGroup: log)) == DeleteFileResult.FileNotFound)
                                            continue;
                                    }
                                }
                            }
                        }
                    }
                }

                return retVal;

            }
        }
    }
}