using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class PackSysExtensionIEnumerable
    {
        public static InstalledPackage FindByUID(this IEnumerable<InstalledPackage> packages, string UniqueID)
        {
            if (packages == null)
                return null;
            foreach(InstalledPackage package in packages)
            {
                if (package.Package != null && !string.IsNullOrWhiteSpace(package.Namespace) && package.Namespace == UniqueID.ToLower())
                    return package;
            }
            return null;
        }
        public static InstalledPackage FindByPath(this IEnumerable<InstalledPackage> packages, string path)
        {
            if (packages == null)
                return null;
            path = Path.GetDirectoryName(path).ToLower();

            foreach (InstalledPackage package in packages)
            {
                if (package.Directory.ToLower() == path)
                    return package;
            }
            return null;
        }
    }
}
