using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    public abstract class RMPackObject
    {
        public enum InstallStatus
        {
            Installed = 3,
            Partial =2,
            NotInstalled = 1,
            NotApplicable = 0
        }

        public abstract InstallStatus InstallationStatus { get; set; } 

        protected InstallStatus GetInstallStatusByListCheck(IEnumerable<RMPackObject> list, bool dualMode = false)
        {
            InstallStatus retVal = InstallStatus.NotApplicable;
            if (list != null && list.Count() > 0)
            {
                foreach (RMPackObject obj in list)
                {
                    if (dualMode)
                    {
                        if (retVal == InstallStatus.NotApplicable)
                            retVal = obj.InstallationStatus;
                        else if (obj.InstallationStatus == InstallStatus.Installed && retVal == InstallStatus.NotInstalled)
                            return InstallStatus.Partial;
                        else if (obj.InstallationStatus == InstallStatus.NotInstalled && retVal == InstallStatus.Installed)
                            return InstallStatus.Partial;
                    }
                    else
                    {
                        if (retVal == InstallStatus.NotApplicable)
                        {
                            retVal = obj.InstallationStatus;
                        }
                        else if (retVal == InstallStatus.NotInstalled)
                        {
                            if (obj.InstallationStatus == InstallStatus.Installed || obj.InstallationStatus == InstallStatus.Partial)
                                return InstallStatus.Partial;
                        }
                        else if (retVal == InstallStatus.Installed)
                        {
                            if (obj.InstallationStatus == InstallStatus.NotInstalled || obj.InstallationStatus == InstallStatus.Partial)
                                return InstallStatus.Partial;
                        }
                    }
                }
            }
            return retVal;
        }

        public override string ToString()
        {
            return ToXMLElement().ToString();
        }

       
        public abstract XElement ToXMLElement();

        public abstract RMPackObject Clone();
    }
}
