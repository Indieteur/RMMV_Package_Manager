using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMMovieGroup : RMPackGroup
    {
        public List<RMMovieFile> Files { get; set; } = new List<RMMovieFile>();
        public RMMovieCollection Parent { get; set; }

        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Files, true); set => throw new NotImplementedException(); }

        public RMMovieGroup(RMMovieCollection parent)
        {
            Parent = parent;
        }

        public RMMovieGroup(XElement whichToParse, RMMovieCollection parent)
        {
            Parent = parent;
            XAttribute attribute = whichToParse.Attribute(RMPConstants.ATTR_NAME);
            if ((string)attribute == null)
                throw new InvalidMovieException(ExceptionMessages.RMPackage.MOVIE_FILE_NO_NAME, InvalidMovieException.WhichInvalid.NoName, parent);
            Name = attribute.Value;
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidMovieException(ExceptionMessages.RMPackage.MOVIE_FILE_NO_NAME, InvalidMovieException.WhichInvalid.InvalidName, parent);
            IEnumerable<XElement> children = whichToParse.Elements();
            foreach (XElement child in children)
            {
                if (child.Name.LocalName == RMPConstants.FIELD_FILE)
                    Files.Add(new RMMovieFile(child, this));
            }
        }
      
        public List<RMPackFile> RetrieveAllFiles()
        {
            if (Files == null || Files.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            foreach (RMMovieFile file in Files)
                files.Add(file);
            return files;
        }


        public void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Files == null || Files.Count == 0)
                return;
            foreach (RMMovieFile movie in Files)
                movie.InstallationStatus = status;
        }

        public override XElement ToXMLElement()
        {
            XElement newElem = new XElement(RMPConstants.FIELD_MOVIE);
            newElem.SetAttributeValue(RMPConstants.ATTR_NAME, Name);
            if (Files != null && Files.Count > 0)
            {
                foreach (RMMovieFile movFile in Files)
                {
                    newElem.Add(movFile.ToXMLElement());
                }
            }
            return newElem;
        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }

        public RMMovieGroup Clone(RMMovieCollection parent)
        {
            RMMovieGroup clone = new RMMovieGroup(parent);
            if (Files != null)
            {
                foreach (RMMovieFile file in Files)
                {
                    clone.Files.Add(file.Clone(clone));
                }
            }
            clone.internalName = internalName;
            clone.Name = Name;
            return clone;
        }

        public override List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            if (Files == null || Files.Count == 0)
                return null;

            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            foreach (RMMovieFile file in Files)
                boolAndRMFileList.Add(file.FileExists(rootDir));

            return boolAndRMFileList;
        }

        public override bool IsGroupEmpty()
        {
            return (Files == null || Files.Count == 0);
        }
    }
}
