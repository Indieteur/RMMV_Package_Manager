using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static partial class  RMImplicit
    {
        static class CharacterProbe
        {
            public static RMCharImageCollection RetrieveCharacterImages(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, RMPackage parent)
            {
                log = new LogDataList();
                RMCharImageCollection newCollection = new RMCharImageCollection(parent);
                List<RMCharImageGroup> ListOfCharacters = new List<RMCharImageGroup>();
                LogDataList outLog = null;

                string appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_CHARACTERS;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveSubCharacterImages(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref ListOfCharacters, RMCharImageFile.ImageTypes.Character, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_ENEMIES;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveSubCharacterImages(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref ListOfCharacters, RMCharImageFile.ImageTypes.Enemy, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_FACES;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveSubCharacterImages(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref ListOfCharacters, RMCharImageFile.ImageTypes.Face, newCollection);
                    log.AppendLogs(outLog);
                }
                

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_SV_ACTORS;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveSubCharacterImages(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref ListOfCharacters, RMCharImageFile.ImageTypes.SV_Actor, newCollection);
                    log.AppendLogs(outLog);
                }

                appendedPath = path + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_SV_ENEMIES;
                if (Directory.Exists(appendedPath))
                {
                    RetrieveSubCharacterImages(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, ref ListOfCharacters, RMCharImageFile.ImageTypes.SV_Enemy, newCollection);
                    log.AppendLogs(outLog);
                }

                if (ListOfCharacters.Count == 0)
                    return null;
                newCollection.Groups.AddRange(ListOfCharacters);
                return newCollection;
            }

            public static void RetrieveSubCharacterImages(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, ref List<RMCharImageGroup> collection, RMCharImageFile.ImageTypes imageType, RMCharImageCollection parent)
            {
                log = new LogDataList();
                log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedAutoData(parent.Parent.Name, path, RMCollectionType.Characters), _namespace);
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path, "*" + RMPConstants.GenFileNamePrefixANDSuffix.PNG);
                    
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(LoggerMessages.RMPackage.Error.RetrieveAutoError(path, parent.Parent.Name, RMCollectionType.Characters), _namespace, ex);
                    return;
                }
                if (files == null || files.Length == 0)
                    return;
                for (int i = 0; i < files.Length; ++i)
                {
                    string realFileName = Path.GetFileNameWithoutExtension(files[i]);
                    if (string.IsNullOrWhiteSpace(realFileName))
                        continue;
                    string nonLoweredPath = files[i];
                    files[i] = files[i].ToLower();
                    string fileName = realFileName.ToLower();
                    string rootFileName;
                    bool atlas = IsAnAtlasPartFile(realFileName, out rootFileName);
                    RMCharImageGroup parentNode;
                    parentNode = collection.FindByInternalName((atlas) ? rootFileName.ToLower() : fileName);
                    if(parentNode == null)
                    {
                        parentNode = new RMCharImageGroup(parent);
                        parentNode.internalName = (atlas) ? rootFileName.ToLower() : fileName;
                        parentNode.Name = (atlas) ? rootFileName : realFileName;
                        collection.Add(parentNode);
                    }
                    RMCharImageFile newFile = new RMCharImageFile(parentNode);
                    newFile.FileName = realFileName;
                    newFile.ImageType = imageType;
                    if (trimRootPath)
                        newFile.Path = Helper.GetRelativePath(nonLoweredPath, rootPath);
                    else
                        newFile.Path = nonLoweredPath;
                    parentNode.Files.Add(newFile);
                    log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedFile(parent.Parent.Name, nonLoweredPath, RMCollectionType.Characters), _namespace);
                }
            }

            static bool IsAnAtlasPartFile(string filename, out string rootFileName)
            {
                rootFileName = null;
                string[] str = filename.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                int tempInt;
                if (str.Length == 1 || !int.TryParse(str[1], out tempInt))
                    return false;
                rootFileName = str[0];
                return true;
            }
        }
    }
}
