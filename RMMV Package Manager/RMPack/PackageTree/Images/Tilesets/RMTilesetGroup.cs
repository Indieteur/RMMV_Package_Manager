using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMTilesetGroup : RMPackGroup
    {
        public List<RMTilesetFile> Files { get; set; } = new List<RMTilesetFile>();
        public RMTilesetCollection Parent { get; set; }
        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Files); set => throw new NotImplementedException(); }

        public RMTilesetGroup(RMTilesetCollection parent)
        {
            Parent = parent;
        }
        public RMTilesetGroup(XElement elementToParse, RMTilesetCollection parent)
        {
            Parent = parent;
            XAttribute name = elementToParse.Attribute(RMPConstants.ATTR_NAME);
            if ((string)name == null)
                throw new InvalidTilesetException(ExceptionMessages.RMPackage.TILESET_GROUP_NO_NAME, InvalidTilesetException.WhichInvalid.NoName, parent);
            Name = name.Value;
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidTilesetException(ExceptionMessages.RMPackage.TILESET_GROUP_NO_NAME, InvalidTilesetException.WhichInvalid.InvalidName, parent);

            IEnumerable<XElement> children = elementToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_FILE)
                    Files.Add(new RMTilesetFile(child, this));
            }
        }

        public List<RMPackFile> RetrieveAllFiles()
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMTilesetFile file in Files)
                files.Add(file);
            return files;
        }


        public void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Files == null || Files.Count == 0)
                return;
            foreach (RMTilesetFile tileset in Files)
                tileset.InstallationStatus = status;
        }

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_TILESET);
            newElem.SetAttributeValue(RMPConstants.ATTR_NAME, Name);
            if (Files != null && Files.Count > 0)
            {
                foreach (RMTilesetFile file in Files)
                {
                    newElem.Add(file.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMTilesetGroup Clone(RMTilesetCollection parent)
        {
            RMTilesetGroup clone = new RMTilesetGroup(parent);
            if (Files != null)
            {
                foreach (RMTilesetFile file in Files)
                {
                    clone.Files.Add(file.Clone(clone));
                }
            }
            clone.internalName = internalName;
            clone.Name = Name;
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Files == null || Files.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMTilesetFile file in Files)
                boolAndRMFileList.Add(file.FileExists(rootDir));

            return boolAndRMFileList;
        }

        public override bool IsGroupEmpty()
        {
            return (Files == null || Files.Count == 0);
        }
    }
}
