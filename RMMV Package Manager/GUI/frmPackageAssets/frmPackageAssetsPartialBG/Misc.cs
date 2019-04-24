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


        bool PackageNullCheck(string _namespace, string logMessage)
        {
            if (PackageOfCollections == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.PACKAGE_NULL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(logMessage, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return true;
            }
            return false;
        }

        bool tViewSelectionCheck(string _namespace, string logMessage)
        {
            if (tViewAssets.SelectedNode == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.NO_SELECTION, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(logMessage, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return true;
            }
            return false;
        }

        void ResetSelection()
        {
            ResetControlsTextValues();
            DisableInfoControls();
        }

        RMCollectionType RetrieveDefaultAddGroupSelection(TreeNode tNode)
        {
            if (tNode == null)
                return RMCollectionType.None;

            frmPackAssetTNodeTag tag = tNode.Tag as frmPackAssetTNodeTag;
            if (tag == null)
                return RMCollectionType.None;

            switch (tag.TagObjectType)
            {
                case frmPackAssetTNodeTag.TagType.AudioGroup:
                    TreeNode nodeParent = tag.AssociatedNode.Parent;
                    if (nodeParent == null)
                        return RMCollectionType.None;
                    frmPackAssetTNodeTag parentTag = nodeParent.Tag as frmPackAssetTNodeTag;
                    if (parentTag == null)
                        return RMCollectionType.None;
                    if (parentTag.TagObjectType != frmPackAssetTNodeTag.TagType.Collection)
                        return RMCollectionType.None;
                    return parentTag.CollectionType;
                case frmPackAssetTNodeTag.TagType.CharacterGroup:
                    return RMCollectionType.Characters;
                case frmPackAssetTNodeTag.TagType.Collection:
                    return tag.CollectionType;
                case frmPackAssetTNodeTag.TagType.GeneratorPartGroup:
                    return RMCollectionType.Generator;
                case frmPackAssetTNodeTag.TagType.MovieGroup:
                    return RMCollectionType.Movies;
                case frmPackAssetTNodeTag.TagType.RMAudioFile:
                    TreeNode nodeParentF = tag.AssociatedNode.Parent;
                    if (nodeParentF == null)
                        return RMCollectionType.None;
                    return RetrieveDefaultAddGroupSelection(nodeParentF);
                case frmPackAssetTNodeTag.TagType.RMCharImageFile:
                    return RMCollectionType.Characters;
                case frmPackAssetTNodeTag.TagType.RMGenFile:
                    return RMCollectionType.Generator;
                case frmPackAssetTNodeTag.TagType.RMMovieFile:
                    return RMCollectionType.Movies;
                case frmPackAssetTNodeTag.TagType.RMSingleFile:
                    return tag.CollectionType;
                case frmPackAssetTNodeTag.TagType.RMTilesetFile:
                    return RMCollectionType.Tilesets;
                default:
                    return RMCollectionType.Tilesets;

            }
        }
    }
}
