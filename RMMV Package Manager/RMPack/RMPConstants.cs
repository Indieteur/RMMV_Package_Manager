using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static class RMPConstants
    {
        public static class Defaults
        {
            public const string PLACEHOLDER_XELEMENT_NAME = "X";
            public const string DATA_COLLECTION_NAME = "Data Collection";
            public const string ANIM_COLLECTION_NAME = "Animation Collection";
            public const string BB1_COLLECTION_NAME = "BattleBacks Floor Collection";
            public const string BB2_COLLECTION_NAME = "BattleBacks Ceiling Collection";
            public const string PARALLAX_COLLECTION_NAME = "Parallax Collection";
            public const string PICTURES_COLLECTION_NAME = "Pictures Collection";
            public const string TITLES1_COLLECTION_NAME = "Title Background Collection";
            public const string SYSTEM_COLLECTION_NAME = "System Image Collection";
            public const string TITLES2_COLLECTION_NAME = "Title Border Collection";
            public const string PLUGINS_COLLECTION_NAME = "Plugins Collection";
        }
        
        public const string ATTR_NAME = "name";
        public const string ATTR_AUTH = "author";
        public const string ATTR_VERSION = "version";
        public const string ATTR_GENDER = "gender";
        public const string ATTR_TYPE = "type";
        public const string ATTR_ORDER = "order";
        public const string ATTR_COLOUR = "colour";
        public const string ATTR_BASEORDER = "base";
        public const string ATTR_IMPLICIT = "implicit";
        public const string ATTR_INSTALLED = "installed";
        public const string ATTR_LOADED = "loaded";
        public const string FIELD_DESC = "Description";
        public const string FIELD_URL = "URL";
        public const string FIELD_CONTENTS = "Contents";
        public const string FIELD_LICENSE = "License";
        public const string ATTR_SOURCE = "src";
        public const string FIELD_PACKAGE = "Package";
        public const string FIELD_FILE = "File";
        public const string FIELD_AUDIO = "Audio";
        public const string FIELD_MOVIE = "Movie";
        public const string FIELD_CHARACTER = "Character";
        public const string FIELD_ROOT_DIRECTORY = "RootDir";
        public const string FIELD_TILESET = "Tileset";
        public const string FIELD_UID = "UID";

        public static class TilesetFileType
        {
            public const string TEXT = "txt";
            public const string DOT_TEXT = ".txt";
            public const string PNG = "png";
            public const string DOT_PNG = ".png";
        }

        public static class TilesetAtlasType
        {
            public const string A1_ANIMATION = "A1";
            public const string A2_GROUND = "A2";
            public const string A3_BUILDINGS = "A3";
            public const string A4_WALLS = "A4";
            public const string A5_NORMAL = "A5";
            public const string B_ATLAS = "B";
            public const string C_ATLAS = "C";
            public const string D_ATLAS = "D";
            public const string E_ATLAS = "E";
        }

        public static class RootDirVar
        {
            public const string DEFAULT_DIR = "%default%";
            public const string PARENT_PACKAGE_ROOTDIR = "%parentroot%";
        }

        public static class InstallStatus
        {
            public const string INSTALLED = "Y";
            public const string PARTIAL = "P";
        }

        public static class CharacterImageType
        {
            public const string CHARACTER = "character";
            public const string ENEMY = "enemy";
            public const string FACE = "face";
            public const string SV_ACTOR = "sv_actor";
            public const string SV_ENEMY = "sv_enemy";
        }

        public static class MovieFileType
        {
            public const string MP4 = "mp4";
            public const string DOT_MP4 = ".mp4";
            public const string WEBM = "webm";
            public const string DOT_WEBM = ".webm";
        }
        public static class AudioFileType
        {
            public const string OGG = "ogg";
            public const string DOT_OGG = ".ogg";
            public const string M4A = "m4a";
            public const string DOT_M4A = ".m4a";
            public const string MP3 = "mp3";
            public const string WAV = "wav";
        }

        public static class LicenseFileExtensions
        {
            public const string DOT_TXT = ".txt";
            public const string DOT_PDF = ".pdf";
            public const string DOT_DOC = ".doc";
            public const string DOT_DOCX = ".docx";
            public const string DOT_RTF = ".rtf";
        }
        
        public static class MiscFileExtensions
        {
            public const string JSON = "json";
            public const string PNG = "png";
            public const string JAVASCRIPT = "js";
            public const string ZIP = "zip";
            public const string XML = "xml";
        }

        public static class GenFileNamePrefixANDSuffix
        {
            public const char SEPARATOR = '_';

            public const string FACE = "FG";
            public const string FACE_LOWER = "fg";

            public const string SV_LOWER = "sv";
            public const string SV = "SV";

            public const string TV_LOWER = "tv";
            public const string TV = "TV";


            public const string TVD_LOWER = "tvd";
            public const string TVD = "TVD";

            public const string VARIATION = "icon";

            public const char POSITION = 'p';
            public const char ORDER = 'c';
            public const char COLOUR = 'm';
            public const string PNG = ".png";
        }

        public static class GenFileFields
        {
            public const string GEN_FILE_TYPE = "generator file type";
            public const string GEN_PART_TYPE = "generator part type";
            public const string POSITION = "position";
            public const string BASE_ORDER = "base order";
            public const string ORDER = "layer";
            public const string COLOUR = "colour";

        }

        public static class LowCaseDirectoryNames 
        {
            public const string GENERATOR = "generator";
            public const string GEN_FACE = "face";
            public const string GEN_SV = "sv";
            public const string GEN_TV = "tv";
            public const string GEN_TVD = "tvd";
            public const string GEN_VARIATION = "variation";
            public const string GEN_PART_MALE = "male";
            public const string GEN_PART_FEMALE = "female";
            public const string GEN_PART_KID = "kid";
            public const string AUDIO = "audio";
            public const string AUDIO_BGM = "bgm";
            public const string AUDIO_BGS = "bgs";
            public const string AUDIO_ME = "me";
            public const string AUDIO_SE = "se";
            public const string DATA = "data";
            public const string IMG = "img";
            public const string IMG_ANIM = "animations";
            public const string IMG_BATTLEBACKS_1 = "battlebacks1";
            public const string IMG_BATTLEBACKS_2 = "battlebacks2";
            public const string IMG_CHARACTERS = "characters";
            public const string IMG_ENEMIES = "enemies";
            public const string IMG_FACES = "faces";
            public const string IMG_PARALLAXES = "parallaxes";
            public const string IMG_SV_ACTORS = "sv_actors";
            public const string IMG_SV_ENEMIES = "sv_enemies";
            public const string IMG_SYSTEM = "system";
            public const string IMG_PICTURES = "pictures";
            public const string IMG_TILESETS = "tilesets";
            public const string IMG_TITLES_1 = "titles1";
            public const string IMG_TITLES_2 = "titles2";
            public const string PLUGINS = "js\\plugins";
            public const string MOVIES = "movies";
        }
        public static class GenPartsTypeFileName
        {
            public const string BEARD = "beard";
            public const string ACCESSORY_A = "acca";
            public const string ACCESSORY_B = "accb";
            public const string BEAST_EARS = "beastears";
            public const string BODY = "body";
            public const string CLOAK = "cloak";
            public const string CLOTHING = "clothing";
            public const string EARS = "ears";
            public const string EYEBROWS = "eyebrows";
            public const string EYES = "eyes";
            public const string FACE = "face";
            public const string FACIAL_MARK = "facialmark";
            public const string FRONT_HAIR = "fronthair";
            public const string GLASSES = "glasses";
            public const string MOUTH = "mouth";
            public const string NOSE = "nose";
            public const string REAR_HAIR = "rearhair";
            public const string TAIL = "tail";
            public const string WING = "wing";
        }

      


        public static class Collections
        {
            public const string GENERATOR = "Generator";
            public const string BGM = "BGM";
            public const string BGS = "BGS";
            public const string ME = "ME";
            public const string SE = "SE";
            public const string DATA = "Data";
            public const string ANIMATIONS = "Animations";
            public const string BATTLEBACKS_1 = "BattleBacks_1";
            public const string BATTLEBACKS_2 = "BattleBacks_2";
            public const string CHARACTERS = "Characters"; //Handles the folders characters, enemies, faces, sv_actors and sv_enemies under img.
            public const string PARALLAXES = "Parallaxes";
            public const string PICTURES = "Pictures";
            public const string SYSTEM = "System";
            public const string TILESETS = "Tilesets";
            public const string TITLES_1 = "Titles_1";
            public const string TITLES_2 = "Titles_2";
            public const string PLUGINS = "Plugins";
            public const string MOVIES = "Movies";
        }
        public static class LicenseSource
        {
            public const string URL = "URL";
            public const string File = "File";
            public const string Text = "Text";
        }

        public static class GenPartsTypeProperName
        {
            public const string BEARD = "Beard";
            public const string ACCESSORY_A = "Accessory A";
            public const string ACCESSORY_B = "Accessory B";
            public const string BEAST_EARS = "Beast Ears";
            public const string BODY = "Body";
            public const string CLOAK = "Cloak";
            public const string CLOTHING = "Clothing";
            public const string EARS = "Ears";
            public const string EYEBROWS = "Eyebrows";
            public const string EYES = "Eyes";
            public const string FACE = "Face";
            public const string FACIAL_MARK = "Facial Mark";
            public const string FRONT_HAIR = "Front Hair";
            public const string GLASSES = "Glasses";
            public const string MOUTH = "Mouth";
            public const string NOSE = "Nose";
            public const string REAR_HAIR = "Rear Hair";
            public const string TAIL = "Tail";
            public const string WING = "Wing";


        }

        public static class GenPartsType
        {
            public const string BEARD = "Beard";
            public const string ACCESSORY_A = "AccA";
            public const string ACCESSORY_B = "AccB";
            public const string BEAST_EARS = "BeastEars";
            public const string BODY = "body";
            public const string CLOAK = "Cloak";
            public const string CLOTHING = "Clothing";
            public const string EARS = "Ears";
            public const string EYEBROWS = "Eyebrows";
            public const string EYES = "Eyes";
            public const string FACE = "Face";
            public const string FACIAL_MARK = "FacialMark";
            public const string FRONT_HAIR = "FrontHair";
            public const string GLASSES = "Glasses";
            public const string MOUTH = "Mouth";
            public const string NOSE = "Nose";
            public const string REAR_HAIR = "RearHair";
            public const string TAIL = "Tail";
            public const string WING = "Wing";
          

        }

        public static class CollectionProperName
        {
            public const string GEN_PART = "Generator Parts";
            public const string ANIMATION = "Animations";
            public const string BGM = "Background Music";
            public const string BGS = "Background Sounds";
            public const string ME = "Musical Effects";
            public const string SE = "Sound Effects";
            public const string BATTLEBACK_1 = "Battle Background Floor Images";
            public const string BATTLEBACK_2 = "Battle Background Top Images";
            public const string DATA = "System Data";
            public const string PARALLAX = "Parallaxes";
            public const string PICTURE = "Pictures";
            public const string PLUGIN = "Plugins";
            public const string SYS_IMAGE = "System Images";
            public const string TITLE_1 = "Title Background Images";
            public const string TITLE_2 = "Title Border Images";
            public const string CHARACTER = "Character Images";
            public const string TILESET = "Tileset Atlas Images";
            public const string MOVIE = "Video Files";
        }

        public static class GenGendersProperName
        {
            public const string MALE = "Male";
            public const string FEMALE = "Female";
            public const string KID = "Kid";
        }

        public static class GenGenders
        {
            public const string MALE = "M";
            public const string FEMALE = "F";
            public const string KID = "K";
        }
        public static class GenFileTypes
        {
            public const string FACE = "Face";
            public const string SV = "SV";
            public const string SV_C = "SV_C";
            public const string TV = "TV";
            public const string TV_C = "TV_C";
            public const string TVD = "TVD";
            public const string TVD_C = "TVD_C";
            public const string VARIATION = "Var";
        }
    }
}
