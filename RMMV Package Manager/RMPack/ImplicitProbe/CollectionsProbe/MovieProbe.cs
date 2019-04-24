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
        static class MovieProbe
        {
            public static RMMovieCollection RetrieveMovieCollection(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, RMPackage parent)
            {
                log = new LogDataList();
               

                RMMovieCollection newCollection = new RMMovieCollection(parent);
                log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedAutoData(parent.Name, path, RMCollectionType.Movies), _namespace);
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                } catch (Exception ex)
                {
                    log.WriteErrorLog(LoggerMessages.RMPackage.Error.RetrieveAutoError(path, parent.Name, RMCollectionType.Movies), _namespace, ex);
                    return null;
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
                    if (fileExtension.Length < RMPConstants.MovieFileType.MP4.Length + 1)
                        continue;
                    fileExtension = fileExtension.Substring(1);

                    RMMovieFile.FileType typeOfFile = RMMovieFile.FileType.none;
                    typeOfFile = typeOfFile.ParseString(fileExtension);

                    if (typeOfFile == RMMovieFile.FileType.none || string.IsNullOrWhiteSpace(originalFileName))
                        continue;

                    RMMovieGroup rmv = newCollection.Groups.FindByInternalName(fileName);
                    if (rmv == null)
                    {
                        rmv = new RMMovieGroup(newCollection);
                        rmv.Name = originalFileName;
                        rmv.internalName = fileName;
                        newCollection.Groups.Add(rmv);
                    }
                    RMMovieFile rmf = new RMMovieFile(rmv);
                    if (trimRootPath)
                        rmf.Path = Helper.GetRelativePath(nonLoweredPath, rootPath);
                    else
                        rmf.Path = nonLoweredPath;
                    rmf.TypeOfFile = typeOfFile;
                    rmv.Files.Add(rmf);
                    log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedFile(parent.Name, nonLoweredPath, RMCollectionType.Movies), _namespace);
                }
                if (newCollection.Groups.Count == 0)
                    return null;

                return newCollection;
            }
            
        }
    }
}
