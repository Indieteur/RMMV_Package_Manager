using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan.Pacman
{
    static class Helper
    {

        public static RMCollectionType RetrieveCollectionSummary(RMPackage package)
        {
            

            RMCollectionType collectionSummary = RMCollectionType.None;
            
            if (package.Collections != null && package.Collections.Count > 0)
            {
                foreach(RMCollection collection in package.Collections)
                {
                    if (collection is RMGeneratorCollection)
                    {
                        collectionSummary = collectionSummary | RMCollectionType.Generator;
                        continue;
                    }
                    RMAudioCollection audioCollect = collection as RMAudioCollection;
                    if (audioCollect != null)
                    {
                        switch (audioCollect.CollectionType)
                        {
                            case RMAudioCollection.AudioType.BGM:
                                collectionSummary = collectionSummary | RMCollectionType.BGM;
                                break;
                            case RMAudioCollection.AudioType.BGS:
                                collectionSummary = collectionSummary | RMCollectionType.BGS;
                                break;
                            case RMAudioCollection.AudioType.ME:
                                collectionSummary = collectionSummary | RMCollectionType.ME;
                                break;
                            default:
                                collectionSummary = collectionSummary | RMCollectionType.SE;
                                break;
                        }
                        continue;
                    }
                    if (collection is RMDataCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Data;
                    else if (collection is RMAnimationCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Animation;
                    else if (collection is RMBattleBacks1_Collection)
                        collectionSummary = collectionSummary | RMCollectionType.BattleBacks_1;
                    else if (collection is RMBattleBacks2_Collection)
                        collectionSummary = collectionSummary | RMCollectionType.BattleBacks_2;
                    else if (collection is RMParallaxCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Parallaxes;
                    else if (collection is RMPictureCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Pictures;
                    else if (collection is RMSysImageCollection)
                        collectionSummary = collectionSummary | RMCollectionType.System_Image;
                    else if (collection is RMTitles1_Collection)
                        collectionSummary = collectionSummary | RMCollectionType.Titles1;
                    else if (collection is RMTitles2_Collection)
                        collectionSummary = collectionSummary | RMCollectionType.Titles2;
                    else if (collection is RMPluginsCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Plugins;
                    else if (collection is RMMovieCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Movies;
                    else if (collection is RMCharImageCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Characters;
                    else if (collection is RMTilesetCollection)
                        collectionSummary = collectionSummary | RMCollectionType.Tilesets;
                }
            }


            return collectionSummary;

         
        }

     
        public static RMGenFile.GenFileType ParseFileType (string strToParse)
        {
            switch (strToParse)
            {
                case RMPConstants.GenFileTypes.FACE:
                    return RMGenFile.GenFileType.Face;
                case RMPConstants.GenFileTypes.SV:
                    return RMGenFile.GenFileType.SV;
                case RMPConstants.GenFileTypes.SV_C:
                    return RMGenFile.GenFileType.SV_C;
                case RMPConstants.GenFileTypes.TV:
                    return RMGenFile.GenFileType.TV;
                case RMPConstants.GenFileTypes.TVD:
                    return RMGenFile.GenFileType.TVD;
                case RMPConstants.GenFileTypes.TVD_C:
                    return RMGenFile.GenFileType.TVD_C;
                case RMPConstants.GenFileTypes.TV_C:
                    return RMGenFile.GenFileType.TV_C;
                case RMPConstants.GenFileTypes.VARIATION:
                    return RMGenFile.GenFileType.Var;
            }
            return RMGenFile.GenFileType.None;
        }



        public static bool RetrieveBoolValue(string strToParse)
        {
            strToParse = strToParse.ToLower();
            if (strToParse == "y")
                return true;
            else
                return false;
        }

      
        public static RMPackObject.InstallStatus GetInstallStatus(string strToParse)
        {
            switch (strToParse)
            {
                case RMPConstants.InstallStatus.INSTALLED:
                    return RMPackObject.InstallStatus.Installed;
                case RMPConstants.InstallStatus.PARTIAL:
                    return RMPackObject.InstallStatus.Partial;
                default:
                    return RMPackObject.InstallStatus.NotInstalled;
            }
        }

    }
}
