using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static partial class RMImplicit
    {
        static class GeneratorProbe
        {
            public static RMGeneratorCollection RetrieveGeneratorCollection(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, RMPackage parent)
            {
                log = new LogDataList();
                RMGeneratorCollection newCollection = new RMGeneratorCollection(parent);
                List<RMGenPart> tempListOfFiles = new List<RMGenPart>();
                LogDataList outLog = null;

                string appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_FACE;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorFileParts(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref tempListOfFiles, RMGenFile.GenFileType.Face, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_SV;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorFileParts(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref tempListOfFiles, RMGenFile.GenFileType.SV, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_TV;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorFileParts(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref tempListOfFiles, RMGenFile.GenFileType.TV, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_TVD;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorFileParts(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref tempListOfFiles, RMGenFile.GenFileType.TVD, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_VARIATION;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorFileParts(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref tempListOfFiles, RMGenFile.GenFileType.Var, newCollection);
                    log.AppendLogs(outLog);
                }

                if (tempListOfFiles.Count == 0)
                    return null;

                newCollection.Parts.AddRange(tempListOfFiles);
                
                return newCollection;
            }

            static void RetrieveGeneratorFileParts(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, ref List<RMGenPart> collection, RMGenFile.GenFileType whichFilePart, RMGeneratorCollection parent)
            {
                log = new LogDataList();
                string appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_PART_FEMALE;
                LogDataList outLog = null;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorPartOnGender(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref collection, whichFilePart, RMGenPart.eGender.Female, parent);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_PART_MALE;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorPartOnGender(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref collection, whichFilePart, RMGenPart.eGender.Male, parent);
                    log.AppendLogs(outLog);
                }
                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.GEN_PART_KID;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveGeneratorPartOnGender(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref collection, whichFilePart, RMGenPart.eGender.Kid, parent);
                    log.AppendLogs(outLog);
                }

            }

            static void RetrieveGeneratorPartOnGender(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, ref List<RMGenPart> partsCollection, RMGenFile.GenFileType whichFilePart, RMGenPart.eGender whichGender, RMGeneratorCollection parent)
            {
                log = new LogDataList();
                log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedAutoData(parent.Parent.Name, path, RMCollectionType.Generator), _namespace);
                string[] listOfFiles = null;
                try
                {
                    listOfFiles = Directory.GetFiles(path, "*" + RMPConstants.GenFileNamePrefixANDSuffix.PNG);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(LoggerMessages.RMPackage.Error.RetrieveAutoError(path, parent.Parent.Name, RMCollectionType.Generator), _namespace, ex);
                    return;
                }
                if (listOfFiles == null || listOfFiles.Length == 0)
                    return;
                string prefix;
                switch (whichFilePart)
                {
                    case RMGenFile.GenFileType.Face:
                        prefix = RMPConstants.GenFileNamePrefixANDSuffix.FACE_LOWER + RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR;
                        break;
                    case RMGenFile.GenFileType.SV:
                        prefix = RMPConstants.GenFileNamePrefixANDSuffix.SV_LOWER + RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR;
                        break;
                    case RMGenFile.GenFileType.TV:
                        prefix = RMPConstants.GenFileNamePrefixANDSuffix.TV_LOWER + RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR;
                        break;
                    case RMGenFile.GenFileType.TVD:
                        prefix = RMPConstants.GenFileNamePrefixANDSuffix.TVD_LOWER + RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR;
                        break;
                    case RMGenFile.GenFileType.Var:
                        prefix = RMPConstants.GenFileNamePrefixANDSuffix.VARIATION + RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR;
                        break;
                    default:
                        return;
                }
                for (int i = 0; i < listOfFiles.Length; ++i)
                {
                  
                    string nonLoweredPath = listOfFiles[i];
                    listOfFiles[i] = listOfFiles[i].ToLower();
                    string fileName = Path.GetFileName(listOfFiles[i]);


                    if (fileName.Length > prefix.Length && fileName.StartsWith(prefix))
                    {
                        TempGenFileNameParsed tgfnm = null;
                        try
                        {
                            tgfnm = new TempGenFileNameParsed(fileName, prefix.Length);
                            VerifyFile(tgfnm, listOfFiles[i]);
                        }
                        catch (Exception ex)
                        {
                            log.WriteErrorLog(LoggerMessages.RMPackage.Error.InvalidGenFile(nonLoweredPath), _namespace, ex);
                            continue;
                        }
                        RMGenPart tempPart = partsCollection.GetPartByInternalPosition(tgfnm.Part, whichGender, tgfnm.Position);
                       
                        RMGenFile newGenFile;

                        string pathToSave = (trimRootPath) ? Helper.GetRelativePath(nonLoweredPath, rootPath) : nonLoweredPath;

                        newGenFile = CreateGenFileFromTempGenFileName(tgfnm, GetProperFileType(tgfnm, whichFilePart), pathToSave);
                        if (tempPart == null)
                        {
                            tempPart = CreateNewPart(tgfnm, whichGender, parent, (partsCollection.CountPartOfType(tgfnm.Part, whichGender) + 1));
                            partsCollection.Add(tempPart);
                        }

                        newGenFile.Parent = tempPart;
                        tempPart.Files.Add(newGenFile);
                        log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedFile(parent.Parent.Name, nonLoweredPath, RMCollectionType.Generator), _namespace);
                    }


                }
            }


            static RMGenFile.GenFileType GetProperFileType(TempGenFileNameParsed parsedFileName, RMGenFile.GenFileType mainType)
            {
                if (mainType == RMGenFile.GenFileType.Face)
                    return RMGenFile.GenFileType.Face;
                else if (mainType == RMGenFile.GenFileType.None)
                    return RMGenFile.GenFileType.None;
                else if (mainType == RMGenFile.GenFileType.Var)
                    return RMGenFile.GenFileType.Var;
                else if (mainType == RMGenFile.GenFileType.SV)
                {
                    if (parsedFileName.IsAcFile)
                        return RMGenFile.GenFileType.SV_C;
                    else
                        return RMGenFile.GenFileType.SV;
                }
                else if (mainType == RMGenFile.GenFileType.TV)
                {
                    if (parsedFileName.IsAcFile)
                        return RMGenFile.GenFileType.TV_C;
                    else
                        return RMGenFile.GenFileType.TV;
                }
                else if (mainType == RMGenFile.GenFileType.TVD)
                {
                    if (parsedFileName.IsAcFile)
                        return RMGenFile.GenFileType.TVD_C;
                    else
                        return RMGenFile.GenFileType.TVD;
                }
                return RMGenFile.GenFileType.None;
            }

            

            static RMGenPart CreateNewPart(TempGenFileNameParsed parsedFileName, RMGenPart.eGender gender, RMGeneratorCollection parent, int countToAppend)
            {
                RMGenPart tempPart = new RMGenPart();
                tempPart.Gender = gender;
                tempPart.Name = parsedFileName.Part.ToParsableXMLString() + "_" +  countToAppend;
                tempPart.implicitID = parsedFileName.Position;
                tempPart.Parent = parent;
                tempPart.PartType = parsedFileName.Part;
                return tempPart;
            }

            static RMGenFile CreateGenFileFromTempGenFileName(TempGenFileNameParsed parsedFileName, RMGenFile.GenFileType typeOfFile, string path)
            {
                RMGenFile newGenFile = new RMGenFile();
                newGenFile.BaseOrder = parsedFileName.BaseOrder;
                newGenFile.Colour = parsedFileName.Colour;
                newGenFile.FileType = typeOfFile;
                newGenFile.Order = parsedFileName.Order;
                newGenFile.Path = path;
                return newGenFile;
            }

            static void VerifyFile(TempGenFileNameParsed whichFile, string path, RMGenFile.FileNameInfo requires = RMGenFile.FileNameInfo.Type | RMGenFile.FileNameInfo.Position)
            {
                
                if (requires.HasFlag(RMGenFile.FileNameInfo.Type) && whichFile.Part == RMGenPart.GenPartType.None)
                    throw new ImplicitInvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_TYPE, ImplicitInvalidGeneratorPartException.WhichInvalid.Type, path);
                else if (requires.HasFlag(RMGenFile.FileNameInfo.BaseOrder) && whichFile.BaseOrder == 0)
                    throw new ImplicitInvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_HIGH_ORDER, ImplicitInvalidGeneratorPartException.WhichInvalid.BaseOrder, path);
                else if (requires.HasFlag(RMGenFile.FileNameInfo.Colour) && whichFile.Colour == 0)
                    throw new ImplicitInvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_COLOUR, ImplicitInvalidGeneratorPartException.WhichInvalid.Colour, path);
                else if (requires.HasFlag(RMGenFile.FileNameInfo.Order) && whichFile.Order == 0)
                    throw new ImplicitInvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_LOW_ORDER, ImplicitInvalidGeneratorPartException.WhichInvalid.Order, path);
                else if (requires.HasFlag(RMGenFile.FileNameInfo.Position) && whichFile.Position < 0)
                    throw new ImplicitInvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_POS, ImplicitInvalidGeneratorPartException.WhichInvalid.Position, path);
            }

           
        }
    }
}
