using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMMV_PackMan.GUI;
using static RMMV_PackMan.GUI.frmPackAssetBGWorker;
using System.Windows.Forms;
using System.Reflection;
using Indieteur.BasicLoggingSystem;
using System.IO;

namespace RMMV_PackMan
{
    public partial class frmPackageAssets
    {
        string FileToOpenPath;
        public void ResetControlsTextValues()
        {
            txtFilePath.Text = "";
            txtGroupName.Text = "";
            comboFileType1.Text = "";
            comboFileType2.Text = "";
            comboFileType3.Text = "";
            comboFileType4.Text = "";
        }
        public void DisableInfoControls()
        {
            if (!ViewOnlyMode)
            {
                btnFileRevert.Enabled = false;
                btnFileSave.Enabled = false;
                btnGroupRevert.Enabled = false;
                btnGroupSave.Enabled = false;
                comboFileType1.Enabled = false;
                lblFileType1.Enabled = false;
                comboFileType2.Enabled = false;
                lblFileType2.Enabled = false;
                comboFileType3.Enabled = false;
                lblFileType3.Enabled = false;
                comboFileType4.Enabled = false;
                lblFileType4.Enabled = false;
                comboGroupGender.Enabled = false;
                comboGroupType.Enabled = false;
                txtGroupName.ReadOnly = true;
                btnRemoveFile.Enabled = false;
                btnRemoveGroup.Enabled = false;
                btnAddFile.Enabled = false;
            }
        }

      

        public void SetupControls(frmPackAssetTNodeTag tag)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            ResetControlsTextValues();

            ObjectAndIntCollection obj = null;
            if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.Collection || tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMSingleFile)
                obj = ConstantSetup.ControlSetupList.FindObjectAndIntItemWithIntAndSub((int)tag.TagObjectType, (int)tag.CollectionType);
            else
                obj = ConstantSetup.ControlSetupList.FindObjectAndIntItemWithInt((int)tag.TagObjectType);

            if (obj == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_LOAD_ASSET_TYPE, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            ControlSetup setup = obj.Object as ControlSetup;

            if (!ViewOnlyMode)
            {
                btnFileRevert.Enabled = setup.FileInfoRevertEnabled;
                btnFileSave.Enabled = setup.FileInfoSaveEnabled;
                btnGroupRevert.Enabled = setup.GroupRevertEnabled;
                btnGroupSave.Enabled = setup.GroupSaveEnabled;
                comboFileType1.Enabled = setup.Type1Enabled;
                lblFileType1.Enabled = setup.Type1Enabled;
                comboFileType2.Enabled = setup.Type2Enabled;
                lblFileType2.Enabled = setup.Type2Enabled;
                comboFileType3.Enabled = setup.Type3Enabled;
                lblFileType3.Enabled = setup.Type3Enabled;
                comboFileType4.Enabled = setup.Type4Enabled;
                lblFileType4.Enabled = setup.Type4Enabled;
                comboGroupGender.Enabled = setup.GenderEnabled;
                comboGroupType.Enabled = setup.CollectionTypeEnabled;
                txtGroupName.ReadOnly = setup.GroupNameReadOnly;
                btnRemoveFile.Enabled = setup.btnRemoveFileEnabled;
                btnRemoveGroup.Enabled = setup.btnRemoveGroupEnabled;
                 btnAddFile.Enabled = setup.btnAddFileEnabled;
              
            }
            openFileDialog.Filter = setup.openFileFilter;
            btnFileOpen.Enabled = setup.btnOpenFileEnabled;
            comboGroupGender.Items.Clear();
            comboGroupGender.Items.AddRange(setup.GenderItems);
            comboGroupType.Items.Clear();
            comboGroupType.Items.AddRange(setup.CollectionTypeItems);
            comboFileType1.Items.Clear();
            comboFileType1.Items.AddRange(setup.Type1Items);
            lblFileType1.Text = setup.Type1Text;
            comboFileType2.Items.Clear();
            comboFileType2.Items.AddRange(setup.Type2Items);
            comboFileType2.DropDownStyle = setup.Type2Style;
            lblFileType2.Text = setup.Type2Text;
            comboFileType3.Items.Clear();
            comboFileType3.Items.AddRange(setup.Type3Items);
            comboFileType3.DropDownStyle = setup.Type3Style;
            lblFileType3.Text = setup.Type3Text;
            comboFileType4.Items.Clear();
            comboFileType4.Items.AddRange(setup.Type4Items);
            comboFileType4.DropDownStyle = setup.Type4Style;
            lblFileType4.Text = setup.Type4Text;

            LoadAssetInfo(tag);
            changesMadeGroup = false;
            changesMadeFile = false;
        }

        void LoadAssetInfo(frmPackAssetTNodeTag tag)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            bool groupTypeSet = false;
            bool genderSet = false;
            bool type1Set = false;
            bool type2Set = false;
            bool type3Set = false;
            bool type4Set = false;
            if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.AudioGroup ||tag.TagObjectType == frmPackAssetTNodeTag.TagType.CharacterGroup || tag.TagObjectType == frmPackAssetTNodeTag.TagType.TilesetGroup || tag.TagObjectType == frmPackAssetTNodeTag.TagType.MovieGroup)
            {
                if (string.IsNullOrWhiteSpace(tag.Name))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_LOAD_NAME, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                txtGroupName.Text = tag.Name;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.Collection)
            {
                ObjectAndIntCollection obj = comboGroupType.Items.FindObjectAndIntItemWithInt(0, (int)tag.CollectionType);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                comboGroupType.SelectedItem = obj;
                groupTypeSet = true;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.GeneratorPartGroup)
            {
                if (string.IsNullOrWhiteSpace(tag.Name))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_LOAD_NAME, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                txtGroupName.Text = tag.Name;
                RMGenPart genPart = tag.Object as RMGenPart;
                if (genPart == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                ObjectAndIntCollection obj = comboGroupType.Items.FindObjectAndIntItemWithInt(0, (int)genPart.PartType);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                comboGroupType.SelectedItem = obj;
                groupTypeSet = true;

                obj = comboGroupGender.Items.FindObjectAndIntItemWithInt(0, (int)genPart.Gender);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                comboGroupGender.SelectedItem = obj;
                genderSet = true;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMAudioFile)
            {
                if (string.IsNullOrWhiteSpace(tag.FullPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_PATH_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
               
                if (string.IsNullOrWhiteSpace(NonVisibleRootDir))
                    FileToOpenPath = tag.FullPath;
                else
                    FileToOpenPath = NonVisibleRootDir + "\\" + tag.FullPath;

                if (File.Exists(FileToOpenPath))
                    txtFilePath.Text = tag.FullPath;
                else
                {
                    btnFileOpen.Enabled = false;
                    txtFilePath.Text = "(Missing!) " + tag.FullPath;
                }

                RMAudioFile audioFile = tag.Object as RMAudioFile;
                if (audioFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                ObjectAndIntCollection obj = comboFileType1.Items.FindObjectAndIntItemWithInt(0, (int)audioFile.TypeOfFile);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                comboFileType1.SelectedItem = obj;
                type1Set = true; //Be wary of this. Set this to false if error occurs.

            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMCharImageFile)
            {
                if (string.IsNullOrWhiteSpace(tag.FullPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_PATH_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
              
                if (string.IsNullOrWhiteSpace(NonVisibleRootDir))
                    FileToOpenPath = tag.FullPath;
                else
                    FileToOpenPath = NonVisibleRootDir + "\\" + tag.FullPath;

                if (File.Exists(FileToOpenPath))
                    txtFilePath.Text = tag.FullPath;
                else
                {
                    btnFileOpen.Enabled = false;
                    txtFilePath.Text = "(Missing!) " + tag.FullPath;
                }

                RMCharImageFile charImageFile = tag.Object as RMCharImageFile;
                if (charImageFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                ObjectAndIntCollection obj = comboFileType1.Items.FindObjectAndIntItemWithInt(0, (int)charImageFile.ImageType);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                comboFileType1.SelectedItem = obj;
                type1Set = true; //Be wary of this. Set this to false if error occurs.
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMGenFile)
            {
                if (string.IsNullOrWhiteSpace(tag.FullPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_PATH_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
              
                if (string.IsNullOrWhiteSpace(NonVisibleRootDir))
                    FileToOpenPath = tag.FullPath;
                else
                    FileToOpenPath = NonVisibleRootDir + "\\" + tag.FullPath;

                if (File.Exists(FileToOpenPath))
                    txtFilePath.Text = tag.FullPath;
                else
                {
                    btnFileOpen.Enabled = false;
                    txtFilePath.Text = "(Missing!) " + tag.FullPath;
                }

                RMGenFile genFile = tag.Object as RMGenFile;
                if (genFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                ObjectAndIntCollection obj = comboFileType1.Items.FindObjectAndIntItemWithInt(0, (int)genFile.FileType);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                comboFileType1.SelectedItem = obj;
                type1Set = true; //Be wary of this. Set this to false if error occurs.

                comboFileType2.Text = genFile.BaseOrder.ToString();
                comboFileType3.Text = genFile.Order.ToString();
                comboFileType4.Text = genFile.Colour.ToString();
                
                type2Set = true; 
                type3Set = true; 
                type4Set = true;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMMovieFile)
            {
                if (string.IsNullOrWhiteSpace(tag.FullPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_PATH_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
               
                if (string.IsNullOrWhiteSpace(NonVisibleRootDir))
                    FileToOpenPath = tag.FullPath;
                else
                    FileToOpenPath = NonVisibleRootDir + "\\" + tag.FullPath;

                if (File.Exists(FileToOpenPath))
                    txtFilePath.Text = tag.FullPath;
                else
                {
                    btnFileOpen.Enabled = false;
                    txtFilePath.Text = "(Missing!) " + tag.FullPath;
                }

                RMMovieFile movieFile = tag.Object as RMMovieFile;
                if (movieFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                ObjectAndIntCollection obj = comboFileType1.Items.FindObjectAndIntItemWithInt(0, (int)movieFile.TypeOfFile);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                comboFileType1.SelectedItem = obj;
                type1Set = true; //Be wary of this. Set this to false if error occurs.
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMSingleFile)
            {
                if (string.IsNullOrWhiteSpace(tag.FullPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_PATH_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
               
                if (string.IsNullOrWhiteSpace(NonVisibleRootDir))
                    FileToOpenPath = tag.FullPath;
                else
                    FileToOpenPath = NonVisibleRootDir + "\\" + tag.FullPath;

                if (File.Exists(FileToOpenPath))
                    txtFilePath.Text = tag.FullPath;
                else
                {
                    btnFileOpen.Enabled = false;
                    txtFilePath.Text = "(Missing!) " + tag.FullPath;
                }
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMTilesetFile)
            {
                if (string.IsNullOrWhiteSpace(tag.FullPath))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_PATH_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
               
                if (string.IsNullOrWhiteSpace(NonVisibleRootDir))
                    FileToOpenPath = tag.FullPath;
                else
                    FileToOpenPath = NonVisibleRootDir + "\\" + tag.FullPath;

                if (File.Exists(FileToOpenPath))
                    txtFilePath.Text = tag.FullPath;
                else
                {
                    btnFileOpen.Enabled = false;
                    txtFilePath.Text = "(Missing!) " + tag.FullPath;
                }

                RMTilesetFile tilesetFile = tag.Object as RMTilesetFile;
                if (tilesetFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                ObjectAndIntCollection obj = comboFileType1.Items.FindObjectAndIntItemWithInt(0, (int)tilesetFile.FileType);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                comboFileType1.SelectedItem = obj;
                type1Set = true; //Be wary of this. Set this to false if error occurs.

                obj = comboFileType2.Items.FindObjectAndIntItemWithInt(0, (int)tilesetFile.AtlasType);
                if (obj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.CORRUPTED_MAIN, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.FAILED_CORRUPTED_DATA, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                comboFileType2.SelectedItem = obj;
                type2Set = true;
            }

            if (!groupTypeSet && comboGroupType.Items.Count > 0)
                    comboGroupType.SelectedIndex = 0;

            if (!genderSet && comboGroupGender.Items.Count > 0)
                comboGroupGender.SelectedIndex = 0;

            if (!type1Set && comboFileType1.Items.Count > 0)
                comboFileType1.SelectedIndex = 0;

            if (!type2Set && comboFileType2.Items.Count > 0)
                comboFileType2.SelectedIndex = 0;

            if (!type3Set && comboFileType3.Items.Count > 0)
                comboFileType3.SelectedIndex = 0;

            if (!type4Set && comboFileType4.Items.Count > 0)
                comboFileType4.SelectedIndex = 0;
        }

      

    }
}
