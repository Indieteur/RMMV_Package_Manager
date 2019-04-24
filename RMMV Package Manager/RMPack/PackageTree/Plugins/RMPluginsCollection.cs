using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public class RMPluginsCollection : RMSingleFileCollection
    {
        public RMPluginsCollection(RMPackage parent) : base(parent)
        {

        }
        public RMPluginsCollection(XElement elementToParse, RMPackage parent) : base(elementToParse, parent)
        {

        }
       
        public override XElement ToXMLElement()
        {
            XElement newElem = GetNoNameXElement();
            newElem.Name = RMPConstants.Collections.PLUGINS;
            return newElem;

        }
        public override RMPackObject Clone()
        {
            return Clone(Parent);
        }
        public RMPluginsCollection Clone(RMPackage parent)
        {
            RMPluginsCollection clone = new RMPluginsCollection(parent);
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
