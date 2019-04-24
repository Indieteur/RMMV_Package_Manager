using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMTilesetFile : RMSingleFile
    {
        public enum eFileType
        {
            Text = 1,
            PNG,
            None = 0
        }
        public enum eAtlasType
        {
            A1_Anim = 1,
            A2_Ground,
            A3_Building,
            A4_Walls,
            A5_Normal,
            B_Atlas,
            C_Atlas,
            D_Atlas,
            E_Atlas,
            NotSpecified = 0
        }
        
        public eAtlasType AtlasType { get; set; }
        public eFileType FileType { get; set; }
        public new RMTilesetGroup Parent { get; set; }

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


        public RMTilesetFile(RMTilesetGroup parent)
        {
            Parent = parent;
        }

        public RMTilesetFile(XElement element, RMTilesetGroup parent)
        {
            Parent = parent;
            Path = element.Value;
            IEnumerable<XAttribute> attributes = element.Attributes();
            if (attributes == null || attributes.Count() == 0)
                throw new InvalidTilesetFileException(ExceptionMessages.RMPackage.TILESET_FILE_TYPE_NOT_SET, InvalidTilesetFileException.WhichInvalid.NoType, parent);

            foreach (XAttribute attribute in attributes)
            {
                if (attribute.Name.LocalName == RMPConstants.ATTR_TYPE)
                {
                    FileType = FileType.ParseString(attribute.Value);
                    if (FileType == eFileType.None)
                        throw new InvalidTilesetFileException(ExceptionMessages.RMPackage.TILESET_FILE_TYPE_NOT_SET, InvalidTilesetFileException.WhichInvalid.InvalidType, parent);
                }
                else if (attribute.Name.LocalName == RMPConstants.ATTR_ORDER)
                {
                    AtlasType = AtlasType.ParseString(attribute.Value);
                    if (AtlasType == eAtlasType.NotSpecified)
                        throw new InvalidTilesetFileException(ExceptionMessages.RMPackage.TILESET_FILE_ORDER_INVALID, InvalidTilesetFileException.WhichInvalid.InvalidOrder, parent);
                }
                else if (Parent.Parent.Parent.Installed && attribute.Name.LocalName == RMPConstants.ATTR_INSTALLED)
                    InstallationStatus = Pacman.Helper.GetInstallStatus(attribute.Value);
            }
            if (FileType == eFileType.None)
                throw new InvalidTilesetFileException(ExceptionMessages.RMPackage.TILESET_FILE_TYPE_NOT_SET, InvalidTilesetFileException.WhichInvalid.NoType, parent);
        }

        

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_FILE);
            newElem.Value = Path;
            newElem.SetAttributeValue(RMPConstants.ATTR_TYPE, FileType.ToXMLString());
            if (AtlasType != eAtlasType.NotSpecified)
                newElem.SetAttributeValue(RMPConstants.ATTR_ORDER, AtlasType.ToXMLString());
            if (Parent.Parent.Parent.Installed && InstallationStatus == InstallStatus.Installed)
                newElem.SetAttributeValue(RMPConstants.ATTR_INSTALLED, "Y");
            return newElem;
        }

        public override string GetTreeViewPrefixString()
        {
            string atlasType;
            if (AtlasType == eAtlasType.NotSpecified)
                atlasType = "Unspecified";
            else
                atlasType = AtlasType.ToXMLString();

            if (FileType == eFileType.None)
                return "[" + atlasType + "] ";
            else
                return "[" + atlasType + " - " + FileType.ToXMLString() + "] ";

        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMTilesetFile Clone(RMTilesetGroup parent)
        {
            RMTilesetFile clone = new RMTilesetFile(parent);
            clone.AtlasType = AtlasType;
            clone.FileName = FileName;
            clone.FileType = FileType;
            clone.InstallationStatus = InstallationStatus;
            clone.InternalFileName = InternalFileName;
            clone.Path = Path;
            clone._path = _path;
            return clone;
        }

    }
}
