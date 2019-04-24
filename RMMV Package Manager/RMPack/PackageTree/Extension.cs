using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMPackExtensions
    {
        public enum RetrievePathMode
        {
            Normal,
            IgnoreRootedPaths,
            IgnoreNonRootedPath
        }
        public static List<string> RetrievePathsOnly(this IEnumerable<RMPackFile> files, RetrievePathMode mode)
        {
            if (files == null || files.Count() == 0)
                return null;

            List<string> retVal = new List<string>(files.Count());

            for (int i = 0; i < files.Count(); ++i)
            {
                RMPackFile item = files.ElementAt(i);
                if (mode == RetrievePathMode.Normal)
                    retVal.Add(item.Path);
                else if (mode == RetrievePathMode.IgnoreNonRootedPath && item.NonRootedPath == false)
                    retVal.Add(item.Path);
                else if (mode == RetrievePathMode.IgnoreRootedPaths && item.NonRootedPath)
                    retVal.Add(item.Path);

            }
            return retVal;
        }
    }
}
