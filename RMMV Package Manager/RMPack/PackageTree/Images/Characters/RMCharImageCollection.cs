using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMCharImageCollection : RMCollection
    {
        public List<RMCharImageGroup> Groups { get; set; } = new List<RMCharImageGroup>();

        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Groups); set => throw new NotImplementedException(); }

        public RMCharImageCollection(RMPackage parent): base(parent)
        {

        }
        public RMCharImageCollection(XElement elementToParse, RMPackage parent) : base(parent)
        {
          
            IEnumerable<XElement> children = elementToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_CHARACTER)
                    Groups.Add(new RMCharImageGroup(child, this));
            }
        }

        public override List<RMPackFile> RetrieveAllFiles()
        {
            if (Groups == null || Groups.Count == 0)
                return null;

            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMCharImageGroup characterImage in Groups)
            {
                List<RMPackFile> retrievedFiles = characterImage.RetrieveAllFiles();
                if (retrievedFiles != null && retrievedFiles.Count > 0)
                    files.AddRange(retrievedFiles);
            }
            return files;
        }


        public override void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Groups == null || Groups.Count == 0)
                return;
            foreach (RMCharImageGroup image in Groups)
                image.SetInstalledPropertyAll(status);
        }

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.Collections.CHARACTERS);
            if (Groups != null && Groups.Count > 0)
            {
                foreach (RMCharImageGroup imageNode in Groups)
                {
                    if (!imageNode.IsGroupEmpty())
                    newElem.Add(imageNode.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMCharImageCollection Clone(RMPackage parent)
        {
            RMCharImageCollection clone = new RMCharImageCollection(parent);
            if (Groups != null)
            {
                foreach (RMCharImageGroup node in Groups)
                {
                    clone.Groups.Add(node.Clone(clone));
                }
            }
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Groups == null || Groups.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMCharImageGroup imageGroup in Groups)
            {
                List<BoolAndRMFile> outRes = imageGroup.CheckFileExistences(rootDir);
                boolAndRMFileList.AddRange(outRes);
            }

            return boolAndRMFileList;
        }
    }
}
