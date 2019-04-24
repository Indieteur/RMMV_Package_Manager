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
        public string Name;
        public frmPackAssetTNodeTag(TreeNode myNode, RMAudioGroup audio, string rootDir)
        {
            AssociatedNode = myNode;
            TagObjectType = TagType.AudioGroup;
            Name = audio.Name;
            Object = audio;

            if (audio.Files == null || audio.Files.Count == 0)
                return;

            foreach (RMAudioFile audioFile in audio.Files)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, audioFile, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                myNode.Nodes.Add(tNode);
            }
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMCharImageGroup characterGroup, string rootDir)
        {
            AssociatedNode = myNode;
            TagObjectType = TagType.CharacterGroup;
            Name = characterGroup.Name;
            Object = characterGroup;

            if (characterGroup.Files == null || characterGroup.Files.Count == 0)
                return;

            foreach (RMCharImageFile characterFile in characterGroup.Files)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, characterFile, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                myNode.Nodes.Add(tNode);
            }
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMGenPart genPart, string rootDir)
        {
            AssociatedNode = myNode;
            TagObjectType = TagType.GeneratorPartGroup;
            Name = genPart.Name;
            Object = genPart;

            if (genPart.Files == null || genPart.Files.Count == 0)
                return;

            foreach (RMGenFile genFile in genPart.Files)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, genFile, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                myNode.Nodes.Add(tNode);
            }
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMMovieGroup movieGroup, string rootDir)
        {
            AssociatedNode = myNode;
            TagObjectType = TagType.MovieGroup;
            Name = movieGroup.Name;
            Object = movieGroup;

            if (movieGroup.Files == null || movieGroup.Files.Count == 0)
                return;

            foreach (RMMovieFile movieFile in movieGroup.Files)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, movieFile, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                myNode.Nodes.Add(tNode);
            }
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMTilesetGroup tilesetGroup, string rootDir)
        {
            AssociatedNode = myNode;
            TagObjectType = TagType.TilesetGroup;
            Name = tilesetGroup.Name;
            Object = tilesetGroup;

            if (tilesetGroup.Files == null || tilesetGroup.Files.Count == 0)
                return;

            foreach (RMTilesetFile tilesetFile in tilesetGroup.Files)
            {
                TreeNode tNode = new TreeNode();
                frmPackAssetTNodeTag tag = new frmPackAssetTNodeTag(tNode, tilesetFile, rootDir);
                tNode.Text = tag.ToString();
                tNode.Tag = tag;
                myNode.Nodes.Add(tNode);
            }
        }
    }
}
