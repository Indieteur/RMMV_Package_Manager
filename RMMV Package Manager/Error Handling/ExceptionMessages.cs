using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static class ExceptionMessages
    {
        public static class General
        {
            public const string FILE_PREFIX = "File ";
            public const string DIR_PREFIX = "Directory ";
            public const string FOR_PACKAGE = " for package ";

            public const string PACK_W_ID = "Package with unique identifier \"";

            public const string COULD_NOT_BE_FOUND = " could not be found.";
            public const string COULD_NOT_BE_FOUND_DQ = "\" could not be found.";
            public const string IS_INVALID = " is invalid.";
            public const string ALREADY_EXISTS = " already exists.";
            public const string ALREADY_EXISTS_DQ = "\" already exists.";


            public const string PROJ_PATH_ARG_NULL = "Argument projectPath is set to null.";
            public const string PACK_PATH_ARG_NULL = "Argument packagePath is set to null.";
            public const string OPEN_PROJ_ARG_OR_DIRPATH_NULL = "Argument openProject is set to null or its DirectoryPath property is null or the directory could not be found.";
            public const string OPEN_PROJ_ARG_NULL = "Argument openProject is set to null.";
            public const string OPEN_PROJ_DIR_PATH_ARG_NULL = "Argument openProject's directory path is set to null.";
            public const string ARG_ARG_NULL = "Argument arg is set to null.";
            public const string ROOTDIR_ARG_NULL = "Argument rootDir is set to null.";
            public const string DESTDIR_ARG_NULL = "Argument destDir is set to null.";

           
            
            public static string PackWIDExists(string UID)
            {
                return PACK_W_ID + UID + ALREADY_EXISTS_DQ;
            }

            public static string FileNotFound(string path)
            {
                return FILE_PREFIX + path + COULD_NOT_BE_FOUND;
            }

            public static string DirNotFound(string path)
            {
                return DIR_PREFIX + path + COULD_NOT_BE_FOUND;
            }

            public const string FILE_EXT_INVALID = "File extension of file ";
            public static string FileExtInvalid(string path)
            {
                return FILE_EXT_INVALID + path + IS_INVALID;
            }

       
            public static string PackWIDNotFound(string UID)
            {
                return PACK_W_ID + UID + COULD_NOT_BE_FOUND_DQ;
            }

            public const string PROJ_NO_PACKAGES = "Project does not have any installed packages.";

        }
        public static class RMGenFile
        {
            public static class RetrieveInstallFileName
            {
                public static class Error
                {
                    public const string INVALID_FIELDS_DETECTED = "Fields ";
                    public const string INVALID_FIELDS_DETECTED_1 = " are invalid or not set.";
                    public const string INVALID_FIELDS_DETECTED_OPT = "A field or field(s) are invalid or not set.";
                    public static string InvalidFieldsDetected(RMMV_PackMan.RMGenFile.GenFileFields fields)
                    {
                        string toStringVal = fields.ToLogString();
                        if (string.IsNullOrWhiteSpace(toStringVal))
                            return INVALID_FIELDS_DETECTED_OPT;
                        return INVALID_FIELDS_DETECTED + toStringVal + INVALID_FIELDS_DETECTED_1;
                    }

                }
            }
            
        }



        public static class PackageManagement
        {
            public static class InstalledPackage
            {
                public const string MISS_XML = "An installed package required XML file is missing. Expected location of file is ";
                public const string INVALID_XML = "An installed package XML file is corrupted. Path to file is ";
            }

            public static class Installer
            {

              
              
                public const string ASSETS_NOT_FOUND = "File or Multiple Files could not be found.";
                public const string INSTALLER_FILE_NOT_FOUND = "Installer File not found at ";

                
            }

            public static class Uninstaller
            {
                public const string INSTALL_SCRIPT_FILE_NOT_FOUND = "Unable to find the install script file. Path to file is ";
            }

            public static class Reinstaller
            {
                public const string ARCH_INSTALL_FILE_NOT_FOUND = "Backup package file could not be found";
            }
        }

        public static class RMPackage
        {
            public const string INVALID_PACK = "Package \"";
            public const string COLL_TYPE_PREFIX = "Collection of type \"";

            public const string INVALID_LIC_URL = "\" has an invalid license URL.";
            public static string InvalidLicURL(string packageName)
            {
                return INVALID_PACK + packageName + INVALID_LIC_URL;
            }

            public const string NOT_VALID_PACK_FILE = " is not a valid package file.";

            public const string NO_NAME = "\" has no name.";
            public static string NoName(string xmlPathOrPackName)
            {
                return INVALID_PACK + xmlPathOrPackName + NO_NAME;
            }

            public const string NO_LIC = "\" has no License!";
            public static string NoLic(string xmlPathOrPackName)
            {
                return INVALID_PACK + xmlPathOrPackName + NO_LIC;
            }

            public const string NO_UID = "\" has no Unique ID!";
            public static string NoUID(string xmlPathOrPackName)
            {
                return INVALID_PACK + xmlPathOrPackName + NO_UID;
            }

            public const string INVALID_LIC_FILE = "\" has an invalid or missing license file.";
            public static string InvalidLicFile(string packageName)
            {
                return INVALID_PACK + packageName + INVALID_LIC_FILE;
            }

            public const string LIC_SOURCE_INVALID = "License source is invalid for package ";
            public const string LIC_SOURCE_NOT_SET = "License source not specified for package ";
            public const string LIC_FILE_PATH_NULL = "Package License file path is set to null.";

            public const string LIC_FILE_NOT_EXIST = "Package License file ";
            public const string LIC_FILE_NOT_EXIST_1 = " doesn't exist.";
            public static string LicFileNotExist(string path)
            {
                return LIC_FILE_NOT_EXIST + path + LIC_FILE_NOT_EXIST_1;
            }

           

            public const string COLL_NO_TYPE = "Collection does not specify its type or has an invalid type.";
            public const string COLL_NO_NAME = "Collection has no name or has invalid name.";
            public const string COLL_NO_PARENT = "Collection's parent field is set to null";

           
            public static string CollAlreadyExistsType(string collType)
            {
                return COLL_TYPE_PREFIX + collType + General.ALREADY_EXISTS_DQ;
            }

            public const string TILESET_FILE_PATH_NULL = "Tileset file path is set to null.";
            public const string TILESET_GROUP_NO_NAME = "Tileset group has no name or has an invalid name.";
            public const string TILESET_FILE_TYPE_NOT_SET = "Tileset file doesn't specify its file type or has an invalid file type.";
            public const string TILESET_FILE_ORDER_INVALID = "Tileset file has an invalid type/order.";

            public const string MOVIE_FILE_PATH_NULL = "Movie file path is set to null.";
            public const string MOVIE_FILE_NO_NAME = "Movie file has no name or has an invalid name.";
            public const string MOVIE_TYPE_NOT_SET = "Movie file doesn't specify its type or has an invalid type.";

            public const string GEN_FILE_PATH_NULL = "Generator file path is set to null.";
            public const string GEN_FILE_NO_TYPE = "Generator file doesn't specify its type or has an invalid type.";
            public const string GEN_FILE_NO_GENDER = "Gender of generator part is not specified or is invalid.";
            public const string GEN_FILE_NO_HIGH_ORDER = "Generator file doesn't specify its high order or has an invalid high order.";
            public const string GEN_FILE_NO_LOW_ORDER = "Generator file doesn't specify its low order or has an invalid low order.";
            public const string GEN_FILE_NO_COLOUR = "Generator file doesn't specify its colour or has an invalid colour.";
            public const string GEN_FILE_NO_POS = "Generator file doesn't specify its position or has an invalid position.";

            public const string AUDIO_COLL_NO_TYPE = "Audio collection does not specify its type or has an invalid type.";
            public const string AUDIO_GROUP_NO_NAME = "Audio group has no name or has an invalid name.";
            public const string AUDIO_FILE_PATH_NULL = "Audio file path is set to null.";
            public const string AUDIO_INVALID_FILE_TYPE = "Audio file doesn't specify its file type or has an invalid file type.";

            public const string CHAR_FILE_NO_TYPE = "Character image file doesn't specify its type or has an invalid type.";
            public const string CHAR_GROUP_NO_NAME = "Character image group has no name or has an invalid name";
            public const string CHAR_FILE_PATH_NULL = "Character image file path is set to null.";

            public const string FILE_PATH_NULL = "Asset file path is set to null.";

        }

        public static class PackUtil
        {
            public const string AUDIO_PATH_REL = "Audio file's path is already relative.";
            public const string CHAR_IMG_PATH_REL = "Character image file's path is already relative.";
            public const string GEN_FILE_PATH_REL = "Generator file's path is already relative.";
            public const string MOVIE_FILE_PATH_REL = "Movie file's path is already relative.";
            public const string FILE_PATH_REL = "Asset file's path is already relative.";
            public const string TILESET_FILE_PATH_REL = "Tileset file's path is already relative.";

            public const string PACK_FILE_ROOTED = "Package file's path is already rooted.";
        }

        public static class GUI
        {
            public static class frmPropPack
            {
                public const string NO_ASSETS_SEL = "No assets have been selected.";
            }
            public static class frmPackageAssets
            {
                public const string FILE_PATH_NULL = "File path is set to null.";
                public const string TAG_SEL_NODE_NULL = "The tag of the selected node is set to null.";
                public const string ASSOC_OBJ_TAG_SEL_NODE_NULL = "The associated object of the tag of the selected node is set to null.";
                public const string PARENT_NODE_NULL = "The parent node of the selected asset is set to null.";
            }
        }

        public static class Helper
        {
            public const string DATA_TO_WRITE_ARG_NULL = "Argument dataToWrite is set to null.";
        }
    }
}
