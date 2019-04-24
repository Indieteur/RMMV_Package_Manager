using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    
    public class RMPackLic
    {
        public enum Source
        {
            None = 0,
            URL,
            File,
            Text
        }
        public string Data { get; set; }
        public Source LicenseSource { get; set; }

        public bool NonRootedLicenseFile
        {
            get
            {
                if (LicenseSource != Source.File || string.IsNullOrWhiteSpace(Data))
                    return false;
                return !Path.IsPathRooted(Data);
            }
        }


        public RMPackLic(Source type, string data)
        {
            LicenseSource = type;
            Data = data;
        }

        public RMPackLic(XElement elementToParse, RMPackage owningPackage)
        {
            XAttribute tempAttr = elementToParse.Attribute(RMPConstants.ATTR_SOURCE);
            if ((string)tempAttr == null)
                throw new InvalidPackageLicenseException(ExceptionMessages.RMPackage.LIC_SOURCE_NOT_SET + owningPackage.Name, InvalidPackageLicenseException.WhichInvalid.NoSource, owningPackage);
            LicenseSource = Source.None;
            switch (tempAttr.Value)
            {
                case RMPConstants.LicenseSource.File:
                    LicenseSource = Source.File;
                    break;
                case RMPConstants.LicenseSource.Text:
                    LicenseSource = Source.Text;
                    break;
                case RMPConstants.LicenseSource.URL:
                    LicenseSource = Source.URL;
                    break;
            }
            if (LicenseSource == Source.Text)
                Data = elementToParse.Value.ParseXMLString();
            else
             Data = elementToParse.Value;
            if (LicenseSource == Source.None)
                throw new InvalidPackageLicenseException(ExceptionMessages.RMPackage.LIC_SOURCE_INVALID + owningPackage.Name, InvalidPackageLicenseException.WhichInvalid.InvalidSourceType, owningPackage);
            else if (LicenseSource == Source.URL && !Data.IsAValidURL())
                throw new InvalidPackageLicenseException(ExceptionMessages.RMPackage.InvalidLicURL(owningPackage.Name), InvalidPackageLicenseException.WhichInvalid.InvalidLicenseURL, owningPackage);
            else if (LicenseSource == Source.File && !IsAValidLicenseSourceFile(Data, owningPackage.XMLDirectory))
                throw new InvalidPackageLicenseException(ExceptionMessages.RMPackage.InvalidLicFile(owningPackage.Name), InvalidPackageLicenseException.WhichInvalid.InvalidLicenseFile, owningPackage);
        }

        public static bool IsAValidLicenseSourceFile(string strToCheck, string relPath)
        {
            if (string.IsNullOrWhiteSpace(strToCheck))
                return false;
            try
            {
                if (File.Exists(relPath + "\\" + strToCheck))
                    return HasAValidLicFileExtension(Path.GetExtension(strToCheck));
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool HasAValidLicFileExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return false;
            extension = extension.ToLower();
            if (extension == RMPConstants.LicenseFileExtensions.DOT_DOC || extension == RMPConstants.LicenseFileExtensions.DOT_DOCX || extension == RMPConstants.LicenseFileExtensions.DOT_PDF
                 || extension == RMPConstants.LicenseFileExtensions.DOT_RTF || extension == RMPConstants.LicenseFileExtensions.DOT_TXT)
                return true;
            return false;
        }

        string SourceToString(Source src)
        {
            switch (src)
            {
                case Source.File:
                    return RMPConstants.LicenseSource.File;
                case Source.Text:
                    return RMPConstants.LicenseSource.Text;
                case Source.URL:
                    return RMPConstants.LicenseSource.URL;
                default:
                    return string.Empty;
            }
        }
        public override string ToString()
        {
            return ToXElement().ToString();
        }

        public XElement ToXElement()
        {
            string DataVal = (LicenseSource == Source.Text) ? Data.ToXMLString() : Data;
            XElement newElem = new XElement(RMPConstants.FIELD_LICENSE, DataVal);
            newElem.SetAttributeValue(RMPConstants.ATTR_SOURCE, SourceToString(LicenseSource));
            return newElem;
        }

        public RMPackLic Clone()
        {
            RMPackLic clone = new RMPackLic(LicenseSource, Data);
            return clone;
        }
        
    }
    
}
