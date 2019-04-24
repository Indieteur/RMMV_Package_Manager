using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMSingleFileExt
    {
        public static string ToFileExtension(this RMSingleFileCollection.CollectionType typeOfCollection)
        {
            switch (typeOfCollection)
            {
                case RMSingleFileCollection.CollectionType.Data:
                    return RMPConstants.MiscFileExtensions.JSON;
                case RMSingleFileCollection.CollectionType.Plugins:
                    return RMPConstants.MiscFileExtensions.JAVASCRIPT;
                default:
                    return RMPConstants.MiscFileExtensions.PNG;
            }
        }

        public static RMCollectionType GetRMCollectionType(this RMSingleFile file)
        {
            if (file.Parent == null)
                throw new NullReferenceException(ExceptionMessages.RMPackage.COLL_NO_PARENT);
            return file.Parent.GetRMCollectionType();
        }

        public static RMCollectionType ToRMCollectionType(this RMSingleFileCollection.CollectionType typeOfCollection)
        {
            switch (typeOfCollection)
            {
                case RMSingleFileCollection.CollectionType.Animation:
                    return RMCollectionType.Animation;
                case RMSingleFileCollection.CollectionType.BattleBacks_1:
                    return RMCollectionType.BattleBacks_1;
                case RMSingleFileCollection.CollectionType.BattleBacks_2:
                    return RMCollectionType.BattleBacks_2;
                case RMSingleFileCollection.CollectionType.Data:
                    return RMCollectionType.Data;
                case RMSingleFileCollection.CollectionType.Parallax:
                    return RMCollectionType.Parallaxes;
                case RMSingleFileCollection.CollectionType.Pictures:
                    return RMCollectionType.Pictures;
                case RMSingleFileCollection.CollectionType.Plugins:
                    return RMCollectionType.Plugins;
                case RMSingleFileCollection.CollectionType.System:
                    return RMCollectionType.System_Image;
                case RMSingleFileCollection.CollectionType.Titles_1:
                    return RMCollectionType.Titles1;
                default:
                    return RMCollectionType.Titles2;
            }
        }

        public static RMSingleFileCollection ToNewClassInstance(this RMSingleFileCollection.CollectionType typeOfCollection, RMPackage parent)
        {
            RMSingleFileCollection newCollection;
            switch (typeOfCollection)
            {
                case RMSingleFileCollection.CollectionType.Animation:
                    newCollection = new RMAnimationCollection(parent);
                    newCollection.Name = RMPConstants.Defaults.ANIM_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.Data:
                    newCollection = new RMDataCollection(parent);
                    newCollection.Name = RMPConstants.Defaults.DATA_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.BattleBacks_1:
                    newCollection = new RMBattleBacks1_Collection(parent);
                    newCollection.Name = RMPConstants.Defaults.BB1_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.BattleBacks_2:
                    newCollection = new RMBattleBacks2_Collection(parent);
                    newCollection.Name = RMPConstants.Defaults.BB2_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.Parallax:
                    newCollection = new RMParallaxCollection(parent);
                    newCollection.Name = RMPConstants.Defaults.PARALLAX_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.Pictures:
                    newCollection = new RMPictureCollection(parent);
                    newCollection.Name = RMPConstants.Defaults.PICTURES_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.System:
                    newCollection = new RMSysImageCollection(parent);
                    newCollection.Name = RMPConstants.Defaults.SYSTEM_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.Titles_1:
                    newCollection = new RMTitles1_Collection(parent);
                    newCollection.Name = RMPConstants.Defaults.TITLES1_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.Titles_2:
                    newCollection = new RMTitles2_Collection(parent);
                    newCollection.Name = RMPConstants.Defaults.TITLES2_COLLECTION_NAME;
                    break;
                case RMSingleFileCollection.CollectionType.Plugins:
                    newCollection = new RMPluginsCollection(parent);
                    newCollection.Name = RMPConstants.Defaults.PLUGINS_COLLECTION_NAME;
                    break;
                default:
                    newCollection = null;
                    break;
            }
            return newCollection;
        }
        public static string ToDirectoryName (this RMSingleFileCollection.CollectionType collectionType, bool fullPath = true)
        {
            switch (collectionType)
            {
                case RMSingleFileCollection.CollectionType.Animation:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.ANIMATION;
                    else
                        return DirectoryNames.ProjectFiles.Image.ANIMATION;
                case RMSingleFileCollection.CollectionType.BattleBacks_1:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.BATTLEBACKS_1;
                    else
                        return DirectoryNames.ProjectFiles.Image.BATTLEBACKS_1;
                case RMSingleFileCollection.CollectionType.BattleBacks_2:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.BATTLEBACKS_2;
                    else
                        return DirectoryNames.ProjectFiles.Image.BATTLEBACKS_2;
                case RMSingleFileCollection.CollectionType.Data:
                    return DirectoryNames.ProjectFiles.DATA;
                case RMSingleFileCollection.CollectionType.Parallax:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.PARALLAXES;
                    else
                        return DirectoryNames.ProjectFiles.Image.PARALLAXES;
                case RMSingleFileCollection.CollectionType.Pictures:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.PICTURES;
                    else
                        return DirectoryNames.ProjectFiles.Image.PICTURES;
                case RMSingleFileCollection.CollectionType.Plugins:
                    return DirectoryNames.ProjectFiles.PLUGINS;
                case RMSingleFileCollection.CollectionType.System:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.SYSTEM;
                    else
                        return DirectoryNames.ProjectFiles.Image.SYSTEM;
                case RMSingleFileCollection.CollectionType.Titles_1:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.TITLES_1;
                    else
                        return DirectoryNames.ProjectFiles.Image.TITLES_1;
                case RMSingleFileCollection.CollectionType.Titles_2:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.TITLES_2;
                    else
                        return DirectoryNames.ProjectFiles.Image.TITLES_2;
                default:
                    return string.Empty;
            }
        }
    }
}
