using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.SAMAPI;

namespace RMMV_PackMan
{
    static class WebLinks
    {
        public const string BUG_REPORT = "https://indieteur.com/contact-about/";
        public const string SUPPORT_KO_FI = "https://ko-fi.com/indieteur";
        public const string SUPPORT_PATREON = "https://www.patreon.com/Indieteur";
        public const string HOMEPAGE = "https://indieteur.com/rmmv-package-manager/";
        public const string LICENSE = "https://github.com/Indieteur/RMMV_Package_Manager/blob/master/LICENSE";
        public const string GITHUB = "https://github.com/Indieteur/RMMV_Package_Manager";
        public const string PRE_MADE_PACKS = "https://indieteur.com/rmmv-package-manager-packages/";
        public const string UPDATES = "https://indieteur.com/rmmv-package-manager/";
    }

    static class FileDialogFilters
    {
        public const string PACKAGE = "Supported Installer File|*.xml;*.zip|" 
                                   + "Install Script File (*.xml)|*.xml|" 
                                   +  "Packaged Installer (*.zip)|*.zip";

        public const string LICENSE_FILE = "Supported Document File|*.txt;*.pdf;*.doc;*.docx;*.rtf|"
                                        + "Text File (*.txt)|*.txt|"
                                        + "Portable Document Format (*.pdf)|*.pdf|"
                                        + "Legacy Word Document Format (*.doc)|*.doc|"
                                        + "Word Document Format (*.docx)|*.docx|"
                                        + "Rich Text Format (*.rtf)|*.rtf";

        public const string ASSET_FILES = "RMMV Supported Asset File|*.m4a;*.ogg;*.json;*.js;*.png;*.mp4;*.webm|"
                                    + "RMMV Audio Format (*.m4a;*.ogg)|*.m4a;*.ogg|"
                                    + "RMMV System Data Format (*.json)|*.json|"
                                    + "RMMV Plugin File (*.js)|*.js|"
                                    + "RMMV Image Format (*.png)|*.png|"
                                    + "RMMV Video Format (*.mp4;*.webm)|*.mp4;*.webm";

        public const string AUDIO_FORMAT = "RMMV Audio Format (*.m4a;*.ogg)|*.m4a;*.ogg|"
            + "M4A Audio File (*.m4a)|*.m4a|"
            + "OGG Audio File (*.ogg)|*.ogg";

        public const string VIDEO_FORMAT = "RMMV Video Format (*.mp4;*.webm)|*.mp4;*.webm|"
           + "MP4 Video File (*.mp4)|*.mp4|"
           + "WEBM Video File (*.webm)|*.webm";

        public const string TILESET_FORMAT = "RMMV Tileset Format (*.png;*.txt)|*.png;*.txt|"
         + "RMMV Tileset File (*.png)|*.png|"
         + "Tileset Metadata Text File (*.txt)|*.txt";

        public const string SYSTEM_DATA_FORMAT = "RMMV System Data Format (*.json)|*.json";
        public const string PLUGIN_FORMAT = "RMMV Plugin File (*.js)|*.js";
        public const string IMAGE_FORMAT = "RMMV Image Format (*.png)|*.png";

        public const string BACKUP = "Backup File (*.zip)|*.zip";
        public const string MV_PROJ_FILE = "RMMV Project File (*.rpgproject)|*.rpgproject";

        public const string INSTALL_XML = "Install Script File (*.xml)|*.xml";
        public const string INSTALL_ZIP = "Install Archive File (*.zip)|*.zip";
      
    }

    static class Vars
    {
        

        public enum AppMode
        {
            Full,
            MVNotInstalled
        }

        public const string INSTALLATION_MV_FOLDER = "RPG Maker MV"; //Default install folder name of RPG Maker MV
        public const string INSTALLATION_MV_PATH_x86 = @"C:\Program Files\KADOKAWA\RPGMV";
        public const string INSTALLATION_MV_PATH_x64 = @"C:\Program Files (x86)\KADOKAWA\RPGMV";
        public const string INSTALLTION_MV_EXEC = "RPGMV.exe"; //Default install file name of RPG Maker MV
        public const string PACKAGE_MANAGER_DIRECTORY = "idt_packman";
        public const string RESOURCES_FOLDER = "res";
        public const string DEFAULT_PROJECT_FILES_PACKAGE_FILE = "BaseNewProject.xml";
        public const string DEFAULT_GENERATOR_FILES_PACKAGE_FILE = "BaseGenerator.xml";
        public const string INSTALLED_XML_FILENAME = "Package.xml";
        public const string INSTALLED_ARCH_FILENAME = "Package.zip";
        public const string INSTALL_FILE_DEFAULT_FILENAME = "install.xml";
        public const string DEFAULT_GENERATOR_FILE_PACK_UID = "mv.defaultgeneratorparts";
        public const string DEFAULT_PROJFILE_PACK_UID = "mv.defaultnewprojfiles";
        public const string TEMP_DIR_FOLDER = "temp";
        public const string TEMP_INSTALL_DIR = "install";
        public const string TEMP_MAKE_ZIP_DIR = "make";
        public const string NEWDATA_DIR = "NewData";
        public const string TEMP_RENUMBER_DIR = "renumber";
        public const string SDF_GENFLOOR_FILENAME = "GeneratorFloor.sdf";
        public const string SDF_GENSPECIAL_FILENAME = "GeneratorSpecial.sdf";
        public const string USER_LOG_FOLDER = "logs\\user";
        public const string DEBUG_LOG_FOLDER = "logs\\debug";
        public const string LICENSE_FILE_DEF_NAME = "LICENSE";
        public const string TEMP_BACKUP_DIR = "backup";


        public const int STEAM_MV_APPID = 363890;

      
        public static AppMode ApplicationMode = AppMode.Full;


        public static bool FirstRun;
        public static bool MustExitAtStart = false;

        public const string FRMPROPPACK_CREATENEW_TITLE = "Create New Package";
        public const string FRMPROPPACK_MODFY_TITLE = "Modify Package";
        
        

        
    }

    static class FileExtensions
    {
        public const string ZIP = ".zip";
        public const string XML = ".xml";
    }

    static class DirectoryNames
    {
        
        public static class Generator
        {
            public const string ROOT = "Generator";
            public const string FACE = "Face";
            public const string SV = "SV";
            public const string TV = "TV";
            public const string TVD = "TVD";
            public const string VARIATION = "Variation";
            public const string FEMALE = "Female";
            public const string KID = "Kid";
            public const string MALE = "Male";
        }
        public static class Audio
        {
            public const string ROOT = "audio";
            public const string BGM = "bgm";
            public const string BGS = "bgs";
            public const string ME = "me";
            public const string SE = "se";
        }
        public static class ProjectFiles
        {
            public const string DATA = "data";
            public static class Image
            {
                public const string ROOT = "img";
                public const string ANIMATION = "animations";
                public const string BATTLEBACKS_1 = "battlebacks1";
                public const string BATTLEBACKS_2 = "battlebacks2";
                public const string PARALLAXES = "parallaxes";
                public const string PICTURES = "pictures";
                public const string SYSTEM = "system";
                public const string TITLES_1 = "titles1";
                public const string TITLES_2 = "titles2";
                public const string CHARACTERS = "characters";
                public const string ENEMIES = "enemies";
                public const string FACES = "faces";
                public const string SV_ACTORS = "sv_actors";
                public const string SV_ENEMIES = "sv_enemies";
                public const string TILESETS = "tilesets";
            }
            public const string PLUGINS = "js\\plugins";
            public const string MOVIES = "movies";

        }
    }

   
}
