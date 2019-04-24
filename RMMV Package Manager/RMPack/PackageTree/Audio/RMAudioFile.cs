using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMAudioFile : RMPackFile
    {
       
        public enum FileType
        {
            m4a,
            ogg,
            wav,
            mp3,
            none
        }
        public FileType TypeOfFile { get; set; }
        public RMAudioGroup Parent { get; set; }
        public override InstallStatus InstallationStatus { get; set; }

        string _path;
        public override string Path
        {
            get => _path;

            set
            {
                if (value.StartsWith("\\"))
                {
                    if (value.Length > 1)
                        _path =  value.Substring(1);
                }
                else
                    _path = value;
            }
        }

       
        public RMAudioFile(RMAudioGroup parent)
        {
            Parent = parent;
        }

        public RMAudioFile(XElement whichElement, RMAudioGroup parent)
        {
            Parent = parent;
            XAttribute attribute = whichElement.Attribute(RMPConstants.ATTR_TYPE);
            if ((string)attribute == null)
                throw new InvalidAudioFileException(ExceptionMessages.RMPackage.AUDIO_INVALID_FILE_TYPE, InvalidAudioFileException.WhichInvalid.NoType, parent);
            TypeOfFile = TypeOfFile.ParseString(attribute.Value);
            if (TypeOfFile == FileType.none)
                throw new InvalidAudioFileException(ExceptionMessages.RMPackage.AUDIO_INVALID_FILE_TYPE, InvalidAudioFileException.WhichInvalid.InvalidType, parent);
            if (parent.Parent.Parent.Installed)
            {
                attribute = whichElement.Attribute(RMPConstants.ATTR_INSTALLED);
                if ((string)attribute != null)
                    InstallationStatus = Pacman.Helper.GetInstallStatus(attribute.Value);
            }
            Path = whichElement.Value;
        }

        public override XElement ToXMLElement()
        {
            XElement elemToReturn = new XElement(RMPConstants.FIELD_FILE, Path);
            elemToReturn.SetAttributeValue(RMPConstants.ATTR_TYPE, TypeOfFile.ToExtensionString());
            if (Parent.Parent.Parent.Installed && InstallationStatus == InstallStatus.Installed)
                elemToReturn.SetAttributeValue(RMPConstants.ATTR_INSTALLED, "Y");
            return elemToReturn;
        }

        public override string GetTreeViewPrefixString()
        {
            if (TypeOfFile == FileType.none)
                return "[Unspecified]";
            else
                return "[" + TypeOfFile.ToExtensionString() + "] ";
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMAudioFile Clone(RMAudioGroup parent)
        {
            RMAudioFile rmaf = new RMAudioFile(parent);
            rmaf.InstallationStatus = InstallationStatus;
            rmaf.Path = Path;
            rmaf.TypeOfFile = TypeOfFile;
            rmaf._path = _path;
            //rmaf.NonRelativePath = NonRelativePath;
            return rmaf;
        }

        
        
    }
}
