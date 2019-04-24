using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMMV_PackMan.GUI
{
    public partial class frmPackAssetTNodeTag
    {
        public enum TagType
        {
            Collection,
            GeneratorPartGroup,
            AudioGroup,
            CharacterGroup,
            MovieGroup,
            TilesetGroup,
            RMAudioFile,
            RMGenFile,
            RMCharImageFile,
            RMTilesetFile,
            RMMovieFile,
            RMSingleFile
        }
        public TagType TagObjectType;
        public RMCollectionType CollectionType;
       
        public RMPackObject Object;
        public TreeNode AssociatedNode;

        public frmPackAssetTNodeTag(TreeNode myNode, RMCollection collection, string rootDir)
        {
            TagObjectType = TagType.Collection;
            CollectionType = collection.GetRMCollectionType();
            AssociatedNode = myNode;
            Object = collection;

            if (CollectionType == RMCollectionType.BGM || CollectionType == RMCollectionType.BGS || CollectionType == RMCollectionType.ME || CollectionType == RMCollectionType.SE)
            {
                ProcessAudioGroups(myNode, collection as RMAudioCollection, rootDir);
                return;
            }
            else if (CollectionType == RMCollectionType.Characters)
            {
                ProcessCharacterGroups(myNode, collection as RMCharImageCollection, rootDir);
                return;
            }
            else if (CollectionType == RMCollectionType.Generator)
            {
                ProcessGeneratorGroups(myNode, collection as RMGeneratorCollection, rootDir);
                return;
            }
            else if (CollectionType == RMCollectionType.Movies)
            {
                ProcessMovieGroups(myNode, collection as RMMovieCollection, rootDir);
                return;
            }
            else if (CollectionType == RMCollectionType.Tilesets)
            {
                ProcessTilesetGroups(myNode, collection as RMTilesetCollection, rootDir);
                return;
            }
            else
            {
                ProcessSingleFileCollection(myNode, collection as RMSingleFileCollection, rootDir);
                return;
            }
            //else if (CollectionType == RMCollectionType.Animation || CollectionType == RMCollectionType.BattleBacks_1 || CollectionType == RMCollectionType.BattleBacks_2
            //    || CollectionType == RMCollectionType.Data || CollectionType == RMCollectionType.Parallaxes || CollectionType == RMCollectionType.Pictures
            //    || CollectionType == RMCollectionType.Plugins || CollectionType == RMCollectionType.System_Image || CollectionType == RMCollectionType.Titles1
            //    || CollectionType == RMCollectionType.Titles2)
        }

        void ProcessTilesetGroups(TreeNode parentNode, RMTilesetCollection tilesetCollection, string rootDir)
        {
            if (tilesetCollection == null || tilesetCollection.Groups == null || tilesetCollection.Groups.Count == 0)
                return;

            foreach (RMTilesetGroup tilesetGroup in tilesetCollection.Groups)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, tilesetGroup, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                parentNode.Nodes.Add(tNode);
            }
        }

        void ProcessMovieGroups(TreeNode parentNode, RMMovieCollection movieCollection, string rootDir)
        {
            if (movieCollection == null || movieCollection.Groups == null || movieCollection.Groups.Count == 0)
                return;

            foreach (RMMovieGroup videoGroup in movieCollection.Groups)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, videoGroup, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                parentNode.Nodes.Add(tNode);
            }
        }

        void ProcessGeneratorGroups(TreeNode parentNode, RMGeneratorCollection generatorCollection, string rootDir)
        {
            if (generatorCollection == null || generatorCollection.Parts == null || generatorCollection.Parts.Count == 0)
                return;

            foreach (RMGenPart genPart in generatorCollection.Parts)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, genPart, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                parentNode.Nodes.Add(tNode);
            }
        }

        void ProcessCharacterGroups(TreeNode parentNode, RMCharImageCollection characterCollection, string rootDir)
        {
            if (characterCollection == null || characterCollection.Groups == null || characterCollection.Groups.Count == 0)
                return;

            foreach (RMCharImageGroup characterGroup in characterCollection.Groups)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, characterGroup, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                parentNode.Nodes.Add(tNode);
            }
        }

        void ProcessAudioGroups(TreeNode parentNode, RMAudioCollection audioCollection, string rootDir)
        {
            if (audioCollection == null || audioCollection.Groups == null || audioCollection.Groups.Count == 0)
                return;

            foreach (RMAudioGroup audioGroup in audioCollection.Groups)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, audioGroup, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                parentNode.Nodes.Add(tNode);
            }
        }

        void ProcessSingleFileCollection(TreeNode parentNode, RMSingleFileCollection singleFileCollection, string rootDir)
        {
            if (singleFileCollection == null || singleFileCollection.Files== null || singleFileCollection.Files.Count == 0)
                return;
            foreach (RMSingleFile singleFile in singleFileCollection.Files)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, singleFile, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                parentNode.Nodes.Add(tNode);
            }
        }

      
    }
}
