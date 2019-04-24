using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMParallaxCollection : RMSingleFileCollection
    {
        public RMParallaxCollection(RMPackage parent) : base(parent)
        {

        }
        public RMParallaxCollection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
      
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.PARALLAXES;
            return newElem;

        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMParallaxCollection Clone(RMPackage parent)
        {
            RMParallaxCollection clone = new RMParallaxCollection(parent);
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
