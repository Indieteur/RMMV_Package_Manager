using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public abstract class RMPackGroup : RMPackObject
    {
        public string internalName;
        public string Name { get; set; }
        public abstract List<BoolAndRMFile> CheckFileExistences(string rootDir);

        public abstract bool IsGroupEmpty();
    }
}
