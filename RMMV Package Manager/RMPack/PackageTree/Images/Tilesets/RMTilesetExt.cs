using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMTilesetExt
    {
        public static RMTilesetFile.eFileType ParseString(this RMTilesetFile.eFileType type, string str)
        {
            switch (str)
            {
                case RMPConstants.TilesetFileType.TEXT:
                    return RMTilesetFile.eFileType.Text;
                case RMPConstants.TilesetFileType.PNG:
                    return RMTilesetFile.eFileType.PNG;
                default:
                    return RMTilesetFile.eFileType.None;
            }
        }
        public static RMTilesetFile.eAtlasType ParseString(this RMTilesetFile.eAtlasType type, string str)
        {
            switch (str)
            {
                case RMPConstants.TilesetAtlasType.A1_ANIMATION:
                    return RMTilesetFile.eAtlasType.A1_Anim;
                case RMPConstants.TilesetAtlasType.A2_GROUND:
                    return RMTilesetFile.eAtlasType.A2_Ground;
                case RMPConstants.TilesetAtlasType.A3_BUILDINGS:
                    return RMTilesetFile.eAtlasType.A3_Building;
                case RMPConstants.TilesetAtlasType.A4_WALLS:
                    return RMTilesetFile.eAtlasType.A4_Walls;
                case RMPConstants.TilesetAtlasType.A5_NORMAL:
                    return RMTilesetFile.eAtlasType.A5_Normal;
                case RMPConstants.TilesetAtlasType.B_ATLAS:
                    return RMTilesetFile.eAtlasType.B_Atlas;
                case RMPConstants.TilesetAtlasType.C_ATLAS:
                    return RMTilesetFile.eAtlasType.C_Atlas;
                case RMPConstants.TilesetAtlasType.D_ATLAS:
                    return RMTilesetFile.eAtlasType.D_Atlas;
                case RMPConstants.TilesetAtlasType.E_ATLAS:
                    return RMTilesetFile.eAtlasType.E_Atlas;
                default:
                    return RMTilesetFile.eAtlasType.NotSpecified;
            }
        }
        public static string ToXMLString(this RMTilesetFile.eFileType type)
        {
            switch (type)
            {
                case RMTilesetFile.eFileType.PNG:
                    return RMPConstants.TilesetFileType.PNG;
                case RMTilesetFile.eFileType.Text:
                    return RMPConstants.TilesetFileType.TEXT;
                default:
                    return string.Empty;
            }
        }
        public static string ToXMLString(this RMTilesetFile.eAtlasType type)
        {
            switch (type)
            {
                case RMTilesetFile.eAtlasType.A1_Anim:
                    return RMPConstants.TilesetAtlasType.A1_ANIMATION;
                case RMTilesetFile.eAtlasType.A2_Ground:
                    return RMPConstants.TilesetAtlasType.A2_GROUND;
                case RMTilesetFile.eAtlasType.A3_Building:
                    return RMPConstants.TilesetAtlasType.A3_BUILDINGS;
                case RMTilesetFile.eAtlasType.A4_Walls:
                    return RMPConstants.TilesetAtlasType.A4_WALLS;
                case RMTilesetFile.eAtlasType.A5_Normal:
                    return RMPConstants.TilesetAtlasType.A5_NORMAL;
                case RMTilesetFile.eAtlasType.B_Atlas:
                    return RMPConstants.TilesetAtlasType.B_ATLAS;
                case RMTilesetFile.eAtlasType.C_Atlas:
                    return RMPConstants.TilesetAtlasType.C_ATLAS;
                case RMTilesetFile.eAtlasType.D_Atlas:
                    return RMPConstants.TilesetAtlasType.D_ATLAS;
                case RMTilesetFile.eAtlasType.E_Atlas:
                    return RMPConstants.TilesetAtlasType.E_ATLAS;
                default:
                    return string.Empty;
            }
        }
    }
}
