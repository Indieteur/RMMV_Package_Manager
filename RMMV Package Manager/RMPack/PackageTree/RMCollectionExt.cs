using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMCollectionExt
    {
        public static RMCollectionType GetRMCollectionType(this RMCollection collection)
        {
            RMAudioCollection audioCollection = collection as RMAudioCollection;
            if (audioCollection != null)
                return audioCollection.CollectionType.ToRMCollectionType();

            if (collection is RMDataCollection)
                return RMCollectionType.Data;

            if (collection is RMGeneratorCollection)
                return RMCollectionType.Generator;

            if (collection is RMAnimationCollection)
                return RMCollectionType.Animation;

            if (collection is RMBattleBacks1_Collection)
                return RMCollectionType.BattleBacks_1;

            if (collection is RMBattleBacks2_Collection)
                return RMCollectionType.BattleBacks_2;

            if (collection is RMCharImageCollection)
                return RMCollectionType.Characters;

            if (collection is RMParallaxCollection)
                return RMCollectionType.Parallaxes;

            if (collection is RMPictureCollection)
                return RMCollectionType.Pictures;

            if (collection is RMSysImageCollection)
                return RMCollectionType.System_Image;

            if (collection is RMTilesetCollection)
                return RMCollectionType.Tilesets;

            if (collection is RMTitles1_Collection)
                return RMCollectionType.Titles1;

            if (collection is RMTitles2_Collection)
                return RMCollectionType.Titles2;

            if (collection is RMMovieCollection)
                return RMCollectionType.Movies;

            if (collection is RMPluginsCollection)
                return RMCollectionType.Plugins;

            return RMCollectionType.None;
        }

        public static string ToProperName(this RMCollectionType collType)
        {
            switch (collType)
            {
                case RMCollectionType.Generator:
                    return RMPConstants.CollectionProperName.GEN_PART;
                case RMCollectionType.BGM:
                    return RMPConstants.CollectionProperName.BGM;
                case RMCollectionType.Animation:
                    return RMPConstants.CollectionProperName.ANIMATION;
                case RMCollectionType.BGS:
                    return RMPConstants.CollectionProperName.BGS;
                case RMCollectionType.ME:
                    return RMPConstants.CollectionProperName.ME;
                case RMCollectionType.SE:
                    return RMPConstants.CollectionProperName.SE;
                case RMCollectionType.BattleBacks_1:
                    return RMPConstants.CollectionProperName.BATTLEBACK_1;
                case RMCollectionType.BattleBacks_2:
                    return RMPConstants.CollectionProperName.BATTLEBACK_2;
                case RMCollectionType.Data:
                    return RMPConstants.CollectionProperName.DATA;
                case RMCollectionType.Parallaxes:
                    return RMPConstants.CollectionProperName.PARALLAX;
                case RMCollectionType.Pictures:
                    return RMPConstants.CollectionProperName.PICTURE;
                case RMCollectionType.Plugins:
                    return RMPConstants.CollectionProperName.PLUGIN;
                case RMCollectionType.System_Image:
                    return RMPConstants.CollectionProperName.SYS_IMAGE;
                case RMCollectionType.Titles1:
                    return RMPConstants.CollectionProperName.TITLE_1;
                case RMCollectionType.Titles2:
                    return RMPConstants.CollectionProperName.TITLE_2;
                case RMCollectionType.Characters:
                    return RMPConstants.CollectionProperName.CHARACTER;
                case RMCollectionType.Tilesets:
                    return RMPConstants.CollectionProperName.TILESET;
                case RMCollectionType.Movies:
                    return RMPConstants.CollectionProperName.MOVIE;
                default:
                    return "INVALID COLLECTION";
            }
        }

        public static string ToLoggerPluralString(this RMCollectionType collType)
        {
            switch (collType)
            {
                case RMCollectionType.Generator:
                    return LoggerMessages.RMPackage.CollectionsName.GEN_PARTS;
                case RMCollectionType.BGM:
                    return LoggerMessages.RMPackage.CollectionsName.BGM;
                case RMCollectionType.Animation:
                    return LoggerMessages.RMPackage.CollectionsName.ANIMATIONS;
                case RMCollectionType.BGS:
                    return LoggerMessages.RMPackage.CollectionsName.BGS;
                case RMCollectionType.ME:
                    return LoggerMessages.RMPackage.CollectionsName.ME;
                case RMCollectionType.SE:
                    return LoggerMessages.RMPackage.CollectionsName.SE;
                case RMCollectionType.BattleBacks_1:
                    return LoggerMessages.RMPackage.CollectionsName.BATTLEBACKS_1;
                case RMCollectionType.BattleBacks_2:
                    return LoggerMessages.RMPackage.CollectionsName.BATTLEBACKS_2;
                case RMCollectionType.Data:
                    return LoggerMessages.RMPackage.CollectionsName.DATA;
                case RMCollectionType.Parallaxes:
                    return LoggerMessages.RMPackage.CollectionsName.PARALLAXES;
                case RMCollectionType.Pictures:
                    return LoggerMessages.RMPackage.CollectionsName.PICTURES;
                case RMCollectionType.Plugins:
                    return LoggerMessages.RMPackage.CollectionsName.PLUGINS;
                case RMCollectionType.System_Image:
                    return LoggerMessages.RMPackage.CollectionsName.SYS_IMAGES;
                case RMCollectionType.Titles1:
                    return LoggerMessages.RMPackage.CollectionsName.TITLES_1;
                case RMCollectionType.Titles2:
                    return LoggerMessages.RMPackage.CollectionsName.TITLES_2;
                case RMCollectionType.Characters:
                    return LoggerMessages.RMPackage.CollectionsName.CHARACTERS;
                case RMCollectionType.Tilesets:
                    return LoggerMessages.RMPackage.CollectionsName.TILESETS;
                case RMCollectionType.Movies:
                    return LoggerMessages.RMPackage.CollectionsName.MOVIES;
                default:
                    throw new NotImplementedException();
            }
        }
        public static string ToLoggerSingularString(this RMCollectionType collType)
        {
            switch (collType)
            {
                case RMCollectionType.Generator:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.GEN_PART;
                case RMCollectionType.BGM:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.BGM;
                case RMCollectionType.Animation:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.ANIMATION;
                case RMCollectionType.BGS:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.BGS;
                case RMCollectionType.ME:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.ME;
                case RMCollectionType.SE:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.SE;
                case RMCollectionType.BattleBacks_1:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.BATTLEBACK_1;
                case RMCollectionType.BattleBacks_2:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.BATTLEBACK_2;
                case RMCollectionType.Data:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.DATA;
                case RMCollectionType.Parallaxes:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.PARALLAX;
                case RMCollectionType.Pictures:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.PICTURE;
                case RMCollectionType.Plugins:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.PLUGIN;
                case RMCollectionType.System_Image:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.SYS_IMAGE;
                case RMCollectionType.Titles1:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.TITLE_1;
                case RMCollectionType.Titles2:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.TITLE_2;
                case RMCollectionType.Characters:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.CHARACTER;
                case RMCollectionType.Tilesets:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.TILESET;
                case RMCollectionType.Movies:
                    return LoggerMessages.RMPackage.CollectionsName.Singular.MOVIE;
                default:
                    throw new NotImplementedException();
            }
        }
        public static RMCollection ToNewCollection(this RMCollectionType collType, RMPackage parent)
        {
            switch (collType)
            {
                case RMCollectionType.Generator:
                    return new RMGeneratorCollection(parent);
                case RMCollectionType.BGM:
                    return new RMAudioCollection(RMAudioCollection.AudioType.BGM, parent);
                case RMCollectionType.Animation:
                    return new RMAnimationCollection(parent);
                case RMCollectionType.BGS:
                    return new RMAudioCollection(RMAudioCollection.AudioType.BGS, parent);
                case RMCollectionType.ME:
                    return new RMAudioCollection(RMAudioCollection.AudioType.ME, parent);
                case RMCollectionType.SE:
                    return new RMAudioCollection(RMAudioCollection.AudioType.SE, parent);
                case RMCollectionType.BattleBacks_1:
                    return new RMBattleBacks1_Collection(parent);
                case RMCollectionType.BattleBacks_2:
                    return new RMBattleBacks2_Collection(parent);
                case RMCollectionType.Data:
                    return new RMDataCollection(parent);
                case RMCollectionType.Parallaxes:
                    return new RMParallaxCollection(parent);
                case RMCollectionType.Pictures:
                    return new RMPictureCollection(parent);
                case RMCollectionType.Plugins:
                    return new RMPluginsCollection(parent);
                case RMCollectionType.System_Image:
                    return new RMSysImageCollection(parent);
                case RMCollectionType.Titles1:
                    return new RMTitles1_Collection(parent);
                case RMCollectionType.Titles2:
                    return new RMTitles2_Collection(parent);
                case RMCollectionType.Characters:
                    return new RMCharImageCollection(parent);
                case RMCollectionType.Tilesets:
                    return new RMTilesetCollection(parent);
                case RMCollectionType.Movies:
                    return new RMMovieCollection(parent);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
