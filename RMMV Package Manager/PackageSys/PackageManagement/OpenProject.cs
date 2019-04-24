using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
  
    static partial class PackageManagement
    {
        public delegate void ProjectPackMan_Action(ProjectPackMan whichProject);


       

        public static event ProjectPackMan_Action OnOpenProject;
        public static event ProjectPackMan_Action OnCloseProject;

        static ProjectPackMan curOpenProj;
        public static ProjectPackMan OpenedProject
        {
            get
            {
                return curOpenProj;
            }
            private set
            {
                string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
                if (curOpenProj != null && OnCloseProject != null)
                {
                    Logger.WriteInformationLog(LoggerMessages.PackageManagement.OpenProject.Information.ClosedProj(curOpenProj.DirectoryPath), _namespace);
                    OnCloseProject.Invoke(curOpenProj);
                }
                curOpenProj = value;
                if (value != null && OnOpenProject != null)
                {
                    Logger.WriteInformationLog(LoggerMessages.PackageManagement.OpenProject.Information.OpenedProj(value.DirectoryPath), _namespace);
                    OnOpenProject.Invoke(curOpenProj);
                }
            }
        }

        public static void OpenProject(string pathToProjFile, string _namespace, out LogDataList log)
        {
            OpenedProject = new ProjectPackMan(Path.GetDirectoryName(pathToProjFile), _namespace, out log);
        }

        public static void CloseProject()
        {
            OpenedProject = null;
        }
    }
}
