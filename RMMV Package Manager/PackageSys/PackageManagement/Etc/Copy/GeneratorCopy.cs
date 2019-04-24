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
        static void CopyGeneratorParts(string toWhere, RMGeneratorCollection collection, string _namespace, string rootDir = null)
        {
            try
            {
                PerformDirectoryExistenceCheck(toWhere, _namespace);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.DIR_EXISTENCE_GEN_FAILED, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                throw;
            }
            if (collection.Parts != null)
            {
                foreach (RMGenPart part in collection.Parts)
                    CopyGeneratorPart(toWhere + "\\" + DirectoryNames.Generator.ROOT, part, _namespace, rootDir);
            }
        }

        static void CopyGeneratorPart(string toWhere, RMGenPart genGroup, string _namespace, string rootDir = null)
        {
            if (genGroup == null)
                return;

            foreach (RMGenFile genFile in genGroup.Files)
            {
                if (string.IsNullOrWhiteSpace(rootDir) && genFile.NonRootedPath)
                {
                    try
                    {
                        throw new InvalidPathException(ExceptionMessages.PackUtil.GEN_FILE_PATH_REL, genFile.Path);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_ALREADY_RELATIVE + genFile.Path + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string dirContainerFile = genFile.FileType.GetContainingDirectoryName();
                string dirContainerGender = genGroup.Gender.GetContainingDirectoryName();
                if (string.IsNullOrWhiteSpace(dirContainerFile))
                {
                    try
                    {
                        throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_TYPE, InvalidGeneratorPartFileException.WhichInvalid.NoType, genGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.GEN_FILE_NO_TYPE, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                if (string.IsNullOrWhiteSpace(dirContainerGender))
                {
                    try
                    {
                        throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_GENDER, InvalidGeneratorPartFileException.WhichInvalid.NoGender, genGroup);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.GEN_FILE_NO_GENDER, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        throw;
                    }
                }
                string subPath = toWhere + "\\" + dirContainerFile + "\\" + dirContainerGender;
                CopyGeneratorFile(subPath, genFile, _namespace, rootDir);
            }
        }

        static void CopyGeneratorFile(string toWhere, RMGenFile genFile, string _namespace, string rootDir = null)
        {
            int tempPos = genFile.Parent.Parent.Parts.IndexOf(genFile.Parent) + 1;
            string newFileName = null;
            try
            {
                newFileName = genFile.RetrieveInstallFileName(tempPos);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.GEN_FILE_ERR_FILE_NAME, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                throw;
            }
            string copyToPath = toWhere + "\\" + newFileName;
            if (string.IsNullOrWhiteSpace(genFile.Path))
            {
                try
                {
                    throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_PATH_NULL, InvalidGeneratorPartFileException.WhichInvalid.PathNotSet, genFile.Parent);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.PackageMaker.Error.FILE_PATH_NOT_SET, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }
            Exception outEx;

            string originFile = genFile.Path;
            if (!string.IsNullOrWhiteSpace(rootDir))
                originFile = rootDir + "\\" + genFile.Path;

            if (Helper.CopyFileSafely(originFile, copyToPath, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: LoggerMessages.PackageUtil.Error.CopyFileFailed)) != CopyFileResult.Success)
                throw outEx;
        }

        static void PerformDirectoryExistenceCheck(string rootPath, string _namespace)
        {
            string genDir = rootPath + "\\" + DirectoryNames.Generator.ROOT;
            Helper.MakeDirIfNotExistCopy(genDir, _namespace);

            string subDir = genDir + "\\" + DirectoryNames.Generator.FACE;
            Helper.MakeDirIfNotExistCopy(subDir, _namespace);
            PerformGenderDirExistenceCheck(subDir, _namespace);

            subDir = genDir + "\\" + DirectoryNames.Generator.SV;
            Helper.MakeDirIfNotExistCopy(subDir, _namespace);
            PerformGenderDirExistenceCheck(subDir, _namespace);

            subDir = genDir + "\\" + DirectoryNames.Generator.TV;
            Helper.MakeDirIfNotExistCopy(subDir, _namespace);
            PerformGenderDirExistenceCheck(subDir, _namespace);

            subDir = genDir + "\\" + DirectoryNames.Generator.TVD;
            Helper.MakeDirIfNotExistCopy(subDir, _namespace);
            PerformGenderDirExistenceCheck(subDir, _namespace);

            subDir = genDir + "\\" + DirectoryNames.Generator.VARIATION;
            Helper.MakeDirIfNotExistCopy(subDir, _namespace);
            PerformGenderDirExistenceCheck(subDir, _namespace);

        }

        static void PerformGenderDirExistenceCheck(string rootPath, string _namespace)
        {
            string dir = rootPath + "\\" + DirectoryNames.Generator.MALE;
            Helper.MakeDirIfNotExistCopy(dir, _namespace);

            dir = rootPath + "\\" + DirectoryNames.Generator.FEMALE;
            Helper.MakeDirIfNotExistCopy(dir, _namespace);

            dir = rootPath + "\\" + DirectoryNames.Generator.KID;
            Helper.MakeDirIfNotExistCopy(dir, _namespace);

        }
    }
}
