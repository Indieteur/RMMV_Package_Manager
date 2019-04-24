using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    
    public class RMGeneratorCollection : RMCollection
    {
        public List<RMGenPart> Parts { get; set; } = new List<RMGenPart>();
        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Parts); set => throw new NotImplementedException(); }

        public RMGeneratorCollection(XElement elementToParse, RMPackage package) : base(package)
        {
            IEnumerable<XElement> childElements = elementToParse.Elements();
            foreach (XElement elem in childElements)
            {
                RMGenPart rmgp = RMGenPart.TryParse(elem, this);
                if (rmgp != null)
                    Parts.Add(rmgp);
            }
        }
        public RMGeneratorCollection(RMPackage package) : base(package)
        {

        }

        public override List<RMPackFile> RetrieveAllFiles()
        {
            if (Parts == null || Parts.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMGenPart genPart in Parts)
            {
                List<RMPackFile> retrievedPaths = genPart.RetrieveAllFiles();
                if (retrievedPaths != null && retrievedPaths.Count > 0)
                    files.AddRange(retrievedPaths);
            }
            return files;
        }
        public override void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Parts == null || Parts.Count == 0)
                return;
            foreach (RMGenPart part in Parts)
                part.SetInstalledPropertyAll(status);
        }

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.Collections.GENERATOR);
            if (Parts != null && Parts.Count > 0)
            {
                foreach (RMGenPart part in Parts)
                {
                    if (!part.IsGroupEmpty())
                    newElem.Add(part.ToXMLElement());
                }
            }
            return newElem;
        }
        public RMGenFile FindFileWithPath(string path)
        {
            if (Parts != null)
            {
                foreach (RMGenPart part in Parts)
                {
                    RMGenFile foundFile = part.FindFileWithPath(path);
                    if (foundFile != null)
                        return foundFile;
                }
            }
            return null;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMGeneratorCollection Clone(RMPackage parent)
        {
            RMGeneratorCollection clone = new RMGeneratorCollection(parent);
            if (Parts != null)
            {
                foreach (RMGenPart part in Parts)
                {
                    clone.Parts.Add(part.Clone(clone));
                }
            }
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Parts == null || Parts.Count == 0)
                return null;
            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMGenPart part in Parts)
            {
                List<BoolAndRMFile> outRes = part.CheckFileExistences(rootDir);
                boolAndRMFileList.AddRange(outRes);
            }

            return boolAndRMFileList;
        }
    }
}
