using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMMovieFile : RMPackFile
    {
        public enum FileType
        {
            mp4,
            webm,
            none
        }
        public FileType TypeOfFile { get; set; }
        public RMMovieGroup Parent { get; set; }
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
                        _path = value.Substring(1);
                }
                else
                    _path = value;
            }
        }

        public RMMovieFile(RMMovieGroup parent)
        {
            Parent = parent;
        }

        public RMMovieFile(XElement whichElement, RMMovieGroup parent)
        {
            Parent = parent;
            XAttribute attribute = whichElement.Attribute(RMPConstants.ATTR_TYPE);
            if ((string)attribute == null)
                throw new InvalidMovieFileException(ExceptionMessages.RMPackage.MOVIE_TYPE_NOT_SET, InvalidMovieFileException.WhichInvalid.NoType, parent);
            TypeOfFile = TypeOfFile.ParseString(attribute.Value);
            if (TypeOfFile == FileType.none)
                throw new InvalidMovieFileException(ExceptionMessages.RMPackage.MOVIE_TYPE_NOT_SET, InvalidMovieFileException.WhichInvalid.InvalidType, parent);
            if (Parent.Parent.Parent.Installed)
            {
                attribute = whichElement.Attribute(RMPConstants.ATTR_INSTALLED);
                if ((string)attribute != null)
                    InstallationStatus = Pacman.Helper.GetInstallStatus(attribute.Value);
            }
            Path = whichElement.Value;
        }

       

      

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_FILE, Path);
            newElem.SetAttributeValue(RMPConstants.ATTR_TYPE, TypeOfFile.ToXMLString());
            if (Parent.Parent.Parent.Installed && InstallationStatus == InstallStatus.Installed)
                newElem.SetAttributeValue(RMPConstants.ATTR_INSTALLED, "Y");
            return newElem;
        }

        public override string GetTreeViewPrefixString()
        {
            if (TypeOfFile == FileType.none)
                return string.Empty;
            return "[" + TypeOfFile.ToXMLString() + "] ";
        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMMovieFile Clone(RMMovieGroup parent)
        {
            RMMovieFile clone = new RMMovieFile(parent);
            clone.InstallationStatus = InstallationStatus;
            clone.Path = Path;
            clone.TypeOfFile = TypeOfFile;
            clone._path = _path;
            return clone;
        }
    }
}
