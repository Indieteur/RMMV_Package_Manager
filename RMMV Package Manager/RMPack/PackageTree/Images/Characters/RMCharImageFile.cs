using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMCharImageFile : RMSingleFile
    {
        public enum ImageTypes
        {
            Character,
            Enemy,
            Face,
            SV_Actor,
            SV_Enemy,
            None
        }
        public ImageTypes ImageType { get; set; }
        public new RMCharImageGroup Parent { get; set; }

        public override InstallStatus InstallationStatus { get; set; }

        string _path;
        public override string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (value.StartsWith("\\"))
                {
                    if (value.Length > 1)
                        _path = value.Substring(1);
                }
                else
                    _path = value;
                InternalFileName = System.IO.Path.GetFileNameWithoutExtension(value);
            }
        }


        public RMCharImageFile(RMCharImageGroup parent)
        {
            Parent = parent;
        }
        public RMCharImageFile(XElement elementToParse, RMCharImageGroup parent)
        {
            Parent = parent;
            XAttribute typeAttr = elementToParse.Attribute(RMPConstants.ATTR_TYPE);
            if ((string)typeAttr == null)
                throw new InvalidCharacterImageFileException(ExceptionMessages.RMPackage.CHAR_FILE_NO_TYPE, InvalidCharacterImageFileException.WhichInvalid.NoType, parent);
            ImageType = ImageType.ParseString(typeAttr.Value);
            if (ImageType == ImageTypes.None)
                throw new InvalidCharacterImageFileException(ExceptionMessages.RMPackage.CHAR_FILE_NO_TYPE, InvalidCharacterImageFileException.WhichInvalid.InvalidType, parent);
            string path = elementToParse.Value;
            Path = path;
            FileName = System.IO.Path.GetFileNameWithoutExtension(path);
            if (Parent.Parent.Parent.Installed)
            {
                typeAttr = elementToParse.Attribute(RMPConstants.ATTR_INSTALLED);
                if ((string)typeAttr != null)
                    InstallationStatus = Pacman.Helper.GetInstallStatus(typeAttr.Value);
            }
        }

       

       
        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_FILE, Path);
            newElem.SetAttributeValue(RMPConstants.ATTR_TYPE, ImageType.ToXMLString());
            if (Parent.Parent.Parent.Installed && InstallationStatus == InstallStatus.Installed)
                newElem.SetAttributeValue(RMPConstants.ATTR_INSTALLED, "Y");
            return newElem;
        }

        public override string GetTreeViewPrefixString()
        {
            if (ImageType == ImageTypes.None)
                return "[Unspecified] ";
            else
                return "[" + ImageType.ToXMLString() + "] ";
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMCharImageFile Clone(RMCharImageGroup parent)
        {
            RMCharImageFile clone = new RMCharImageFile(parent);
            clone.FileName = FileName;
            clone.ImageType = ImageType;
            clone.InstallationStatus = InstallationStatus;
            clone.InternalFileName = InternalFileName;
            clone.Path = Path;
            clone._path = _path;
            return clone;
        }
    }
}
