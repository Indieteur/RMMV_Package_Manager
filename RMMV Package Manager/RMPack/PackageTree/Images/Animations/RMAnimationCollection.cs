using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMAnimationCollection : RMSingleFileCollection
    {
        public RMAnimationCollection(RMPackage parent) : base(parent)
        {

        }
        public RMAnimationCollection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
      
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.ANIMATIONS;
            return newElem;

        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMAnimationCollection Clone(RMPackage parent)
        {
            RMAnimationCollection clone = new RMAnimationCollection(parent);
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
