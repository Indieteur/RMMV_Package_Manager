using Indieteur.BasicLoggingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public class InstalledPackage
    {
        public RMPackage Package { get; set; }
        public PackArchive PackageInstallArchive { get; set; }
        public string Namespace { get => Package.UniqueID.ToLower(); }
        public string FSFriendlyNamespace { get => Package.UniqueIDInMD5; }
        public string Name { get => Package.Name; }
        public string Directory { get; set; }
        public string XMLPath { get => Directory + "\\" + Vars.INSTALLED_XML_FILENAME; }
        //public bool Installed { get => Package.Installed; set => Package.Installed = value; }
        internal bool ChangesMade { get; set; } = false;


        public string ArchivePath
        {
            get
            {
                if (PackageInstallArchive != null)
                    return PackageInstallArchive.ArchivePath;
                return null;
            }
        }

        

        
        public InstalledPackage (string FolderPath, string _namespace, out LogDataList log)
        {
            log = new LogDataList();
            log.WriteInformationLog(LoggerMessages.PackageManagement.InstalledPackage.Information.START_RETRIEVE_INFO + FolderPath + ".", _namespace);
            Directory = FolderPath;
            if (!File.Exists(XMLPath))
                throw new FileNotFoundException(ExceptionMessages.PackageManagement.InstalledPackage.MISS_XML + XMLPath + ".");
            LogDataList logRes = null;
            try
            {
                Package = new RMPackage(XMLPath, _namespace, out logRes);
            }
            catch (Exception ex)
            {
                log.AppendLogs(logRes);
                throw new InvalidInstalledPackageFile(ExceptionMessages.PackageManagement.InstalledPackage.INVALID_XML + XMLPath + ".", XMLPath, ex);
            }
            log.AppendLogs(logRes);
            log.WriteInformationLog(LoggerMessages.PackageManagement.InstalledPackage.Information.SuccessReading(Package.Name, XMLPath), _namespace);

            if (File.Exists(Directory + "\\" + Vars.INSTALLED_ARCH_FILENAME))
                PackageInstallArchive = new PackArchive(this);
            else
                log.WriteWarningLog(LoggerMessages.PackageManagement.InstalledPackage.Warning.MissInstall(Package.Name, Directory + "\\" + Vars.INSTALLED_ARCH_FILENAME), _namespace);

        }

        public override string ToString()
        {
            try
            {
                return Name + " [" + Namespace + "]";
            }
            catch (Exception ex)
            {
                string _namespace = "UNKNOWN";
                if (ex.TargetSite != null)
                    _namespace = ex.TargetSite.ToLogFormatFullName();
                Logger.WriteWarningLog(LoggerMessages.PackageManagement.InstalledPackage.Warning.WARN_TOSTRING, _namespace, ex);
                return "Error reading package name and/or namespace!";
            }
        }

    }
    
    public class InvalidInstalledPackageFile : Exception
    {
        public string Path;
        public InvalidInstalledPackageFile(string message, string path, Exception innerException) : base(message, innerException)
        {
            Path = path;
        }
    }
}
