using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMAudioGroup : RMPackGroup
    {
      
       
        public List<RMAudioFile> Files { get; set; } = new List<RMAudioFile>();
        public RMAudioCollection Parent { get; set; }
        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Files, true); set => throw new NotImplementedException(); }


        public RMAudioGroup(RMAudioCollection parent)
        {
            Parent = parent;
        }

        public RMAudioGroup(XElement whichToParse, RMAudioCollection parent)
        {
           
            Parent = parent;
            XAttribute attribute = whichToParse.Attribute(RMPConstants.ATTR_NAME);
            if ((string)attribute == null)
                throw new InvalidAudioException(ExceptionMessages.RMPackage.AUDIO_GROUP_NO_NAME, InvalidAudioException.WhichInvalid.NoName, parent);
            Name = attribute.Value;
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidAudioException(ExceptionMessages.RMPackage.AUDIO_GROUP_NO_NAME, InvalidAudioException.WhichInvalid.InvalidName, parent);
            IEnumerable<XElement> children = whichToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_FILE)
                    Files.Add(new RMAudioFile(child, this));
            }
        }

        public List<RMPackFile> RetrieveAllFiles()
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMAudioFile audioFile in Files)
                files.Add(audioFile);
            return files;

        }

        public void SetInstalledPropertyAll (InstallStatus status)
        {
            if (Files == null || Files.Count == 0)
                return;
            foreach (RMAudioFile audioFile in Files)
                audioFile.InstallationStatus = status;
        }
       

        public override XElement ToXMLElement()
        {
            
            XElement newElem = new XElement(RMPConstants.FIELD_AUDIO);
            newElem.SetAttributeValue(RMPConstants.ATTR_NAME, Name);
            if (Files != null && Files.Count > 0)
            {
                foreach (RMAudioFile audioFile in Files)
                {
                    newElem.Add(audioFile.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMAudioGroup Clone(RMAudioCollection parent)
        {
            RMAudioGroup clone = new RMAudioGroup(parent);
            if (Files != null)
            {
                foreach (RMAudioFile audioFile in Files)
                {
                    clone.Files.Add(audioFile.Clone(clone));
                }
            }
            clone.Name = Name;
            clone.internalName = internalName;
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Files == null || Files.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMAudioFile file in Files)
                boolAndRMFileList.Add(file.FileExists(rootDir));

            return boolAndRMFileList;
        }

        public override bool IsGroupEmpty()
        {
            return (Files == null || Files.Count == 0);
        }
    }
}
