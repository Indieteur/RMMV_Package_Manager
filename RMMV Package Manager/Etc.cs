using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
   public class BoolAndRMFile
   {
        public RMPackFile PackFile { get; set; }
        public bool Boolean { get; set; }
        public BoolAndRMFile(RMPackFile packFile, bool boolean)
        {
            PackFile = packFile;
            Boolean = boolean;
        }

       
    }
}
