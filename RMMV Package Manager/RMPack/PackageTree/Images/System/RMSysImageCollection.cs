using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMSysImageCollection : RMSingleFileCollection
    {
        public RMSysImageCollection(RMPackage parent) : base(parent)
        {

        }
        public RMSysImageCollection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
     
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.SYSTEM;
            return newElem;

        }

        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMSysImageCollection Clone(RMPackage parent)
        {
            RMSysImageCollection clone = new RMSysImageCollection(parent);
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
