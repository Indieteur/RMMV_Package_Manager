using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public static partial class Uninstaller
        {
            public static class Generator
            {
                public static UninstallResult UninstallGeneratorParts(string whereFrom, string _namespace, RMGeneratorCollection collection)
                {
                    if (collection == null || collection.Parts == null)
                        return UninstallResult.normal;
                    List<RMPackFile> files = collection.RetrieveAllFiles();
                    UninstallResult retVal = UninstallResult.normal;
                    if (files != null)
                    {
                        foreach (RMPackFile file in files)
                        {
                            if (string.IsNullOrWhiteSpace(file.Path))
                                continue;
                            string concatPath = whereFrom + "\\" + file.Path;
                            if (File.Exists(concatPath))
                            {
                                Exception ex = null;
                                if (Helper.DeleteFileSafely(concatPath, _namespace, out ex, LoggerMessages.GeneralError.DELETE_UNUSED_FILE_FAILED_ARG) == DeleteFileResult.UserCancelled)
                                    throw ex;

                                retVal = UninstallResult.genPartsRemoved;
                            }
                        }
                    }

                    return retVal;

                }
            }
        }
    }
}
