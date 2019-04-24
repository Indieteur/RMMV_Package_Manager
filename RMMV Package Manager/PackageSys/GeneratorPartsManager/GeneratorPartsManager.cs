using Indieteur.BasicLoggingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static partial class GeneratorPartsManager
    {
        public static void RenumberParts(string _namespace, out LogDataList log, bool relinkGlobalPackages = false)
        {
            RenumberParts(PMFileSystem.MV_Installation_Directory, _namespace, out log, relinkGlobalPackages);
        }
        public static void RenumberParts(string parentFolderPath, string _namespace, out LogDataList log, bool relinkGlobalPackages = false)
        {
            log = new LogDataList();
            log.WriteInformationLog(LoggerMessages.GeneratorPartsManager.RenumberParts.Info.RENUMBER_START + parentFolderPath + ".", _namespace);
            Exception outEx;
            if (!Directory.Exists(parentFolderPath))
                return;


            if (Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempRenumberDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled)
                throw outEx;

            if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempRenumberDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
                throw outEx;

            RMPackage tempGenPack = new RMPackage();
            tempGenPack.Name = "Generator Parts Renumber Class";
            LogDataList outLog;
            RMImplicit.RetrievePackFromDir(parentFolderPath, _namespace, true, out outLog, ref tempGenPack);
            log.AppendLogs(outLog);

            if (tempGenPack.Collections == null || tempGenPack.Collections.Count == 0)
                return;

            List<ComparedPath> comparedPaths = null;
            foreach (RMCollection collection in tempGenPack.Collections)
            {
                if (collection is RMGeneratorCollection)
                {
                    RMGeneratorCollection genCollection = collection as RMGeneratorCollection;
                    try
                    {
                        if (genCollection.Parts != null)
                            comparedPaths = PerformRenumber(genCollection, parentFolderPath, _namespace);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(LoggerMessages.GeneratorPartsManager.RenumberParts.Error.RENUMBER_ABORT_GENERAL, _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                        throw;
                    }
                }
            }

          


            if (comparedPaths != null && comparedPaths.Count > 0)
            {
                foreach (ComparedPath comparedPath in comparedPaths)
                {
                    string completeTempPath = PMFileSystem.PackMan_TempRenumberDir + "\\" + comparedPath.New;
                    string completeFinalPath = parentFolderPath + "\\" + comparedPath.New;

                    MoveFileResult moveResult = Helper.MoveFileSafely(completeTempPath, completeFinalPath, true, _namespace, out outEx, 
                        new MoveFileLogMessages(sourceFileNotFound: LoggerMessages.GeneratorPartsManager.RenumberParts.Error.UnableMoveFinalSrcNotFound
                       , moveFileFailed: LoggerMessages.GeneratorPartsManager.RenumberParts.Error.UnableMoveFinal));
                    if (moveResult == MoveFileResult.UserCancelled || moveResult == MoveFileResult.SourceFileNotFound)
                        throw outEx;

                    if (relinkGlobalPackages && PackageManagement.GlobalPackages != null)
                    {
                        foreach (InstalledPackage package in PackageManagement.GlobalPackages)
                        {
                            if (package.Package != null)
                            {
                                RMGenFile foundFile = package.Package.FindGenFileWithPath(comparedPath.Old);
                                if (foundFile != null)
                                {
                                    foundFile.Path = comparedPath.New;
                                    package.ChangesMade = true;
                                    goto continuehere;
                                }
                            }
                        }
                    }
                    continuehere:;
                }
                if (relinkGlobalPackages && PackageManagement.GlobalPackages != null)
                {
                    foreach (InstalledPackage package in PackageManagement.GlobalPackages)
                    {
                        if (package.ChangesMade && package.Package != null)
                        {
                            package.Package.SaveToFile(package.XMLPath, _namespace, logMessage: new WriteAllTextLogMessages(writeFailed: LoggerMessages.GeneratorPartsManager.RenumberParts.Error.FailedSaveXML));
                            package.ChangesMade = false;
                        }
                    }
                }


            }

            Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempRenumberDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG);
            log.WriteInformationLog(LoggerMessages.GeneratorPartsManager.RenumberParts.Info.RENUMBER_END + parentFolderPath + ".", _namespace);
        }
        static List<ComparedPath> PerformRenumber(RMGeneratorCollection collection, string rootPath, string _namespace)
        {
            List<ComparedPath> retVal = new List<ComparedPath>();
            PerformRenumberOnGender(collection, RMGenPart.eGender.Female, ref retVal, rootPath, _namespace);
            PerformRenumberOnGender(collection, RMGenPart.eGender.Male, ref retVal, rootPath, _namespace);
            PerformRenumberOnGender(collection, RMGenPart.eGender.Kid, ref retVal, rootPath, _namespace);
            return retVal;

        }

        static void PerformRenumberOnGender(RMGeneratorCollection collection, RMGenPart.eGender gender, ref List<ComparedPath> comparedPaths, string rootPath, string _namespace)
        {
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Accessory_A, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Accessory_B, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Beard, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Beast_Ears, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Body, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Cloak, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Clothing, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Ears, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Eyebrows, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Eyes, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Face, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Facial_Mark, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Front_Hair, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Glasses, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Mouth, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Nose, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Rear_Hair, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Tail, ref comparedPaths, rootPath, _namespace);
            PerformRenumberOnPart(collection, gender, RMGenPart.GenPartType.Wing, ref comparedPaths, rootPath, _namespace);
        }

        static void PerformRenumberOnPart(RMGeneratorCollection collection, RMGenPart.eGender gender, RMGenPart.GenPartType partType, ref List<ComparedPath> comparedPaths, string rootPath, string _namespace)
        {
            int floorPositionValue = Precision.GetPositionFloorOfPart(gender, partType);
            RMGenPart nextLowestGenPart = collection.Parts.GetPartByInternalPosition(partType, gender, floorPositionValue);
            if (nextLowestGenPart == null)
            {
                nextLowestGenPart = collection.Parts.GetLowestPartByInternalPosition(partType, gender);
                if (nextLowestGenPart == null)
                    return;
                nextLowestGenPart.implicitID = floorPositionValue;
                PerformRenumberOnPartsFile(nextLowestGenPart, ref comparedPaths, rootPath, _namespace);
            }
            RMGenPart prevLowestGenPart;
            while (true)
            {
                prevLowestGenPart = nextLowestGenPart;
                collection.Parts.Remove(nextLowestGenPart);
                nextLowestGenPart = collection.Parts.GetLowestPartByInternalPosition(partType, gender);
                if (nextLowestGenPart == null)
                    break;
                if ((nextLowestGenPart.implicitID - prevLowestGenPart.implicitID) > 1)
                {
                    if (Precision.ShouldSkipPosition(gender, partType, prevLowestGenPart.implicitID + 1))
                    {
                       
                        int nextPosition = Precision.GetNextIntPosition(gender, partType, prevLowestGenPart.implicitID);
                        if (nextPosition == nextLowestGenPart.implicitID)
                            continue;
                    }
                    nextLowestGenPart.implicitID = Precision.GetNextIntPosition(gender, partType, prevLowestGenPart.implicitID);
                    PerformRenumberOnPartsFile(nextLowestGenPart, ref comparedPaths, rootPath, _namespace);
                }
                
            }

        }

        static void PerformRenumberOnPartsFile(RMGenPart part, ref List<ComparedPath> comparedPaths, string rootPath, string _namespace)
        {
            Exception outEx;
            if (part.Files == null)
                return;
            foreach(RMGenFile file in part.Files)
            {
                if (string.IsNullOrWhiteSpace(file.Path))
                    continue;
                ComparedPath comparedPath = new ComparedPath();
                comparedPath.Old = file.Path;
                string newPath = string.Empty;

                try
                {
                    newPath = Path.GetDirectoryName(file.Path);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.GeneratorPartsManager.RenumberParts.Error.InvalidPath(file.Path), _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }
                try
                {
                    newPath += "\\" + file.RetrieveInstallFileName(part.implicitID);
                }
                catch (Exception ex)
                {

                    Logger.WriteErrorLog(LoggerMessages.GeneratorPartsManager.RenumberParts.Error.InvalidPart(comparedPath.Old), _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }


                comparedPath.New = newPath;
                string completeOldPath = rootPath + "\\" + comparedPath.Old;
                string completeTempPath = PMFileSystem.PackMan_TempRenumberDir + "\\" + comparedPath.New;

                string dirPath = null;
                try
                {
                    dirPath = Path.GetDirectoryName(completeTempPath);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.GeneratorPartsManager.RenumberParts.Error.InvalidTempPath(file.Path), _namespace, ex, BasicDebugLogger.DebugErrorType.CriticalError);
                    throw;
                }

                if (!Directory.Exists(dirPath) && Helper.CreateFolderSafely(dirPath, _namespace, out outEx, LoggerMessages.GeneralError.CREATE_REQUIRED_DIR_FAILED_ARG) == CreateFolderResult.UserCancelled)
                    throw outEx;

                MoveFileResult moveRes = Helper.MoveFileSafely(completeOldPath, completeTempPath, true, _namespace, out outEx
                    , new MoveFileLogMessages(sourceFileNotFound: LoggerMessages.GeneratorPartsManager.RenumberParts.Error.UnableMoveToTempSrcNotFound
                    , moveFileFailed: LoggerMessages.GeneratorPartsManager.RenumberParts.Error.UnableMoveToTemp));
                if (moveRes == MoveFileResult.UserCancelled || moveRes == MoveFileResult.SourceFileNotFound)
                    throw outEx;

                comparedPaths.Add(comparedPath);
            }

        }


        class ComparedPath
        {
            public string Old;
            public string New;
            public ComparedPath()
            {

            }
            
        }
    }
}
