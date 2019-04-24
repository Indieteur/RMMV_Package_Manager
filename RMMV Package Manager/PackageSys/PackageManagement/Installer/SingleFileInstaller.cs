using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static partial class Installer
        {
            static class SingleFile
            {
                public static void InstallSingleFileCollection (string toWhere, string fromRootDirectory, RMSingleFileCollection.CollectionType collectionType, RMSingleFileCollection collection, string _namespace)
                {
                    string collType = collectionType.ToDirectoryName();

                    if (string.IsNullOrWhiteSpace(collType))
                    {
                        try
                        {
                            throw new InvalidSingleFileCollectionException(ExceptionMessages.RMPackage.COLL_NO_TYPE, InvalidSingleFileCollectionException.WhichInvalid.NoType, collection.Parent);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_SF_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }
                    string dirPath = toWhere + "\\" + collType;
                    Helper.MakeDirIfNotExistInstall(dirPath, _namespace);

                    if (collection.Files != null)
                        foreach (RMSingleFile singleFile in collection.Files)
                        {
                            if (string.IsNullOrWhiteSpace(singleFile.Path))
                            {
                                try
                                {
                                    throw new InvalidSingleFileException(ExceptionMessages.RMPackage.FILE_PATH_NULL);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_SF_NO_PATH, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            string fileName = Path.GetFileName(singleFile.Path);
                            string newFile = dirPath + "\\" + fileName;
                            string oldFile = fromRootDirectory + "\\" + singleFile.Path;

                            Exception outEx;
                            if (Helper.CopyFileSafely(oldFile, newFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageManagement.Installer.Error.UnableInstallSFCopyFailed)) != CopyFileResult.Success)
                                throw outEx;

                            //singleFile.InstallationStatus = RMPackObject.InstallStatus.Installed;
                            singleFile.Path = Helper.GetRelativePath(newFile, toWhere);
                        }
                    
                }


            }
        }
    }
}
