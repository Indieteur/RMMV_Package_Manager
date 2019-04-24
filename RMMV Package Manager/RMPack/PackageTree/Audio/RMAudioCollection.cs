using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMAudioCollection : RMCollection
    {
        public enum AudioType
        {
            BGM,
            BGS,
            ME,
            SE
        }
        public AudioType CollectionType { get; set; }
        public List<RMAudioGroup> Groups { get; set; } = new List<RMAudioGroup>();

        public override InstallStatus InstallationStatus
        {
            get => GetInstallStatusByListCheck(Groups);
            set => throw new NotImplementedException();
        }

      

        public RMAudioCollection(AudioType collectionType, RMPackage parent) : base(parent)
        {
            CollectionType = collectionType;
        }

        public RMAudioCollection(XElement elementToParse, AudioType collectionType, RMPackage parent) : base(parent)
        {
            CollectionType = collectionType;
            //Parse install.xml by retrieving all audio child elements of this collection.
            IEnumerable<XElement> children = elementToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_AUDIO)
                    Groups.Add(new RMAudioGroup(child, this));
            }
        }

        

        public override List<RMPackFile> RetrieveAllFiles()
        {
            if (Groups == null || Groups.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMAudioGroup audio in Groups)
            {
                List<RMPackFile> retrievedFiles = audio.RetrieveAllFiles();
                if (retrievedFiles != null && retrievedFiles.Count > 0)
                    files.AddRange(retrievedFiles);
            }
            return files;
        }

        public override void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Groups == null || Groups.Count == 0)
                return;
            foreach (RMAudioGroup audio in Groups)
                audio.SetInstalledPropertyAll(status);
        }



        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(CollectionType.ToXMLString());
            if (Groups != null && Groups.Count > 0)
            {
                foreach (RMAudioGroup audio in Groups)
                {
                    if (!audio.IsGroupEmpty())
                        newElem.Add(audio.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMAudioCollection Clone(RMPackage parent)
        {
            RMAudioCollection clone = new RMAudioCollection(CollectionType, parent);
            if (Groups != null)
            {
                foreach (RMAudioGroup audio in Groups)
                {
                    clone.Groups.Add(audio.Clone(clone));
                }
            }
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Groups == null || Groups.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMAudioGroup audio in Groups)
            {
                List<BoolAndRMFile> outRes = audio.CheckFileExistences(rootDir);
                boolAndRMFileList.AddRange(outRes);
            }

            return boolAndRMFileList;
        }
    }
}
