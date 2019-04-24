using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMDataCollection : RMSingleFileCollection
    {
        public RMDataCollection(RMPackage parent) : base(parent)
        {
        }
        public RMDataCollection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {
            
        }
       
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.DATA;
            return newElem;
            
        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMDataCollection Clone(RMPackage parent)
        {
            RMDataCollection clone = new RMDataCollection(parent);
            if (Files != null)
            {
                foreach(RMSingleFile file in Files)
                {
                    clone.Files.Add(file.Clone(clone));
                }
            }
            clone.Name = Name;
            return clone;
        }
        
    }
}
