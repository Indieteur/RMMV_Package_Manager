using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMMV_PackMan
{
    [Flags]
    public enum RMCollectionType
    {
        None = 0,
        Generator,
        BGM,
        BGS = 4,
        ME = 8,
        SE = 16,
        Animation = 32,
        BattleBacks_1 = 64,
        BattleBacks_2 = 128,
        Characters = 256,
        Parallaxes = 512,
        Pictures = 1024,
        System_Image = 2048,
        Tilesets = 4096,
        Titles1 = 8192,
        Titles2 = 16384,
        Plugins = 32768,
        Movies = 65536,
        Data = 131072
    }
    public abstract class RMCollection : RMPackObject
    {
        public RMPackage Parent { get; set; }

        protected RMCollection (RMPackage package)
        {
            Parent = package;
            
        }

        public static RMCollection TryParse(XElement elementToParse, RMPackage package, bool skipGenParts = false)
        {
            switch (elementToParse.Name.LocalName)
            {
                case RMPConstants.Collections.GENERATOR:
                    if (skipGenParts)
                        return null;
                    return new RMGeneratorCollection(elementToParse, package);
                case RMPConstants.Collections.BGM:
                    return new RMAudioCollection(elementToParse, RMAudioCollection.AudioType.BGM, package);
                case RMPConstants.Collections.BGS:
                    return new RMAudioCollection(elementToParse, RMAudioCollection.AudioType.BGS, package);
                case RMPConstants.Collections.ME:
                    return new RMAudioCollection(elementToParse, RMAudioCollection.AudioType.ME, package);
                case RMPConstants.Collections.SE:
                    return new RMAudioCollection(elementToParse, RMAudioCollection.AudioType.SE, package);
                case RMPConstants.Collections.DATA:
                    return new RMDataCollection(elementToParse, package);
                case RMPConstants.Collections.ANIMATIONS:
                    return new RMAnimationCollection(elementToParse, package);
                case RMPConstants.Collections.BATTLEBACKS_1:
                    return new RMBattleBacks1_Collection(elementToParse, package);
                case RMPConstants.Collections.BATTLEBACKS_2:
                    return new RMBattleBacks2_Collection(elementToParse, package);
                case RMPConstants.Collections.PARALLAXES:
                    return new RMParallaxCollection(elementToParse, package);
                case RMPConstants.Collections.PICTURES:
                    return new RMPictureCollection(elementToParse, package);
                case RMPConstants.Collections.SYSTEM:
                    return new RMSysImageCollection(elementToParse, package);
                case RMPConstants.Collections.TITLES_1:
                    return new RMTitles1_Collection(elementToParse, package);
                case RMPConstants.Collections.TITLES_2:
                    return new RMTitles2_Collection(elementToParse, package);
                case RMPConstants.Collections.PLUGINS:
                    return new RMPluginsCollection(elementToParse, package);
                case RMPConstants.Collections.CHARACTERS:
                    return new RMCharImageCollection(elementToParse, package);
                case RMPConstants.Collections.MOVIES:
                    return new RMMovieCollection(elementToParse, package);
                case RMPConstants.Collections.TILESETS:
                    return new RMTilesetCollection(elementToParse, package);
            }
            return null;
        }

        public abstract List<RMPackFile> RetrieveAllFiles();
        public abstract void SetInstalledPropertyAll(InstallStatus status);
        public abstract List<BoolAndRMFile> CheckFileExistences(string rootDir);
        public bool IsCollectionEmpty()
        {
            List<RMPackFile> listRetrieved = RetrieveAllFiles();
            return (listRetrieved == null || listRetrieved.Count == 0);
        }
    }

}
