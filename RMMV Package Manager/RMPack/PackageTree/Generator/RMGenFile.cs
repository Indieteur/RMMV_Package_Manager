using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMGenFile : RMPackFile
    {
        [Flags]
        public enum FileNameInfo
        {
            Type = 1,
            BaseOrder = 2,
            Position = 4,
            Order = 8,
            Colour = 16,
            IsAcFile = 32,
            Waiting = 64
        }
        public enum GenFileType
        {
            None,
            Face,
            SV,
            SV_C,
            TV,
            TV_C,
            TVD,
            TVD_C,
            Var
        }

        [Flags]
        public enum GenFileFields
        {
            None = 0,
            GenFileType = 1,
            GenPartType = 2,
            Position = 4,
            BaseOrder = 8,
            Order = 16,
            Colour = 32
        }


        public RMGenPart Parent { get; set; }
        public int BaseOrder { get; set; } = 0;
        public int Order { get; set; } = 0;
        public int Colour { get; set; } = 0;
        public GenFileType FileType { get; set; }

        public override InstallStatus InstallationStatus { get; set; }

        string _path;
        public override string Path
        {
            get => _path;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _path = value;
                    return;
                }
                if (value.StartsWith("\\"))
                {
                    if (value.Length > 1)
                        _path = value.Substring(1);
                }
                else
                    _path = value;
            }
        }


        public RMGenFile()
        {

        }

        public RMGenFile(RMGenPart parent)
        {
            Parent = parent;
        }
        

        public RMGenFile (XElement elementToParse, RMGenPart parent)
        {
            Parent = parent;
            IEnumerable<XAttribute> attributes = elementToParse.Attributes();
            if (attributes == null || attributes.Count() == 0)
                throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_TYPE, InvalidGeneratorPartFileException.WhichInvalid.NoType, parent);
            foreach (XAttribute attr in attributes)
            {
                if (attr.Name.LocalName == RMPConstants.ATTR_TYPE)
                {
                    FileType = Pacman.Helper.ParseFileType(attr.Value);
                    if (FileType == GenFileType.None)
                        throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_TYPE, InvalidGeneratorPartFileException.WhichInvalid.TypeInvalid, parent);
                }
                else if (attr.Name.LocalName == RMPConstants.ATTR_ORDER)
                {
                    
                    int tInt;
                    if (!int.TryParse(attr.Value, out tInt))
                    {
                        throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_LOW_ORDER, InvalidGeneratorPartFileException.WhichInvalid.OrderInvalid, parent);
                    }
                    Order = tInt;
                }
                else if (attr.Name.LocalName == RMPConstants.ATTR_COLOUR)
                {
                    int tInt;
                    if (!int.TryParse(attr.Value, out tInt))
                        throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_COLOUR, InvalidGeneratorPartFileException.WhichInvalid.ColourInvalid, parent);
                    Colour = tInt;
                }
                else if (attr.Name.LocalName == RMPConstants.ATTR_BASEORDER)
                {
                    int tInt;
                    if (!int.TryParse(attr.Value, out tInt))
                        throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_NO_HIGH_ORDER, InvalidGeneratorPartFileException.WhichInvalid.InvalidBaseOrder, parent);
                    BaseOrder = tInt;
                }
                else if (Parent.Parent.Parent.Installed && attr.Name.LocalName == RMPConstants.ATTR_INSTALLED)
                    InstallationStatus = Pacman.Helper.GetInstallStatus(attr.Value);

            }

            if (string.IsNullOrWhiteSpace(elementToParse.Value))
                throw new InvalidGeneratorPartFileException(ExceptionMessages.RMPackage.GEN_FILE_PATH_NULL, InvalidGeneratorPartFileException.WhichInvalid.PathNotSet, parent);
            Path = elementToParse.Value;
           
        }

       

       

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_FILE, Path);
            newElem.SetAttributeValue(RMPConstants.ATTR_TYPE, FileType.ToParsableString());
            if (Order > 0)
                newElem.SetAttributeValue(RMPConstants.ATTR_ORDER, Order.ToString());
            if (Colour >0)
                newElem.SetAttributeValue(RMPConstants.ATTR_COLOUR, Colour.ToString());
            if (BaseOrder > 0)
                newElem.SetAttributeValue(RMPConstants.ATTR_BASEORDER, BaseOrder.ToString());
            if (Parent.Parent.Parent.Installed && InstallationStatus == InstallStatus.Installed)
                newElem.SetAttributeValue(RMPConstants.ATTR_INSTALLED, "Y");
            return newElem;
        }

        public string RetrieveInstallFileName(int position, bool throwErrorOnException = true)
        {
            GenFileFields fieldsInvalid = HasValidFields(position);
            if (fieldsInvalid != GenFileFields.None)
            {
                if (throwErrorOnException)
                    throw new InvalidGeneratorFileFieldsException(fieldsInvalid, ExceptionMessages.RMGenFile.RetrieveInstallFileName.Error.InvalidFieldsDetected(fieldsInvalid));
                return null;
            }
            StringBuilder sb = new StringBuilder(FileType.ToFilePrefix() + RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR + Parent.PartType.ToParsableXMLString());
            if (BaseOrder > 0)
                sb.Append(BaseOrder);
            sb.Append(FormatPosition(position));
            if (Order > 0)
                sb.Append("_c" + Order);
            if (Colour > 0)
                sb.Append(FormatColour(Colour));
            if (FileType.IsACFile())
                sb.Append("_c");
            sb.Append("." + RMPConstants.MiscFileExtensions.PNG);
            return sb.ToString();
        }


        public static string FormatPosition(int pos)
        {
            
            string retString = "_p";
            if (pos < 10)
                retString += "0" + pos;
            else
                retString += pos;
            return retString;
        }
        public static string FormatColour(int colour)
        {
            string retString = "_m";
            if (colour < 10)
                retString += "00" + colour;
            else if (colour < 100)
                retString += "0" + colour;
            else
                retString += colour;
            return retString;
        }

        public GenFileFields HasValidFields(int position = 0)
        {
            GenFileFields fieldsInvalid = GenFileFields.None;
            if (FileType == GenFileType.None)
                fieldsInvalid = fieldsInvalid | GenFileFields.GenFileType;
            if (Parent == null || Parent.PartType == RMGenPart.GenPartType.None)
                fieldsInvalid = fieldsInvalid | GenFileFields.GenPartType;
            if (position <= 0)
                fieldsInvalid = fieldsInvalid | GenFileFields.Position;
            return fieldsInvalid;
        }

        public override string GetTreeViewPrefixString()
        {
            if (FileType == GenFileType.None)
                return "[Unspecified]";
            else
                return "[" + FileType.ToParsableString() + "] ";
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMGenFile Clone(RMGenPart parent)
        {
            RMGenFile clone = new RMGenFile(parent);
            clone.BaseOrder = BaseOrder;
            clone.Colour = Colour;
            clone.FileType = FileType;
            clone.InstallationStatus = InstallationStatus;
            clone.Order = Order;
            clone.Path = Path;
            clone._path = _path;
            return clone;
        }

        public class InvalidGeneratorFileFieldsException : Exception
        {
            
            public GenFileFields InvalidFields { get; set; }
            public InvalidGeneratorFileFieldsException()
            {

            }
            public InvalidGeneratorFileFieldsException(GenFileFields whichFields) 
            {
                InvalidFields = whichFields;
                
            }
            public InvalidGeneratorFileFieldsException(GenFileFields whichFields, string message) : base (message)
            {
                InvalidFields = whichFields;
            }
        }
    }
}
