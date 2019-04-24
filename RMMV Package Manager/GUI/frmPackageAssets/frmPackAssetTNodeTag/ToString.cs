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
        
        public override string ToString()
        {
            if (TagObjectType == TagType.Collection)
                return CollectionType.ToProperName();
            else if (TagObjectType == TagType.AudioGroup)
                return ProcessGroupToString("Audio Group");
            else if (TagObjectType == TagType.CharacterGroup)
                return ProcessGroupToString("Character Images Group");
            else if (TagObjectType == TagType.GeneratorPartGroup)
                return ProcessGroupToString("Generator Parts Group", true, Object as RMGenPart);
            else if (TagObjectType == TagType.MovieGroup)
                return ProcessGroupToString("Video Files Group");
            else if (TagObjectType == TagType.TilesetGroup)
                return ProcessGroupToString("Tileset Atlas Files Group");
            else 
                return ProcessFileToString(Object as RMPackFile);

        }

        string ProcessGroupToString(string GroupType, bool GeneratorPart = false, RMGenPart genPart = null)
        {
            if (GeneratorPart)
            {
                string genPartPrefix = genPart.GetTreeNodePrefixString();
                if (string.IsNullOrWhiteSpace(Name))
                {
                    if (string.IsNullOrWhiteSpace(genPartPrefix))
                        return GroupType;
                    return genPartPrefix;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(genPartPrefix))
                        return "[" + GroupType + "] " + Name;
                    return genPartPrefix + Name;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return GroupType;
                else
                    return "[" + GroupType + "] " + Name;
            }
        }

        string ProcessFileToString(RMPackFile file)
        {
            if (string.IsNullOrWhiteSpace(FullPath))
                return "INVALID FILE";
            else
                return file.GetTreeViewPrefixString() + FullPath;
        }
    }
}
