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
        public string PathInPackage;
        public string RootDirectory;
        public string FullPath
        {
            get
            {
                if (!NonRootedPath || string.IsNullOrWhiteSpace(RootDirectory))
                    return PathInPackage;
                return RootDirectory + "\\" + PathInPackage;
            }
        }

        public bool NonRootedPath;

        public frmPackAssetTNodeTag(TreeNode myNode, RMAudioFile audioFile, string rootDir)
        {
            RootDirectory = rootDir;
            AssociatedNode = myNode;
            TagObjectType = TagType.RMAudioFile;
            PathInPackage = audioFile.Path;
            Object = audioFile;
            NonRootedPath = audioFile.NonRootedPath;
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMSingleFile singleFile, string rootDir)
        {
            RootDirectory = rootDir;
            AssociatedNode = myNode;
            TagObjectType = TagType.RMSingleFile;
            PathInPackage = singleFile.Path;
            CollectionType = singleFile.GetRMCollectionType();
            Object = singleFile;
            NonRootedPath = singleFile.NonRootedPath;
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMCharImageFile characterFile, string rootDir)
        {
            RootDirectory = rootDir;
            AssociatedNode = myNode;
            TagObjectType = TagType.RMCharImageFile;
            PathInPackage = characterFile.Path;
            Object = characterFile;
            NonRootedPath = characterFile.NonRootedPath;
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMGenFile genFile, string rootDir)
        {
            RootDirectory = rootDir;
            AssociatedNode = myNode;
            TagObjectType = TagType.RMGenFile;
            PathInPackage = genFile.Path;
            Object = genFile;
            NonRootedPath = genFile.NonRootedPath;
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMMovieFile movieFile, string rootDir)
        {
            RootDirectory = rootDir;
            AssociatedNode = myNode;
            TagObjectType = TagType.RMMovieFile;
            PathInPackage = movieFile.Path;
            Object = movieFile;
            NonRootedPath = movieFile.NonRootedPath;
        }

        public frmPackAssetTNodeTag(TreeNode myNode, RMTilesetFile tilesetFile, string rootDir)
        {
            RootDirectory = rootDir;
            AssociatedNode = myNode;
            TagObjectType = TagType.RMTilesetFile;
            PathInPackage = tilesetFile.Path;
            Object = tilesetFile;
            NonRootedPath = tilesetFile.NonRootedPath;
        }
    }
}
