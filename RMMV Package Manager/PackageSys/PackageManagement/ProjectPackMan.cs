using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    static partial class PackageManagement
    {
        public class ProjectPackMan
        {
            public string DirectoryPath { get; set; }
            public List<InstalledPackage> InstalledPackages { get; private set; } = new List<InstalledPackage>();
            public ProjectPackMan(string ProjectDirectoryPath, string _namespace, out LogDataList log)
            {
                log = new LogDataList();
                if (!Directory.Exists(ProjectDirectoryPath))
                    throw new DirectoryNotFoundException(ExceptionMessages.General.DirNotFound(ProjectDirectoryPath));
                DirectoryPath = ProjectDirectoryPath;
                string packManStorePath = ProjectDirectoryPath + "\\" + Vars.PACKAGE_MANAGER_DIRECTORY;
                if (!Directory.Exists(packManStorePath))
                    return;

                string[] directories = Directory.GetDirectories(packManStorePath);
                if (directories == null || directories.Length == 0)
                    return;

                foreach (string directory in directories)
                {
                   
                    try
                    {
                        LogDataList outLog;
                        InstalledPackages.AddSafely(new InstalledPackage(directory, _namespace, out outLog));
                        log.AppendLogs(outLog);
                    }
                    catch (Exception ex)
                    {
                        log.WriteWarningLog(LoggerMessages.PackageManagement.ProjectPackMan.Warning.InvalidPackage(directory), _namespace, ex);
                    }
                }
            }
        }
    }
}
