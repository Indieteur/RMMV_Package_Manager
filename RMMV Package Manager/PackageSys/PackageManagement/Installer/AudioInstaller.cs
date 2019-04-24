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
            static class Audio
            {
                public static void InstallAudioCollection (string toWhere, string fromRootDirectory, RMAudioCollection collection, string _namespace)
                {
                    string audioDirPath = null;
                    string audioCollectionType = null;
                    if (string.IsNullOrWhiteSpace(audioCollectionType = collection.CollectionType.ToDirectoryName()))
                    {
                        try
                        {
                            throw new InvalidAudioCollectionTypeException(ExceptionMessages.RMPackage.AUDIO_COLL_NO_TYPE);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_AUDIO_NO_DIR_NAME, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }
                    audioDirPath = toWhere + "\\" + DirectoryNames.Audio.ROOT + "\\" + audioCollectionType;
                   

                    Helper.MakeDirIfNotExistInstall(audioDirPath, _namespace);


                    if (collection.Groups != null)
                        foreach (RMAudioGroup audio in collection.Groups)
                            InstallAudio(audioDirPath, fromRootDirectory, toWhere, audio, _namespace);
                    
                }
                public static void InstallAudio(string toWhere, string fromRootDirectory, string rootInstallPath, RMAudioGroup audio, string _namespace)
                {

                    if (audio.Files != null)
                    {
                        foreach (RMAudioFile audioFile in audio.Files)
                        {
                            //if (audioFile.TypeOfFile == RMAudioFile.FileType.mp3 || audioFile.TypeOfFile == RMAudioFile.FileType.wav)
                            //ConvertFileFirst();

                            if (string.IsNullOrWhiteSpace(audioFile.Path))
                            {
                                try
                                {
                                    throw new InvalidAudioFileException(ExceptionMessages.RMPackage.AUDIO_FILE_PATH_NULL, InvalidAudioFileException.WhichInvalid.InvalidPath, audio);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_AUDIO_FILE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            string audioFileType = audioFile.TypeOfFile.ToExtensionString();
                            string newFileName = null;
                            if (string.IsNullOrWhiteSpace(audioFileType))
                            {
                                try
                                {
                                    throw new InvalidAudioFileException(ExceptionMessages.RMPackage.AUDIO_INVALID_FILE_TYPE, InvalidAudioFileException.WhichInvalid.NoType, audio);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_INSTALL_AUDIO_FILE_TYPE_INVALID, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            newFileName = Path.GetFileNameWithoutExtension(audioFile.Path) + "." + audioFileType;

                            string oldFile = fromRootDirectory + "\\" + audioFile.Path;
                            string newFilePath = toWhere + "\\" + newFileName;

                            Exception outEx;
                            if (Helper.CopyFileSafely(oldFile, newFilePath, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageManagement.Installer.Error.UnableCopyAudioFile)) != CopyFileResult.Success)
                                throw outEx;

                            audioFile.Path = Helper.GetRelativePath(newFilePath, rootInstallPath);
                           // audioFile.InstallationStatus = RMPackObject.InstallStatus.Installed;
                        }
                    }
                }


            }
        }
    }
}
