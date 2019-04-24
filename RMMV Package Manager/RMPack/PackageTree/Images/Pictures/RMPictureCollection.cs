using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMPictureCollection : RMSingleFileCollection
    {
        public RMPictureCollection(RMPackage parent) : base(parent)
        {

        }
        public RMPictureCollection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
      
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.PICTURES;
            return newElem;

        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMPictureCollection Clone(RMPackage parent)
        {
            RMPictureCollection clone = new RMPictureCollection(parent);
            if (Files != null)
            {
                foreach (RMSingleFile file in Files)
                {
                    clone.Files.Add(file.Clone(clone));
                }
            }
            clone.Name = Name;
            return clone;
        }

    }
}
