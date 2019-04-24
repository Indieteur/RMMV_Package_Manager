using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMGenPart : RMPackGroup
    {
        public enum GenPartType
        {
            Accessory_A,
            Accessory_B,
            Beast_Ears,
            Beard,
            Body,
            Cloak,
            Clothing,
            Ears,
            Eyebrows,
            Eyes,
            Face,
            Facial_Mark,
            Front_Hair,
            Glasses,
            Mouth,
            Nose,
            Rear_Hair,
            Tail,
            Wing,
            None
        }
        [Flags]
        public enum eGender
        {
            None = 0,
            Male = 1,
            Female = 2,
            Kid = 4,
        }
        public eGender Gender { get; set; }
        public List<RMGenFile> Files { get; set; } = new List<RMGenFile>();
        public RMGeneratorCollection Parent { get; set; }
        public GenPartType PartType { get; set; }
        internal int implicitID { get; set; } //The number after the letter "p" in generator filenames.

        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Files, true); set => throw new NotImplementedException(); }


        public RMGenPart(RMGeneratorCollection parent)
        {
            Parent = parent;
        }

        public RMGenPart()
        {

        }

        public static RMGenPart TryParse(XElement elementToParse, RMGeneratorCollection parent)
        {
            GenPartType parsedValue = GenPartType.None;
            parsedValue = parsedValue.ParseXMLString(elementToParse.Name.LocalName);
            if (parsedValue == GenPartType.None)
                return null;
            else
                return new RMGenPart(elementToParse, parent, parsedValue);
        }

        protected RMGenPart(XElement elementToParse, RMGeneratorCollection parent, GenPartType genPartType)
        {
            Parent = parent;
            PartType = genPartType;
            IEnumerable<XAttribute> attributes = elementToParse.Attributes();
            if (attributes == null || attributes.Count() == 0)
                throw new InvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_GENDER, InvalidGeneratorPartException.WhichInvalid.NoGender, parent);
            foreach (XAttribute attr in attributes)
            {
                if (attr.Name.LocalName == RMPConstants.ATTR_NAME)
                    Name = attr.Value;
                else if (attr.Name.LocalName == RMPConstants.ATTR_GENDER)
                {
                    Gender = Gender.ParseXMLString(attr.Value);
                    if (Gender == eGender.None)
                        throw new InvalidGeneratorPartException(ExceptionMessages.RMPackage.GEN_FILE_NO_GENDER, InvalidGeneratorPartException.WhichInvalid.GenderInvalid, parent);
                }
            }



            IEnumerable<XElement> partFiles = elementToParse.Elements();
           
                foreach (XElement file in partFiles) 
                    if (file.Name.LocalName == RMPConstants.FIELD_FILE)
                        Files.Add(new RMGenFile(file, this));
                 
        }




        public List<RMPackFile> RetrieveAllFiles()
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMGenFile file in Files)
                files.Add(file);
            return files;
        }

        public void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Files == null || Files.Count == 0)
                return;
            foreach (RMGenFile file in Files)
                file.InstallationStatus = status;
        }


        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(PartType.ToParsableXMLString());
            newElem.SetAttributeValue(RMPConstants.ATTR_NAME, Name);
            newElem.SetAttributeValue(RMPConstants.ATTR_GENDER, Gender.ToParsableXMLString());
            if (Files != null && Files.Count > 0)
            {
                foreach (RMGenFile file in Files)
                {
                    newElem.Add(file.ToXMLElement());
                }
            }
            return newElem;
        }

        public RMGenFile FindFileWithPath(string path)
        {
            if (Files != null)
            {
                foreach (RMGenFile file in Files)
                {
                    if (!string.IsNullOrWhiteSpace(file.Path) && file.Path.ToLower() == path.ToLower())
                        return file;
                }
            }
            return null;
        }

        public string GetTreeNodePrefixString()
        {
            string partType;
            if (PartType == GenPartType.None)
                partType = "Unspecified";
            else
                partType = PartType.ToProperName();

            if (Gender == eGender.None)
                return "[" + partType + "] ";
            return "[" + Gender.ToProperName() + " - " + partType + "] ";
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMGenPart Clone(RMGeneratorCollection parent)
        {
            RMGenPart clone = new RMGenPart(parent);
            if (Files != null)
            {
                foreach (RMGenFile file in Files)
                {
                    clone.Files.Add(file.Clone(clone));
                }
            }
            clone.Gender = Gender;
            clone.implicitID = implicitID;
            clone.internalName = internalName;
            clone.Name = Name;
            clone.PartType = PartType;
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMGenFile file in Files)
            {
                boolAndRMFileList.Add(file.FileExists(rootDir));
            }
            return boolAndRMFileList;
        }

        public override bool IsGroupEmpty()
        {
            return (Files == null || Files.Count == 0);
        }
    }
}
