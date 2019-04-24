using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public abstract class RMSingleFileCollection : RMCollection
    {
        public enum CollectionType
        {
            Data,
            Animation,
            BattleBacks_1,
            BattleBacks_2,
            Parallax,
            Pictures,
            System,
            Titles_1,
            Titles_2,
            Plugins
        }

        public List<RMSingleFile> Files { get; set; } = new List<RMSingleFile>();
        public string Name { get; set; }
        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Files, true); set => throw new NotImplementedException(); }
        public RMSingleFileCollection(RMPackage parent) : base(parent)
        {

        }
        public RMSingleFileCollection(XElement elementToParse, RMPackage parent, bool nameRequired = false) : base(parent)
        {
            XAttribute name = elementToParse.Attribute(RMPConstants.ATTR_NAME);
            if (nameRequired && (string)name == null)
                throw new InvalidSingleFileCollectionException(ExceptionMessages.RMPackage.COLL_NO_NAME, InvalidSingleFileCollectionException.WhichInvalid.NoName, parent);
            if ((string)name != null)
                Name = name.Value;
            if (nameRequired && string.IsNullOrWhiteSpace(Name))
                throw new InvalidSingleFileCollectionException(ExceptionMessages.RMPackage.COLL_NO_NAME, InvalidSingleFileCollectionException.WhichInvalid.InvalidName, parent);

            IEnumerable<XElement> children = elementToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_FILE)
                    Files.Add(new RMSingleFile(child, this));
            }
        }

        public override List<RMPackFile> RetrieveAllFiles()
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMSingleFile singleFile in Files)
                files.Add(singleFile);
            return files;
        }

        public override void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Files == null || Files.Count == 0)
                return;
            foreach (RMSingleFile file in Files)
                file.InstallationStatus = status;
        }


        protected XElement GetNoNameXElement()
        {
            XElement newElem = new XElement(RMPConstants.Defaults.PLACEHOLDER_XELEMENT_NAME);
            if (Files != null && Files.Count > 0)
            {
                foreach (RMSingleFile file in Files)
                {
                    newElem.Add(file.ToXMLElement());
                }
            }
            return newElem;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Files == null || Files.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMSingleFile file in Files)
                boolAndRMFileList.Add(file.FileExists(rootDir));

            return boolAndRMFileList;
        }



    }
}
