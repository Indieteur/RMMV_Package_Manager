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
        static class TilesetProbe
        {
            public static RMTilesetCollection RetrieveTilesetCollection(string path, string rootPath, string _namespace, bool trimRootPath, out LogDataList log, RMPackage parent)
            {
                log = new LogDataList();
                RMTilesetCollection newCollection = new RMTilesetCollection(parent);
                log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedAutoData(parent.Name, path, RMCollectionType.Tilesets), _namespace);
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }catch (Exception ex)
                {
                    log.WriteErrorLog(LoggerMessages.RMPackage.Error.RetrieveAutoError(path, parent.Name, RMCollectionType.Tilesets), _namespace, ex);
                    return null;
                }

                if (files == null || files.Length == 0)
                    return null;

                for (int i = 0; i < files.Length; ++i)
                {
                    string originalFileName = Path.GetFileNameWithoutExtension(files[i]);
                    string nonLoweredPath = files[i];
                    files[i] = files[i].ToLower();
                    string fileExtension = Path.GetExtension(files[i]);
                    if (fileExtension.Length < RMPConstants.TilesetFileType.PNG.Length + 1)
                        continue;
                    fileExtension = fileExtension.Substring(1);

                    RMTilesetFile.eFileType typeOfFile = RMTilesetFile.eFileType.None;
                    typeOfFile = typeOfFile.ParseString(fileExtension);


                    if (typeOfFile == RMTilesetFile.eFileType.None || string.IsNullOrWhiteSpace(originalFileName))
                        continue;

                    TilesetTypeAndName parsedFileName = new TilesetTypeAndName(originalFileName);


                    RMTilesetGroup tileset = newCollection.Groups.FindByInternalName(parsedFileName.internalName);
                    if (tileset == null)
                    {
                        tileset = new RMTilesetGroup(newCollection);
                        tileset.Name = parsedFileName.Name;
                        tileset.internalName = parsedFileName.internalName;
                        newCollection.Groups.Add(tileset);
                    }
                    RMTilesetFile tilefile = new RMTilesetFile(tileset);
                    if (trimRootPath)
                        tilefile.Path = Helper.GetRelativePath(nonLoweredPath, rootPath);
                    else
                        tilefile.Path = nonLoweredPath;
                    tilefile.FileType = typeOfFile;
                    tilefile.AtlasType = parsedFileName.atlasType;
                    tileset.Files.Add(tilefile);
                    log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrievedFile(parent.Name, nonLoweredPath, RMCollectionType.Tilesets), _namespace);
                }
                if (newCollection.Groups.Count == 0)
                    return null;

                return newCollection;
            }
           
        }
    }
}
