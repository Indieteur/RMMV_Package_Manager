using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;
using frmPropPackMessagse = RMMV_PackMan.LoggerMessages.GUI.frmPropPack;

namespace RMMV_PackMan
{
    public partial class frmPropPack
    {
        void ProcessLicenseAndImplicitDirCommonPath()
        {
            if (License != null && License.LicenseSource == RMPackLic.Source.File && !string.IsNullOrWhiteSpace(License.Data))
            {
                if (string.IsNullOrWhiteSpace(txtAssetDir.Text))
                {
                    btnSaveXML.Enabled = true;
                    return;
                }
                string loweredLicPath = License.Data.ToLower();
                string loweredAssetDir = txtAssetDir.Text.ToLower();
                if (loweredLicPath.StartsWith(loweredAssetDir))
                {
                    LicenseFileRelativePath = Helper.GetRelativePath(License.Data, loweredAssetDir);
                    btnSaveXML.Enabled = true;
                    return;
                }
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.LICENSE_FILE_NON_RELATIVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSaveXML.Enabled = false;
                return;
            }
            btnSaveXML.Enabled = true;

        }


        void ProcessCustomAssetsCommonPath(string _namespace)
        {
            if (CustomAssetPack == null || CustomAssetPack.Collections == null || CustomAssetPack.Collections.Count == 0)
            {
                btnSaveXML.Enabled = true;
                FormattedCustomAssetPack = null;
                return;
            }

            FormattedCustomAssetPack = (RMPackage)CustomAssetPack.Clone();
            if (License != null)
                FormattedCustomAssetPack.License = License.Clone();
            List<RMPackFile> retrievedFiles = null;
            string commonPath = null;

            frmLoading loadingForm = new frmLoading(StringConst.frmLoading.FORMAT_PACK_META);
            Thread thread = new Thread(delegate ()
            {
                commonPath  = FormattedCustomAssetPack.TrimPrefixCommonPathOfFiles(out retrievedFiles, true);
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (retrievedFiles == null || retrievedFiles.Count == 0 || ((FormattedCustomAssetPack.License == null || FormattedCustomAssetPack.License.LicenseSource != RMPackLic.Source.File) 
                && retrievedFiles.Count == 1))
            {
                btnSaveXML.Enabled = true;
                if (retrievedFiles != null && retrievedFiles.Count == 1)
                    CustomAssetsCommonPath = Path.GetDirectoryName(retrievedFiles[0].Path);
                return; // User did not select any assets or has selected 1 asset but the license source is not  a file.
            }

            

            if (string.IsNullOrWhiteSpace(commonPath))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_COMMON_PATH, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomAssetsCommonPath = null;
                btnSaveXML.Enabled = false;
                return;
            }

            bool allPathRelative = true;
            LogDataList warningLog = new LogDataList();

            if (FormattedCustomAssetPack.License != null && FormattedCustomAssetPack.License.LicenseSource == RMPackLic.Source.File && !FormattedCustomAssetPack.License.NonRootedLicenseFile)
            {
                warningLog.WriteWarningLog(frmPropPackMessagse.Warning.NonCommonPathFile(License.Data, commonPath, true), _namespace);
                allPathRelative = false;
            }

            foreach (RMPackFile file in retrievedFiles)
            {
                if (!file.NonRootedPath)
                {
                    warningLog.WriteWarningLog(frmPropPackMessagse.Warning.NonCommonPathFile(file.Path, commonPath), _namespace);
                    allPathRelative = false;
                }
            }
            CustomAssetsCommonPath = commonPath;
            if (allPathRelative)
            {
                btnSaveXML.Enabled = true;
                return;
            }

            if (warningLog != null && warningLog.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: warningLog);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            btnSaveXML.Enabled = false;
        }


        void CreateXMLFile(string _namespace, bool implicitAssets)
        {
            if (implicitAssets)
            {
                if (string.IsNullOrWhiteSpace(txtAssetDir.Text))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_IMPLICIT_DIR_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Directory.Exists(txtAssetDir.Text))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_IMPLICIT_DIR_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.SelCommonPathSaveXML(txtAssetDir.Text), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(CustomAssetsCommonPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_EXPLICIT_ASSET_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Directory.Exists(CustomAssetsCommonPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_EXPLICIT_ASSET_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.SelCommonPathSaveXML(CustomAssetsCommonPath), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RMPackage retVal = CreatePackageFromUserDetails(implicitAssets);
            if (retVal == null)
                return;

            saveFileDialog.FileName = Vars.INSTALL_FILE_DEFAULT_FILENAME;
            if (implicitAssets)
                saveFileDialog.InitialDirectory = txtAssetDir.Text;
            else
                saveFileDialog.InitialDirectory = CustomAssetsCommonPath;

            saveFileDialog.Filter = FileDialogFilters.INSTALL_XML;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            Logger.WriteInformationLog(LoggerMessages.GUI.frmPropPack.Info.SAVE_XML_INIT + saveFileDialog.FileName + ".", _namespace);

            if (!implicitAssets && FormattedCustomAssetPack != null)
                retVal.Collections = FormattedCustomAssetPack.Collections;
            try
            {
                retVal.SaveToFile(saveFileDialog.FileName, _namespace, logMessage: new WriteAllTextLogMessages(writeFailed: frmPropPackMessagse.Error.UnableSaveXML));
            }
            catch
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.UnableSaveXML(saveFileDialog.FileName), MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MadeChanges = false;

            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.SUCCESS_XML + saveFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Logger.WriteInformationLog(LoggerMessages.GUI.frmPropPack.Info.SAVE_XML_DONE + saveFileDialog.FileName + ".", _namespace);
        }

        void CreateZIPFile(string _namespace, bool implicitAssets)
        {
            if (implicitAssets)
            {
                if (string.IsNullOrWhiteSpace(txtAssetDir.Text))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_IMPLICIT_DIR_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Directory.Exists(txtAssetDir.Text))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_IMPLICIT_DIR_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (CustomAssetPack == null || CustomAssetPack.Collections == null || CustomAssetPack.Collections.Count == 0)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_EXPLICIT_ASSET_SEL_SAVE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            RMPackage finalPackage = CreatePackageFromUserDetails(true, false);
            if (finalPackage == null)
                return;

            saveFileDialog.FileName = string.Empty;
            saveFileDialog.InitialDirectory = string.Empty;
            saveFileDialog.Filter = FileDialogFilters.INSTALL_ZIP;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            Logger.WriteInformationLog(LoggerMessages.GUI.frmPropPack.Info.SAVE_ZIP_INIT + saveFileDialog.FileName + ".", _namespace);

            RMPackage temppackage = null;
            Thread thread = null;
            frmLoading loadingForm = null;
            bool error = false;
            if (implicitAssets)
            {
                temppackage = new RMPackage();
                temppackage.Name = "Package Manager Probe";
                LogDataList log = null;

                loadingForm = new frmLoading(StringConst.frmLoading.RETRIEVING_ASSETS_DIR + txtAssetDir.Text + "."); 
                thread = new Thread(delegate ()
                {
                    try
                    {
                        RMImplicit.RetrievePackFromDir(txtAssetDir.Text, _namespace, false, out log, ref temppackage);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.UNABLE_RETRIEVE_PACK, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(frmPropPackMessagse.Error.UnableRetrievePack(txtAssetDir.Text), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        error = true;
                        loadingForm.SafeClose();
                        return;
                    }
                    loadingForm.SafeClose();
                });
                thread.Start();
                loadingForm.ShowDialog();
                if (error)
                    return;

                if (log != null && log.HasErrorsOrWarnings())
                {
                    Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    frmLogger loggerForm = new frmLogger(_logList: log);
                    loggerForm.StartPosition = FormStartPosition.CenterParent;
                    loggerForm.ShowDialog();
                }

            }
            else
                temppackage = CustomAssetPack;

            RMPackLic license = null;
            loadingForm = new frmLoading(StringConst.frmLoading.COPYING_ASSETS_TO_TEMP + temppackage + "."); 
            thread = new Thread(delegate ()
            {
                try
                {
                    CopyAssetFilesToTemp(_namespace, temppackage, out license);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.ZIP_FILE_MAKE_ERR_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPropPackMessagse.Error.ZipFileCopyAssetToTempErr(PMFileSystem.PackMan_TempMakeDir), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    error = true;
                    loadingForm.SafeClose();
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (error)
            {
                CleanupMakeTemp(_namespace, false);
                return;
            }
        

            finalPackage.License = license;

            string xmlPath = PMFileSystem.PackMan_TempMakeDir + "\\" + Vars.INSTALL_FILE_DEFAULT_FILENAME;

            try
            {
                finalPackage.SaveToFile(xmlPath, _namespace, logMessage: new WriteAllTextLogMessages(writeFailed: frmPropPackMessagse.Error.ZipSaveXMLFailed));
            }
            catch
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.ZIP_FILE_MAKE_ERR_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanupMakeTemp(_namespace, false);
                return;
            }

            loadingForm = new frmLoading(StringConst.frmLoading.CREATE_AN_ARCH_OF_PACK + saveFileDialog.FileName + "."); 
            thread = new Thread(delegate ()
            {
                try
                {
                    ArchiveManagement.CreateNewZip(PMFileSystem.PackMan_TempMakeDir, saveFileDialog.FileName, _namespace);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.ZIP_FILE_MAKE_ERR_GEN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPropPackMessagse.Error.ZIP_MAKE + saveFileDialog.FileName + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    loadingForm.SafeClose();
                    error = true;
                    return;
                }
                loadingForm.SafeClose();
            });
            thread.Start();
            loadingForm.ShowDialog();

            if (error)
            {
                CleanupMakeTemp(_namespace, false);
                return;
            }

            MadeChanges = false;

            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.ZIP_SUCCESS + saveFileDialog.FileName + ".", MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Logger.WriteInformationLog(LoggerMessages.GUI.frmPropPack.Info.SAVE_ZIP_SUCCESS + saveFileDialog.FileName + ".", _namespace);
        }

        void CopyAssetFilesToTemp(string _namespace, RMPackage rootedFilesPack, out RMPackLic license)
        {
            license = License;
            if (rootedFilesPack == null || rootedFilesPack.Collections == null || rootedFilesPack.Collections.Count == 0)
            {
                throw new NullReferenceException(ExceptionMessages.GUI.frmPropPack.NO_ASSETS_SEL);
            }
            Exception outEx;
            
                CleanupMakeTemp(_namespace);
           

            if (Helper.CreateFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out outEx, LoggerMessages.GeneralError.UNABLE_CREATE_TEMP_DIR_ARG) == CreateFolderResult.UserCancelled)
                throw outEx;

            try
            {
                PackageUtil.ImplicitCopyAssetFilesTo(rootedFilesPack, PMFileSystem.PackMan_TempMakeDir, _namespace);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(frmPropPackMessagse.Error.COPY_SEL_FAILED + PMFileSystem.PackMan_TempMakeDir + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                CleanupMakeTemp(_namespace, false);
                throw;
            }

            if (License != null && License.LicenseSource == RMPackLic.Source.File)
            {
                if (string.IsNullOrWhiteSpace(License.Data))
                {
                    try
                    {
                        throw new InvalidPackageLicenseException(ExceptionMessages.RMPackage.LIC_FILE_PATH_NULL, InvalidPackageLicenseException.WhichInvalid.InvalidLicenseFile, rootedFilesPack);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(frmPropPackMessagse.Error.LICENSE_FILE_NULL, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        CleanupMakeTemp(_namespace, false);
                        throw;
                    }
                }
                if (!File.Exists(License.Data))
                {
                    try
                    {
                        throw new InvalidPackageLicenseException(ExceptionMessages.RMPackage.LicFileNotExist(License.Data), InvalidPackageLicenseException.WhichInvalid.InvalidLicenseFile, rootedFilesPack);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(frmPropPackMessagse.Error.LicenseFileNotExist(License.Data), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                        CleanupMakeTemp(_namespace,false);
                        throw;
                    }
                }
                string destLicFile = Vars.LICENSE_FILE_DEF_NAME + Path.GetExtension(License.Data);
                string destLicFilePath = PMFileSystem.PackMan_TempMakeDir + "\\" + destLicFile;
                if (Helper.CopyFileSafely(License.Data, destLicFilePath, true, _namespace, out outEx, new CopyFileLogMessages(copyFileFailed: frmPropPackMessagse.Error.LicenseFileNonCopy)) != CopyFileResult.Success)
                {
                    CleanupMakeTemp(_namespace, false);
                    throw outEx;
                }
                license = License.Clone();
                license.Data = destLicFile;

            }

        }

        void CleanupMakeTemp(string _namespace, bool throwErrorOnException = true)
        {
            Exception ex;
            if (Directory.Exists(PMFileSystem.PackMan_TempMakeDir) && Helper.DeleteFolderSafely(PMFileSystem.PackMan_TempMakeDir, _namespace, out ex, LoggerMessages.GeneralError.UNABLE_DELETE_TEMP_DIR_ARG) == DeleteFolderResult.UserCancelled && throwErrorOnException)
                throw ex;
        }

        bool CheckPackageInfo()
        {
            if (string.IsNullOrWhiteSpace(txtNamespace.Text))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_NAMESPACE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_NAME, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (License == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.NO_LIC, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtPackURL.Text) && !txtPackURL.Text.IsAValidURL())
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPropPack.INVALID_PACK_URL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        RMPackage CreatePackageFromUserDetails(bool Implicit, bool autoFormatLicense = true)
        {
            RMPackage retVal = new RMPackage();
            retVal.Implicit = Implicit;
            retVal.UniqueID = txtNamespace.Text;
            retVal.Name = txtName.Text;
            if (!string.IsNullOrWhiteSpace(txtVersion.Text))
                retVal.Version = txtVersion.Text;
            if (!string.IsNullOrWhiteSpace(txtAuthor.Text))
                retVal.Author = txtAuthor.Text;
            if (!string.IsNullOrWhiteSpace(txtPackURL.Text))
                retVal.URL = txtPackURL.Text;
            if (!string.IsNullOrWhiteSpace(txtDesc.Text))
                retVal.Description = txtDesc.Text;
            if (autoFormatLicense)
             retVal.License = GetProperPackLicenseFieldPack();
            return retVal;
        }

        RMPackLic GetProperPackLicenseFieldPack()
        {
            if (License == null || License.LicenseSource != RMPackLic.Source.File)
                return License;
            if (cboxImplicit.Checked)
            {
                RMPackLic lic = License.Clone();
                lic.Data = LicenseFileRelativePath;
                return lic;
            }
            else
            {
                if (FormattedCustomAssetPack == null)
                    return null;
                return FormattedCustomAssetPack.License;
            }
        }

        void LoadPackage(string path, bool skipFileAppend)
        {
            if (!string.IsNullOrWhiteSpace(SelectedPackage.UniqueID))
                txtNamespace.Text = SelectedPackage.UniqueID;
            if (!string.IsNullOrWhiteSpace(SelectedPackage.Name))
                txtName.Text = SelectedPackage.Name;
            if (!string.IsNullOrWhiteSpace(SelectedPackage.Version))
                txtVersion.Text = SelectedPackage.Version;
            if (!string.IsNullOrWhiteSpace(SelectedPackage.Author))
                txtAuthor.Text = SelectedPackage.Author;
            if (!string.IsNullOrWhiteSpace(SelectedPackage.URL))
                txtPackURL.Text = SelectedPackage.URL;

            if (SelectedPackage.License != null)
            {
                License = SelectedPackage.License;
                if (License.LicenseSource == RMPackLic.Source.File && !string.IsNullOrWhiteSpace(License.Data))
                    License.Data = path + "\\" + License.Data;
            }

            if (!string.IsNullOrWhiteSpace(SelectedPackage.Description))
                txtDesc.Text = SelectedPackage.Description;

            if (SelectedPackage.Implicit)
            {
                cboxImplicit.Checked = true;
                if (!string.IsNullOrWhiteSpace(path))
                    txtAssetDir.Text = path;
                ProcessLicenseAndImplicitDirCommonPath();
                btnViewAssetDir.Enabled = true;
            }
            else
            {
                CustomAssetPack = SelectedPackage.Clone() as RMPackage;
                if (!skipFileAppend)
                {
                    List<RMPackFile> listOfFiles = CustomAssetPack.RetrieveAllFiles();
                    if (listOfFiles != null)
                    {
                        foreach (RMPackFile file in listOfFiles)
                        {

                            file.Path = path + "\\" + file.Path;
                        }
                    }
                }
                ProcessCustomAssetsCommonPath(MethodBase.GetCurrentMethod().ToLogFormatFullName());
            }
        }
    }
}
