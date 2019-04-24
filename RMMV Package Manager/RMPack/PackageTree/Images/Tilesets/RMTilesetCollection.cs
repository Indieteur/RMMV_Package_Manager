using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMTilesetCollection : RMCollection
    {
        public List<RMTilesetGroup> Groups { get; set; } = new List<RMTilesetGroup>();
        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Groups); set => throw new NotImplementedException(); }
        public RMTilesetCollection(XElement elementToParse, RMPackage package) : base(package)
        {
            IEnumerable<XElement> childElements = elementToParse.Elements();
            foreach (XElement elem in childElements)
            {
                if (elem.Name.LocalName == RMPConstants.FIELD_TILESET)
                    Groups.Add(new RMTilesetGroup(elem,this));
            }
        }
        public RMTilesetCollection(RMPackage package) : base(package)
        {

        }

        public override List<RMPackFile> RetrieveAllFiles()
        {
            if (Groups == null || Groups.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMTilesetGroup tileset in Groups)
            {
                List<RMPackFile> retrievedFiles = tileset.RetrieveAllFiles();
                if (retrievedFiles != null && retrievedFiles.Count > 0)
                    files.AddRange(retrievedFiles);
            }
            return files;
        }


        public override void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Groups == null || Groups.Count == 0)
                return;
            foreach (RMTilesetGroup tileset in Groups)
                tileset.SetInstalledPropertyAll(status);
        }

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.Collections.TILESETS);
            if (Groups != null && Groups.Count > 0)
            {
                foreach (RMTilesetGroup tileset in Groups)
                {
                    if (!tileset.IsGroupEmpty())
                    newElem.Add(tileset.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMTilesetCollection Clone(RMPackage parent)
        {
            RMTilesetCollection clone = new RMTilesetCollection(parent);
            if (Groups != null)
            {
                foreach (RMTilesetGroup tileset in Groups)
                {
                    clone.Groups.Add(tileset.Clone(clone));
                }
            }
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Groups == null || Groups.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMTilesetGroup tileset in Groups)
            {
                List<BoolAndRMFile> outRes = tileset.CheckFileExistences(rootDir);
                boolAndRMFileList.AddRange(outRes);
            }

            return boolAndRMFileList;
        }
    }
}
