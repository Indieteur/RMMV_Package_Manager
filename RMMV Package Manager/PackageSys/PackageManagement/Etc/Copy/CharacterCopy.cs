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
        static void CopyCharacterAssetsCollection(string toWhere, RMCharImageCollection imageCollection, string _namespace, string rootDir = null)
        {
            string imgDir = toWhere + "\\" + DirectoryNames.ProjectFiles.Image.ROOT;
            Helper.MakeDirIfNotExistCopy(imgDir, _namespace);
            if (imageCollection.Groups != null)
                foreach (RMCharImageGroup charGroup in imageCollection.Groups)
                    CopyCharacterAssetsGroup(toWhere, charGroup, _namespace, rootDir);
        }

        static void CopyCharacterAssetsGroup(string toWhere, RMCharImageGroup charGroup, string _namespace, string rootDir = null)
        {
            string pathToCharacterDir = toWhere + "\\";
            if (charGroup.Files == null)
                return;

            foreach (RMCharImageFile charFile in charGroup.Files)
            {
                if (string.IsNullOrWhiteSpace(rootDir) && charFile.NonRootedPath)
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.PackUtil.CHAR_IMG_PATH_REL, charFile.Path);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_ALREADY_RELATIVE + charFile.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                string imageTypeToDirName = charFile.ImageType.ToDirectoryName();
               
                if (string.IsNullOrWhiteSpace(imageTypeToDirName))
                {
                    try
                    {
                        throw new InvalidCharacterFileException(ExceptionMessages.RMPackage.CHAR_FILE_NO_TYPE);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.CHAR_FILE_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string dirPath = pathToCharacterDir + imageTypeToDirName;
                Helper.MakeDirIfNotExistCopy(dirPath, _namespace);

                if (string.IsNullOrWhiteSpace(charFile.Path))
                {
                    try
                    {
                        throw new InvalidCharacterFileException(ExceptionMessages.RMPackage.CHAR_FILE_PATH_NULL);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_PATH_NOT_SET, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                string copyTo = dirPath + "\\" + Path.GetFileName(charFile.Path);

                string originFile = charFile.Path;
                if (!string.IsNullOrWhiteSpace(rootDir))
                    originFile = rootDir + "\\" + charFile.Path;

                Exception outEx;
                if (Helper.CopyFileSafely(originFile, copyTo, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                    throw outEx;
            }
        }
    }
}
