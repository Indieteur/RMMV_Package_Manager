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
            public static class Tilesets
            {
                public static void InstallTileset(string toWhere, string fromDirectory, RMTilesetCollection collection, string _namespace)
                {
                    string dir = toWhere + "\\" + DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.TILESETS;
                    Helper.MakeDirIfNotExistInstall(dir, _namespace);
                    if (collection.Groups != null)
                        foreach (RMTilesetGroup tileset in collection.Groups)
                            InstallTilesetFile(dir, toWhere, fromDirectory, tileset, _namespace);
                            
                }
                static void InstallTilesetFile(string toWhere, string rootDir, string fromDirectory, RMTilesetGroup tileset, string _namespace)
                {
                    if (tileset.Files != null)
                        foreach (RMTilesetFile file in tileset.Files)
                        {
                            string atlasType = file.AtlasType.ToXMLString();
                            string fileType = file.FileType.ToXMLString();

                            if (string.IsNullOrWhiteSpace(atlasType))
                                atlasType = "unspecified";

                            if (string.IsNullOrWhiteSpace(fileType))
                            {
                                try
                                {
                                    throw new InvalidTilesetException(ExceptionMessages.RMPackage.TILESET_FILE_TYPE_NOT_SET, InvalidTilesetException.WhichInvalid.FileType, tileset.Parent);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.INVALID_TILESET_FILE_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            string newFileName = tileset.Name + "_" + atlasType + "." + fileType;
                            string newFile = toWhere + "\\" + newFileName;

                            if (string.IsNullOrWhiteSpace(file.Path))
                            {
                                try
                                {
                                    throw new InvalidTilesetFileException(ExceptionMessages.RMPackage.TILESET_FILE_PATH_NULL, InvalidTilesetFileException.WhichInvalid.NoPath, tileset);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.INVALID_TILESET_PATH_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            string oldFilePath = fromDirectory + "\\" + file.Path;
                            Exception outEx;
                            if (Helper.CopyFileSafely(oldFilePath, newFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageManagement.Installer.Error.TilesetUnableToCopy)) != CopyFileResult.Success)
                                throw outEx;

                            file.Path = Helper.GetRelativePath(newFile, rootDir);
                            //file.InstallationStatus = RMPackObject.InstallStatus.Installed;
                        }
                }
            }

        }
    }
}
