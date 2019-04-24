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
        static void CopyMovieAssetsCollection(string toWhere, RMMovieCollection collection, string _namespace, string rootDir = null)
        {
            string dir = toWhere + "\\" + DirectoryNames.ProjectFiles.MOVIES;
            Helper.MakeDirIfNotExistInstall(dir, _namespace);
            if (collection.Groups != null)
                foreach (RMMovieGroup movieGroup in collection.Groups)
                    CopyMovieAsset(dir, movieGroup, _namespace, rootDir);
        }
        static void CopyMovieAsset(string toWhere, RMMovieGroup movieGroup, string _namespace, string rootDir = null)
        {
            if (movieGroup.Files == null)
                return;

            foreach (RMMovieFile movieFile in movieGroup.Files)
            {
                if (string.IsNullOrWhiteSpace(rootDir) && movieFile.NonRootedPath)
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.PackUtil.MOVIE_FILE_PATH_REL, movieFile.Path);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_ALREADY_RELATIVE + movieFile.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string typeOfFile;
                if (string.IsNullOrWhiteSpace(typeOfFile = movieFile.TypeOfFile.ToXMLString()))
                {
                    try
                    {
                        throw new InvalidMovieFileException(ExceptionMessages.RMPackage.MOVIE_TYPE_NOT_SET, InvalidMovieFileException.WhichInvalid.NoType, movieGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.MOVIE_FILE_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string newFileName = Path.GetFileNameWithoutExtension(movieFile.Path) + "." + typeOfFile;
                string newFile = toWhere + "\\" + newFileName;
                if (string.IsNullOrWhiteSpace(movieFile.Path))
                {
                    try
                    {
                        throw new InvalidMovieFileException(ExceptionMessages.RMPackage.MOVIE_FILE_PATH_NULL, InvalidMovieFileException.WhichInvalid.NoPath, movieGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_PATH_NOT_SET, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }

                Exception outEx;

                string originFile = movieFile.Path;
                if (!string.IsNullOrWhiteSpace(rootDir))
                    originFile = rootDir + "\\" + movieFile.Path;

                if (Helper.CopyFileSafely(originFile, newFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                    throw outEx;
            }

        }
    }
}
