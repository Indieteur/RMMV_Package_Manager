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
            public static class Characters
            {
                public static void InstallCharacters(string toWhere, string fromRootDirectory, RMCharImageCollection imageCollection, string _namespace)
                {
                    string imgDir = toWhere + "\\" + DirectoryNames.ProjectFiles.Image.ROOT;
                    Helper.MakeDirIfNotExistInstall(imgDir, _namespace);
                    if (imageCollection.Groups != null)
                        foreach (RMCharImageGroup character in imageCollection.Groups)
                            InstallCharacterFile(toWhere, fromRootDirectory, character, _namespace);

                }
                public static void InstallCharacterFile(string toWhere, string fromRootDirectory, RMCharImageGroup character, string _namespace)
                {
                    string pathToCharacterDir = toWhere + "\\";
                    if (character.Files != null)
                    {
                        foreach(RMCharImageFile imageFile in character.Files)
                        {
                            string imageTypeToDirName = imageFile.ImageType.ToDirectoryName();
                            if (string.IsNullOrWhiteSpace(imageTypeToDirName))
                            {
                                try
                                {
                                    throw new InvalidCharacterFileException(ExceptionMessages.RMPackage.CHAR_FILE_NO_TYPE);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_CHAR_FILE_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }
                            string dirPath = pathToCharacterDir + imageTypeToDirName;
                            Helper.MakeDirIfNotExistInstall(dirPath, _namespace);

                            if (string.IsNullOrWhiteSpace(imageFile.Path))
                            {
                                try
                                {
                                    throw new InvalidCharacterFileException(ExceptionMessages.RMPackage.CHAR_FILE_PATH_NULL);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_CHAR_FILE_PATH, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            string copyTo = dirPath + "\\" + Path.GetFileName(imageFile.Path);
                            string originalFile = fromRootDirectory + "\\" + imageFile.Path;

                            Exception outEx;
                            if (Helper.CopyFileSafely(originalFile, copyTo, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageManagement.Installer.Error.UnableInstallCharFileCopy)) != CopyFileResult.Success)
                                throw outEx;
                            imageFile.Path = Helper.GetRelativePath(copyTo, toWhere);
                            //imageFile.InstallationStatus = RMPackObject.InstallStatus.Installed;
                        }
                    }
                }
            }
        }
    }
}
