using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Indieteur.BasicLoggingSystem;
using System.IO;
using System.Threading;

namespace RMMV_PackMan
{
    public static partial class PackageUtil
    {
        public static void ImplicitCopyAssetFilesTo(RMPackage filesPack, string destination, string _namespace, string rootDir = null)
        {
            if (filesPack == null || filesPack.Collections == null || filesPack.Collections.Count == 0)
                return;

            foreach (RMCollection collection in filesPack.Collections)
            {
                if (collection is RMAudioCollection)
                    CopyAudioAssets(destination, collection as RMAudioCollection, _namespace, rootDir);
                else if (collection is RMDataCollection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Data, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMAnimationCollection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Animation, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMBattleBacks1_Collection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.BattleBacks_1, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMBattleBacks2_Collection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.BattleBacks_2, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMParallaxCollection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Parallax, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMPictureCollection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Pictures, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMSysImageCollection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.System, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMTitles1_Collection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Titles_1, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMTitles2_Collection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Titles_2, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMPluginsCollection)
                    CopySingleFileCollection(destination, RMSingleFileCollection.CollectionType.Plugins, collection as RMSingleFileCollection, _namespace, rootDir);
                else if (collection is RMCharImageCollection)
                    CopyCharacterAssetsCollection(destination, collection as RMCharImageCollection, _namespace, rootDir);
                else if (collection is RMTilesetCollection)
                    CopyTilesetCollection(destination, collection as RMTilesetCollection, _namespace, rootDir);
                else if (collection is RMMovieCollection)
                    CopyMovieAssetsCollection(destination, collection as RMMovieCollection, _namespace, rootDir);
                else if (collection is RMGeneratorCollection)
                    CopyGeneratorParts(destination, collection as RMGeneratorCollection, _namespace, rootDir);
            }
        }

        public static void ExplicitCopyAssetsAndLicFileTo(RMPackage package, string rootDir, string destDir, string _namespace)
        {
            if (package == null || package.Collections == null)
                return;

            if (string.IsNullOrWhiteSpace(rootDir))
            {
                try
                {
                    throw new InvalidPathException(ExceptionMessages.General.ROOTDIR_ARG_NULL, string.Empty);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.ROOT_DIR_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }
            if (string.IsNullOrWhiteSpace(destDir))
            {
                try
                {
                    throw new InvalidPathException(ExceptionMessages.General.DESTDIR_ARG_NULL, string.Empty);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.DEST_DIR_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }
            Logger.WriteInformationLog(LoggerMessages.PackageUtil.Info.CopyAssetsInit(rootDir, destDir), _namespace);
            Exception outEx;
            List<RMPackFile> files = package.RetrieveAllFiles();
                if (files != null)
                {
                    foreach (RMPackFile file in files)
                    {
                        string sourceFile = file.Path;
                        if (file.NonRootedPath)
                            sourceFile = rootDir + "\\" + file.Path;
                        else
                        {
                            try
                            {
                                throw new InvalidPathException(ExceptionMessages.PackUtil.PACK_FILE_ROOTED, file.Path);
                            }
                            catch (Exception ex)
                            {
                                Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.ROOTED_PATH + file.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                throw;
                            }
                        }

                        string destFile = destDir + "\\" + file.Path;
                        string dirDestFinalPath = Path.GetDirectoryName(destFile);

                        if (!Directory.Exists(dirDestFinalPath) && Helper.CreateFolderSafely(dirDestFinalPath, _namespace, out outEx, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                            throw outEx;

                        if (Helper.CopyFileSafely(sourceFile, destFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                            throw outEx;
                    }
                }

                if (package.License != null && package.License.LicenseSource == RMPackLic.Source.File)
                {
                    if (string.IsNullOrWhiteSpace(package.License.Data))
                    {
                        try
                        {
                            throw new InvalidPathException(ExceptionMessages.RMPackage.LIC_FILE_PATH_NULL, string.Empty);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.LIC_FILE_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }

                    string sourceLicFile = rootDir + "\\" + package.License.Data;
                    string destLicFile = destDir + "\\" + package.License.Data;
                    if (!File.Exists(sourceLicFile))
                    {
                        try
                        {
                            throw new FileNotFoundException(ExceptionMessages.RMPackage.LicFileNotExist(sourceLicFile));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.LicFileNotExists(sourceLicFile), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }

                    string dirDestFinalPath = Path.GetDirectoryName(destLicFile);
                    if (!Directory.Exists(dirDestFinalPath) && Helper.CreateFolderSafely(dirDestFinalPath, _namespace, out outEx, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                        throw outEx;

                    if (Helper.CopyFileSafely(sourceLicFile, destLicFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyLicFailed)) != CopyFileResult.Success)
                        throw outEx;
                }
            Logger.WriteInformationLog(LoggerMessages.PackageUtil.Info.CopyAssetsDone(rootDir, destDir), _namespace);
        }

        public static void RemoveGeneratorFilesAndCollection(RMPackage package, string rootDir, string _namespace)
        {
            if (package == null || package.Collections == null)
                return;

            if (string.IsNullOrWhiteSpace(rootDir))
            {
                try
                {
                    throw new InvalidPathException(ExceptionMessages.General.ROOTDIR_ARG_NULL, string.Empty);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.ROOT_DIR_NULL_GEN_DEL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }
            Logger.WriteInformationLog(LoggerMessages.PackageUtil.Info.REMOVE_GEN_FILE_START + rootDir + ".", _namespace);
            List<RMGeneratorCollection> generatorCollectionToRemove = new List<RMGeneratorCollection>();
            foreach (RMCollection collection in package.Collections)
            {
                if (collection is RMGeneratorCollection)
                    generatorCollectionToRemove.Add(collection as RMGeneratorCollection);
            }
            if (generatorCollectionToRemove.Count == 0)
                return;

            List<RMPackFile> retrievedGenFiles = new List<RMPackFile>();
            foreach (RMGeneratorCollection genCollection in generatorCollectionToRemove)
            {
                List<RMPackFile> outGenFiles = genCollection.RetrieveAllFiles();
                if (outGenFiles != null)
                    retrievedGenFiles.AddRange(outGenFiles);
                package.Collections.Remove(genCollection);
            }

            if (retrievedGenFiles.Count == 0)
                return;
            Exception outEx;
                foreach (RMPackFile file in retrievedGenFiles)
                {
                    string fileToRemove = null;
                    if (file.NonRootedPath)
                        fileToRemove = rootDir + "\\" + file.Path;
                    else
                    {
                        try
                        {
                            throw new InvalidPathException(ExceptionMessages.PackUtil.PACK_FILE_ROOTED, file.Path);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.PackageUtil.Error.ROOTED_PATH_GEN_DEL + file.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }

                    if (Helper.DeleteFileSafely(fileToRemove, _namespace, out outEx, LoggerMessages.GeneralError.DELETE_UNUSED_FILE_FAILED_ARG) == DeleteFileResult.UserCancelled)
                        throw outEx;
                }
          
            Logger.WriteInformationLog(LoggerMessages.PackageUtil.Info.REMOVE_GEN_FILE_DONE + rootDir + ".", _namespace);
        }
    }
}
