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
            public static class Movies
            {
                public static void InstallMovie(string toWhere, string fromDirectory, RMMovieCollection collection, string _namespace)
                {
                    string dir = toWhere + "\\" + DirectoryNames.ProjectFiles.MOVIES;
                    Helper.MakeDirIfNotExistInstall(dir, _namespace);
                    if (collection.Groups != null)
                        foreach (RMMovieGroup movie in collection.Groups)
                            InstallMovieFile(dir, toWhere, fromDirectory, movie, _namespace);
                            
                }
                static void InstallMovieFile(string toWhere, string rootDir, string fromDirectory, RMMovieGroup movie, string _namespace)
                {
                    if (movie.Files != null)
                        foreach (RMMovieFile file in movie.Files)
                        {
                            string typeOfFile;
                            if (string.IsNullOrWhiteSpace(typeOfFile = file.TypeOfFile.ToXMLString()))
                            {
                                try
                                {
                                    throw new InvalidMovieFileException(ExceptionMessages.RMPackage.MOVIE_TYPE_NOT_SET, InvalidMovieFileException.WhichInvalid.NoType, movie);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.INVALID_MOVIE_FILE_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }
                            string newFileName = Path.GetFileNameWithoutExtension(file.Path) + "." + typeOfFile;
                            string newFile = toWhere + "\\" + newFileName;

                            if (string.IsNullOrWhiteSpace(file.Path))
                            {
                                try
                                {
                                    throw new InvalidMovieFileException(ExceptionMessages.RMPackage.MOVIE_FILE_PATH_NULL , InvalidMovieFileException.WhichInvalid.NoPath, movie);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.INVALID_MOVIE_PATH_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }

                            string oldFilePath = fromDirectory + "\\" + file.Path;
                            Exception outEx;
                            if (Helper.CopyFileSafely(oldFilePath, newFile, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageManagement.Installer.Error.MovieUnableCopy)) != CopyFileResult.Success)
                                throw outEx;

                            file.Path = Helper.GetRelativePath(newFile, rootDir);
                            //file.InstallationStatus = RMPackObject.InstallStatus.Installed;
                        }
                }
            }

        }
    }
}
