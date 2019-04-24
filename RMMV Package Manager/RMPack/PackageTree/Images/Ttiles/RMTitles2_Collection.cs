using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMTitles2_Collection : RMSingleFileCollection
    {
        public RMTitles2_Collection(RMPackage parent) : base(parent)
        {

        }
        public RMTitles2_Collection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
       
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.TITLES_2;
            return newElem;

        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMTitles2_Collection Clone(RMPackage parent)
        {
            RMTitles2_Collection clone = new RMTitles2_Collection(parent);
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
