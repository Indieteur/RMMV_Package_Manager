using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMSingleFile : RMPackFile
    {

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
        internal string InternalFileName { get; set; }

        public override InstallStatus InstallationStatus { get; set; }

        public string FileName { get; set; }
        public RMSingleFileCollection Parent { get; set; }

       
        protected RMSingleFile()
        {

        }
        public RMSingleFile(RMSingleFileCollection parent)
        {
            Parent = parent;
        }
        public RMSingleFile(XElement whichElement, RMSingleFileCollection parent)
        {
            Parent = parent;
            string path = whichElement.Value;
            Path = path;
            FileName = System.IO.Path.GetFileNameWithoutExtension(path);
            if (parent.Parent.Installed)
            {
                XAttribute installedAttr = whichElement.Attribute(RMPConstants.ATTR_INSTALLED);
                if ((string)installedAttr != null)
                    InstallationStatus = Pacman.Helper.GetInstallStatus(installedAttr.Value);

            }
        }
        
        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_FILE, Path);
            if (Parent.Parent.Installed && InstallationStatus == InstallStatus.Installed)
                newElem.SetAttributeValue(RMPConstants.ATTR_INSTALLED, "Y");
            return newElem;
        }

        public override string GetTreeViewPrefixString()
        {
            return string.Empty;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMSingleFile Clone(RMSingleFileCollection parent)
        {
            RMSingleFile clone = new RMSingleFile(parent);
            clone.FileName = FileName;
            clone.InstallationStatus = InstallationStatus;
            clone.InternalFileName = InternalFileName;
            clone.Path = Path;
            clone._path = _path;
            return clone;
        }
    }
}
