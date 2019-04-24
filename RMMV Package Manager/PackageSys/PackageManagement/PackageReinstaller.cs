using Indieteur.BasicLoggingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static class Reinstaller
        {
            public static event PackageReinstallDelegate OnReinstallBegin;
            public static event PackageReinstallDelegate OnReinstallDone;

            public enum ReinstallResult
            {
                InstallPackageNotFoundOrNull,
                InstallFilesMissing,
                ProjectNotFoundOrNull,
                Successful
            }

            public static void ReinstallLocalPackage(string UID, string projectPath, string _namespace, out LogDataList log)
            {
                if (File.Exists(projectPath))
                    projectPath = Path.GetDirectoryName(projectPath);

                if (!Directory.Exists(projectPath))
                {
                    try
                    {
                        throw new DirectoryNotFoundException(projectPath + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.OpenProjNullOrMiss(projectPath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                log = new LogDataList();

                ProjectPackMan openedProject = null;

                LogDataList outLog = null;
                try
                {
                    openedProject = new ProjectPackMan(projectPath, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.LOAD_FAILED_PROJ + projectPath + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                ReinstallLocalPackage(UID, openedProject, _namespace, out outLog);
                log.AppendLogs(outLog);

            }

            public static void ReinstallLocalPackage(string UID, ProjectPackMan openProject, string _namespace, out LogDataList log)
            {
                if (openProject == null || openProject.InstalledPackages == null)
                {
                    try
                    {
                        throw new InstalledPackageNotFoundException(ExceptionMessages.General.PackWIDNotFound(UID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.PackageMissng(UID), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                InstalledPackage foundInstalledPackage = openProject.InstalledPackages.FindByUID(UID);
                if (foundInstalledPackage == null)
                {
                    try
                    {
                        throw new InstalledPackageNotFoundException(ExceptionMessages.General.PackWIDNotFound(UID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.PackageMissng(UID), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                ReinstallLocalPackage(foundInstalledPackage, openProject, _namespace, out log);
            }

            public static void ReinstallLocalPackage(InstalledPackage installedPackage, ProjectPackMan openProject, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Reinstaller.Information.REINSTALL_PACKAGE_START_L, _namespace);
                Exception outEx;
                if (openProject == null || !Directory.Exists(openProject.DirectoryPath))
                {
                    try
                    {
                        throw new DirectoryNotFoundException(ExceptionMessages.General.OPEN_PROJ_ARG_OR_DIRPATH_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.OpenProjNullOrMiss(openProject.DirectoryPath), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (installedPackage == null || string.IsNullOrWhiteSpace(installedPackage.ArchivePath) || !File.Exists(installedPackage.ArchivePath))
                {
                    try
                    {
                        throw new FileNotFoundException(ExceptionMessages.PackageManagement.Reinstaller.ARCH_INSTALL_FILE_NOT_FOUND);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.ARCH_NOT_FOUND, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled)
                    throw outEx;

                if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
                    throw outEx;

                if (OnReinstallBegin != null)
                    OnReinstallBegin.Invoke(installedPackage, false, openProject);


                try
                {
                    ArchiveManagement.ExtractZip(installedPackage.ArchivePath, PMFileSystem.PackMan_TempInstall, _namespace);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.UNABLE_EXTRACT_ARCH + PMFileSystem.PackMan_TempInstall + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                LogDataList outLog = null;

                try
                {
                    Uninstaller.UninstallLocalPackage(openProject, installedPackage.Namespace, _namespace, out outLog, procEvents: false);
                }
                catch  (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.UNABLE_UNINSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage retVal = null;
                try
                {
                    retVal = Installer.InstallLocalPackage(PMFileSystem.PackMan_TempInstall + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME, openProject, _namespace, out outLog, true, true);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.UNABLE_INSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                if (OnReinstallDone != null)
                    OnReinstallDone.Invoke(retVal, false, openProject);

                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Reinstaller.Information.REINSTALL_PACKAGE_DONE_L, _namespace);
            }

            public static void ReinstallGlobalPackage (string UID, string _namespace, out LogDataList log)
            {
                if (GlobalPackages == null)
                {
                    try
                    {
                        throw new InstalledPackageNotFoundException(ExceptionMessages.General.PackWIDNotFound(UID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.PackageMissng(UID), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                InstalledPackage foundInstalledPackage = GlobalPackages.FindByUID(UID);
                if (foundInstalledPackage == null)
                {
                    try
                    {
                        throw new InstalledPackageNotFoundException(ExceptionMessages.General.PackWIDNotFound(UID));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.PackageMissng(UID), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                ReinstallGlobalPackage(foundInstalledPackage, _namespace, out log);
            }

            public static void ReinstallGlobalPackage(InstalledPackage installedPackage, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Reinstaller.Information.REINSTALL_PACKAGE_START_G, _namespace);
                Exception outEx;
                if (installedPackage == null || string.IsNullOrWhiteSpace(installedPackage.ArchivePath) || !File.Exists(installedPackage.ArchivePath))
                {
                    try
                    {
                        throw new FileNotFoundException(ExceptionMessages.PackageManagement.Reinstaller.ARCH_INSTALL_FILE_NOT_FOUND);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.ARCH_NOT_FOUND, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled)
                    throw outEx;

                if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempInstall, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
                    throw outEx;

                if (OnReinstallBegin != null)
                    OnReinstallBegin.Invoke(installedPackage, true, null);

                try
                {
                    ArchiveManagement.ExtractZip(installedPackage.ArchivePath, PMFileSystem.PackMan_TempInstall, _namespace);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.UNABLE_EXTRACT_ARCH + PMFileSystem.PackMan_TempInstall + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                LogDataList outLog = null;
                try
                {
                    Uninstaller.UninstallGlobalPackage(installedPackage.Namespace, _namespace, out outLog, renumberParts: false, procEvents: false);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.UNABLE_UNINSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                InstalledPackage retVal = null;
                try
                {
                    retVal = Installer.InstallGlobalPackage(PMFileSystem.PackMan_TempInstall + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME, _namespace, out outLog, true, true, false);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Reinstaller.Error.UNABLE_INSTALL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
                log.AppendLogs(outLog);

                if (OnReinstallDone != null)
                    OnReinstallDone.Invoke(retVal, true, null);
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.Reinstaller.Information.REINSTALL_PACKAGE_DONE_G, _namespace);

            }
        }
    }
}