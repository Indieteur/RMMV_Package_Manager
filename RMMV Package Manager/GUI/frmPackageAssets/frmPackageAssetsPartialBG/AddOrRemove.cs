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
using frmPackageAssetsMessages = RMMV_PackMan.LoggerMessages.GUI.frmPackageAssets;

namespace RMMV_PackMan
{
    public partial class frmPackageAssets
    {

        bool RemoveAssetGroup(TreeNode tNode)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            frmPackAssetTNodeTag groupTag = tNode.Tag as frmPackAssetTNodeTag;
            if (groupTag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_TAG_NULL, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return false;
            }

            if (tNode.Parent == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_PARENT_NULL, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return false;
            }

            frmPackAssetTNodeTag collTag = tNode.Parent.Tag as frmPackAssetTNodeTag;
            if (collTag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_PARENT_NULL, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return false;
            }

            if (collTag.TagObjectType != frmPackAssetTNodeTag.TagType.Collection)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_PARENT_NULL, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return false;
            }


            if (groupTag.TagObjectType == frmPackAssetTNodeTag.TagType.AudioGroup)
            {
                if (collTag.CollectionType != RMCollectionType.BGM && collTag.CollectionType != RMCollectionType.BGS && collTag.CollectionType != RMCollectionType.SE && collTag.CollectionType != RMCollectionType.ME)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMAudioCollection audioCollection = collTag.Object as RMAudioCollection;
                if (audioCollection == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMAudioGroup audio = groupTag.Object as RMAudioGroup;
                if (audio == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                tNode.Remove();
                audioCollection.Groups.Remove(audio);
                if (audioCollection.Groups.Count == 0)
                    audioCollection.Parent.Collections.Remove(audioCollection);
                return true;
            }
            else if (groupTag.TagObjectType == frmPackAssetTNodeTag.TagType.CharacterGroup)
            {
                if (collTag.CollectionType != RMCollectionType.Characters)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMCharImageCollection charCollection = collTag.Object as RMCharImageCollection;
                if (charCollection == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMCharImageGroup charGroup = groupTag.Object as RMCharImageGroup;
                if (charGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                tNode.Remove();
                charCollection.Groups.Remove(charGroup);
                if (charCollection.Groups.Count == 0)
                    charCollection.Parent.Collections.Remove(charCollection);
                return true;
            }
            else if (groupTag.TagObjectType == frmPackAssetTNodeTag.TagType.GeneratorPartGroup)
            {
                if (collTag.CollectionType != RMCollectionType.Generator)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMGeneratorCollection genCollection = collTag.Object as RMGeneratorCollection;
                if (genCollection == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMGenPart genGroup = groupTag.Object as RMGenPart;
                if (genGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                tNode.Remove();
                genCollection.Parts.Remove(genGroup);
                if (genCollection.Parts.Count == 0)
                    genCollection.Parent.Collections.Remove(genCollection);
                return true;
            }
            else if (groupTag.TagObjectType == frmPackAssetTNodeTag.TagType.MovieGroup)
            {
                if (collTag.CollectionType != RMCollectionType.Movies)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMMovieCollection movieCollection = collTag.Object as RMMovieCollection;
                if (movieCollection == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMMovieGroup movieGroup = groupTag.Object as RMMovieGroup;
                if (movieGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                tNode.Remove();
                movieCollection.Groups.Remove(movieGroup);
                if (movieCollection.Groups.Count == 0)
                    movieCollection.Parent.Collections.Remove(movieCollection);
                return true;
            }
            else if (groupTag.TagObjectType == frmPackAssetTNodeTag.TagType.TilesetGroup)
            {
                if (collTag.CollectionType != RMCollectionType.Tilesets)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMTilesetCollection tilesetCollection = collTag.Object as RMTilesetCollection;
                if (tilesetCollection == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID_PARENT, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                RMTilesetGroup tilesetGroup = groupTag.Object as RMTilesetGroup;
                if (tilesetGroup == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_GROUP_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_GROUP_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                tNode.Remove();
                tilesetCollection.Groups.Remove(tilesetGroup);
                if (tilesetCollection.Groups.Count == 0)
                    tilesetCollection.Parent.Collections.Remove(tilesetCollection);
                return true;
            }

            return false;
        }
       
        TreeNode AddAssetGroup(frmAddAssetGroup formShown)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (formShown.DialogResult == DialogResult.Cancel)
                return null;
            
            if (string.IsNullOrWhiteSpace(formShown.TypeName))
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_NO_NAME_GROUP_ASSET, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            frmPackAssetTNodeTag collectionTag = tViewAssets.Nodes.FindNodeOfCollectionType(formShown.TypeToAdd);
            if (collectionTag == null)
            {
                RMCollection rmc = null;
                try
                {
                    rmc = formShown.TypeToAdd.ToNewCollection(PackageOfCollections);
                }
                catch (Exception ex)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_ASSET_GROUP_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_CREATE_COLLECTION, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    return null;
                }
                if (PackageOfCollections.Collections == null)
                    PackageOfCollections.Collections = new List<RMCollection>();
                PackageOfCollections.Collections.Add(rmc);
                collectionTag = AddCollectionToTView(tViewAssets, rmc, RootDirectory);
            }

            if (formShown.TypeToAdd == RMCollectionType.BGM || formShown.TypeToAdd == RMCollectionType.BGS || formShown.TypeToAdd == RMCollectionType.ME || formShown.TypeToAdd == RMCollectionType.SE)
            {
                RMAudioCollection collectionObj = collectionTag.Object as RMAudioCollection;
                if (collectionObj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_ASSET_GROUP_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_ADD_GROUP_INVALID_COLLECTION, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return null;
                }
                RMAudioGroup audioGroup = new RMAudioGroup(collectionObj);
                audioGroup.Name = formShown.TypeName;
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag newGroupTag = new frmPackAssetTNodeTag(tNode, audioGroup, RootDirectory);
                tNode.Text = newGroupTag.ToString();
                tNode.Tag = newGroupTag;
                collectionTag.AssociatedNode.Nodes.Add(tNode);

                if (collectionObj.Groups == null)
                    collectionObj.Groups = new List<RMAudioGroup>();
                collectionObj.Groups.Add(audioGroup);

                if (!PackageOfCollections.Collections.Contains(collectionObj))
                    PackageOfCollections.Collections.Add(collectionObj);

                return tNode;
            }
            else if (formShown.TypeToAdd == RMCollectionType.Characters)
            {
                RMCharImageCollection collectionObj = collectionTag.Object as RMCharImageCollection;
                if (collectionObj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_ASSET_GROUP_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_ADD_GROUP_INVALID_COLLECTION, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return null;
                }
                RMCharImageGroup charImageGroup = new RMCharImageGroup(collectionObj);
                charImageGroup.Name = formShown.TypeName;
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag newGroupTag = new frmPackAssetTNodeTag(tNode, charImageGroup, RootDirectory);
                tNode.Text = newGroupTag.ToString();
                tNode.Tag = newGroupTag;
                collectionTag.AssociatedNode.Nodes.Add(tNode);

                if (collectionObj.Groups == null)
                    collectionObj.Groups = new List<RMCharImageGroup>();
                collectionObj.Groups.Add(charImageGroup);

                if (!PackageOfCollections.Collections.Contains(collectionObj))
                    PackageOfCollections.Collections.Add(collectionObj);

                return tNode;
            }
            else if (formShown.TypeToAdd == RMCollectionType.Tilesets)
            {
                RMTilesetCollection collectionObj = collectionTag.Object as RMTilesetCollection;
                if (collectionObj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_ASSET_GROUP_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_ADD_GROUP_INVALID_COLLECTION, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return null;
                }
                RMTilesetGroup tilesetGroup = new RMTilesetGroup(collectionObj);
                tilesetGroup.Name = formShown.TypeName;
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag newGroupTag = new frmPackAssetTNodeTag(tNode, tilesetGroup, RootDirectory);
                tNode.Text = newGroupTag.ToString();
                tNode.Tag = newGroupTag;
                collectionTag.AssociatedNode.Nodes.Add(tNode);

                if (collectionObj.Groups == null)
                    collectionObj.Groups = new List<RMTilesetGroup>();
                collectionObj.Groups.Add(tilesetGroup);

                if (!PackageOfCollections.Collections.Contains(collectionObj))
                    PackageOfCollections.Collections.Add(collectionObj);

                return tNode;
            }
            else if (formShown.TypeToAdd == RMCollectionType.Movies)
            {
                RMMovieCollection collectionObj = collectionTag.Object as RMMovieCollection;
                if (collectionObj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_ASSET_GROUP_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_ADD_GROUP_INVALID_COLLECTION, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return null;
                }
                RMMovieGroup movieGroup = new RMMovieGroup(collectionObj);
                movieGroup.Name = formShown.TypeName;
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag newGroupTag = new frmPackAssetTNodeTag(tNode, movieGroup, RootDirectory);
                tNode.Text = newGroupTag.ToString();
                tNode.Tag = newGroupTag;
                collectionTag.AssociatedNode.Nodes.Add(tNode);

                if (collectionObj.Groups == null)
                    collectionObj.Groups = new List<RMMovieGroup>();
                collectionObj.Groups.Add(movieGroup);

                if (!PackageOfCollections.Collections.Contains(collectionObj))
                    PackageOfCollections.Collections.Add(collectionObj);

                return tNode;
            }
            else if (formShown.TypeToAdd == RMCollectionType.Generator)
            {
                RMGeneratorCollection collectionObj = collectionTag.Object as RMGeneratorCollection;
                if (collectionObj == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.ADD_ASSET_GROUP_ERR_GENERAL, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_ADD_GROUP_INVALID_COLLECTION, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return null;
                }
                RMGenPart genGroup = new RMGenPart(collectionObj);
                genGroup.Name = formShown.TypeName;
                genGroup.PartType = formShown.GeneratorType;
                genGroup.Gender = formShown.GeneratorGender;
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag newGroupTag = new frmPackAssetTNodeTag(tNode, genGroup, RootDirectory);
                tNode.Text = newGroupTag.ToString();
                tNode.Tag = newGroupTag;
                collectionTag.AssociatedNode.Nodes.Add(tNode);

                if (collectionObj.Parts == null)
                    collectionObj.Parts = new List<RMGenPart>();
                collectionObj.Parts.Add(genGroup);

                if (!PackageOfCollections.Collections.Contains(collectionObj))
                    PackageOfCollections.Collections.Add(collectionObj);

                return tNode;
            }
            return null;
        }

        TreeNode AddFiles(string[] filePaths, string _namespace)
        {
            if (filePaths == null)
                return null;
            TreeNode retVal = null;
            LogDataList log = new LogDataList();
            foreach (string path in filePaths)
            {
                try
                {
                    retVal = AddFile(tViewAssets.SelectedNode, path, _namespace); 
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(frmPackageAssetsMessages.Error.UnableAddFileSingle(path), _namespace, ex);
                }
            }

            if (log != null && log.HasErrorsOrWarnings())
            {
                Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmLogger loggerForm = new frmLogger(_logList: log);
                loggerForm.StartPosition = FormStartPosition.CenterParent;
                loggerForm.ShowDialog();
            }

            return retVal;
        }

        TreeNode AddFile(TreeNode selectedNode, string path, string _namespace, bool skipSanityChecks = false, bool pathCanBeTrimmed = false)
        {

            if (!skipSanityChecks)
            {
                if (string.IsNullOrWhiteSpace(path))
                    throw new FileNotFoundException(ExceptionMessages.GUI.frmPackageAssets.FILE_PATH_NULL);

                if (!File.Exists(path))
                    throw new FileNotFoundException(ExceptionMessages.General.FileNotFound(path));
                if (!string.IsNullOrWhiteSpace(RootDirectory) && path.ToLower().StartsWith(RootDirectory.ToLower()))
                    pathCanBeTrimmed = true;
            }
         

           

            frmPackAssetTNodeTag parentingTag = selectedNode.Tag as frmPackAssetTNodeTag;
            if (parentingTag == null)
                throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.TAG_SEL_NODE_NULL);

            if (parentingTag.TagObjectType == frmPackAssetTNodeTag.TagType.AudioGroup)
            {
                RMAudioGroup audioGroup = parentingTag.Object as RMAudioGroup;
                if (audioGroup == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.ASSOC_OBJ_TAG_SEL_NODE_NULL);
                TreeNode tNode = AddFile(path, audioGroup, pathCanBeTrimmed);
                parentingTag.AssociatedNode.Nodes.Add(tNode);
                return tNode;
            }
            else if (parentingTag.TagObjectType == frmPackAssetTNodeTag.TagType.CharacterGroup)
            {
                RMCharImageGroup charNode = parentingTag.Object as RMCharImageGroup;
                if (charNode == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.ASSOC_OBJ_TAG_SEL_NODE_NULL);
                TreeNode tNode = AddFile(path, charNode, pathCanBeTrimmed);
                parentingTag.AssociatedNode.Nodes.Add(tNode);
                return tNode;
            }
            else if (parentingTag.TagObjectType == frmPackAssetTNodeTag.TagType.Collection)
            {
                RMSingleFileCollection singleFileC = parentingTag.Object as RMSingleFileCollection;
                if (singleFileC == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.ASSOC_OBJ_TAG_SEL_NODE_NULL);
                TreeNode tNode = AddFile(path, singleFileC, pathCanBeTrimmed);
                parentingTag.AssociatedNode.Nodes.Add(tNode);

                if (!PackageOfCollections.Collections.Contains(singleFileC))
                    PackageOfCollections.Collections.Add(singleFileC);

                return tNode;
            }
            else if (parentingTag.TagObjectType == frmPackAssetTNodeTag.TagType.GeneratorPartGroup)
            {
                RMGenPart genGroup = parentingTag.Object as RMGenPart;
                if (genGroup == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.ASSOC_OBJ_TAG_SEL_NODE_NULL);
                TreeNode tNode = AddFile(path, genGroup, pathCanBeTrimmed);
                parentingTag.AssociatedNode.Nodes.Add(tNode);
                return tNode;
            }
            else if (parentingTag.TagObjectType == frmPackAssetTNodeTag.TagType.MovieGroup)
            {
                RMMovieGroup movieGroup = parentingTag.Object as RMMovieGroup;
                if (movieGroup == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.ASSOC_OBJ_TAG_SEL_NODE_NULL);
                TreeNode tNode = AddFile(path, movieGroup, pathCanBeTrimmed);
                parentingTag.AssociatedNode.Nodes.Add(tNode);
                return tNode;
            }
            else if (parentingTag.TagObjectType == frmPackAssetTNodeTag.TagType.TilesetGroup)
            {
                RMTilesetGroup tilesetGroup = parentingTag.Object as RMTilesetGroup;
                if (tilesetGroup == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.ASSOC_OBJ_TAG_SEL_NODE_NULL);
                TreeNode tNode = AddFile(path, tilesetGroup, pathCanBeTrimmed);
                parentingTag.AssociatedNode.Nodes.Add(tNode);
                return tNode;
            }
            else
            {
                TreeNode parentingNode = parentingTag.AssociatedNode.Parent;
                if (parentingNode == null)
                    throw new NullReferenceException(ExceptionMessages.GUI.frmPackageAssets.PARENT_NODE_NULL);
                return AddFile(parentingNode, path, _namespace, true, pathCanBeTrimmed);
            }
        }



        TreeNode AddFile(string path, RMAudioGroup audioGroup, bool pathCanBeTrimmed = false)
        {
            string ext = Path.GetExtension(path).ToLower();

            RMAudioFile audioFile = new RMAudioFile(audioGroup);
            if (pathCanBeTrimmed)
                audioFile.Path = Helper.GetRelativePath(path, RootDirectory);
            else
            {
                audioFile.Path = path;
                //audioFile.NonRelativePath = true;
            }

            if (ext == RMPConstants.AudioFileType.DOT_M4A)
                audioFile.TypeOfFile = RMAudioFile.FileType.m4a;
            else if (ext == RMPConstants.AudioFileType.DOT_OGG)
                audioFile.TypeOfFile = RMAudioFile.FileType.ogg;
            else
                throw new InvalidAudioFileException(ExceptionMessages.General.FileExtInvalid(path), InvalidAudioFileException.WhichInvalid.InvalidType, audioGroup);
            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, audioFile, RootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;

            if (audioGroup.Files == null)
                audioGroup.Files = new List<RMAudioFile>();
            audioGroup.Files.Add(audioFile);

            return tNode;
        }

        TreeNode AddFile(string path, RMCharImageGroup charGroup, bool pathCanBeTrimmed = false)
        {
            RMCharImageFile charFile = new RMCharImageFile(charGroup);
            charFile.FileName = Path.GetFileName(path);
            charFile.ImageType = RMCharImageFile.ImageTypes.Character;
            if (pathCanBeTrimmed)
                charFile.Path = Helper.GetRelativePath(path, RootDirectory);
            else
            {
                charFile.Path = path;
                //charFile.NonRelativePath = true;
            }

            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, charFile, RootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;

            if (charGroup.Files == null)
                charGroup.Files = new List<RMCharImageFile>();
            charGroup.Files.Add(charFile);

            return tNode;
        }

        TreeNode AddFile(string path, RMSingleFileCollection sFCollection, bool pathCanBeTrimmed = false)
        {
            RMSingleFile singleFile = new RMSingleFile(sFCollection);
            singleFile.FileName = Path.GetFileName(path);
            if (pathCanBeTrimmed)
                singleFile.Path = Helper.GetRelativePath(path, RootDirectory);
            else
            {
                singleFile.Path = path;
                //singleFile.NonRelativePath = true;
            }

            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, singleFile, RootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;

            if (sFCollection.Files == null)
                sFCollection.Files = new List<RMSingleFile>();
            sFCollection.Files.Add(singleFile);

            return tNode;
        }

        TreeNode AddFile(string path, RMGenPart genGroup, bool pathCanBeTrimmed = false)
        {
            RMGenFile genFile = new RMGenFile(genGroup);
            if (pathCanBeTrimmed)
                genFile.Path = Helper.GetRelativePath(path, RootDirectory);
            else
            {
                genFile.Path = path;
                //genFile.NonRelativePath = true;
            }
            string fileName = Path.GetFileName(path).ToLower();
            int startPos;
            genFile.FileType = TempGenFileNameParsed.RetrieveType(fileName, out startPos);
            if (genFile.FileType == RMGenFile.GenFileType.None)
            {
                genFile.BaseOrder = 0;
                genFile.Colour = 0;
                genFile.FileType = RMGenFile.GenFileType.Face;
                genFile.Order = 0;
            }
            else
            {
                try
                {
                    TempGenFileNameParsed genFileNameParsed = new TempGenFileNameParsed(fileName, startPos);
                    genFile.BaseOrder = genFileNameParsed.BaseOrder;
                    genFile.Colour = genFileNameParsed.Colour;
                    genFile.Order = genFileNameParsed.Order;
                    if (genFileNameParsed.IsAcFile)
                    {
                        switch (genFile.FileType)
                        {
                            case RMGenFile.GenFileType.SV:
                                genFile.FileType = RMGenFile.GenFileType.SV_C;
                                break;
                            case RMGenFile.GenFileType.TV:
                                genFile.FileType = RMGenFile.GenFileType.TV_C;
                                break;
                            case RMGenFile.GenFileType.TVD:
                                genFile.FileType = RMGenFile.GenFileType.TVD_C;
                                break;
                        }
                    }
                }
                catch
                {
                    genFile.BaseOrder = 0;
                    genFile.Colour = 0;
                    genFile.Order = 0;
                    
                }
            }

            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, genFile, RootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;

            if (genGroup.Files == null)
                genGroup.Files = new List<RMGenFile>();
            genGroup.Files.Add(genFile);

            return tNode;
        }

        TreeNode AddFile(string path, RMMovieGroup movieGroup, bool pathCanBeTrimmed = false)
        {
            string ext = Path.GetExtension(path).ToLower();

            RMMovieFile movieFile = new RMMovieFile(movieGroup);
            if (pathCanBeTrimmed)
                movieFile.Path = Helper.GetRelativePath(path, RootDirectory);
            else
            {
                movieFile.Path = path;
                //movieFile.NonRelativePath = true;
            }

            if (ext == RMPConstants.MovieFileType.DOT_MP4)
                movieFile.TypeOfFile =  RMMovieFile.FileType.mp4;
            else if (ext == RMPConstants.MovieFileType.DOT_WEBM)
                movieFile.TypeOfFile = RMMovieFile.FileType.webm;
            else
                throw new InvalidMovieFileException(ExceptionMessages.General.FileExtInvalid(path), InvalidMovieFileException.WhichInvalid.InvalidType, movieGroup);
            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, movieFile, RootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;

            if (movieGroup.Files == null)
                movieGroup.Files = new List<RMMovieFile>();
            movieGroup.Files.Add(movieFile);

            return tNode;

        }

        TreeNode AddFile(string path, RMTilesetGroup tilesetGroup, bool pathCanBeTrimmed = false)
        {
            string ext = Path.GetExtension(path).ToLower();

            RMTilesetFile tilesetFile = new RMTilesetFile(tilesetGroup);
            if (pathCanBeTrimmed)
                tilesetFile.Path = Helper.GetRelativePath(path, RootDirectory);
            else
            {
                tilesetFile.Path = path;
                //tilesetFile.NonRelativePath = true;
            }

            try
            {
                TilesetTypeAndName parsedFileName = new TilesetTypeAndName(Path.GetFileNameWithoutExtension(path));
                tilesetFile.AtlasType = parsedFileName.atlasType;
            }
            catch
            {
                tilesetFile.AtlasType = RMTilesetFile.eAtlasType.NotSpecified;
            }

            if (ext == RMPConstants.TilesetFileType.DOT_PNG)
                tilesetFile.FileType = RMTilesetFile.eFileType.PNG;
            else if (ext == RMPConstants.TilesetFileType.DOT_TEXT)
                tilesetFile.FileType = RMTilesetFile.eFileType.Text;
            else
                throw new InvalidTilesetFileException(ExceptionMessages.General.FileExtInvalid(path), InvalidTilesetFileException.WhichInvalid.InvalidType, tilesetGroup);
            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, tilesetFile, RootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;

            if (tilesetGroup.Files == null)
                tilesetGroup.Files = new List<RMTilesetFile>();
            tilesetGroup.Files.Add(tilesetFile);

            return tNode;

        }

        bool RemoveAsset(TreeNode tNode)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            frmPackAssetTNodeTag assetTag = tNode.Tag as frmPackAssetTNodeTag;
            if (assetTag == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_NULL, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                return false;
            }
            if (assetTag.TagObjectType == frmPackAssetTNodeTag.TagType.RMAudioFile)
            {
                RMAudioFile audioFile = assetTag.Object as RMAudioFile;
                if (audioFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                if (audioFile.Parent == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                audioFile.Parent.Files.Remove(audioFile);
                assetTag.AssociatedNode.Remove();
                return true;
            }
            else if (assetTag.TagObjectType == frmPackAssetTNodeTag.TagType.RMCharImageFile)
            {
                RMCharImageFile charImageFile = assetTag.Object as RMCharImageFile;
                if (charImageFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                if (charImageFile.Parent == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                charImageFile.Parent.Files.Remove(charImageFile);
                assetTag.AssociatedNode.Remove();
                return true;
            }
            else if (assetTag.TagObjectType == frmPackAssetTNodeTag.TagType.RMGenFile)
            {
                RMGenFile genFile = assetTag.Object as RMGenFile;
                if (genFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                if (genFile.Parent == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                genFile.Parent.Files.Remove(genFile);
                assetTag.AssociatedNode.Remove();
                return true;
            }
            else if (assetTag.TagObjectType == frmPackAssetTNodeTag.TagType.RMMovieFile)
            {
                RMMovieFile movieFile = assetTag.Object as RMMovieFile;
                if (movieFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                if (movieFile.Parent == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                movieFile.Parent.Files.Remove(movieFile);
                assetTag.AssociatedNode.Remove();
                return true;
            }
            else if (assetTag.TagObjectType == frmPackAssetTNodeTag.TagType.RMSingleFile)
            {
                RMSingleFile singleFile = assetTag.Object as RMSingleFile;
                if (singleFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                if (singleFile.Parent == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                singleFile.Parent.Files.Remove(singleFile);
                assetTag.AssociatedNode.Remove();
                return true;
            }
            else if (assetTag.TagObjectType == frmPackAssetTNodeTag.TagType.RMTilesetFile)
            {
                RMTilesetFile tilesetFile = assetTag.Object as RMTilesetFile;
                if (tilesetFile == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_TAG_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                if (tilesetFile.Parent == null)
                {
                    Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                    return false;
                }
                tilesetFile.Parent.Files.Remove(tilesetFile);
                assetTag.AssociatedNode.Remove();
                return true;
            }
            Helper.ShowMessageBox(MessageBoxStrings.GUI.frmPackageAssets.DELETE_ASSET_ERR, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logger.WriteErrorLog(frmPackageAssetsMessages.Error.UNABLE_REMOVE_ASSET_INVALID, _namespace, null, BasicDebugLogger.DebugErrorType.Error);
            return false;   

            
        }
    }
}
