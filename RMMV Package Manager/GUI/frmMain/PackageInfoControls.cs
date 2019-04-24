using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan.GUI
{
    public partial class PackageInfoControls
    {

        public Label lblName;
        public TextBox txtName;
        public Label lblVersion;
        public TextBox txtVersion;
        public Label lblAuthor;
        public TextBox txtAuthor;
        public Label lblLic;
        public LinkLabel linkLic;
        public Label lblPackURL;
        public LinkLabel linkPackURL;
        public Label lblDesc;
        public TextBox txtDesc;
        public ToolTip toolTip;
        public Control[] OtherControls;
        public ToolStripItem[] ToolStripItems;

        public virtual void ViewLicense(RMPackage instPackage)
        {

            frmMainBGWorker.ViewLicense(linkLic, instPackage);
        }



        public virtual void LoadInfoInstalledPackage(InstalledPackage package, string _namespace)
        {
            string packName;
            LoadInfoInstalledPackage(package, _namespace, out packName);
        }
        public virtual void LoadInfoInstalledPackage(InstalledPackage package, string _namespace, out string packNameOnErr)
        {
            packNameOnErr = string.Empty;

            if (package == null || package.Package == null)
            {
                SetEnableOnPackSelControls(false);
                return;
            }
            SetEnableOnPackSelControls(true);
            LoadInfoInstalledPackage(package.Package, _namespace, out packNameOnErr);

        }

        public virtual void LoadInfoInstalledPackage(RMPackage package, string _namespace)
        {
            string packName;
            LoadInfoInstalledPackage(package, _namespace, out packName);
        }
        public virtual void LoadInfoInstalledPackage(RMPackage package, string _namespace, out string packNameOnErr)
        {
            packNameOnErr = string.Empty;
            if (package == null)
            {
                SetEnableOnPackSelControls(false);
                return;
            }
            SetEnableOnPackSelControls(true);
            try
            {
                txtName.Text = package.Name;
                packNameOnErr = package.Name;
            }
            catch (Exception ex)
            {
                txtName.Text = "N/A";
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveField("Name", null), _namespace, ex);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(package.Version))
                    txtVersion.Text = "Unknown";
                else
                    txtVersion.Text = package.Version;
            }
            catch (Exception ex)
            {
                txtName.Text = "N/A";
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveField("Version", packNameOnErr), _namespace, ex);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(package.Author))
                    txtAuthor.Text = "Unknown";
                else
                    txtAuthor.Text = package.Author;
            }
            catch (Exception ex)
            {
                txtName.Text = "N/A";
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveField("Author", packNameOnErr), _namespace, ex);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(package.Description))
                    txtDesc.Text = "Unknown";
                else
                    txtDesc.Text = package.Description;
            }
            catch (Exception ex)
            {
                txtName.Text = "N/A";
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveField("Description", packNameOnErr), _namespace, ex);
            }

            try
            {
                if (string.IsNullOrWhiteSpace(package.URL))
                {
                    linkPackURL.Enabled = false;
                    linkPackURL.Text = "Not Specified";
                    linkPackURL.Tag = null;
                    toolTip.SetToolTip(linkPackURL, string.Empty);
                }
                else
                {
                    linkPackURL.Enabled = true;
                    linkPackURL.Text = "Visit Package URL";
                    linkPackURL.Tag = package.URL;
                    toolTip.SetToolTip(linkPackURL, package.URL);
                }

            }
            catch (Exception ex)
            {
                linkPackURL.Text = "N/A";
                linkPackURL.Enabled = false;
                toolTip.SetToolTip(linkPackURL, string.Empty);
                linkPackURL.Tag = null;
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveField("Package URL", packNameOnErr), _namespace, ex);
            }

            try
            {
                if (package.License == null)
                {
                    linkLic.Enabled = false;
                    linkLic.Text = "Not Specified";
                    linkLic.Tag = null;
                    toolTip.SetToolTip(linkLic, string.Empty);
                }
                else
                {
                    linkLic.Enabled = true;
                    linkLic.Text = "View License";
                    if (!string.IsNullOrWhiteSpace(package.License.Data))
                    {
                        if (package.License.LicenseSource == RMPackLic.Source.URL)
                        {
                            toolTip.SetToolTip(linkLic, package.License.Data);
                        }
                        else if (package.License.LicenseSource == RMPackLic.Source.File)
                        {
                            if (!string.IsNullOrWhiteSpace(package.XMLDirectory))
                                toolTip.SetToolTip(linkLic, package.XMLDirectory + "\\" + package.License.Data);
                            else
                                toolTip.SetToolTip(linkLic, package.License.Data);
                        }
                        else
                            toolTip.SetToolTip(linkLic, string.Empty);
                    }
                    else
                        toolTip.SetToolTip(linkLic, string.Empty);

                    linkLic.Tag = package.License;
                }
            }
            catch (Exception ex)
            {
                linkLic.Text = "N/A";
                linkLic.Enabled = false;
                toolTip.SetToolTip(linkLic, string.Empty);
                linkLic.Tag = null;
                Logger.WriteWarningLog(LoggerMessages.GUI.GlobalOrLocalSharedControls.Warning.ErrorRetrieveField("License", packNameOnErr), _namespace, ex);
            }


        }

        public virtual void SetEnableOnPackSelControls(bool enabled)
        {
            lblName.Enabled = enabled;
            txtName.Enabled = enabled;
            lblVersion.Enabled = enabled;
            txtVersion.Enabled = enabled;
            lblAuthor.Enabled = enabled;
            txtAuthor.Enabled = enabled;
            lblLic.Enabled = enabled;
            linkLic.Enabled = enabled;
            lblPackURL.Enabled = enabled;
            linkPackURL.Enabled = enabled;
            lblDesc.Enabled = enabled;
            txtDesc.Enabled = enabled;
            if (!enabled)
            {
                txtName.Text = string.Empty;
                txtAuthor.Text = string.Empty;
                txtDesc.Text = string.Empty;
                txtVersion.Text = string.Empty;
                linkLic.Text = "N/A";
                linkPackURL.Text = "N/A";
                toolTip.SetToolTip(linkPackURL, string.Empty);
                toolTip.SetToolTip(linkLic, string.Empty);
                linkPackURL.Tag = null;
                linkLic.Tag = null;
            }

            if (ToolStripItems != null)
            {
                foreach (ToolStripItem toolStripItem in ToolStripItems)
                {
                    toolStripItem.Enabled = enabled;
                }
            }

            if (OtherControls != null)
            {
                foreach (Control ctrl in OtherControls)
                {
                    ctrl.Enabled = enabled;
                    if (!enabled)
                    {
                        ctrl.Tag = null;
                        toolTip.SetToolTip(ctrl, string.Empty);
                        TextBox tBox = ctrl as TextBox;
                        if (tBox != null)
                        {
                            tBox.Text = string.Empty;
                            continue;
                        }
                        LinkLabel linkLabel = ctrl as LinkLabel;
                        if (linkLabel != null)
                        {
                            linkLabel.Text = "N/A";
                            continue;
                        }

                    }
                }
            }
        }
    }

    
}
