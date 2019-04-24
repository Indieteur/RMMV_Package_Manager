using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMCharImageGroup : RMPackGroup
    {
        public List<RMCharImageFile> Files { get; set; } = new List<RMCharImageFile>();
        public RMCharImageCollection Parent { get; set; }

        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Files); set => throw new NotImplementedException(); }

        public RMCharImageGroup(RMCharImageCollection parent)
        {
            Parent = parent;
        }
        public RMCharImageGroup(XElement elementToParse, RMCharImageCollection parent)
        {
            Parent = parent;
            XAttribute name = elementToParse.Attribute(RMPConstants.ATTR_NAME);
            if ((string)name == null)
                throw new InvalidCharacterImageNodeException(ExceptionMessages.RMPackage.CHAR_GROUP_NO_NAME, InvalidCharacterImageNodeException.WhichInvalid.NoName, parent);
            Name = name.Value;
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidCharacterImageNodeException(ExceptionMessages.RMPackage.CHAR_GROUP_NO_NAME, InvalidCharacterImageNodeException.WhichInvalid.InvalidName, parent);

            IEnumerable<XElement> children = elementToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_FILE)
                    Files.Add(new RMCharImageFile(child, this));
            }
        }

        public List<RMPackFile> RetrieveAllFiles()
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMCharImageFile image in Files)
                files.Add(image);
            return files;
        }

        public void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Files == null || Files.Count == 0)
                return;
            foreach (RMCharImageFile image in Files)
                image.InstallationStatus =  status;
        }



        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_CHARACTER);
            newElem.SetAttributeValue(RMPConstants.ATTR_NAME, Name);
            if (Files != null && Files.Count > 0)
            {
                foreach (RMCharImageFile image in Files)
                {
                    newElem.Add(image.ToXMLElement());
                }
            }
            return newElem;
        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMCharImageGroup Clone(RMCharImageCollection parent)
        {
            RMCharImageGroup clone = new RMCharImageGroup(parent);
            if (Files != null)
            {
                foreach (RMCharImageFile image in Files)
                {
                    clone.Files.Add(image.Clone(clone));
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
            foreach (RMCharImageFile file in Files)
                boolAndRMFileList.Add(file.FileExists(rootDir));

            return boolAndRMFileList;
        }

        public override bool IsGroupEmpty()
        {
            return (Files == null || Files.Count == 0);
        }
    }
}
