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
        static void CopyTilesetCollection(string toWhere, RMTilesetCollection collection, string _namespace, string rootDir = null)
        {
            string dir = toWhere + "\\" + DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.TILESETS;
            Helper.MakeDirIfNotExistCopy(dir, _namespace);
            if (collection.Groups != null)
                foreach (RMTilesetGroup tilesetGroup in collection.Groups)
                    CopyTilesetAssetsGroup(dir, tilesetGroup, _namespace, rootDir);
        }

        static void CopyTilesetAssetsGroup(string toWhere, RMTilesetGroup tilesetGroup, string _namespace, string rootDir = null)
        {
            if (tilesetGroup.Files == null)
                return;

            foreach (RMTilesetFile tilesetFile in tilesetGroup.Files)
            {
                if (string.IsNullOrWhiteSpace(rootDir) && tilesetFile.NonRootedPath)
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.PackUtil.TILESET_FILE_PATH_REL, tilesetFile.Path);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_ALREADY_RELATIVE + tilesetFile.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string atlasType = tilesetFile.AtlasType.ToXMLString();
                string fileType = tilesetFile.FileType.ToXMLString();

                if (string.IsNullOrWhiteSpace(atlasType))
                    atlasType = "unspecified";

                if (string.IsNullOrWhiteSpace(fileType))
                {
                    try
                    {
                        throw new InvalidTilesetException(ExceptionMessages.RMPackage.TILESET_FILE_TYPE_NOT_SET, InvalidTilesetException.WhichInvalid.FileType, tilesetGroup.Parent);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.ATLAS_FILE_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                string newFileName = tilesetGroup.Name + "_" + atlasType + "." + fileType;
                string newFile = toWhere + "\\" + newFileName;

                if (string.IsNullOrWhiteSpace(tilesetFile.Path))
                {
                    try
                    {
                        throw new InvalidTilesetFileException(ExceptionMessages.RMPackage.TILESET_FILE_PATH_NULL, InvalidTilesetFileException.WhichInvalid.NoPath, tilesetGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_PATH_NOT_SET, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                Exception outEx;

                string originFile = tilesetFile.Path;
                if (!string.IsNullOrWhiteSpace(rootDir))
                    originFile = rootDir + "\\" + tilesetFile.Path;

                if (Helper.CopyFileSafely(originFile, newFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                    throw outEx;
            }
        }
    }
}
