using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public abstract class RMPackFile : RMPackObject
    {
        public abstract string GetTreeViewPrefixString();
        public abstract string Path { get; set; }
        public bool NonRootedPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Path))
                    return false;
                return !System.IO.Path.IsPathRooted(Path);
            }
        }
        public BoolAndRMFile FileExists(string rootDir)
        {
            if (string.IsNullOrWhiteSpace(Path))
                return new BoolAndRMFile(this, false);

            if (string.IsNullOrWhiteSpace(rootDir))
                return new BoolAndRMFile(this, File.Exists(Path));
            
            return new BoolAndRMFile(this, File.Exists(rootDir + "\\" + Path));
        }
    }
}
