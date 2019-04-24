using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Indieteur.BasicLoggingSystem;
using System.IO;

namespace RMMV_PackMan
{
    public static partial class PackageUtil
    {
        static void CopySingleFileCollection(string toWhere, RMSingleFileCollection.CollectionType collectionType, RMSingleFileCollection collection, string _namespace, string rootDir = null)
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
                    Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.COLLECTION_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }

            string dirPath = toWhere + "\\" + collType;

            Helper.MakeDirIfNotExistCopy(dirPath, _namespace);

            if (collection.Files == null)
                return;

            foreach (RMSingleFile singleFile in collection.Files)
            {
                if (string.IsNullOrWhiteSpace(rootDir) && singleFile.NonRootedPath)
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.PackUtil.FILE_PATH_REL, singleFile.Path);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_ALREADY_RELATIVE + singleFile.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                if (string.IsNullOrWhiteSpace(singleFile.Path))
                {
                    try
                    {
                        throw new InvalidSingleFileException(ExceptionMessages.RMPackage.FILE_PATH_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_PATH_NOT_SET, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                string fileName = Path.GetFileName(singleFile.Path);
                string newFile = dirPath + "\\" + fileName;

                string originFile = singleFile.Path;
                if (!string.IsNullOrWhiteSpace(rootDir))
                    originFile = rootDir + "\\" + singleFile.Path;


                Exception outEx;
                if (Helper.CopyFileSafely(originFile, newFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                    throw outEx;
            }
        }
    }
}
