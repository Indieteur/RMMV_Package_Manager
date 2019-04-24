using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMTitles1_Collection : RMSingleFileCollection
    {
        public RMTitles1_Collection(RMPackage parent) : base(parent)
        {

        }
        public RMTitles1_Collection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
     
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.TITLES_1;
            return newElem;

        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMTitles1_Collection Clone(RMPackage parent)
        {
            RMTitles1_Collection clone = new RMTitles1_Collection(parent);
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
