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
        static class AudioProbe
        {
            public static RMAudioCollection RetrieveAudioCollection(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, RMAudioCollection.AudioType typeOfCollection, RMPackage parent)
            {
                log = new LogDataList();
                log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedAutoData(parent.Name, path, typeOfCollection.ToRMCollectionType()), _namespace);
                RMAudioCollection newCollection = new RMAudioCollection(typeOfCollection, parent);
                string[] files = null;
                try
                {
                     files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(LoggerMessages.RMPackage.Error.RetrieveAutoError(path, parent.Name, typeOfCollection.ToRMCollectionType()), _namespace, ex);
                    return newCollection;
                }
                if (files == null || files.Length == 0)
                    return null;
                for (int i = 0; i < files.Length; ++i)
                {
                    string originalFileName = Path.GetFileNameWithoutExtension(files[i]);
                    string nonLoweredPath = files[i];
                   
                    files[i] = files[i].ToLower();
                    
                    string fileName = originalFileName.ToLower();
                    string fileExtension = Path.GetExtension(files[i]);
                    if (fileExtension.Length < RMPConstants.AudioFileType.M4A.Length + 1)
                        continue;
                    fileExtension = fileExtension.Substring(1);

                    RMAudioFile.FileType typeOfFile = RMAudioFile.FileType.none;
                    typeOfFile = typeOfFile.ParseString(fileExtension);
                    if (typeOfFile == RMAudioFile.FileType.none || string.IsNullOrWhiteSpace(originalFileName))
                        continue;

                    RMAudioGroup rma = newCollection.Groups.FindByInternalName(fileName);
                    if (rma == null)
                    {
                        rma = new RMAudioGroup(newCollection);
                        rma.Name = originalFileName;
                        rma.internalName = fileName;
                        newCollection.Groups.Add(rma);
                    }
                    RMAudioFile rmaf = new RMAudioFile(rma);
                    if (trimRootPath)
                        rmaf.Path = Helper.GetRelativePath(nonLoweredPath, rootPath);
                    else
                        rmaf.Path = nonLoweredPath;
                    rmaf.TypeOfFile = typeOfFile;
                    rma.Files.Add(rmaf);
                    log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedFile(parent.Name, nonLoweredPath, typeOfCollection.ToRMCollectionType()), _namespace);
                }
                if (newCollection.Groups.Count == 0)
                    return null;

                return newCollection;
            }
            
        }
    }
}
