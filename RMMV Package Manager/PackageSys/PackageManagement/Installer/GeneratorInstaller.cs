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
            static class Generator
            {
                public static void InstallGeneratorParts(string toWhere, string fromRootDir, RMGeneratorCollection collection, string _namespace)
                {
                    try
                    {
                        PerformDirectoryExistenceCheck(toWhere, _namespace);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.UNABLE_MAKE_GEN_DIR_TOP, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }

                    if (collection.Parts != null)
                    {
                        foreach (RMGenPart part in collection.Parts)
                             InstallGeneratorPartOnGender(toWhere + "\\" + DirectoryNames.Generator.ROOT, fromRootDir, toWhere, part, _namespace);
                    }
                    
                }

                public static void InstallGeneratorPartOnGender(string toWhere, string fromRootDir, string rootInstallPath, RMGenPart part, string _namespace)
                {
                    if (part.Files != null)
                    {
                        int lowestPossiblePos = GeneratorPartsManager.Precision.GetPositionFloorOfPart(part.Gender, part.PartType);
                        int positionOfPart = GeneratorPartsManager.Precision.GetNextVacantPosition(part.Gender, part.PartType, lowestPossiblePos - 1);
                        foreach (RMGenFile file in part.Files)
                        {
                            string dirContainerFile = file.FileType.GetContainingDirectoryName();
                            string dirContainerGender = part.Gender.GetContainingDirectoryName();
                            if (string.IsNullOrWhiteSpace(dirContainerFile))
                            {
                                try
                                {
                                    throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_TYPE, InvalidGeneratorPartFileException.WhichInvalid.NoType, part);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.GEN_INVALID_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }
                            if (string.IsNullOrWhiteSpace(dirContainerGender))
                            {
                                try
                                {
                                    throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_GENDER, InvalidGeneratorPartFileException.WhichInvalid.NoGender, part);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.GEN_INVALID_GENDER, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                                    throw;
                                }
                            }
                            string subPath = toWhere + "\\" + dirContainerFile + "\\" + dirContainerGender;
                            InstallGeneratorFile(subPath, fromRootDir, rootInstallPath, file, positionOfPart, _namespace);
                           
                        }
                     
                    }
                }

                public static void InstallGeneratorFile(string toWhere, string fromRootDir, string rootInstallPath, RMGenFile fileToInstall, int position, string _namespace)
                {
                    
                    string newFileName = null;
                    try
                    {
                        newFileName = fileToInstall.RetrieveInstallFileName(position);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.INVALID_GEN_FILE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }

                    string copyToPath = toWhere + "\\" + newFileName;
                    if (string.IsNullOrWhiteSpace(fileToInstall.Path))
                    {
                        try
                        {
                            throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_PATH_NULL, InvalidGeneratorPartFileException.WhichInvalid.PathNotSet, fileToInstall.Parent);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.PackageManagement.Installer.Error.NO_FILE_PATH_GEN_FILE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }

                    Exception outEx;
                    string sourceFile = fromRootDir + "\\" + fileToInstall.Path;
                    if (Helper.CopyFileSafely(sourceFile, copyToPath, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageManagement.Installer.Error.GeneratorFileFailedCopy)) != CopyFileResult.Success)
                        throw outEx;
                   // fileToInstall.InstallationStatus = RMPackObject.InstallStatus.Installed;
                    fileToInstall.Path = Helper.GetRelativePath(copyToPath, rootInstallPath);
                }

              

                static void PerformDirectoryExistenceCheck(string rootPath, string _namespace)
                {
                    string genDir = rootPath + "\\" + DirectoryNames.Generator.ROOT;
                    Helper.MakeDirIfNotExistInstall(genDir, _namespace);

                    string subDir = genDir + "\\" + DirectoryNames.Generator.FACE;
                    Helper.MakeDirIfNotExistInstall(subDir, _namespace);
                    PerformGenderDirExistenceCheck(subDir, _namespace);

                    subDir = genDir + "\\" + DirectoryNames.Generator.SV;
                    Helper.MakeDirIfNotExistInstall(subDir, _namespace);
                    PerformGenderDirExistenceCheck(subDir, _namespace);

                    subDir = genDir + "\\" + DirectoryNames.Generator.TV;
                    Helper.MakeDirIfNotExistInstall(subDir, _namespace);
                    PerformGenderDirExistenceCheck(subDir, _namespace);

                    subDir = genDir + "\\" + DirectoryNames.Generator.TVD;
                    Helper.MakeDirIfNotExistInstall(subDir, _namespace);
                    PerformGenderDirExistenceCheck(subDir, _namespace);

                    subDir = genDir + "\\" + DirectoryNames.Generator.VARIATION;
                    Helper.MakeDirIfNotExistInstall(subDir, _namespace);
                    PerformGenderDirExistenceCheck(subDir, _namespace);

                }

                static void PerformGenderDirExistenceCheck(string rootPath, string _namespace)
                {
                    string dir = rootPath + "\\" + DirectoryNames.Generator.MALE;
                    Helper.MakeDirIfNotExistInstall(dir, _namespace);

                    dir = rootPath + "\\" + DirectoryNames.Generator.FEMALE;
                    Helper.MakeDirIfNotExistInstall(dir, _namespace);

                    dir = rootPath + "\\" + DirectoryNames.Generator.KID;
                    Helper.MakeDirIfNotExistInstall(dir, _namespace);

                }

               
            }
            
        }
    }
}
