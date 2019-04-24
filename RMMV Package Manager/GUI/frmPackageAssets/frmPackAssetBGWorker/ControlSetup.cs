using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMMV_PackMan.GUI
{
    public static partial class frmPackAssetBGWorker
    {
       

        public static void LoadWithPackageCollection(this TreeView tView, RMPackage package, string rootDir)
        {
            tView.Nodes.Clear();
            if (package == null)
                return;

            if (package.Collections == null)
                package.Collections = new List<RMCollection>();
            foreach (RMCollection collection in package.Collections)
            {
                AddCollectionToTView(tView, collection, rootDir);
            }
            CreateEmptyPackageCollection(tView, package);
            
        }



        static void CreateEmptyPackageCollection(TreeView tView, RMPackage package)
        {
            RMCollectionType whichCollections = package.ContentsSummary;

            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Animation)))
                AddCollectionToTView(tView, new RMAnimationCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.BattleBacks_1)))
                AddCollectionToTView(tView, new RMBattleBacks1_Collection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.BattleBacks_2)))
                AddCollectionToTView(tView, new RMBattleBacks2_Collection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.BGM)))
                AddCollectionToTView(tView, new RMAudioCollection(RMAudioCollection.AudioType.BGM, package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.BGS)))
                AddCollectionToTView(tView, new RMAudioCollection(RMAudioCollection.AudioType.BGS, package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Characters)))
                AddCollectionToTView(tView, new RMCharImageCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Data)))
                AddCollectionToTView(tView, new RMDataCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Generator)))
                AddCollectionToTView(tView, new RMGeneratorCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.ME)))
                AddCollectionToTView(tView, new RMAudioCollection(RMAudioCollection.AudioType.ME, package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Movies)))
                AddCollectionToTView(tView, new RMMovieCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Parallaxes)))
                AddCollectionToTView(tView, new RMParallaxCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Pictures)))
                AddCollectionToTView(tView, new RMPictureCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Plugins)))
                AddCollectionToTView(tView, new RMPluginsCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.SE)))
                AddCollectionToTView(tView, new RMAudioCollection(RMAudioCollection.AudioType.SE, package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.System_Image)))
                AddCollectionToTView(tView, new RMSysImageCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Tilesets)))
                AddCollectionToTView(tView, new RMTilesetCollection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Titles1)))
                AddCollectionToTView(tView, new RMTitles1_Collection(package));
            if (whichCollections == RMCollectionType.None || (whichCollections != RMCollectionType.None && !whichCollections.HasFlag(RMCollectionType.Titles2)))
                AddCollectionToTView(tView, new RMTitles2_Collection(package));
        }
        public static frmPackAssetTNodeTag AddCollectionToTView(TreeView tView, RMCollection collection, string rootDirectory = null)
        {
            TreeNode tNode = new TreeNode();
            frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, collection, rootDirectory);
            tNode.Text = tag.ToString();
            tNode.Tag = tag;
            tView.Nodes.Add(tNode);
            return tag;
        }

        public static frmPackAssetTNodeTag FindNodeOfCollectionType(this TreeNodeCollection tNodes, RMCollectionType collType)
        {
            if (tNodes == null)
                return null;

            foreach (TreeNode tNode in tNodes)
            {
                frmPackAssetTNodeTag tag = tNode.Tag as frmPackAssetTNodeTag;
                if (tag != null)
                {
                    if (tag.TagObjectType == frmPackAssetTNodeTag.TagType.Collection && tag.CollectionType == collType)
                        return tag;
                }

            }
            return null;
        }
        
     
    }

  


}
