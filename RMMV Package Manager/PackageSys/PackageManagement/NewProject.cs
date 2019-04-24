using Indieteur.BasicLoggingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NewProjectMessages = RMMV_PackMan.LoggerMessages.PackageManagement.NewProject;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static class NewProject
        {
            public static void CopyPackageInstallInfo(string dirPath, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                log.WriteInformationLog(NewProjectMessages.Info.CopyInstallInfoInit(dirPath), _namespace);
                Exception exOut;
                if (!Directory.Exists(dirPath))
                    return;
                string packageXMLFile = dirPath + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME;

                if (!File.Exists(packageXMLFile))
                {
                    try
                    {
                        throw new FileNotFoundException(packageXMLFile + ExceptionMessages.General.COULD_NOT_BE_FOUND);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(NewProjectMessages.Error.XML_FILE_NOT_FOUND + packageXMLFile + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                LogDataList outLog = null;
                RMPackage package = null;
                try
                {
                    package = new RMPackage(packageXMLFile, _namespace, out outLog);
                }
                catch (Exception ex)
                {
                    log.AppendLogs(outLog);
                    throw new InvalidInstalledPackageFile(ExceptionMessages.PackageManagement.InstalledPackage.INVALID_XML + packageXMLFile + ".", packageXMLFile, ex);
                }
                log.AppendLogs(outLog);


                if (package.ContentsSummary == RMCollectionType.Generator)//If package only contains generator parts then we don't copy it.
                    return;

                if (package.ContentsSummary.HasFlag(RMCollectionType.Generator))
                {
                    PackageUtil.RemoveGeneratorFilesAndCollection(package, dirPath, _namespace);

                    package.SaveToFile(packageXMLFile, _namespace, logMessage: new WriteAllTextLogMessages(writeFailed: LoggerMessages.PackageManagement.NewProject.Error.XMLFileSaveFailed));
                    
                    try
                    {
                        Helper.DeleteEmptySubDir(dirPath, _namespace);
                    }
                    catch (Exception ex)
                    {
                        log.WriteWarningLog(NewProjectMessages.Warning.DELETE_SUB_DIR + PMFileSystem.PackMan_TempMakeDir + ".", _namespace, ex);
                    }
                }

                string newDataNewDir = PMFileSystem.MV_NewDataMan_Dir + "\\" + package.UniqueIDInMD5;

                if (Directory.Exists(newDataNewDir) && Helper.DeleteFolderSafely(newDataNewDir, _namespace, out exOut, LoggerMessages.GeneralError.REQUIRED_DIR_EXISTS_DEL_FAILED_ARG) == DeleteFolderResult.UserCancelled)
                    throw exOut;

                if (Helper.CreateFolderSafely(newDataNewDir, _namespace, out exOut, new CreateFolderLogMessages(createFailed: NewProjectMessages.Error.ErrorMakeDir)) == CreateFolderResult.UserCancelled)
                    throw exOut;

                string destArch = newDataNewDir + "\\" + Vars.INSTALLED_ARCH_FILENAME;
                ArchiveManagement.CreateNewZip(dirPath, destArch, _namespace);

                if (package.License.LicenseSource == RMPackLic.Source.File)
                {
                    if (RMPackLic.IsAValidLicenseSourceFile(package.License.Data, package.XMLDirectory))
                    {
                        string licSourcePath = package.XMLDirectory + "\\" + package.License.Data;
                        string fileName = null;
                        try
                        {
                            fileName = Path.GetFileName(licSourcePath);
                        }
                        catch (Exception ex)
                        {
                            package.License = null;
                            log.WriteWarningLog(NewProjectMessages.Warning.LIC_COPY_FAILED_GEN, _namespace, ex);
                            goto skipLic;
                        }
                        string destPath = newDataNewDir + "\\" + fileName;

                        if (Helper.CopyFileSafely(licSourcePath, destPath, true, _namespace, out exOut, new CopyFileLogMessages(copyFileFailed: NewProjectMessages.Warning.LicCopyFailed, logGroup: log)) != CopyFileResult.Success)
                        {
                            package.License = null;
                            goto skipLic;
                        }
                        package.License.Data = fileName;
                    }
                    else
                    {
                        package.License = null;
                        log.WriteWarningLog(NewProjectMessages.Warning.LIC_COPY_FAILED_GEN, _namespace);
                    }
                }


                skipLic:
                string newPackageXMLFile = newDataNewDir + "\\" + Vars.INSTALLED_XML_FILENAME;
                //package.Installed = true;
                //package.SetInstalledPropertyAll(RMPackObject.InstallStatus.Installed);
                package.SaveToFile(newPackageXMLFile, _namespace, RMPackage.SaveToFileMode.ExplicitAssetInfo, 
                    logMessage: new WriteAllTextLogMessages(writeFailed: LoggerMessages.PackageManagement.NewProject.Error.XMLFileSaveFailed));

                log.WriteInformationLog(NewProjectMessages.Info.CopyInstallInfoDone(dirPath), _namespace);
            }
            
            public static void DeletePackageInstallInfo (string MD5UID, string _namespace, bool throwExceptionOnErr = false)
            {
                Exception ex;
                string dirToDelete = PMFileSystem.MV_NewDataMan_Dir + "\\" + MD5UID;
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.NewProject.Info.DelInstallInfoInit(dirToDelete), _namespace);
                if (Helper.DeleteFolderSafely(dirToDelete, _namespace, out ex, LoggerMessages.GeneralError.UNABLE_DELETE_UNUSED_DIR_ARG) == DeleteFolderResult.UserCancelled && throwExceptionOnErr)
                        throw ex;
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.NewProject.Info.DelInstallInfoDone(dirToDelete), _namespace);
            }
        }
    }
}
