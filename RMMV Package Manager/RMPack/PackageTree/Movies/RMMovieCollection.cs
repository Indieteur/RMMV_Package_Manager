using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMMovieCollection : RMCollection
    {
       
        public List<RMMovieGroup> Groups { get; set; } = new List<RMMovieGroup>();

        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Groups); set => throw new NotImplementedException(); }

        public RMMovieCollection(RMPackage parent) : base(parent)
        {
        }

        public RMMovieCollection(XElement elementToParse, RMPackage parent) : base(parent)
        {
            IEnumerable<XElement> children = elementToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_MOVIE)
                    Groups.Add(new RMMovieGroup(child, this));
            }
        }

        public override List<RMPackFile> RetrieveAllFiles()
        {
            if (Groups == null || Groups.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach(RMMovieGroup movie in Groups)
            {
                List<RMPackFile> retrievedFiles = movie.RetrieveAllFiles();
                if (retrievedFiles != null && retrievedFiles.Count > 0)
                    files.AddRange(retrievedFiles);
            }
            return files;
        }


        public override void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Groups == null || Groups.Count == 0)
                return;
            foreach (RMMovieGroup movie in Groups)
                movie.SetInstalledPropertyAll(status);
        }



        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.Collections.MOVIES);
            if (Groups != null && Groups.Count > 0)
            {
                foreach (RMMovieGroup movie in Groups)
                {
                    if (!movie.IsGroupEmpty())
                    newElem.Add(movie.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMMovieCollection Clone(RMPackage parent)
        {
            RMMovieCollection clone = new RMMovieCollection(parent);
            if (Groups != null)
            {
                foreach (RMMovieGroup movie in Groups)
                {
                    clone.Groups.Add(movie.Clone(clone));
                }
            }
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Groups == null || Groups.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMMovieGroup movie in Groups)
            {
                List<BoolAndRMFile> outRes = movie.CheckFileExistences(rootDir);
                boolAndRMFileList.AddRange(outRes);
            }

            return boolAndRMFileList;
        }
    }
}
