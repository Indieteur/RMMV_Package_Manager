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
        static void CopyAudioAssets(string toWhere, RMAudioCollection collection, string _namespace, string rootDir = null)
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
                    Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.AUDIO_COLLECTION_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }
            audioDirPath = toWhere + "\\" + DirectoryNames.Audio.ROOT + "\\" + audioCollectionType;
            Helper.MakeDirIfNotExistCopy(audioDirPath, _namespace);
            if (collection.Groups != null)
                foreach (RMAudioGroup audio in collection.Groups)
                    CopyAudioAsset(audioDirPath, audio, _namespace, rootDir);

        }

        static void CopyAudioAsset(string toWhere, RMAudioGroup audioGroup, string _namespace, string rootDir = null)
        {
            if (audioGroup.Files == null)
                return;
            foreach (RMAudioFile audioFile in audioGroup.Files)
            {
                if (string.IsNullOrWhiteSpace(rootDir) && audioFile.NonRootedPath)
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.PackUtil.AUDIO_PATH_REL, audioFile.Path);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_ALREADY_RELATIVE + audioFile.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                if (string.IsNullOrWhiteSpace(audioFile.Path))
                {
                    try
                    {
                        throw new InvalidAudioFileException(ExceptionMessages.RMPackage.AUDIO_FILE_PATH_NULL, InvalidAudioFileException.WhichInvalid.InvalidPath, audioGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_PATH_NOT_SET, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string audioFileType = audioFile.TypeOfFile.ToExtensionString();
                string newFileName = null;
                if (string.IsNullOrWhiteSpace(audioFileType))
                {
                    try
                    {
                        throw new InvalidAudioFileException(ExceptionMessages.RMPackage.AUDIO_INVALID_FILE_TYPE, InvalidAudioFileException.WhichInvalid.NoType, audioGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.AUDIO_FILE_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                newFileName = Path.GetFileNameWithoutExtension(audioFile.Path) + "." + audioFileType;
                string newFilePath = toWhere + "\\" + newFileName;

                string originFile = audioFile.Path;
                if (!string.IsNullOrWhiteSpace(rootDir))
                    originFile = rootDir + "\\" + audioFile.Path;

                Exception outEx;
                if (Helper.CopyFileSafely(originFile, newFilePath, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                    throw outEx;
            }
        }
    }
}
