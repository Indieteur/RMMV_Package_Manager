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

namespace RMMV_PackMan
{
    public partial class frmPackageAssets
    {
        void SaveGroupInfo(TreeNode node)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (string.IsNullOrWhiteSpace(txtGroupName.Text))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.INVALID_GROUP_NAME, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }

            if (node.Tag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            frmPackAssetTNodeTag tag = node.Tag as frmPackAssetTNodeTag;
            if (tag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.AudioGroup)
            {
                RMAudioGroup audioGroup = tag.Object as RMAudioGroup;
                if (audioGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                tag.Name = txtGroupName.Text;
                audioGroup.Name = txtGroupName.Text;
                node.Text = tag.ToString();
                changesMadeGroup = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.CharacterGroup)
            {
                RMCharImageGroup characterGroup = tag.Object as RMCharImageGroup;
                if (characterGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                tag.Name = txtGroupName.Text;
                characterGroup.Name = txtGroupName.Text;
                node.Text = tag.ToString();
                changesMadeGroup = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.GeneratorPartGroup)
            {
                RMGenPart genGroup = tag.Object as RMGenPart;
                if (genGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                tag.Name = txtGroupName.Text;
                genGroup.Name = txtGroupName.Text;

                ObjectAndIntCollection objAndIntColl = comboGroupGender.SelectedItem as ObjectAndIntCollection;
                genGroup.Gender = (RMGenPart.eGender)objAndIntColl.IntegerCollection[0];

                objAndIntColl = comboGroupType.SelectedItem as ObjectAndIntCollection;
                genGroup.PartType = (RMGenPart.GenPartType)objAndIntColl.IntegerCollection[0];

                node.Text = tag.ToString();
                changesMadeGroup = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.MovieGroup)
            {
                RMMovieGroup movieGroup = tag.Object as RMMovieGroup;
                if (movieGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                tag.Name = txtGroupName.Text;
                movieGroup.Name = txtGroupName.Text;
                node.Text = tag.ToString();
                changesMadeGroup = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.TilesetGroup)
            {
                RMTilesetGroup tilesetGroup = tag.Object as RMTilesetGroup;
                if (tilesetGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                tag.Name = txtGroupName.Text;
                tilesetGroup.Name = txtGroupName.Text;
                node.Text = tag.ToString();
                changesMadeGroup = false;
                return;
            }
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_GROUP_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_GROUP_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
            return;
        }

        void SaveFileInfo(TreeNode node)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (node.Tag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            frmPackAssetTNodeTag tag = node.Tag as frmPackAssetTNodeTag;
            if (tag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return;
            }

            if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMAudioFile)
            {
                RMAudioFile audioFile = tag.Object as RMAudioFile;
                if (audioFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                ObjectAndIntCollection objAndIntColl = comboFileType1.SelectedItem as ObjectAndIntCollection;
                audioFile.TypeOfFile = (RMAudioFile.FileType)objAndIntColl.IntegerCollection[0];
                node.Text = tag.ToString();
                changesMadeFile = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMCharImageFile)
            {
                RMCharImageFile charImageFile = tag.Object as RMCharImageFile;
                if (charImageFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                ObjectAndIntCollection objAndIntColl = comboFileType1.SelectedItem as ObjectAndIntCollection;
                charImageFile.ImageType = (RMCharImageFile.ImageTypes)objAndIntColl.IntegerCollection[0];
                node.Text = tag.ToString();
                changesMadeFile = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMGenFile)
            {
                RMGenFile genFile = tag.Object as RMGenFile;
                if (genFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                ObjectAndIntCollection objAndIntColl = comboFileType1.SelectedItem as ObjectAndIntCollection;
                genFile.FileType = (RMGenFile.GenFileType)objAndIntColl.IntegerCollection[0];

                int highOrderParse = comboFileType2.Text.TryToInt(0);
                int lowOrderParse = comboFileType3.Text.TryToInt(0);
                int colourParse = comboFileType4.Text.TryToInt(0);
                if (highOrderParse == -1)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.HIGH_ORDER_INVALID, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lowOrderParse == -1)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.LOW_ORDER_INVALID, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (colourParse == -1)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.COLOUR_INVALID, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                genFile.Order = lowOrderParse;
                genFile.Colour = colourParse;
                genFile.BaseOrder = highOrderParse;

                node.Text = tag.ToString();
                changesMadeFile = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMMovieFile)
            {
                RMMovieFile movieFile = tag.Object as RMMovieFile;
                if (movieFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }
                ObjectAndIntCollection objAndIntColl = comboFileType1.SelectedItem as ObjectAndIntCollection;
                movieFile.TypeOfFile = (RMMovieFile.FileType)objAndIntColl.IntegerCollection[0];
                node.Text = tag.ToString();
                changesMadeFile = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMTilesetFile)
            {
                RMTilesetFile tilesetFile = tag.Object as RMTilesetFile;
                if (tilesetFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return;
                }

                ObjectAndIntCollection objAndIntColl = comboFileType1.SelectedItem as ObjectAndIntCollection;
                tilesetFile.FileType = (RMTilesetFile.eFileType)objAndIntColl.IntegerCollection[0];

                objAndIntColl = comboFileType2.SelectedItem as ObjectAndIntCollection;
                tilesetFile.AtlasType = (RMTilesetFile.eAtlasType)objAndIntColl.IntegerCollection[0];

                node.Text = tag.ToString();
                changesMadeFile = false;
                return;
            }
            else if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.RMSingleFile)
                return;

            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.UNABLE_SAVE_FILE_INFO, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logger.WriteErrorLog(LoggerMessages.GUI.frmPackageAssets.Error.UNABLE_SAVE_FILE_INFO_NULL_TAG, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
            return;
        }
    }
}
