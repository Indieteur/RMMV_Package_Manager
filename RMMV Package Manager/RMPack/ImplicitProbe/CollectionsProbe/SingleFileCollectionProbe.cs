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
        static class SingleFileCollectionProbe
        {
           

            internal static RMSingleFileCollection RetrieveSingleFileCollection(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, RMSingleFileCollection.CollectionType typeOfCollection , RMPackage package)
            {
                log = new LogDataList();
                log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedAutoData(package.Name, path, typeOfCollection.ToRMCollectionType()), _namespace);
                RMSingleFileCollection newCollection = typeOfCollection.ToNewClassInstance(package);
                string fileExtension = typeOfCollection.ToFileExtension();
                if (newCollection == null)
                    return null;
                string[] files = null;
                try
                {
                   files  = Directory.GetFiles(path, "*." + fileExtension);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(LoggerMessages.RMPackage.Error.RetrieveAutoError(path, package.Name, typeOfCollection.ToRMCollectionType()), _namespace, ex);
                    return null;
                }
                if (files == null || files.Length == 0)
                    return null;

                for (int i = 0; i < files.Length; i++)
                {
                    string originalFileName = Path.GetFileNameWithoutExtension(files[i]);
                    if (string.IsNullOrWhiteSpace(originalFileName))
                        continue;
                    RMSingleFile newDataFile = new RMSingleFile(newCollection);
                    if (trimRootPath)
                        newDataFile.Path = Helper.GetRelativePath(files[i], rootPath);
                    else
                        newDataFile.Path = files[i];
                    newDataFile.FileName = originalFileName;
                    newCollection.Files.Add(newDataFile);
                    log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedFile(package.Name, files[i], typeOfCollection.ToRMCollectionType()), _namespace);
                }
                if (newCollection.Files.Count == 0)
                    return null;
                return newCollection;

            }


           

          
        }
    }
  
}
