using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    static class MessageBoxStrings
    {
        public const string MESSAGEBOX_NAME = "RMMV Package Manager";
        public const string MESSAGEBOX_NAME_LIC = "License";
        public const string UNHANDLED_EXCEPTION = "The application has encoutered an unexpected error. Due to this, we recommend that you quit the application as it might cause corruption or further corruption to your RPG MV/Project Files. Do you wish to exit the application?";
        public const string REPORT_BUG = "We advise you to report the unexpected issue to the developer of the application so that it can be looked at and resolved. We might ask for the log files stored in the application's directory so please do not delete them. Do you wish to report the issue now?";
        public const string RESTART_REQ = ". The application will need to restart in order for the changes to take effect.";
        public const string TO_STR = " to ";

        public static class Helper
        {
            public const string FILE_SUFFIX = "! Please close the program(s) accessing the file. Press the OK button to retry or CANCEL if you want to abort the operation.";

            public const string COPY_FILE_FAIL_ASK = "Unable to copy file ";
            public const string COPY_FILE_FAIL_ASK_1 = "! Please check if the destination directory exists. Make sure to also check that the destination file doesn't already exist or if it does, that it can be overwritten. Press the OK button to retry or CANCEL if you want to abort the operation.";
            public static string CopyFileFailed(string sourceFile, string destFile)
            {
                return COPY_FILE_FAIL_ASK + sourceFile + TO_STR + destFile + COPY_FILE_FAIL_ASK_1;
            }

            public const string WRITE_FAIL_ASK = "Unable to write to file ";
            public static string WriteFailed(string path)
            {
                return WRITE_FAIL_ASK + path + FILE_SUFFIX;
            }

            public const string DEL_FILE_FAIL_ASK = "Unable to delete file ";
            public static string DelFileFailed(string path)
            {
                return DEL_FILE_FAIL_ASK + path + FILE_SUFFIX;
            }

            public const string DEL_DIR_FAIL_ASK = "Unable to delete folder ";
            public const string DEL_DIR_FAIL_ASK_1 = "! Please close the program(s) browsing or accessing any file(s) in this directory. Press the OK button to retry or CANCEL if you want to abort the operation.";
            public static string DelDirFailed(string path)
            {
                return DEL_DIR_FAIL_ASK + path + DEL_DIR_FAIL_ASK_1;
            }

            public const string CREATE_DIR_TREE_FAIL_ASK = "Unable to create directory tree at ";
            public const string CREATE_DIR_TREE_FAIL_ASK_1 = "! Please make sure that you have granted the program the right permissions. Press the OK button to retry or CANCEL if you want to abort the operation.";
            public static string CreateDirTreeFailed(string path)
            {
                return CREATE_DIR_TREE_FAIL_ASK + path + CREATE_DIR_TREE_FAIL_ASK_1;
            }
        }

        public static class AtTheStart
        {
            public const string UNABLE_TO_LOCATE_MV_DIR = "The application was unable to locate RPG Maker MV's installation directory. Please specifiy the path to RPG Maker MV's root directory.";
            public const string INVALID_MV_DIR = "The path you specified is not a valid RPG Maker MV installation directory. Please try again.";
            public const string MV_DIR_NOT_SPECIFIED = "The path to RPG Maker MV installation directory was not specified. Some of the Application features will be disabled.";
            public const string EXPIRED_MV_DIR = "The stored path to RPG Maker MV installation directory is not valid anymore. Please specify the path to RPG Maker MV's root directory.";
            public const string MV_DIR_CHANGED = "Please be advised that the RPG Maker MV installation directory has changed. Currently installed packages might be different or have changed.";
            public const string TEMP_DIR_NOT_ABLE = "Unable to create the required temporary folder. Exiting application.";
            public const string CONFIG_FILE_NOT_FOUND = "Unable to start application due to a problem when reading the default config file. Please make sure that the application has been granted the right permissions and that the default config file that came with the application exists.";
            public const string SI_PROC_RETRIEVE_FAIL = "An error occured when retrieving a list of running processes. Application initialization has been aborted.";
            public const string SI_CHECK_FAIL = "An instance of the application is already running. Initialization has been aborted.";

        }

        public static class Logger
        {
            public const string USER_DIR_NOT_ABLE = "Unable to create user log folder. Event logging will be disabled.";
            public const string DEBUG_DIR_NOT_ABLE = "Unable to create debug log folder. Event logging will be disabled.";
            public const string LIMIT_NOT_ABLE = "An error occured when trying to clean the log folders.";
            public const string LOG_NOT_ABLE = "An error occured when trying to create log files. Event logging will be disabled.";
            public const string LOG_NOT_ABLE_CHOICE = "An error occured when trying to write a message to log files. Message might not have been written. Do you wish to disable event logging?";
            public const string UNABLE_VIEW_BUG_REPORT_URL = "An error occured when launching default web browser to open Bug Reporting URL.";

        }
        public static class GUI
        {
            public const string PROJECT_STR = "Project ";
            public const string INVALID_PROJ_URL = "Unable to open link. Package has an invalid Project URL.";
            public const string UNABLE_VIEW_PROJ_URL = "An error occured when launching default web browser to view Project URL.";
            public const string UNABLE_VIEW_LIC_FILE = "An error occured when launching default application to view License File.";
            public const string UNABLE_VIEW_LIC_URL = "An error occured when launching default application to open the web page containing the package license.";
            public const string INVALID_LICENSE = "Unable to show license for package as it is invalid. We strongly advise you to not use this package and/or consult with the author of the package.";
            public const string INSTALL_PACKAGE_INVALID_SELECTION = "A problem occured when trying to load selected package. The package you have selected might be corrupted or is being accessed by another program. Please try again.";
            public const string INSTALL_PACKAGE_FAILED = "The program has encountered a fatal error when installing package. Please see log for more details.";
            public const string INSTALL_PACKAGE_CORRUPTED = "A problem occured when trying to load selected package. The package you have selected is corrupted.";
            public const string INSTALL_PACKAGE_NO_CHECKSUM = "The program has detected no stored checksum information in the archive that you have selected. Most of the time, this is caused by the package being created outside of the package manager. However, this could also mean that the package is corrupted or has been maliciously modified. Do you still wish to proceed?";
            public const string INSTALL_PACKAGE_DONE = "Package located at ";
            public const string INSTALL_PACKAGE_DONE_1 = " has successfully been installed.";
            public static string InstallPackageDone(string packagePath)
            {
                return INSTALL_PACKAGE_DONE + packagePath + INSTALL_PACKAGE_DONE_1;
            }



            public const string BACKUP_PROMPT_GLOBAL = "The operation will modify the globally installed assets (e.g. Generator Parts). Do you wish to make a backup of the installed assets first?"; 
            public const string BACKUP_PROMPT_LOCAL = "The operation will modify the project assets (e.g. audio, sprites). Do you wish to make a backup of the project first?";

            public const string INSTALL_PACKAGE_SIMILAR = "Another version of package ";
            public const string INSTALL_PACKAGE_SIMILAR_1 =   " is already installed. The currently installed version is ";
            public const string INSTALL_PACKAGE_SIMILAR_2 = " while the version of the package to be installed is ";
            public const string INSTALL_PACKAGE_SIMILAR_3 = ". Do you wish to proceed?";

            

            public static string InstallPackageSimilar(string oldVer, string newVer, string packageName)
            {
                if (string.IsNullOrWhiteSpace(oldVer))
                    oldVer = "not specified";
                if (string.IsNullOrWhiteSpace(newVer))
                    newVer = "not specified";
                return INSTALL_PACKAGE_SIMILAR + packageName + INSTALL_PACKAGE_SIMILAR_1 + oldVer + INSTALL_PACKAGE_SIMILAR_2 + newVer + INSTALL_PACKAGE_SIMILAR_3;
            }

            public const string UNABLE_PERFORM_ACTION_NO_OPEN_PROJ = "Unable to perform requested action as there is currently no open project.";
            public const string UNABLE_MAKE_BACKUP_GLOBAL = "An error occured when creating a backup of global assets at ";
            public const string MAKE_BACKUP_GLOBAL_SUCCESS = "A backup of the RMMV global assets has been successfully created at ";

            public const string UNABLE_RESTORE_BACKUP_GLOBAL = "An error occured when restoring a backup of global assets from ";
            public const string RESTORE_GLOBAL_BACKUP_SUCCESS = "A backup of the RMMV global assets has been successfully restored from ";
            
            public static string RestoreGlobalBackupSuccess(string path)
            {
                return RESTORE_GLOBAL_BACKUP_SUCCESS + path + RESTART_REQ;
            }

            public const string UNABLE_MAKE_BACKUP_LOCAL = "An error occured when creating a backup of project assets at ";
            public const string MAKE_BACKUP_LOCAL_SUCCESS = "A backup of the opened project assets has been successfully created at ";
            public const string UNABLE_MAKE_LOCAL_BACKUP_NO_OPEN_PROJ = "You haven't opened any project. Unable to proceed with making a project backup.";
            public const string UNABLE_RESTORE_LOCAL_BACKUP_NO_OPEN_PROJ = "You haven't opened any project. Unable to proceed with restoring a project backup.";

            public const string UNABLE_RESTORE_LOCAL_BACKUP = "An error occured when restoring a backup of project assets from ";
            public const string RESTORE_LOCAL_BACKUP_SUCCESS = "A backup of the project assets has been successfully restored from ";
            public const string RESTORE_LOCAL_BACKUP_SUCCESS_1 = ". The program will reload the project in order for the changes to take effect.";
            public static string RestoredLocalBackupSuccess(string path)
            {
                return RESTORE_GLOBAL_BACKUP_SUCCESS + path + RESTORE_LOCAL_BACKUP_SUCCESS_1;
            }

            public const string OPEN_PROJECT_ERROR = "An error occured when opening project ";
            public const string RELOADING_PROJECT_ERROR = "An error occured when reloading project ";

           
            public const string OPEN_PROJECT_SUCCESS_1 = " has successfully been opened.";
            public static string OpenProjectSuccess(string path)
            {
                return PROJECT_STR + path + OPEN_PROJECT_SUCCESS_1;
            }

            public const string CLOSE_PROJECT = " has successfully been closed.";
            public static string CloseProjectSuccess(string path)
            {
                return PROJECT_STR + path + CLOSE_PROJECT;
            }

            public const string RESTORE_PROJECT = " has successfully been re-opened.";
            public static string RestoreProjectSuccess(string path)
            {
                return PROJECT_STR + path + RESTORE_PROJECT;
            }

            public const string UNINSTALL_PACKAGE = "Do you wish to uninstall package ";
            public const string UNINSTALL_PACKAGE_NO_PACK_NAME = "Do you wish to uninstall the selected package";
            public const string UNINSTALL_PACKAGE_PROJ = " from the currently open project located at ";
            public static string UninstallPackageProject(string packageName, string projectPath)
            {
                if (string.IsNullOrWhiteSpace(packageName))
                {
                    if (string.IsNullOrWhiteSpace(projectPath))
                        return UNINSTALL_PACKAGE_NO_PACK_NAME + "?";
                    return UNINSTALL_PACKAGE_NO_PACK_NAME + UNINSTALL_PACKAGE_PROJ + projectPath + "?";
                }
                if (string.IsNullOrWhiteSpace(projectPath))
                    return UNINSTALL_PACKAGE + packageName + "?";
                return UNINSTALL_PACKAGE + packageName + UNINSTALL_PACKAGE_PROJ + projectPath + "?";
            }

            public const string UNINSTALL_PACKAGE_ERR_GENERAL = "An error occured when uninstalling selected package. Please see log for more details.";
            public const string PACK_WITH_NAMESPACE = "Package with namespace ";
            public const string UNINSTALL_PACKAGE_DONE_1 = " has successfully been uninstalled.";
            public static string UninstallPackageDone(string packageNamespace)
            {
                return PACK_WITH_NAMESPACE + packageNamespace + UNINSTALL_PACKAGE_DONE_1;
            }

            public const string UNINSTALL_SEL_PACKAGE_FIRST = "Unable to proceed with operation. Please select a package to uninstall first.";
            public const string UPDATE_SEL_PACKAGE_FIRST = "Unable to proceed with operation. Please select a package to update first.";
            public const string VIEW_ASSETS_SEL_PACKAGE_FIRST = "Unable to proceed with operation. Please select a package first and then try again.";

            public const string UPDATE_PACKAGE_ERR_GENERAL = "An error occured when updating selected package. Please see log for more details.";
            public const string UPDATE_PACKAGE_NON_SIMILAR = "Unable to update currently installed package with the selected one. Their namespace does not match. Please try again.";
            public const string UPDATE_PACKAGE_FAILED = "The program has encountered a fatal error when installing a newer version of the package. Please see log for more details.";
            public const string UPDATE_PACKAGE_DONE = "Package ";
            public const string UPDATE_PACKAGE_DONE_1 = " has successfully been update.";
            public static string UpdatePackageDone(string packageName)
            {
                return UPDATE_PACKAGE_DONE + packageName + UPDATE_PACKAGE_DONE_1;
            }

            public const string REINSTALL_PACKAGE_ERR_GENERAL = "An error occured when reinstalling selected package. Please see log for more details.";
            public const string REINSTALL_PACKAGE_PROMPT = "Do you wish to reinstall the selected package?";

            public const string REINSTALL_PACKAGE_FAILED = "The program has encountered a fatal error when reinstalling package. Please see log for more details.";

            public const string REINSTALL_PACKAGE_DONE_1 = " has successfully been reinstalled.";
            public static string ReinstallPackageDone(string packageNamespace)
            {
                return PACK_WITH_NAMESPACE + packageNamespace + REINSTALL_PACKAGE_DONE_1;
            }

            public const string REINSTALL_SEL_PACKAGE_FIRST = "Unable to proceed with operation. Please select a package to reinstall first.";
            public const string MODIFY_PACK_SELECT_XML_DIR = "You have selected a standalone XML script. Please specify the script's asset directory.";

            public const string MODIFY_PACK_XML_READ_ERR = "Unable to load standalone XML file. The XML file you have selected is invalid or corrupted.";
            public const string MODIFY_PACK_ZIP_TEMP_DIR_ERR = "Unable to load package archive due to a file system issue. Please see log for more details.";

            public const string MODIFY_PACK_ZIP_GEN = "Unable to load package archive. The file you have selected might be invalid or corrupted.";
            public const string MODIFY_PACK_ZIP_ARCH_NO_CHECK= "The package you have selected doesn't have a stored checksum and it might be corrupted. But, it could also be due to the archive being created using a 3rd party application. Do you wish to continue loading the package?";

            public const string VIEW_PACKAGE_ERR_GENERAL = "An error occured when viewing assets of selected package. Please see log for more details.";

            public const string SUPPORT_KO_FI_ERROR = "An error occured when launching default application to view Ko-Fi page. Please manually visit \"" + WebLinks.SUPPORT_KO_FI + "\".";
            public const string SUPPORT_PATREON_ERROR = "An error occured when launching default application to view Patreon page. Please manually visit \"" + WebLinks.SUPPORT_PATREON + "\".";
            public const string VIEW_PREMADE_PACK_ERROR = "An error occured when launching default application to view Pre-Made packages page. Please manually visit \"" + WebLinks.PRE_MADE_PACKS + "\".";

            public const string VIEW_LINK_ERROR = "An error occured when launching default application to view selected link.";

            public const string CHECK_FOR_UPDATES_CUR_VER = "Please take note of the current version of the program you are using which is ";
            public const string CHECK_FOR_UPDATES_ERROR = "An error occured when launching default application to view the application webpage. Please manually visit \"" + WebLinks.UPDATES + "\".";


            public static class frmLicenseEdit
            {
                public const string FILE_NOT_FOUND = "An error occured when saving license information. License file ";
                public const string FILE_NOT_FOUND_1 = " could not be found or is invalid.";
                public static string FileNotFound(string filePath)
                {
                    return FILE_NOT_FOUND + filePath + FILE_NOT_FOUND_1;
                }

                public const string TEXT_EMPTY = "An error occured when saving license information. License text cannot be empty.";
                public const string INVALID_URL = "An error occured when saving license information. Provided URL is invalid.";
            }
            public static class frmAddAssetGroup
            {
                public const string NULL_NAME = "Unable to add asset group. Name cannot be empty or contain invalid filename characters (e.g. \\, /, ?, %, *, :, |, \", <, >).";
            }

            public static class frmPropPack
            {
                public const string INVALID_DIR = "An error occured when probing for RMMV compatible assets in the specified directory. The directory does not exist anymore or is invalid.";
                public const string UNABLE_RETRIEVE_PACK = "An error occured when probing for RMMV compatible assets in the specified directory. Please see log for more details.";
                public const string NO_IMPLICIT_DIR = "An error occured when probing for RMMV compatible assets in the specified directory. The directory does not exist anymore or is invalid.";
                public const string NO_COMMON_PATH = "Please be advised that the assets and/or the license file you have selected does not have a common path. Due to this, you won't be able to make a standalone XML install script.";
                public const string LICENSE_FILE_NON_RELATIVE = "Please be advised that the license file you have selected is not under the assets directory you have chosen. Due to this, you won't be able to make a standalone XML install script.";
                public const string SEL_COMMON_PATH_SAVE_XML = "The common root directory of the assets you have selected is ";
                public const string SEL_COMMON_PATH_SAVE_XML_1 = ". For the install script to be parsed properly, the XML file that will be generated needs to be under that directory.";
                public static string SelCommonPathSaveXML(string path)
                {
                    return SEL_COMMON_PATH_SAVE_XML + path + SEL_COMMON_PATH_SAVE_XML_1;
                }

                public const string NO_IMPLICIT_DIR_SEL_SAVE = "Unable to proceed with the operation. Please select an asset directory first and then try again.";
                public const string NO_EXPLICIT_ASSET_SEL_SAVE = "Unable to proceed with the operation. Please select an asset(s) to be imported first and then try again.";

                public const string UNABLE_SAVE_XML = "An error occured when saving install script file to ";
                public const string UNABLE_SAVE_XML_1 = ". Operation has been aborted. Please see log for more details.";
                public static string UnableSaveXML(string path)
                {
                    return UNABLE_SAVE_XML + path + UNABLE_SAVE_XML_1;
                }

                public const string SUCCESS_XML = "Install script has been successfully generated at ";
                public const string ZIP_SUCCESS = "Package asset archive has been successfully generated at ";

                public const string ZIP_FILE_MAKE_ERR_GEN = "An error occured when creating an archive of the selected assets. Please see log for more details.";
                public const string NO_ASSET_SELECTED = "Unable to create a package archive. No asset(s) have been selected.";

                public const string NO_NAMESPACE = "Unable to make install script for package. The namespace field of the package is empty.";
                public const string NO_NAME = "Unable to make install script for package. The nmae field of the package is empty.";
                public const string NO_LIC = "Unable to make install script for package. No license for package was selected.";
                public const string INVALID_PACK_URL = "Unable to make install script for package. The Package URL you have specified is invalid.";

                public const string WHAT_NAMESPACE = "The Namespace field allows the program to uniquely identify one package from another. If two packages have similar namespaces, they'll be treated as different versions of the same package.\n\n The recommended format for the namespace is as follows: \"author.packagename\" (without the double quotes). The namespace should only contain small letters and no symbols (with the exception of the period symbol) and/or whitespaces.";
                public const string WHAT_NAME = "The Name field is the human readable identifier of the package.";
                public const string WHAT_VERSION = "The Version field refers to the version of the package being modified or created. If you want to modify the contents of an existing package, the newer package should have similar namespace with the older package but they should have different version number/string.";
                public const string WHAT_AUTHOR = "The Author field refers to the author/creator of the package and/or the assets in the package.";
                public const string WHAT_PACKAGE_URL = "The Package URL field must point to the official website/page of the package.";
                public const string EXIT_PROMPT = "Are you sure that you want to exit? Some changes might not have been saved yet.";

            }
            public static class frmOpenFile
            {
                public const string NO_PATH_TO_FILE = "Unable to proceed with operation. File to be opened could not be found or is invalid.";
                public const string NO_PATH_TO_APP = "Unable to proceed with operation. The application to be launched could not be found or is invalid.";
                public const string INVALID_ARG = "Unable to proceed with operation. The specified argument for the application to be launched is empty.";
                public const string FAILED_DEFAULT_APP_LAUNCH = "An error occured when launching default application to view selected file.";
                public const string FAILED_APP_LAUNCH = "An error occured when launching application to view selected file.";
            }
            public static class frmPackageAssets
            {
                public const string NO_FILE_TO_OPEN = "Unable to proceed with operation. File to be opened could not be found or is invalid.";
                public const string PACKAGE_NULL = "An error occured when performing operation. The program could not load the package assets.";
                public const string NO_SELECTION = "An error occured when performing operation. There is currently no selected item.";
                public const string ADD_NO_NAME_GROUP_ASSET = "Unable to add asset group due to it having no specified name.";
                public const string ADD_ASSET_GROUP_ERR_GENERAL = "A fatal error occured when adding asset group. See log for more details.";
                public const string CORRUPTED_MAIN = "A fatal error occured. Unable to load asset information. This is most likely due to the the package being corrupted. Please see log for more details.";
                public const string DELETE_SURE_GROUP = "Are you sure that you want to delete the selected asset group?";
                public const string DELETE_SURE_ASSET = "Are you sure that you want to delete the selected asset?";
                public const string DELETE_GROUP_ERR = "An error occured when deleting asset group. Please see log for more details.";
                public const string DELETE_ASSET_ERR = "An error occured when deleting asset. Please see log for more details.";
                public const string CHANGES_MADE = "Changes have not yet been saved. Are you sure you want to navigate away?";
                public const string HIGH_ORDER_INVALID = "High Order value of generator file must be numeric and greater than zero.";
                public const string LOW_ORDER_INVALID = "Low Order value of generator file must be numeric and greater than zero.";
                public const string COLOUR_INVALID = "Colour layer value of generator file must be numeric and greater than zero.";
                public const string INVALID_GROUP_NAME = "The name of the selected group cannot be empty/null.";
                public const string UNABLE_SAVE_GROUP_INFO = "An error occured when saving group information. Please see log for more information.";
                public const string UNABLE_SAVE_FILE_INFO = "An error occured when saving asset information. Please see log for more information.";
                public const string EXIT_PROMPT = "Are you sure that you want to exit without saving? Any changes made would be lost.";
            }

        }

        public static class PMFileSystem
        {
            public const string RESOURCE_DIR_NOT_FOUND = "Unable to start application due to resource directory being missing!";
            public const string BASEGEN_XML_MISS = "The Base Generator Xml Template was not found. Please be advised that the application might not work properly.";
            public const string DEFPROJFILE_XML_MISS = "The Default Project Files Xml Template was not found. Please be advised that the application might not work properly.";
            public const string SDF_UNINSTALLGENFILES_MISS = "A configuration file required during renumbering of generator parts after a package uninstall by the application is missing. Please be advised that the application might not work properly.";
            public const string BASEGEN_NOT_PARSED = "The application has detected that a package summary file has not yet been created for the currently installed Generator Parts. The application will now create that and it will also make a backup of the current Generator Parts.";
            public const string DEFPROJFILE_NOT_PARSED = "The application has detected that a package summary file has not yet been created for the default new project files. The application will now create that and it will also make a backup of the default new project files.";
            public const string BASEGEN_XML_MISS_REQ = "The Base Generator Xml Template required to create a summary file could not be found. Unable to create a package summary.";
            public const string DEFPROJFILE_XML_MISS_REQ = "The Default Project Files Xml Template required to create a summary file could not be found. Unable to create a package summary.";
            public const string MAN_DIR_CREATE_NOTABLE = "The application was unable to create the directory which will contain the required information for the installed global packages. Application might not work properly.";
            public const string BASEGEN_UNABLE_INST_DATA = "An error occured when trying to create an installed package metadata file and backup archive for the currently installed Generator Parts.";
            public const string NEWPROJ_UNABLE_INST_DATA = "An error occured when trying to create an installed package metadata file and backup archive for the currently installed New Project assets";
            public const string WARN_OCCUR_NEW_PROJ = "Warning(s) and/or Error(s) have been encountered while trying to create an installed package metadata file and backup archive for the currently installed New Project assets.";

        }
        public static class PackageManagement
        {
            public const string INIT_GLOBAL_GET_DIRS_ERROR = "Unable to retrieve the global packages installed. Global Packages Manifest directory might be missing or the application was not granted the right permissions.";
            public const string INIT_GLOBAL_ERRORS_FOUND = "An error or multiple errors have occured while trying to read the global packages manifest(s).";
            public const string INIT_ERROR_WARNS_FOUND = "A problem or problems were detected while trying to read the global packages manifest(s).";
            public const string UNABLE_MAKE_INST_PACKAGE = "Unable to create installed package metadata for existing files of package \"";

            public const string FAILED_MAKE_INST_PACKAGE_ARCH = "An error occured when trying to create a backup of files for package \"";
            public const string FAILED_MAKE_INST_PACKAGE_ARCH_1 = "\". The operation has been aborted. However, the application can still create a manifest of the currently installed files of the package which will allow you to uninstall the package anytime. Do you wish to proceed?";
            public static string FailedMakeInstallPackageArch(string packageName)
            {
                return FAILED_MAKE_INST_PACKAGE_ARCH + packageName + FAILED_MAKE_INST_PACKAGE_ARCH_1;
            }

            public const string ERR_INVALID_XML = "An error occured when trying to read ";
            public const string ERR_INVALID_XML_1 = " as an RMMV Package metadata file. The file might be corrupted or the application might have not been granted the right permissions.";
            public static string ErrorInvalidXML(string XMLPath)
            {
                return ERR_INVALID_XML + XMLPath + ERR_INVALID_XML_1;
            }


        }
        public static class General
        {
            public const string HAS_ERRORS_WARNINGS = "The application has encountered error(s) and/or warning(s) while performing the operation.";
        }
    }
}
