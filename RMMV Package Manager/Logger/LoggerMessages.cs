using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class LoggerMessages
    {
        public const string UNHANDLED_EXCEPTION = "Unhandled and unwrapped exception occured!";
        public const string MISSING_CFG_FILE = "Default config file required by the application cannot be read or is missing.";
        public const string TO_STR = " to "; 
        public const string FAILED = " has failed. "; 
        public const string OPPS_ABORTED = ". The operation has been aborted. ";

        public static class GeneralError
        {
            public const string UNABLE_DELETE_TEMP_DIR = "Unable to delete existing temporary directory at ";
            public static string UnableDeleteTempDir(string dirPath)
            {
                return UNABLE_DELETE_TEMP_DIR + dirPath + ". " + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
            }
            public static DeleteFolderLogMessages UNABLE_DELETE_TEMP_DIR_ARG = new DeleteFolderLogMessages(deleteFailed: UnableDeleteTempDir);

            public const string CREATE_TEMP_DIR_FAILED = "Unable to create required temporary directory at ";
            public static string CreateTempDirFailed(string path)
            {
                return CREATE_TEMP_DIR_FAILED + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
            }
            public static CreateFolderLogMessages UNABLE_CREATE_TEMP_DIR_ARG = new CreateFolderLogMessages(createFailed: CreateTempDirFailed);

            public const string UNABLE_DELETE_UNUSED_FOLDER = "Unable to delete unused directory at ";
            public static string UnableDeleteUnusedFolder(string path)
            {
                return UNABLE_DELETE_UNUSED_FOLDER + path + ". " + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
            }
            public static DeleteFolderLogMessages UNABLE_DELETE_UNUSED_DIR_ARG = new DeleteFolderLogMessages(deleteFailed: UnableDeleteUnusedFolder);

            public const string REQUIRED_DIR_EXISTS_DEL_FAILED = "A required directory for the operation being performed already exists. The program was unsuccessful in deleting it. Path to directory is ";
            public static string RequiredDirExistsDelFailed(string path)
            {
                return REQUIRED_DIR_EXISTS_DEL_FAILED + path + ". " + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
            }
            public static DeleteFolderLogMessages REQUIRED_DIR_EXISTS_DEL_FAILED_ARG = new DeleteFolderLogMessages(deleteFailed: RequiredDirExistsDelFailed);

            public const string OVERWRITE_FILE_FAILED = "Unable to overwrite file ";
            public static string OverwriteFailed(string path)
            {
                return OVERWRITE_FILE_FAILED + path + ". " + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
            }
            public static DeleteFileLogMessages OVERWRITE_FILE_FAILED_ARG = new DeleteFileLogMessages(deleteFailed: OverwriteFailed);

            public const string CREATE_REQUIRED_DIR_FAILED = "Unable to create the required directory by the operation at ";
            public static string CreateRequiredDirFailed(string path)
            {
                return CREATE_REQUIRED_DIR_FAILED + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
            }
            public static CreateFolderLogMessages CREATE_REQUIRED_DIR_FAILED_ARG = new CreateFolderLogMessages(createFailed: CreateRequiredDirFailed);

            public const string DELETE_UNUSED_FILE_FAILED = "Unable to delete unused file ";
            public static string DeleteUnusedFileFailed(string path)
            {
                return DELETE_UNUSED_FILE_FAILED + path + ". " + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
            }
            public static DeleteFileLogMessages DELETE_UNUSED_FILE_FAILED_ARG = new DeleteFileLogMessages(deleteFailed: DeleteUnusedFileFailed);

            public const string OVERWRITE_DIR_FAILED = "Unable to overwrite directory ";
            public static string OverwriteDirFailed(string path)
            {
                return OVERWRITE_DIR_FAILED + path + ". " + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
            }
            public static DeleteFolderLogMessages OVERWRITE_DIR_FAILED_ARG = new DeleteFolderLogMessages(deleteFailed: OverwriteDirFailed);

            public const string UNABLE_LAUNCH_URL = "An error occured when opening default web browser to view link \"";
            public const string UNABLE_VIEW_FILE = "An error occured when opening default application to view file ";
        }

      

        public static class Extension
        {
            public const string MISS_FILE = "File "; 
            public const string MISS_FILE_1 = " doesn't exist.";
           
        }

        public static class GeneratorPartsManager
        {
            public static class RenumberParts
            {

                public static class Info
                {
                    public const string RENUMBER_START = "Renumbering generator parts located under directory ";
                    public const string RENUMBER_END = "Successfully renumbered generator parts located under directory ";
                }

                public static class Error
                {
                    public const string INVALID_PATH_1 = " is invalid.";


                    public const string INVALID_PATH = "An error occured when renumbering generator parts. The path to file ";
                    public static string InvalidPath(string path)
                    {
                        return INVALID_PATH + path + INVALID_PATH_1;
                    }

                    public const string INVALID_PART = "An error occured when renumbering generator parts. ";
                    public const string INVALID_PART_1 = " cannot be renumbered due to invalid field(s).";
                    public static string InvalidPart(string path)
                    {
                        return INVALID_PART + path + INVALID_PART_1;
                    }

                    public const string INVALID_TEMP_PATH = "An error occured when renumbering generator parts. The destination path ";
                    public static string InvalidTempPath(string path)
                    {
                        return INVALID_TEMP_PATH + path + INVALID_PATH_1;
                    }

                    public const string UNABLE_MOVE = "An error occured when renumbering generator parts. Unable to move generator file ";

                    public const string UNABLE_MOVE_TO_TEMP = " to temporary location at ";
                    public static string UnableMoveToTemp(string sourceFile, string destFile)
                    {
                        return UNABLE_MOVE + sourceFile + UNABLE_MOVE_TO_TEMP + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_MOVE_TO_TEMP_SRC_NOT_FOUND = " to temporary location as the file could not be found.";
                    public static string UnableMoveToTempSrcNotFound(string sourceFile)
                    {
                        return UNABLE_MOVE + sourceFile + UNABLE_MOVE_TO_TEMP_SRC_NOT_FOUND;
                    }

                    public const string RENUMBER_ABORT_GENERAL = "An error occured when renumbering generator parts. Operation has been aborted.";

                   

                    public const string UNABLE_MOVE_FINAL = " from temporary folder back to ";
                    public static string UnableMoveFinal(string sourceFile, string destFile)
                    {
                        return UNABLE_MOVE + sourceFile + UNABLE_MOVE_FINAL + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }
                    public const string UNABLE_MOVE_FINAL_SRC_NOT_FOUND = " from temporary folder as it doesn't exist anymore.";
                    public static string UnableMoveFinalSrcNotFound(string sourceFile)
                    {
                        return UNABLE_MOVE + sourceFile + UNABLE_MOVE_FINAL_SRC_NOT_FOUND;
                    }




                    public const string FAILED_SAVE_XML = "A problem has been encountered when renumbering generator parts. One of the global packages was not updated due to a file system error.";
                    public const string FAILED_SAVE_XML_END = " This might cause issues if you uninstall any global package in the future.";
                    public const string FAILED_SAVE_XML_1 = "The XML file that was supposed to be updated is located at ";

                    public static string FailedSaveXML(string path)
                    {
                        if (!string.IsNullOrWhiteSpace(path))
                            return FAILED_SAVE_XML + FAILED_SAVE_XML_1 + path + "." + FAILED_SAVE_XML_END;
                        else
                            return FAILED_SAVE_XML + FAILED_SAVE_XML_END;
                    }

                }
            }
        }

        public static class FileSystem
        {
            public const string UNABLE_DELETE_FOLDER_CLOSING = "Application might not have been granted the right permissions or the directory, the file or a file in the directory is being accessed by another program.";
            public const string UNABLE_CREATE_FOLDER_OR_FILE_CLOSING = "Application might not have been granted the right permissions.";

            public const string CREATED_DIR = "Created directory at ";
            public const string DELETED_DIR = "Deleted directory ";
            public const string DELETED_FILE = "Deleted file ";
            public const string WRITE_FILE = "Successfully written to file at ";
            public const string COPY_FILE = "Successfully copied file from ";
            public const string MOVE_FILE = "Successfully moved file from ";
          

            public static string CopyFile(string sourceFile, string destination)
            {
                return COPY_FILE + sourceFile + TO_STR + destination + ".";
            }

            public const string PARTIAL_MOVE = "Request to move file from ";
            public const string PARTIAL_MOVE_1 = " partially failed due to the program not being able to delete the original file.";

            public static string MoveFile(string sourceFile, string destination)
            {
                return PARTIAL_MOVE + sourceFile + TO_STR + destination + ".";
            }

            public static string MoveFilePartial(string sourceFile, string destination)
            {
                return PARTIAL_MOVE + sourceFile + TO_STR + destination + PARTIAL_MOVE_1 + UNABLE_DELETE_FOLDER_CLOSING;
            }

            

            public static class Error
            {
               
                public const string OPPS_ABORTED_SHORT = ". Operation has been aborted.";

                public const string UNABLE_DEL_TEMP = "Unable to delete temporary directory at ";
                public const string UNABLE_CREATE_TEMP = "Unable to create temporary directory at ";
                public static string UnableToDeleteOrCreateTempDirAbort(string dirPath, bool delete)
                {
                    if (delete)
                        return UNABLE_DEL_TEMP + dirPath + OPPS_ABORTED_SHORT + " " + UNABLE_DELETE_FOLDER_CLOSING;
                    else
                        return UNABLE_CREATE_TEMP + dirPath + OPPS_ABORTED_SHORT + " " + UNABLE_DELETE_FOLDER_CLOSING; 
                }

                public const string CREATE_DIR_FAILED = "Request to create directory at ";
                public static string CreateDirFailed(string path)
                {
                    return CREATE_DIR_FAILED + path + FAILED + UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string DEL_DIR_FAILED = "Request to delete directory at ";
                public static string DeleteDirFailed(string path)
                {
                    return DEL_DIR_FAILED + path + FAILED + UNABLE_DELETE_FOLDER_CLOSING;
                }

                public const string DEL_FILE_FAILED = "Request to delete file ";
                public static string DeleteFileFailed(string path)
                {
                    return DEL_FILE_FAILED + path + FAILED + UNABLE_DELETE_FOLDER_CLOSING;
                }

                public const string WRITE_FILE_FAILED = "Request to write to file at ";
                public static string WriteToFileFailed(string path)
                {
                    return WRITE_FILE_FAILED + path + FAILED + UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string COPY_FILE_FAILED = "Request to copy file from ";
                public static string CopyFileFailed(string sourceFile, string destFile)
                {
                    return COPY_FILE_FAILED + sourceFile + TO_STR + destFile + FAILED + UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string MOVE_FILE_FAILED = "Request to move file from ";
                public static string MoveFileFailed(string sourceFile, string destFile)
                {
                    return MOVE_FILE_FAILED + sourceFile + TO_STR + destFile + FAILED + UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string WRITE_FILE_FAILED_NULL_TEXT = "The string to be written is set to null.";
                public static string WriteToFileFailedNullText(string path)
                {
                    return WRITE_FILE_FAILED + path + FAILED + WRITE_FILE_FAILED_NULL_TEXT;
                }

                public const string SOURCE_NOT_EXIST = " has failed as it doesn't exist.";

                public const string MOVE_FILE_FAILED_NULL_SOURCE = "An operation to move a file has failed due to a null source file path.";

                public const string MOVE_FILE_FAILED_PREFIX_A = "An operation to move file ";

                public const string MOVE_FILE_FAILED_NULL_DEST_1 = " has failed due to a null destination file path.";
                public static string MoveFileFailedNullDest(string sourceFile)
                {
                    return MOVE_FILE_FAILED_PREFIX_A + sourceFile + MOVE_FILE_FAILED_NULL_DEST_1;
                }

               
                public static string MoveFileFailedNotExistSource(string sourceFile)
                {
                    return MOVE_FILE_FAILED_PREFIX_A + sourceFile + SOURCE_NOT_EXIST;
                }

                public const string COPY_FILE_FAILED_NULL_SOURCE = "An operation to copy a file has failed due to a null source file path.";

                public const string COPY_FILE_FAILED_PREFIX_A = "An operation to copy file ";

                public const string COPY_FILE_FAILED_NULL_DEST_1 = " has failed due to a null destination file path.";
                public static string CopyFileFailedNullDest(string sourceFile)
                {
                    return COPY_FILE_FAILED_PREFIX_A + sourceFile + COPY_FILE_FAILED_NULL_DEST_1;
                }

                public static string CopyFileFailedNotExistSource(string sourceFile)
                {
                    return COPY_FILE_FAILED_PREFIX_A + sourceFile + SOURCE_NOT_EXIST;
                }

                public const string WRITE_FILE_FAILED_NULL_DEST = "An operation to write to a file has failed due to a null destination file path.";

                public const string DELETE_FILE_FAILED_NULL_PATH = "An operation to delete a file has failed due to a null provided path.";
                public const string DELETE_FOLDER_FAILED_NULL_PATH = "An operation to delete a directory has failed due to a null provided path.";
                public const string CREATE_FOLDER_FAILED_NULL_PATH = "An operation to create a directory has failed due to a null provided path.";
            }

            public static class Warning
            {
                public const string DELETE_FILE_FAILED_NOT_EXIST = "An operation to delete a file ";
                public const string DELETE_FILE_FAILED_NOT_EXIST_1 = " has failed as it doesn't exist.";
                public static string DeleteFileFailedNotExist(string path)
                {
                    return DELETE_FILE_FAILED_NOT_EXIST + path + DELETE_FILE_FAILED_NOT_EXIST_1;
                }

                public const string DELETE_FOLDER_FAILED_NOT_EXIST = "An operation to delete a folder ";
                public const string DELETE_FOLDER_FAILED_NOT_EXIST_1 = " has failed as it doesn't exist.";
                public static string DeleteFolderFailedNotExist(string path)
                {
                    return DELETE_FOLDER_FAILED_NOT_EXIST + path + DELETE_FOLDER_FAILED_NOT_EXIST_1;
                }

                public const string CREATE_FOLDER_FAILED_NOT_EXIST = "An operation to create a folder ";
                public const string CREATE_FOLDER_FAILED_NOT_EXIST_1 = " has failed as it already exists.";
                public static string CreateFolderFailedAlreadyExists(string path)
                {
                    return CREATE_FOLDER_FAILED_NOT_EXIST + path + CREATE_FOLDER_FAILED_NOT_EXIST_1;
                }
            }
        }
        public static class RequiredData
        {
            public static class Error
            {
                public const string RESOURCEDIR_MISS = "Required resource directory was not found. Expected location is ";
            }
            public static class Warning
            {
                public const string MAN_DIR_UNABLE_CREATE = "The application was unable to create the global packages manifest directory at ";
                public static string ManDirUnableCreate (string path)
                {
                    return MAN_DIR_UNABLE_CREATE + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string MAN_DIR_NOT_FOUND = "The packages installed directory under the RPG Maker MV installation directory was not found. Ignore this message if this application is launched for the first time. Expected directory path is ";
                public const string BASEGEN_XML_MISS = "The base generator xml template was not found. Application might not work properly. Expected location is ";
                public const string BASENEWPROJ_XML_MISS = "The base new project assets xml template was not found. Application might not work properly. Expected location is ";
                public const string GENFLOOR_CFG_MISS = "Configuration file for Generator parts floor count was not found. Application might not work as intended. Expected location is ";
                public const string GENSPECIAL_CFG_MISS = "Configuration file which contains the list of special Generator parts was not found. Application might not work as intended. Expected location is ";
            }
            public static class Information
            {
                public const string GENFLOOR_LOADED = "Configuration file for Generator parts floor count was loaded. File path is ";
                public const string GENSPECIAL_LOADED = "Configuration file which contains the list of special Generator parts was loaded. File path is ";
                public const string MAN_DIR_VERIFIED = "Location of global packages manifest directory is at ";
            }
        }
        public static class AtTheStart
        {
            public const string APP_STARTED = "Application has started in directory ";
            public const string MV_DIR = "RPG Maker MV installation directory is located at ";
            public const string MV_DIR_NOT_FOUND = "RPG Maker MV installation directory was not found and/or was not specified!";
            public const string MV_DIR_NOT_VALID = "Previously set RPG Maker MV installation directory is not valid anymore. Path is ";
        }
        public static class PackageManagement
        {
            public const string FOR_PACKAGE = " for package \"";
            public const string GLOBAL_DIR_ERROR = "Application was unable to retrieve global packages manifest at ";
            public const string INIT_GLOBAL_DIR = "Successfully read global packages manifest(s) located at ";
            
            public static class OpenProject
            {
                public static class Information
                {
                    public const string OPEN_OR_CLOSE_PROJ_START = "Project with root directory path ";
                    public const string OPEN_OR_CLOSE_PROJ_START_OPT = "A project";

                    public const string CLOSED_PROJ_1 = " has been closed.";
                    public static string ClosedProj(string path)
                    {
                        if (string.IsNullOrWhiteSpace(path))
                            return OPEN_OR_CLOSE_PROJ_START_OPT + CLOSED_PROJ_1;
                        return OPEN_OR_CLOSE_PROJ_START + path + CLOSED_PROJ_1;
                    }

                    public const string OPEN_PROJ_1 = " has been opened.";
                    public static string OpenedProj(string path)
                    {
                        if (string.IsNullOrWhiteSpace(path))
                            return OPEN_OR_CLOSE_PROJ_START_OPT + OPEN_PROJ_1;
                        return OPEN_OR_CLOSE_PROJ_START + path + OPEN_PROJ_1;
                    }

                }
            }

            public static class InstalledPackage
            {
                public static class Information
                {
                    public const string SUCCESS_READING_XML = "Successfully read installed package \"";
                    public const string SUCCESS_READING_XML_23 = "\" located at ";
                    public static string SuccessReading(string packageName, string XMLPath)
                    {
                        return SUCCESS_READING_XML + packageName + SUCCESS_READING_XML_23 + XMLPath + ".";
                    }

                    public const string START_RETRIEVE_INFO = "Retrieving installed package information located at ";
                    
                }
                public static class Warning
                {
                    public const string MISS_INSTALL_ARCH = "The install files archive for package \"";
                    public const string MISS_INSTALL_ARCH_23 = "\" is missing. Re-installation for this package will not be possible. Expected location of archive is ";

                    public static string MissInstall(string packageName, string archivePath)
                    {
                        return MISS_INSTALL_ARCH + packageName + MISS_INSTALL_ARCH_23 + archivePath + ".";
                    }

                    public const string ERR_WARN_FOUND = "Warning(s) and/or Error(s) have occured while trying to load installed package information located at directory ";

                    public const string WARN_TOSTRING = "A problem occured when trying to retrieve the value of Name and/or Namespace field. Value or Package Field might be null.";

                }
                public static class Error
                {
                    public const string ERR_TRY_LOAD = "An error occured when trying to load installed package information located at directory ";
                    public const string ERR_TRY_LOAD_1 = ". The installed package metadata file might be corrupted or the application might have not been granted the right permission.";
                    public static string ErrorTryLoad(string dirPath)
                    {
                        return ERR_TRY_LOAD + dirPath + ERR_TRY_LOAD_1;
                    }
                }
            }

            public static class DefaultPackages
            {
                public static class Error
                {
                    public const string GEN_TEMPLATE_NOT_FOUND = "The Generator parts package info template file could not be found. Unable to backup Generator parts and create manifest file. Expected location of template file is ";
                    public const string NEWDATA_TEMPLATE_NOT_FOUND = "The New Project assets package info template file could not be found. Unable to backup assets and create manifest file. Expected location of template file is ";

                    public const string ERR_OCCUR_GEN = "An error occured when trying to create an installed package metadata file and backup archive for the currently installed Generator Parts.";
                    public const string ERR_OCCUR_NEW_PROJ = "An error occured when trying to create an installed package metadata file and backup archive for the currently installed New Project assets.";

                }
                public static class Warning
                {
                    public const string GEN_PACK_NOT_FOUND = "The manifest file and the backup archive for the currently installed Generator parts has not been created yet.";
                    public const string NEWDATA_PACK_NOT_FOUND = "The manifest file and the backup archive for the currently installed New Project assets has not been created yet.";
                }
                public static class Info
                {
                    public const string GEN_PACK_FOUND = "Manifest file for the originally installed Generator parts have been detected. Path to the file is ";
                    public const string NEWDATA_PACK_FOUND = "Manifest file for the originally installed New Project assets have been detected. Path to the file is ";
                }
            }

            public static class InitPackageInstaller
            {
                public static class Error
                {

                    public const string INST_FILE_MISSING = "Installer file at ";
                    public const string INST_FILE_MISSING_1 = " is missing.";
                    public static string MissInstallFile(string path)
                    {
                        return INST_FILE_MISSING + path + INST_FILE_MISSING_1;
                    }

                    public const string EXTRACT_ZIP_FAILED = "A problem occured when trying to extract package archive files from ";
                    public static string ExtractZipFailed(string source, string dest)
                    {
                        return EXTRACT_ZIP_FAILED + source + TO_STR + dest + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string COPY_PACK_DIR_FAILED = "A problem occured when trying to copy package files from ";
                    public static string CopyPackageFilesFailed(string source, string dest)
                    {
                        return COPY_PACK_DIR_FAILED + source + TO_STR + dest + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }
                }

                public static class Info
                {
                    public const string INIT_START = "Copying/Extracting package files to temporary directory.";
                    public const string INIT_DONE = "Successfully copied/extracted package files to temporary directory.";
                }
            }

            public static class MakeInstalledPackages
            {
                public static class Error
                {
                    public const string INVALID_XML = "Unable to create Installed Package manifest for ";
                    public const string INVALID_XML_2 = " with the installed files located at ";
                    public const string INVALID_XML_3 = ". The provided XML metadata file is invalid.";
                    public static string InvalidXML(string pathToXml, string rootDir)
                    {
                        if (rootDir == null)
                            rootDir = Path.GetDirectoryName(pathToXml);
                        return INVALID_XML + pathToXml + INVALID_XML_2 + rootDir + INVALID_XML_3;
                    }

                    public const string UNABLE_DELETE_EXIST_FOLDER = "A required directory at ";
                    public const string UNABLE_DELETE_EXIST_FOLDER_1 = " already exists and the program tried to delete it but failed. ";
                    public static string UnableToDeleteExistingFolder(string folderPath)
                    {
                        return UNABLE_DELETE_EXIST_FOLDER + folderPath + UNABLE_DELETE_EXIST_FOLDER_1 + FileSystem.UNABLE_DELETE_FOLDER_CLOSING;
                    }

                   
                    public const string UNABLE_MAKE_MAIN_DIR = "Unable to create root directory for installed package metadata and backup at ";
                    public static string UnableCreateRootDir(string dirPath)
                    {
                        return UNABLE_MAKE_MAIN_DIR + dirPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_MAKE_INST_XML = "An error occured when trying to create a backup installer XML file at ";
                    public static string UnableCreateInstXML(string filePath)
                    {
                        return UNABLE_DELETE_EXIST_FOLDER + filePath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string USER_ASKED_CONTINE_FAILED_BACKUP = "The user was asked if they wished to continue even though a backup of package \"";
                    public const string USER_ASKED_CONTINE_FAILED_BACKUP_1 = "\" has not been created. User selected No. Creation of installed manifest XML for the package has been aborted.";
                    public static string UserRequiredOnFailedBackUpNo(string packageName)
                    {
                        return USER_ASKED_CONTINE_FAILED_BACKUP + packageName + USER_ASKED_CONTINE_FAILED_BACKUP_1;
                    }

                    public const string UNABLE_MAKE_BACKUP = "An error occured when creating a backup archive of package \"";
                    public const string UNABLE_MAKE_BACKUP_1 = "\" with the files located at ";
                    public const string UNABLE_MAKE_BACKUP_2 = " and archive saved to ";
                    public static string UnableMakeBackup(string packageName, string sourceDir, string destArch)
                    {
                        return UNABLE_MAKE_BACKUP + packageName + UNABLE_MAKE_BACKUP_1 + sourceDir + UNABLE_MAKE_BACKUP_2 + destArch + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_MAKE_MAIN_XML = "An error occured when creatig the installed files metadata XML at ";
                    public static string UnableMakeMainXML(string filePath)
                    {
                        return UNABLE_MAKE_MAIN_XML + filePath  + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_COPY_LIC_FILE = "An error occured when copying the license file from ";
                    public static string UnableCopyLicenseFile(string source, string dest)
                    {
                        return UNABLE_COPY_LIC_FILE + source + TO_STR + dest + ".";
                    }



                }
                public static class Info
                {
                    public const string CREATE_INSTALLED_PACK = "Creating Installed Package manifest for ";
                    public const string CREATE_INSTALLED_PACK_2 = " with the installed files located at ";
                    public static string CreateInstalledPack(string pathToXml, string rootDir)
                    {
                        if (rootDir == null)
                            rootDir = Path.GetDirectoryName(pathToXml);
                        return CREATE_INSTALLED_PACK + pathToXml + CREATE_INSTALLED_PACK_2 + rootDir + ".";
                    }

                    public const string SUCCESS_CREATE = "Successfully created Installed Package manifest for package ";
                }
                public static class Warning
                {
                    public const string INSTALLED_PACK_EXISTS_EXIT_REP = "A version of package \"";
                    public const string INSTALLED_PACK_EXISTS_EXIT = "\" is already installed. Application will not replace the installed package manifest with a newer one.";
                    public static string InstalledPackExistsExit(string packageName)
                    {
                        return INSTALLED_PACK_EXISTS_EXIT_REP + packageName + INSTALLED_PACK_EXISTS_EXIT;
                    }

                    public const string INSTALLED_PACK_EXISTS_REPLACE = "\" is already installed. Application will replace the installed package manifest with a newer one.";
                    public static string InstalledPackExistsReplace(string packageName)
                    {
                        return INSTALLED_PACK_EXISTS_EXIT_REP + packageName + INSTALLED_PACK_EXISTS_REPLACE;
                    }

                    public const string UNABLE_COPY_NEWPROJ = "The application was unable to make a copy of the installed package metadata file and backup archive at the NewData folder for package \"";
                    public const string UNABLE_COPY_NEWPROJ_1 = "\" with the source directory at ";
                    public const string UNABLE_COPY_NEWPROJ_2 = ". You won't be able to remove this package on projects you will create in the future that includes this package.";
                    public static string UnableCopyNewProj(string packageName, string sourceDir)
                    {
                        return UNABLE_COPY_NEWPROJ + packageName + UNABLE_COPY_NEWPROJ_1 + sourceDir + UNABLE_COPY_NEWPROJ_2;
                    }

                }
            }

            public static class NewProject
            {
                public static class Error
                {
                    public const string ERR_MAKE_DIR = "The root directory that will be used to store installed package information which will be copied along when creating a new RMMV project was created unsuccessfully at ";
                    public static string ErrorMakeDir(string dirPath)
                    {
                        return ERR_MAKE_DIR + dirPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string XML_FILE_NOT_FOUND = "An error occured when making a copy of an install script file to NewData directory. File was not found. Expected location of file is ";

                    public const string XML_FILE_SAVE_FAILED = "An error occured when saving a copy of the install script to ";
                    public static string XMLFileSaveFailed(string path)
                    {
                        return XML_FILE_SAVE_FAILED + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }
                
                }

                public static class Warning
                {
                    public const string DELETE_SUB_DIR = "An error occured when copying installation files of newly installed package. The program was unable to delete empty sub directories at ";
                    public const string LIC_COPY_FAILED_GEN = "An error occured when copying installation files of newly installed package. Unable to make a copy of license file of package.";

                    public const string LIC_COPY_FAILED = "An error occured when copying installation files of newly installed package. Unable to make a copy of license file of package from ";
                    public static string LicCopyFailed(string sourceFile, string destFile)
                    {
                        return LIC_COPY_FAILED + sourceFile + TO_STR + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }
                }

                public static class Info
                {
                    public const string COPY_INSTALL_INFO_INIT = "Copying installed package backup archive and manifest from ";
                    public const string COPY_INSTALL_INFO_INIT_DONE_1 = " to NewData directory.";
                    public static string CopyInstallInfoInit(string packagePath)
                    {
                        return COPY_INSTALL_INFO_INIT + packagePath + COPY_INSTALL_INFO_INIT_DONE_1;
                    }

                    public const string COPY_INSTALL_INFO_DONE = "Copying installed package backup archive and manifest from ";
                    public static string CopyInstallInfoDone(string packagePath)
                    {
                        return COPY_INSTALL_INFO_DONE + packagePath + COPY_INSTALL_INFO_INIT_DONE_1;
                    }

                    public const string DEL_INSTALL_INFO_INIT = "Deleting installed package backup archive and manifest located at ";
                    public const string DEL_INSTALL_INFO_INIT_DONE = " under the NewData directory.";
                    public static string DelInstallInfoInit(string path)
                    {
                        return DEL_INSTALL_INFO_INIT + path + DEL_INSTALL_INFO_INIT_DONE;
                    }

                    public const string DEL_INSTALL_INFO_DONE = "Successfully deleted installed package backup archive and manifest located at ";
                    public static string DelInstallInfoDone(string path)
                    {
                        return DEL_INSTALL_INFO_DONE + path + DEL_INSTALL_INFO_INIT_DONE;
                    }
                }
            }

            public static class Installer
            {
                public static class Information
                {
                    public const string PACKAGE_INSTALL_START_G = "Global package installation has started. Package to be installed is located at ";
                    public const string PACKAGE_INSTALL_DONE_G = "Successfully installed global package.";

                    public const string PACKAGE_INSTALL_START_L = "Project package installation has started. Package to be installed is located at ";
                    public const string PACKAGE_INSTALL_DONE_L = "Successfully installed project package.";

                    public const string CREATE_INSTALLED_FILE_START = "Creating uninstaller file manifest and backup archive of newly installed package.";
                    public const string CREATE_INSTALLED_FILE_DONE = "Operation to create uninstaller file manifest and backup archive of newly installed package was completed.";
                }
                public static class Error
                {
                    public const string UNABLE_OPEN_LOCAL_PACKAGE = "An error occured when installing a package. Unable to load project located at ";
                    public const string UNABLE_OPEN_LOCAL_PACKAGE_NULL = "An error occured when installing a package. The location of the project to load is set to null.";

                    public const string PACKAGE_PATH_NULL = "An error occured when installing package. Provided path to package is set to null.";
                    public const string NO_OPEN_PROJECT = "An error occured when installing package. The project where the package will be installed under has not been provided.";

                    public const string PACKAGE_ALREADY_EXISTS = "An error occured when installing package. Package with the same namespace already exists.";

                    public const string INIT_ERROR = "An error occured when installing package. Unable to initialize installer of package ";

                    public const string XML_READ_ERROR = "An error occured when installing package. Problem has been encountered when parsing installer script ";

                    public const string UNABLE_MAKE_GEN_DIR_TOP = "An error occured when installing package. Unable to install generator parts due to the program not being able to create a required directory.";
                    public const string GEN_INVALID_TYPE = "A problem occured when installing generator parts. One of the generator file to be installed does not specify its type.";
                    public const string GEN_INVALID_GENDER = "A problem occured when installing generator parts. The gender of one of the generator parts to be installed is not specified.";
                    public const string INVALID_GEN_FILE = "An error occured when installing generator parts. One of the generator files to be installed has an invalid field(s).";
                    public const string NO_FILE_PATH_GEN_FILE = "An error occured when installing generator parts. One of the generator files to be installed could not be found.";
                    public const string GEN_FILE_FAILED_COPY = "An error occured when installing generator parts. Unable to copy generator file ";
                    public static string GeneratorFileFailedCopy(string sourceDir, string destination)
                    {
                        return GEN_FILE_FAILED_COPY + sourceDir + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING; ;
                    }

                    public const string UNABLE_INSTALL_GEN = "An error occured when installing generator parts. Installation has been aborted.";

                    public const string UNABLE_INSTALL_AUDIO_NO_DIR_NAME = "An error occured when installing audio files. One of the audio package to be installed does not specify its type.";
                    public const string UNABLE_INSTALL_AUDIO_FILE = "An error occured when installing audio files. One of the audio file to be installed could not be located.";

                    public const string UNABLE_INSTALL_AUDIO_FILE_TYPE_INVALID = "An error occured when installing audio files. One of the audio file to be installed doesn't specify its file extension.";
                    public const string UNABLE_COPY_AUDIO_FILE = "An error occured when installing audio files. Unable to copy audio file ";
                    public static string UnableCopyAudioFile(string sourceDir, string destination)
                    {
                        return UNABLE_COPY_AUDIO_FILE + sourceDir + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING; ;
                    }


                    public const string UNABLE_INSTALL_SF_NO_TYPE = "An error occured when installing a package. One of the collection to be installed doesn't specify its type.";
                    public const string UNABLE_INSTALL_SF_NO_PATH = "An error occured when installing a package. One of the asset to be installed could not be found.";

                    public const string UNABLE_INSTALL_SF_COPY_FAILED = "An error occured when installing a package. Unable to copy asset ";
                    public static string UnableInstallSFCopyFailed(string source, string destination)
                    {
                        return UNABLE_INSTALL_SF_COPY_FAILED + source + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING; ;
                    }

                    public const string UNABLE_INSTALL_CHAR_FILE_TYPE = "An error occured when installing a character collection. One of the character files to be installed does not specify its type.";
                    public const string UNABLE_INSTALL_CHAR_FILE_PATH = "An error occured when installing a character collection. One of the character files to be installed could not be found.";

                    public const string UNABLE_INSTALL_CHAR_FILE_COPY = "An error occured when installing a character collection. Unable to copy character file ";
                    public static string UnableInstallCharFileCopy(string source, string destination)
                    {
                        return UNABLE_INSTALL_CHAR_FILE_COPY + source + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING; ;
                    }

                    public const string INVALID_TILESET_FILE_TYPE = "An error occured when installing a tileset collection. One of the atlas to be installed doesn't specify its file type.";
                    public const string INVALID_TILESET_PATH_NULL = "An error occured when installing a tileset collection. One of the atlas to be installed could not be found.";

                    public const string TILESET_UNABLE_COPY = "An error occured when installing a tileset collection. Unable to copy tileset ";
                    public static string TilesetUnableToCopy(string source, string destination)
                    {
                        return TILESET_UNABLE_COPY + source + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING; ;
                    }

                    public const string INVALID_MOVIE_FILE_TYPE = "An error occured when installing a video collection. One of the videos to be installed doesn't specify its file type.";
                    public const string INVALID_MOVIE_PATH_NULL = "An error occured when installing a video collection. One of the videos to be installed could not be found.";

                    public const string MOVIE_UNABLE_COPY = "An error occured when installing a video collection. Unable to copy video ";
                    public static string MovieUnableCopy(string source, string destination)
                    {
                        return MOVIE_UNABLE_COPY + source + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING; ;
                    }

                    public const string UNABLE_INSTALL_COLLECTION = "A problem was encountered when installing a package collection. Operation has been aborted.";
                    public const string INSTALL_PACK_FAILED = "An error occured when installing package. Installation has been aborted.";

                    public const string UNABLE_SAVE_INSTALLED_XML = "An error occured when installing package. Unable to save installed package metadata to ";
                    public static string UnableSaveInstalledXml(string path)
                    {
                        return UNABLE_SAVE_INSTALLED_XML + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_CREATE_INSTALLED_FILES = "An error occured when installing package. Installed package metadata required by the uninstaller could not be created.";
                    public const string MISS_FILE_PREFIX = "An error occured when installing package. ";
                    public const string FAIL_PACKAGE_PARSE = "An error occured when installing selected package. The selected package has an invalid or corrupted install script. Path to package is ";
                    public const string EMPTY_PACKAGE = "An error occured when installing selected package. The selected package doesn't contain any assets to be installed.";

                    public const string SAVE_COPY_XML_FAILED = "An error occured when installing selected package. The program was unable to make a copy of the install script of the package at ";
                    public static string SaveCopyXMLFailed(string path)
                    {
                        return SAVE_COPY_XML_FAILED + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                }
                public static class Warning
                {
                    public const string UNABLE_MAKE_ZIP = "An error occured when installing package. Unable to create installed package backup archive at ";
                    public const string UNABLE_MAKE_ZIP_1 = ". Please be advised that you won't be able to reinstall the package from the backup if you need to in the future.";
                    public static string UnableMakeZip(string path)
                    {
                        return UNABLE_MAKE_ZIP + path + UNABLE_MAKE_ZIP_1;
                    }

                    public const string LIC_SOURCE_NULL = "A problem has been encountered when installing package. The package license is not specified. We strongly recommend you not to use this package.";
                    public const string INVALID_LIC = "A problem has been encountered when installing package. The package license file could not be found or is invalid. We strongly recommend you not to use this package.";
                    public const string INVALID_FILE_NAME_LIC = "A problem has been encountered when installing package. The path to the license file is not valid. We strongly recommend you not to use this package.";

                    public const string UNABLE_COPY_LIC = "A problem has been encountered when installing package. Unable to copy license file ";
                    public static string UnableCopyLicFile(string source, string destination)
                    {
                        return UNABLE_COPY_LIC + source + TO_STR + destination + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_LOAD_PACK = "A problem has been encountered when installing package. Unable to load newly installed package to current instance of the package manager. Please try restarting the application to see if that fixes the problem.";
                    public const string UNABLE_COPY_NEWDATA_DIR = "A problem has been encountered when installing package. Unable to copy installed package metadata and backup to new project data directory.";
                    public const string DELETE_SUB_DIR = "An error occured when creating an installed file for package. The program was unable to delete empty sub directories at ";
                }
            }
            public static class Updater
            {
                public static class Information
                {
                    public const string LOCAL_PROJECT_STARTER = "A package for project located at ";
                    public const string UPDATE_PACKAGE_START_G = "Global package update has started with install script located at ";
                    public const string UPDATE_PACKAGE_DONE_G = "Successfully updated global package.";
                    
                    public const string UPDATE_PACKAGE_START_L = " will now be updated. The install script path is ";
                    public static string UpdatePackageStart(string projPath, string installScriptPath)
                    {
                        return LOCAL_PROJECT_STARTER + projPath + UPDATE_PACKAGE_START_L + installScriptPath + ".";
                    }
                    public const string UPDATE_PACKAGE_DONE_L = " was successfully updated.";
                    public static string UpdatePackageDone(string projPath)
                    {
                        return LOCAL_PROJECT_STARTER + projPath + UPDATE_PACKAGE_DONE_L;
                    }
                }

                public static class Warning
                {
                    public const string PACKAGE_TO_BE_UPDATED_NOT_FOUND = "The package to be updated was not found. Operation aborted.";
                }

                public static class Error
                {
                    public const string XML_INVALID = "An error occured when performing package update. Installer script of the package to be installed is invalid. Install script is located at ";
                    public const string NO_GLOBAL_PACKAGES = "An error occured when performing package update. No global packages were detected by the application.";
                    public const string FAILED_UNINSTALL = "An error occured when performing package update. Uninstallation of the package to be replaced has failed.";
                    public const string FAILED_INSTALL = "An error occured when performing package update. Installation of the package selected has failed.";
                    public const string UNABLE_EXTRACT_LOCAL = "An error occured when performing project package update. Unable to extract asset files of package ";
                    public const string UNABLE_EXTRACT_GLOBAL = "An error occured when performing global package update. Unable to extract asset files of package ";
                    public const string NO_OPEN_PROJ = "An error occured when performing project package update. There is currently no open project.";
                    public const string INVALID_OPEN_PROJ_DIR = "An error occured when performing project package update. Project's directory path is not valid or not set.";
                    public const string UNABLE_PARSE_XML_LOCAL = "An error occured when performing project package update. A problem has been encountered when parsing install script file. Path to file is ";
                    public const string OPEN_PROJ_NOT_ABLE_FIND_PACK = "An error occured when performing project package update. Unable to find the package to be updated.";
                }
            }
            public static class ProjectPackMan
            {
                public static class Warning
                {
                    public const string INVALID_PACKAGE = "A error occured when loading packages from the selected project. Directory ";
                    public const string INVALID_PACKAGE_1 = " doesn't contain or has an invalid package metadata file.";
                    public static string InvalidPackage(string dirPath)
                    {
                        return INVALID_PACKAGE + dirPath + INVALID_PACKAGE_1;
                    }

                }
            }

            public static class Reinstaller
            {
                public static class Information
                {
                    public const string REINSTALL_PACKAGE_START_G = "Global package reinstallation has started.";
                    public const string REINSTALL_PACKAGE_DONE_G = "Successfully reinstalled global package.";
                    public const string REINSTALL_PACKAGE_START_L = "Project package reinstallation has started.";
                    public const string REINSTALL_PACKAGE_DONE_L = "Successfully reinstalled project package.";
                }
                public static class Error
                {
                    public const string ARCH_NOT_FOUND = "An error occured when reinstalling package. Could not find archived install files.";

                    public const string UNABLE_EXTRACT_ARCH = "An error occured when reinstalling package. Unable to extract archived install files to temporary directory ";
                    public const string UNABLE_UNINSTALL = "An error occured when reinstalling package. Unable to uninstall currently installed package.";
                    public const string UNABLE_INSTALL = "An error has been encountered when reinstalling package.";

                    public const string PACK_MISS = "An error has been encountered when reinstalling package. Package with namespace ";
                    public const string PACK_MISS_1 = " could not be found";
                    public static string PackageMissng (string _namespace)
                    {
                        return PACK_MISS + _namespace + PACK_MISS_1;
                    }

                    public const string OPEN_PROJ_NULL_OR_MISS = "An error occured when reinstalling package. Project root directory could not be found.";
                    public const string OPEN_PROJ_NULL_OR_MISS_1 = " Expected directory location is ";
                    public static string OpenProjNullOrMiss(string path)
                    {
                        if (string.IsNullOrWhiteSpace(path))
                            return OPEN_PROJ_NULL_OR_MISS;
                        return OPEN_PROJ_NULL_OR_MISS + OPEN_PROJ_NULL_OR_MISS_1 + path;
                    }

                    public const string LOAD_FAILED_PROJ = "An error occured when reinstalling package. Unable to load project located at ";
  
                }
            }

            public static class Uninstaller
            {
                public static class Information
                {
                    public const string UNINSTALL_PACKAGE_START_G = "Global package uninstallation has started.";
                    public const string UNINSTALL_PACKAGE_DONE_G = "Successfully uninstalled global package.";
                    public const string UNINSTALL_PACKAGE_START_L = "Project package uninstallation has started.";
                    public const string UNINSTALL_PACKAGE_DONE_L = "Successfully uninstalled project package.";
                }
                public static class Error
                {
                    public const string XML_FILE_NOT_FOUND = "An error occured when uninstalling a package. The install script file has not been found. Expected file location is ";
                    public const string XML_FILE_INVALID = "An error occured when uninstalling a package. The install script is invalid. Location of file is ";
                    public const string INVALID_PROCDELNONREADXMLPACKAGE_ARG = "An error occured when uninstalling a package. Argument argType value is invalid.";

                    public const string INVALID_XML_PATH = "An error occured when uninstalling a package. The path provided is invalid. Path is ";
                    public const string PACKAGE_UID_NOT_FOUND = "An error occured when uninstalling a package. The package with the provided unique id is not found. Unique ID is ";

                    public const string GEN_PART_RENUMBER_FAILED = "An error occured when uninstalling a package. Renumbering of generator parts failed. Some generator parts might have been corrupted due to the process being halted abruptly.";
                    public const string ARG_INVALID = "An error occured when uninstalling a package. The argument provided for arg is invalid.";
                    public const string UNABLE_OPEN_LOCAL_PACKAGE = "An error occured when uninstalling a package. Unable to load project located at ";
                    public const string UNABLE_OPEN_LOCAL_PACKAGE_NULL = "An error occured when uninstalling a package. The location of the project to load is set to null.";
                }
                public static class Warning
                {
                    public const string FILE_DELETE_FAILED = "An error occuer when trying to delete asset file located at ";
                    public const string FILE_DELETE_FAILED_1 = ". Please try deleting it manually to prevent any issues.";
                    public static string FileDeleteFailed(string path)
                    {
                        return FILE_DELETE_FAILED + path + FILE_DELETE_FAILED_1;
                    }

                    public const string DIRECTORY_UNABLE_DELETE = "An error occured when trying to uninstall a package. Unable to delete package install info and backup archive root directory at ";
                    public const string DIRECTORY_UNABLE_DELETE_1 = ". Please try deleting it manually to prevent any issues.";
                    public static string DirectoryUnableDelete(string path)
                    {
                        return DIRECTORY_UNABLE_DELETE + path + DIRECTORY_UNABLE_DELETE_1;
                    }

                }
            }
        }

        public static class BackupManagement
        {
            public static class RestoreProjectBackup
            {
                public static class Error
                {
                    public const string INVALID_SOURCE = "An error occured when restoring a backup of a project. The path to the archived backup is invalid.";
                    public const string INVALID_DEST = "An error occured when restoring a backup of a project. The path to the project is invalid.";
                }

            }

            public static class CreateProjectBackup
            {
                public static class Error
                {
                    public const string INVALID_SOURCE = "An error occured when creating a backup for a project. Path to project directory is invalid.";
                    public const string INVALID_DEST = "An error occured when creating a backup for a project. The path provided where the backup will be created is invalid.";

                    public const string MISSING_PROJ_DIR = "An error occured when creating a backup for a project. Path to project directory ";
                    public const string MISSING_PROJ_DIR_1 = " was not found.";
                    public static string MissingProjectDir(string pathToDir)
                    {
                        return MISSING_PROJ_DIR + pathToDir + MISSING_PROJ_DIR_1;
                    }

                }
            }
            public static class RestoreGlobalBackup
            {
                public static class Error
                {
                    public const string INVALID_SOURCE = "An error occured when restoring a RMMV global assets backup. Path to archived backup is invalid.";
                    public const string INVALID_DEST = "An error occured when restoring a RMMV global assets backup. Destination directory is invalid.";

                    public const string UNABLE_EXTRACT_ARCH = "An error occured when restoring a RMMV global assets backup. Unable to extract archive ";
                    public static string UnableExtractArchive(string source, string dest)
                    {
                        return UNABLE_EXTRACT_ARCH + source + TO_STR + dest + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }
                }
            }

            public static class CreateGlobalBackup
            {
                
                public static class Error
                {
                    public const string COPY_GEN_FILE_FAILED = "An error was encountered when making a copy of the generator file assets from ";
                    public static string CopyGenFileFailed(string sourcePath, string destPath)
                    {
                        return COPY_GEN_FILE_FAILED + sourcePath + TO_STR + destPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string COPY_NEWDATA_FILE_FAILED = "An error was encountered when making a copy of the new project data assets from ";
                    public static string CopyNewDataFailed(string sourcePath, string destPath)
                    {
                        return COPY_NEWDATA_FILE_FAILED + sourcePath + TO_STR + destPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string COPY_PACKMAN_DATA_FAILED = "An error was encountered when making a copy of the package manager installed packages metadata files from ";
                    public static string CopyPackManDataFailed(string sourcePath, string destPath)
                    {
                        return COPY_PACKMAN_DATA_FAILED + sourcePath + TO_STR + destPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_MAKE_ZIP = "Unable to make archived backup from ";
                    public static string UnableMakeZip(string sourceDir, string destFile)
                    {
                        return UNABLE_MAKE_ZIP + sourceDir + TO_STR + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string INVALID_SAVETO = "The provided path where the archive will be saved is invalid.";
                    public const string INVALID_INSTALLPATH = "The provided path for the RMMV installation directory is invalid.";

                   


                }
                public static class Warning
                {
                    public const string GEN_FOLDER_NOT_DETECTED = "The generator assets directory was not detected. Expected location is at ";
                    public const string NEWDATA_FOLDER_NOT_DETECTED = "The new project data assets directory was not detected. Expected location is at ";
                    public const string PACKMAN_DATA_FOLDER_NOT_DETECTED = "The package manager installed packages metadata files directory was not detected. Expected location is at ";
                }

                
            }

            public static class Information
            {
                public const string CREATE_BACKUP_BEGIN = "Creating backup of asset files located under ";
                public const string CREATE_BACKUP_BEGIN_1 = ". The backup will be saved at ";
                public static string CreateBackupBegin(string sourceDir, string destArch)
                {
                    return CREATE_BACKUP_BEGIN + sourceDir + CREATE_BACKUP_BEGIN_1 + destArch + ".";
                }

                public const string CREATE_BACKUP_DONE = "Successfully created backup of asset files located under ";
                public const string CREATE_BACKUP_DONE_1 = ". The backup was saved at ";
                public static string CreateBackupDone(string sourceDir, string destArch)
                {
                    return CREATE_BACKUP_DONE + sourceDir + CREATE_BACKUP_DONE_1 + destArch + ".";
                }

                public const string RESTORE_BACKUP_BEGIN = "Restoring backup file ";
                public static string RestoreBackupBegin(string sourceFile, string destDir)
                {
                    return RESTORE_BACKUP_BEGIN + TO_STR + destDir + ".";
                }

                public const string RESTORE_BACKUP_DONE = "Successfully restored backup file ";
                public static string RestoreBackupDone(string sourceFile, string destDir)
                {
                    return RESTORE_BACKUP_DONE + TO_STR + destDir + ".";
                }
            }
        }

      

        public static class Helper
        {
            public const string DIR_COPY_ERR_GEN = "Function directory copy has encountered an error at ";
            public const string DELETE_EMPTY_SUB_DIR = "Deleting empty sub directories under ";

            public const string COPY_DIR = "Copying directory ";
            public static string CopyDir(string sourceDir, string destDir)
            {
                return COPY_DIR + sourceDir + TO_STR + destDir + ".";
            }
        }

        public static class ArchiveManagement
        {
            public static class Information
            {
                public const string CREATE_ZIP_START = "Creating archive file from ";
                public static string CreateZipStart (string source, string dest)
                {
                    return CREATE_ZIP_START + source + TO_STR + dest + ".";
                }

                public const string EXTRACT_ZIP_START = "Extracting archive file ";
                public static string ExtractZipStart(string source, string dest)
                {
                    return EXTRACT_ZIP_START + source + TO_STR + dest + ".";
                }

                public const string CREATE_ZIP_SUCCESS = "Successfully created archive file from ";
                public static string CreateZipSuccess(string source, string dest)
                {
                    return CREATE_ZIP_SUCCESS + source + TO_STR + dest + ".";
                }

                public const string EXTRACT_ZIP_SUCCESS = "Successfully extract archive file ";
                public static string ExtractZipSuccess(string source, string dest)
                {
                    return EXTRACT_ZIP_SUCCESS + source + TO_STR + dest + ".";
                }

            }

            public static class Error
            {
                public const string CREATE_ZIP_FAILED = "A problem occured when creating archive from ";
                public static string CreateZipFailed(string source, string dest)
                {
                    return CREATE_ZIP_FAILED + source + TO_STR + dest + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string EXTRACT_ZIP_FAILED = "A problem occured when extracting archive ";
               
                public static string ExtractZipFailed(string source, string dest)
                {
                    return EXTRACT_ZIP_FAILED + source + TO_STR + dest + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }
            }

        }



        public static class GUI
        {
            public static class ActionTaken
            {
                public const string SHOW_OPEN_FILE_DLG = "Show open file dialog.";
                public const string SHOW_SAVE_FILE_DLG = "Show save file dialog.";
                public const string INSTALL_PACKAGE_SELECTED = "Copy or extract package files to temporary directory and read package information.";
            }
            public static class Global
            {
                public static class Actions
                {
                    public const string INSTALL_PROJ_SPEC_PACK = " install a project specific package ";
                    public const string UNINSTALL_PROJ_SPEC_PACK = " uninstall a project specific package ";
                    public const string UPDATE_PROJ_SPEC_PACK = " update a project specific package ";
                    public const string REINSTALL_PROJ_SPEC_PACK = " reinstall a project specific package ";
                    public const string VIEW_ASSETS_PROJ_SPEC_PACK = " view assets of a project specific package ";
                }
                public static class Error
                {
                    public const string NO_OPEN_PROJ = "User wants to ";
                    public const string NO_OPEN_PROJ_1 = " but there is no open project. Operation aborted.";
                    public static string NoOpenProject(string action)
                    {
                        return NO_OPEN_PROJ + action + NO_OPEN_PROJ_1;
                    }
                }
            }

            public static class frmMain
            {
                public const string INIT_START = "Initializing required resources...";
                public const string INIT_DONE = "Initialization complete.";
                public const string CREATE_PACKAGE = "Create new package.";
                public const string MODIFY_PACKAGE = "Modify a package.";
                public const string MODIFY_PACKAGE_SEL = "The selected package to be modified is located at ";
             
            }
            public static class frmOpenFile
            {
                public const string UNABLE_LAUNCH_APP = "An error occured when launching application ";
                public const string UNABLE_LAUNCH_APP_1_OR_2 = " to view file ";
                public const string UNABLE_LAUNCH_APP_1 = " with argument(s) \"";
                public static string UnableLaunchApp(string pathToApp, string pathToFile, string appArgs = null)
                {
                    if (string.IsNullOrWhiteSpace(appArgs))
                        return UNABLE_LAUNCH_APP + pathToApp + UNABLE_LAUNCH_APP_1_OR_2 + pathToFile + ".";
                    else
                        return UNABLE_LAUNCH_APP + pathToApp + UNABLE_LAUNCH_APP_1 + appArgs + "\"" + UNABLE_LAUNCH_APP_1_OR_2 + pathToFile + ".";
                }

                public const string OPEN_FILE = "Opening file ";
                public const string OPEN_FILE_DEF = " using default application.";
                public static string OpenFileDefaultApp(string filePath)
                {
                    return OPEN_FILE + filePath + OPEN_FILE_DEF;
                }

                public const string OPEN_FILE_APP_SEL = " using application located at ";
                public const string OPEN_FILE_APP_SEL_W_PARAMS = " with parameters \"";
                public static string OpenFileSelApp(string filePath, string appPath, string parameters = null)
                {
                    if (string.IsNullOrWhiteSpace(parameters))
                        return OPEN_FILE + filePath + OPEN_FILE_APP_SEL + appPath + ".";
                    else
                        return OPEN_FILE + filePath + OPEN_FILE_APP_SEL + appPath + OPEN_FILE_APP_SEL_W_PARAMS + parameters + "\".";
                }
            }

            public static class BGWorker
            {
               public static class Backup
                {
                    public static class Information
                    {
                        public const string BACKUP_SELECTED_PATH = "Backup will be saved at ";
                        public const string BACKUP_RESTORE_SELECTED_PATH = "Backup file to be restored is located at ";

                        public const string MAKE_GLOBAL_BACKUP_BEGIN = "Create backup of global assets."; 
                        public const string MAKE_GLOBAL_BACKUP_CANCELLED = "Create global assets backup operation was cancelled."; 
                        public const string MAKE_GLOBAL_BACKUP_SUCCESS = "Global assets backup was successfully created at "; 

                        public const string RESTORE_GLOBAL_BACKUP_BEGIN = "Restore global assets backup."; 

                        public const string RESTORE_GLOBAL_BACKUP_CANCELLED = "Operation to restore global assets backup was cancelled."; 
                        public const string RESTORE_GLOBAL_BACKUP_SUCCESS = "Operation to restore global assets backup from "; 
                        public const string RESTORE_GLOBAL_BACKUP_SUCCESS_1 = " was successful."; 

                        public static string RestoreGlobalBackupSuccess(string path) 
                        {
                            return RESTORE_GLOBAL_BACKUP_SUCCESS + path + RESTORE_GLOBAL_BACKUP_SUCCESS_1;
                        }

                        public const string MAKE_LOCAL_BACKUP_BEGIN = "Create backup of project assets";
                        public const string MAKE_LOCAL_BACKUP_CANCELLED = "Creation of project assets backup was cancelled."; 
                        public const string MAKE_LOCAL_BACKUP_SUCCESS = "Project assets backup was successfully created at "; 

                        public const string RESTORE_PROJECT_BACKUP_BEGIN = "Restore project assets backup."; 
                        public const string RESTORE_PROJECT_BACKUP_CANCELLED = "Operation to restore project assets backup was cancelled."; 

                        public const string RESTORE_PROJECT_BACKUP_SUCCESS = "Operation to restore project assets backup from "; 
                        public const string RESTORE_PROJECT_BACKUP_SUCCESS_1 = " was successful."; 
                        public static string RestoreProjBackupSuccess(string path) 
                        {
                            return RESTORE_PROJECT_BACKUP_SUCCESS + path + RESTORE_PROJECT_BACKUP_SUCCESS_1;
                        }
                    }

                    public static class Error
                    {
                        public const string BACKUP_CREATE_FAILED_GLOBAL = "An error occured when trying to create a global assets backup at ";
                        public static string BackupCreateFailedGlobal(string path) 
                        {
                            return BACKUP_CREATE_FAILED_GLOBAL + path + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }

                        public const string BACKUP_RESTORE_FAILED_GLOBAL = "An error occured when trying to restore a global assets backup from "; 
                        public static string BackupRestoreFailedGlobal(string path)
                        {
                            return BACKUP_RESTORE_FAILED_GLOBAL + path + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }

                        public const string BACKUP_CREATE_FAILED_LOCAL = "An error occured when trying to create a project assets backup at "; 
                        public static string BackupCreateFailedLocal(string path) 
                        {
                            return BACKUP_CREATE_FAILED_LOCAL + path + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }

                        public const string BACKUP_RESTORE_FAILED_LOCAL = "An error occured when trying to restore a project assets backup from "; 
                        public static string BackupRestoreFailedLocal(string path)
                        {
                            return BACKUP_RESTORE_FAILED_LOCAL + path + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }
                    }
                }
                public static class Misc
                {
                    public static class Information
                    {
                        public const string OPEN_PROJ_BEGIN = "Open project.";
                        public const string OPEN_PROJ_CANCELLED = "Open project operation was cancelled.";

                        public const string OPEN_PROJ_SUCCESS = "Project ";
                        public const string OPEN_PROJ_SUCCESS_1 = " has successfully been opened.";
                        public static string OpenProjSuccess(string path)
                        {
                            return OPEN_PROJ_SUCCESS + path + OPEN_PROJ_SUCCESS_1;
                        }

                        public const string RELOAD_PROJ = "Reloading project ";
                        public const string CLOSE_PROJ = "Closing project ";
                    }

                    public static class Error
                    {
                        public const string OPEN_PROJ_FAILED = "An error occured when loading project ";
                        public static string OpenProjFailed(string path)
                        {
                            return OPEN_PROJ_FAILED + path + "." + OPPS_ABORTED + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }

                        public const string MODIFY_PACK_XML_READ_ERR = "An error occured when parsing XML script ";
                        public static string ModifyPackXMLReadError(string xmlPath)
                        {
                            return MODIFY_PACK_XML_READ_ERR + xmlPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }

                        public const string UNABLE_READ_ARCH_STATUS = "An error occured when extracting package archive. The archive file ";
                        public const string UNABLE_READ_ARCH_STATUS_1 = " might be in use by another application or the program has not been granted the right permission.";
                        public static string UnableReadArchStatus(string path)
                        {
                            return UNABLE_READ_ARCH_STATUS + path + UNABLE_READ_ARCH_STATUS_1;
                        }

                        public const string FAILED_CHECKSUM_ZIP = "An error occured when extracting package archive. The archive file ";
                        public const string FAILED_CHECKSUM_ZIP_1 = " has failed a checksum check.";
                        public static string FailedChecksumZip(string path)
                        {
                            return FAILED_CHECKSUM_ZIP + path + FAILED_CHECKSUM_ZIP_1;
                        }

                        public const string EXTRACT_ARCH_ERR = "An error occured when parsing package archive ";
                        public static string UnableExtractZip(string archivePath, string dirPath)
                        {
                            return EXTRACT_ARCH_ERR + archivePath + TO_STR + dirPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                        }

                        public const string ZIP_EXTRACT_INSTALL_XML_NULL = ". Unable to find the install script of the package.";
                        public static string ZipExtractInstallXmlNull(string path)
                        {
                            return EXTRACT_ARCH_ERR + path + ZIP_EXTRACT_INSTALL_XML_NULL;
                        }

                        public const string ZIP_EXTRACT_INSTALL_XML_ERR = ". The install script of the package is invalid or corrupted.";
                        public static string ZipExtractInstallXMLErr(string path)
                        {
                            return EXTRACT_ARCH_ERR + path + ZIP_EXTRACT_INSTALL_XML_ERR;
                        }

                        public const string ZIP_EXTRACT_UNABLE_RETRIEVE_ASSET = ". Unable to retrieve assets file stored in the archive.";
                        public static string ZipExtractUnableRetrieveAsset(string path)
                        {
                            return EXTRACT_ARCH_ERR + path + ZIP_EXTRACT_UNABLE_RETRIEVE_ASSET;
                        }
                    }
                    
                }
            }

          
            public static class GlobalOrLocalSharedControls
            {
             
               
                public static class Information
                {
                    public const string BACKUP_PROMPT = "Backup currently installed assets prompt.";
                    public const string BACKUP_PROMPT_NO = "User doesn't want to create a backup.";

                    public const string INSTALL_PACKAGE_BEGIN = "Select a package to install.";
                   
                    public const string INSTALL_PACKAGE_BEGIN_SPEC_PROJ = "Select a package to install for project located at ";
                    public const string INSTALL_PACKAGE_SELECTED = " was selected as the package to be installed.";
                    public const string INSTALL_PACKAGE_CANCELLED = "Install package operation was cancelled.";
                    public const string INSTALL_PACKAGE_DONE = "Successfully installed package ";
                    public const string INSTALL_PACKAGE_DONE_SPEC_PROJECT = " to project located at ";
                    public static string InstallPackageDoneSpecProj(string packageName, string projectPath)
                    {
                        return INSTALL_PACKAGE_DONE + packageName + INSTALL_PACKAGE_DONE_SPEC_PROJECT + projectPath + ".";
                    }

                   

           

                    public const string UNINSTALL_PACKAGE_GLOBAL = "Uninstall selected global package.";
                    public const string UNINSTALL_PACKAGE_LOCAL = "Uninstall selected project package.";
                    public const string UNINSTALL_PACKAGE_CANCELLED = "Uninstall package operation was cancelled.";

                    public const string UNINSTALL_PACKAGE_DONE = "Successfully uninstalled package with namespace ";
                    public const string UNINSTALL_PACKAGE_DONE_SPEC_PROJECT = " from project located at ";
                    public static string UninstallPackageDoneProjSpec(string packageNamespace, string projectPath)
                    {
                        return UNINSTALL_PACKAGE_DONE + packageNamespace + UNINSTALL_PACKAGE_DONE_SPEC_PROJECT + projectPath + ".";
                    }
                    public const string FOR_PROJ_LOCATED_AT = " for project located at ";

                    public const string UPDATE_PACKAGE_BEGIN = "Update package which has a namespace of ";
                    public static string UpdatePackageBeginSpecProj(string packageUID, string projectPath)
                    {
                        return UPDATE_PACKAGE_BEGIN + packageUID + FOR_PROJ_LOCATED_AT + projectPath + ".";
                    }

                    public const string UPDATE_PACKAGE_CANCELLED = "Update package operation was cancelled.";
                    public const string UPDATE_PACKAGE_DONE = "Successfully updated package ";
                   
                    public static string UpdatePackageDoneSpecProj(string packageName, string projectPath)
                    {
                        return UPDATE_PACKAGE_DONE + packageName + FOR_PROJ_LOCATED_AT + projectPath + ".";
                    }

                    public const string REINSTALL_PACKAGE_GLOBAL = "Reinstall selected global package.";
                    public const string REINSTALL_PACKAGE_LOCAL = "Reinstall selected project package.";
                    public const string REINSTALL_PACKAGE_CANCELLED = "Reinstall package operation was cancelled.";

                    public const string REINSTALL_PACKAGE_DONE = "Successfully reinstalled package that has a namespace ";
                    public static string ReinstallPackageDoneSpecProj(string packageName, string projectPath)
                    {
                        return REINSTALL_PACKAGE_DONE + packageName + FOR_PROJ_LOCATED_AT + projectPath + ".";
                    }

                    public const string VIEW_PACKAGE_GLOBAL = "View assets of selected global package.";
                    public const string VIEW_PACKAGE_LOCAL = "View assets of selected local package.";
                }
                public static class Warning
                {
                    public const string ERR_RETR_FIELD = "An error occured when trying to retrieve field ";
                    public static string ErrorRetrieveField(string fieldName, string packageName)
                    {
                        if (string.IsNullOrWhiteSpace(packageName))
                            return ERR_RETR_FIELD + fieldName + ".";
                        else
                            return ERR_RETR_FIELD + fieldName + PackageManagement.FOR_PACKAGE + packageName + "\".";
                    }

                    public const string ERR_RETR_ARCH_BACKUP = "An error occured when trying to check package \"";
                    public const string ERR_RETR_ARCH_BACKUP_1 = "\" archived backup.";
                    public const string ERR_RETR_ARCH_BACKUP_ALT = "An error occured when trying to check a package archived backup.";
                    public static string ErrorRetrieveArch(string packageName)
                    {
                        if (string.IsNullOrEmpty(packageName))
                            return ERR_RETR_ARCH_BACKUP_ALT;
                        else
                            return ERR_RETR_ARCH_BACKUP + packageName + ERR_RETR_ARCH_BACKUP_1;
                    }

                    public const string SIMILAR_PACKAGE_FOUND = "Package with similar namespace already installed";
                    public const string SIMILAR_PACKAGE_FOUND_OPT_1 = ". Installed version is ";
                    public const string SIMILAR_PACKAGE_FOUND_OPT_1_1 = " while version of package that the user wishes to install is ";
                    public const string SIMILAR_PACKAGE_FOUND_2 = ". User input required.";
                    public static string SimilarPackageFound(string installedVer, string packToInstallVer)
                    {
                        if (string.IsNullOrWhiteSpace(installedVer) && string.IsNullOrWhiteSpace(packToInstallVer))
                        {
                            return SIMILAR_PACKAGE_FOUND + SIMILAR_PACKAGE_FOUND_2;
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(installedVer))
                                installedVer = "unknown";
                            if (string.IsNullOrWhiteSpace(packToInstallVer))
                                packToInstallVer = "not specified";
                            return SIMILAR_PACKAGE_FOUND + SIMILAR_PACKAGE_FOUND_OPT_1 + installedVer + SIMILAR_PACKAGE_FOUND_OPT_1_1 + packToInstallVer + SIMILAR_PACKAGE_FOUND_2;
                        }
                    }

                    public const string ZIP_CHECKSUM_NOT_FOUND = " doesn't have a stored checksum value. User intervention is required.";
                    

                }
                public static class Error
                {
                    public const string INSTALL_PACKAGE_SELECT_EMPTY = "Empty filename string for openFileDialog detected. The open file dialog box is supposed to not allow this to happen.";
                    public const string INSTALL_PACKAGE_SELECT_NOT_FOUND = "Package to be installed was not found. Path to file is ";
                    public const string INVALID_PACKAGE_XML = "An error occured when parsing install script of package ";
                    
                    public const string ZIP_CHECKSUM_FAILED = " has failed the checksum matching test. Operation has been aborted.";

                    public const string INSTALL_PACKAGE_EXTRACT_COPY_FAILED = "An error occured when trying to copy or extract package files from ";
                    public const string INSTALL_PACKAGE_EXTRACT_COPY_FAILED_1 = " to temporary directory. ";
                    public static string InstallPackage_ExtractCopyFailed(string source)
                    {
                        return INSTALL_PACKAGE_EXTRACT_COPY_FAILED + source + INSTALL_PACKAGE_EXTRACT_COPY_FAILED_1 + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }
                     
                   

                    public const string UPDATE_PACK_FAILED = "An error occured when updating package ";
                    public const string INSTALL_PACK_FAILED = "An error occured when installing package ";

                    public const string UNINSTALL_PACK_ERR_INVALID_SCRIPT = "An error occured when uninstalling selected package. Either the package field of the installed package selected is null or its Unique ID or namespace is.";
                    public const string UNINSTALL_PACK_FAILED = "An error occured when uninstalling package with namespace ";

                    public const string UPDATE_PACK_ERR_INVALID_SCRIPT = "An error occured when updating selected package. Either the package field of the installed package selected is null or its Unique ID or namespace is.";

                    public const string UPDATE_PACK_NON_SIMILAR = "An error occured when updating selected package. The package selected for installation has a namespace of ";
                    public const string UPDATE_PACK_NON_SIMILAR_1 = " while the currently selected package namespace is ";
                    public static string UpdatePackNonSimilar(string packToInstallNamespace, string curInstalledNamespace)
                    {
                        return UPDATE_PACK_NON_SIMILAR + packToInstallNamespace + UPDATE_PACK_NON_SIMILAR_1 + curInstalledNamespace + ".";
                    }

                    public const string REINSTALL_PACK_ERR_INVALID_SCRIPT = "An error occured when reinstalling selected package. Either the package field of the installed package selected is null or its Unique ID or namespace is.";
                    public const string REINSTALL_PACK_NO_ARCH = "An error occured when reinstalling selected package. Could not find the selected package install files.";
                    public const string REINSTALL_PACK_CHECKSUM_FAILED = "An error occured when reinstalling selected package. The archived install files of the selected package is corrupted.";
                    public const string REINSTALL_PACK_FAILED = "An error occured when reinstalling package which has a namespace of ";

                    public const string FAILED_PRE_EXISTENCE_CHECK = "An error occured when doing asset files existence check for package ";
                    public const string FAILED_PRE_EXISTENCE_CHECK_1 = ". One or more asset files could not be found.";
                    public static string FailedPreExistenceCheck(string packagePath)
                    {
                        return FAILED_PRE_EXISTENCE_CHECK + packagePath + FAILED_PRE_EXISTENCE_CHECK_1;
                    }

                    public const string VIEW_PACK_ERR_INVALID_SCRIPT = "An error occured when viewing assets of selected package. Either the package field of the installed package selected is null or its Unique ID or namespace is.";

                    public const string UNABLE_READ_ARCHIVE_END = "An error occured when loading contents of archive ";
                    public const string UNABLE_READ_ARCHIVE_END_1 = ". The package might be corrupted or is being accessed by another application.";
                    public static string UnableReadArchiveEnd(string path)
                    {
                        return UNABLE_READ_ARCHIVE_END + path + UNABLE_READ_ARCHIVE_END_1;
                    }
                }
              
            }

            public static class frmPropPack
            {
                public static class Info
                {
                    public const string SAVE_ZIP_INIT = "Saving package as an archive at ";
                    public const string SAVE_ZIP_SUCCESS = "Successfully saved package as an archive at ";

                    public const string SAVE_XML_INIT = "Saving package as a standalone XML at ";
                    public const string SAVE_XML_DONE = "Successfully saved package as a standalone XML at ";
                }

                public static class Warning
                {
                    public const string NON_COMMON_PATH_FILE = "File ";
                    public const string NON_COMMON_PATH_FILE_LIC = "License File ";
                    public const string NON_COMMON_PATH_FILE_1 = " is not located under the most common directory ";
                    public static string NonCommonPathFile(string filePath, string dirPath, bool LicenseFile = false)
                    {
                        if (LicenseFile)
                            return NON_COMMON_PATH_FILE_LIC + filePath + NON_COMMON_PATH_FILE_1 + dirPath + ".";
                        else
                            return NON_COMMON_PATH_FILE + filePath + NON_COMMON_PATH_FILE_1 + dirPath + ".";
                    }
                }

                public static class Error
                {
                    public const string UNABLE_RETRIEVE_PACK = "Unable to retrieve assets file located under directory ";
                    public static string UnableRetrievePack(string dirPath)
                    {
                        return UNABLE_RETRIEVE_PACK + dirPath + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_SAVE_XML = "Unable to save install script file to ";
                    public static string UnableSaveXML(string path)
                    {
                        return UNABLE_SAVE_XML + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string ZIP_MAKE_COPY_ASSET_TO_TEMP = "An error occured when creating an archive of the assets selected. The application was unable to copy the files to the temporary directory located at ";
                    public static string ZipFileCopyAssetToTempErr(string path)
                    {
                        return ZIP_MAKE_COPY_ASSET_TO_TEMP + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }


                    public const string ZIP_SAVE_XML = "An error occured when creating an archive of the assets selected. Unable to save package install script to ";
                    public static string ZipSaveXMLFailed(string path)
                    {
                        return ZIP_SAVE_XML + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string ZIP_MAKE = "A problem has been encountered when creating an archive of the assets selected at ";

                    public const string COPY_SEL_FAILED = "An error occured when creating an archive of the assets selected. Unable to copy selected asset(s) to temporary directory ";
                    public const string LICENSE_FILE_NULL = "An error occured when creating an archive of the assets selected. Package License file's path is set to null.";

                    public const string LICENSE_FILE_NOT_EXIST = "An error occured when creating an archive of the assets selected. Package License file ";
                    public const string LICENSE_FILE_NOT_EXIST_1 = " doesn't exist.";
                    public static string LicenseFileNotExist(string path)
                    {
                        return LICENSE_FILE_NOT_EXIST + path + LICENSE_FILE_NOT_EXIST_1;
                    }

                    public const string LICENSE_FILE_NON_COPY = "An error occured when creating an archive of the assets selected. Unable to copy license file ";
                    public static string LicenseFileNonCopy(string sourceFile, string destFile)
                    {
                        return LICENSE_FILE_NON_COPY + sourceFile + TO_STR + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }


                }
            }

            public static class frmPackageAssets
            {
                public static class Error
                {
                    public const string FAILED_LOAD_ASSET_TYPE = "An error occured when viewing asset information. The GUI system failed to perform a controls setup change due to missing setup info array.";
                    public const string FAILED_LOAD_NAME = "An error occured when viewing asset information. The asset or asset group's name is not set.";
                    public const string FAILED_CORRUPTED_DATA = "An error occured when viewing asset information. The asset or asset group data is invalid or corrupted.";
                    public const string FAILED_PATH_INVALID = "An error occured when viewing asset information. The asset's file path is empty or invalid.";
                    public const string UNABLE_ADD_GROUP_NO_PACK = "Unable to proceed with adding an asset group due to a null package.";
                    public const string UNABLE_REMOVE_GROUP_NO_PACK = "Unable to proceed with removing an asset group due to a null package.";
                    public const string UNABLE_ADD_FILE_NO_PACK = "Unable to proceed with importing an asset due to a null package.";
                    public const string UNABLE_REMOVE_FILE_NO_PACK = "Unable to proceed with removing an asset due to a null package.";
                    public const string UNABLE_REMOVE_GROUP_NULL_NODE = "Unable to proceed with removing an asset group due to the Tree View selected node being null.";
                    public const string UNABLE_ADD_FILE_NULL_NODE = "Unable to proceed with adding an asset due to the Tree View selected node being null.";
                    public const string UNABLE_REMOVE_FILE_NULL_NODE = "Unable to proceed with removing an asset due to the Tree View selected node being null.";
                    public const string UNABLE_CREATE_COLLECTION = "A problem occured when adding a new asset group. Cannot create the collection which will parent the new group.";
                    public const string UNABLE_REMOVE_GROUP_TAG_NULL = "A problem occured when removing asset group. The associated tag of the selected node is set to null.";
                    public const string UNABLE_REMOVE_GROUP_INVALID = "A problem occured when removing asset group. The group to be removed is invalid.";
                    public const string UNABLE_REMOVE_GROUP_PARENT_NULL = "A problem occured when removing asset group. Unable to retrieve the parent of the selected node.";
                    public const string UNABLE_REMOVE_GROUP_INVALID_PARENT = "A problem occured when removing asset group. The parenting collection of the group is invalid.";
                    public const string UNABLE_ADD_GROUP_INVALID_COLLECTION = "A problem occured when adding an asset group. The collection that will parent the group is invalid.";

                    public const string UNABLE_ADD_FILE_SINGLE = "A problem occured when importing asset file located at ";
                    public const string UNABLE_ADD_FILE_SINGLE_NULL_PATH = "A problem occured when importing an asset file. The file to be imported could not be found.";
                    public static string UnableAddFileSingle(string path)
                    {
                        return UNABLE_ADD_FILE_SINGLE + path + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                    }

                    public const string UNABLE_REMOVE_ASSET_TAG_NULL = "A problem occured when removing asset file. Tag of the selected node is null.";
                    public const string UNABLE_REMOVE_ASSET_TAG_INVALID = "A problem occured when removing asset file. Tag of the selected node is invalid.";
                    public const string UNABLE_REMOVE_ASSET_INVALID = "A problem occured when removing asset file. The selected node is either not an asset or has invalid data.";
                    public const string UNABLE_REMOVE_ASSET_PARENT_NULL_INVALID = "A problem occured when removing asset file. The parent group or collection of the selected asset is null or invalid.";

                    public const string UNABLE_SAVE_GROUP_INFO_NULL_PACK = "Unable to save group information changes due to a null package.";
                    public const string UNABLE_SAVE_GROUP_INFO_NULL_SELECT = "Unable to save group information changes due to the Tree View selected node being null.";
                    public const string UNABLE_SAVE_GROUP_INFO_NULL_TAG = "A problem occured when saving asset group information. The associated tag of the selected node is null or invalid.";

                    public const string UNABLE_SAVE_FILE_INFO_NULL_PACK = "Unable to save file information changes due to a null package.";
                    public const string UNABLE_SAVE_FILE_INFO_NULL_SELECT = "Unable to save file information changes due to the Tree View selected node being null.";
                    public const string UNABLE_SAVE_FILE_INFO_NULL_TAG = "A problem occured when saving asset file information. The associated tag of the selected node is null or invalid.";
                }
            }

           
        }

        public static class PackageMaker
        {
            public static class Error
            {
                public const string AUDIO_COLLECTION_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the audio collection does not specify its type.";
                public const string AUDIO_FILE_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the audio asset does not specify its type.";

                public const string COLLECTION_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the single file collection does not specify its type.";

                public const string CHAR_FILE_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the character image asset does not specify its type.";
                public const string MOVIE_FILE_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the video asset does not specify its type.";
                public const string ATLAS_FILE_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the tileset asset does not specify its type.";

                public const string GEN_FILE_NO_TYPE = "A problem occured when copying selected assets to temporary directory. One of the generator parts asset does not specify its type.";
                public const string GEN_FILE_NO_GENDER = "A problem occured when copying selected assets to temporary directory. One of the generator parts asset does not specify its gender compatibility.";
                public const string GEN_FILE_ERR_FILE_NAME = "A problem occured when copying selected assets to temporary directory. Unable to retrieve the formatted file name of one of the generator parts asset.";

                public const string FILE_ALREADY_RELATIVE = "A problem occured when copying selected assets to temporary directory. One of the selected asset's internal path is already formatted. The internal path of the asset is ";
                public const string FILE_PATH_NOT_SET = "A problem occured when copying selected assets to temporary directory. One of the selected asset's path is not set.";
                public const string DIR_EXISTENCE_GEN_FAILED = "A problem occured when copying selected assets to temporary directory. Unable to create required temporary directory(s) for generator parts.";

            }
        }

        public static class PackageUtil
        {

            public static class Error
            {
                public const string ROOT_DIR_NULL = "An error occured when copying assets of package to directory. The root directory of package argument is null.";
                public const string DEST_DIR_NULL = "An error occured when copying assets of package to directory. The destination directory argument is null.";
                public const string ROOTED_PATH = "An error occured when copying assets of package to directory. One of the asset files' path is already rooted. The path of the asset is ";

                public const string LIC_FILE_NULL = "An error occured when copying assets of package to directory. The package license file path is set to null.";

                public const string LIC_FILE_NOT_EXIST = "An error occured when copying assets of package to directory. The package license file ";
                public const string LIC_FILE_NOT_EXIST_1 = " does not exist.";
                public static string LicFileNotExists(string path)
                {
                    return LIC_FILE_NOT_EXIST + path + LIC_FILE_NOT_EXIST_1;
                }

                public const string COPY_LIC_FAILED = "An error occured when copying assets of package to directory. The program was unable to copy package license file ";
                public static string CopyLicFailed(string sourceFile, string destFile)
                {
                    return COPY_LIC_FAILED + sourceFile + TO_STR + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string COPY_FILE_FAILED = "An error occured when copying assets of package to directory. The program was unable to copy asset file ";
                public static string CopyFileFailed(string sourceFile, string destFile)
                {
                    return COPY_FILE_FAILED + sourceFile + TO_STR + destFile + ". " + FileSystem.UNABLE_CREATE_FOLDER_OR_FILE_CLOSING;
                }

                public const string ROOT_DIR_NULL_GEN_DEL = "An error occured when removing generator assets from package. The root directory of package argument is null.";
                public const string ROOTED_PATH_GEN_DEL = "An error occured when removing generator assets from package. One of the asset files' path is already rooted. The path of the asset is ";
                public const string GEN_DEL_FAILED = "An error occured when removing generator assets from package. The program was unable to delete asset file ";
            }

            public static class Info
            {
                public const string COPY_ASSETS_INIT = "Copying package assets from directory ";
                public static string CopyAssetsInit(string sourceDir, string destDir)
                {
                    return COPY_ASSETS_INIT + sourceDir + TO_STR + destDir;
                }

                public const string COPY_ASSETS_DONE = "Successfully copied package assets from directory ";
                public static string CopyAssetsDone(string sourceDir, string destDir)
                {
                    return COPY_ASSETS_DONE + sourceDir + TO_STR + destDir;
                }

                public const string REMOVE_GEN_FILE_START = "Removing package generator files from directory ";
                public const string REMOVE_GEN_FILE_DONE = "Successfully removed package generator files from directory ";
                
            }
        }
        public static class RMPackage
        {
           
            public static class CollectionsName
            {
                public const string GEN_PARTS = "generator parts";
                public const string ANIMATIONS = "animations";
                public const string BGM = "audio BGM";
                public const string BGS = "audio BGS";
                public const string ME = "audio ME";
                public const string SE = "audio SE";
                public const string BATTLEBACKS_1 = "battle backgrounds 1";
                public const string BATTLEBACKS_2 = "battle backgrounds 2";
                public const string DATA = "system data";
                public const string PARALLAXES = "parallaxes";
                public const string PICTURES = "pictures";
                public const string PLUGINS = "plugins";
                public const string SYS_IMAGES = "system images";
                public const string TITLES_1 = "titles 1";
                public const string TITLES_2 = "titles 2";
                public const string CHARACTERS = "character images";
                public const string TILESETS = "tileset files";
                public const string MOVIES = "video files";
                public static class Singular
                {
                    public const string GEN_PART = "Generator part";
                    public const string ANIMATION = "Animation";
                    public const string BGM = "Audio BGM";
                    public const string BGS = "Audio BGS";
                    public const string ME = "Audio ME";
                    public const string SE = "Audio SE";
                    public const string BATTLEBACK_1 = "Battle Background 1";
                    public const string BATTLEBACK_2 = "Battle Background 2";
                    public const string DATA = "System Data";
                    public const string PARALLAX = "Parallax";
                    public const string PICTURE = "Picture";
                    public const string PLUGIN = "Plugin";
                    public const string SYS_IMAGE = "System Image";
                    public const string TITLE_1 = "Title 1";
                    public const string TITLE_2 = "Title 2";
                    public const string CHARACTER = "Character image";
                    public const string TILESET = "Tileset file";
                    public const string MOVIE = "Video file";
                }
            }



            public static class Info
            {
                public const string READING_XML = "Reading Package metadata at ";
                public const string RETRIEVE_AUTO = "Package \"";
                public const string RETRIEVE_AUTO_2 = "\" at ";
                public const string RETRIEVE_AUTO_3 = " has implicit value set to true. Retrieving install files automatically from the XML's directory.";
                public static string RetrieveAuto(string packageName, string XMLPath)
                {
                    return RETRIEVE_AUTO + packageName + RETRIEVE_AUTO_2 + XMLPath + RETRIEVE_AUTO_3;
                }

                public const string RETRIEVED_AUTO_DATA = "Retrieving ";
                public const string RETRIEVED_AUTO_DATA_1 = " automatically at ";
                public const string RETRIEVED_AUTO_DATA_2 = " for package \"";
                public static string RetrievedAutoData(string packageName, string folderPath, RMCollectionType collType)
                {
                    return RETRIEVED_AUTO_DATA + collType.ToLoggerPluralString() + RETRIEVED_AUTO_DATA_1 + folderPath + RETRIEVED_AUTO_DATA_2 + packageName + "\".";
                }

                public const string RETRIEVED_FILE = " has been retrieved automatically for package \"";
                public static string RetrievedFile(string packageName, string filePath, RMCollectionType collType)
                {
                    return collType.ToLoggerSingularString() + " " + filePath + RETRIEVED_FILE + packageName + "\".";
                }

                public const string RETRIEVE_DONE = "Successfully read metadata of package \"";
                public const string RETRIEVE_DONE_1 = "\" at ";
                public static string RetrieveDone(string packageName, string XMLPath)
                {
                    return RETRIEVE_DONE + packageName + RETRIEVE_DONE_1 + XMLPath + ".";
                }
            }

            public static class Warning
            {
                public const string INVALID_PACK = "Package \"";

                public const string INVALID_PACK_URL = "\" has an invalid Project URL.";
                public static string InvalidPackURL(string packageName)
                {
                    return INVALID_PACK + packageName + INVALID_PACK_URL;
                }
            }

            public static class Error
            {
                public const string RETRIEVE_AUTO_ERR = "Unable to retrieve ";
                public const string RETRIEVE_AUTO_ERR_1 = " automatically at ";
                public const string RETRIEVE_AUTO_ERR_2 = " for package \"";
                public const string RETRIEVE_AUTO_ERR_3 = "\". Make sure that you have granted the application the correct permissions and that the folder still exists.";
                public static string RetrieveAutoError(string folderPath, string packageName, RMCollectionType collType)
                {
                    return RETRIEVE_AUTO_ERR + collType.ToLoggerPluralString() + RETRIEVE_AUTO_ERR_1 + folderPath + RETRIEVE_AUTO_ERR_2 + packageName + RETRIEVE_AUTO_ERR_3;

                }

                public const string INVALID_GEN_FILE = "File ";
                public const string INVALID_GEN_FILE_1 = " is not a valid generator part. Skipping file. Make sure that the filename is in correct format.";
                public static string InvalidGenFile(string filePath)
                {
                    return INVALID_GEN_FILE + filePath + INVALID_GEN_FILE_1;
                }

                public const string CREATE_COLL_FAILED = "Unable to create collection of type ";

                public const string UNABLE_SAVE_XML = "An error occured when saving package XML to ";

                public const string UNABLE_SAVE_XML_OVERWRITE_FAIL = ". The file already exists.";
                public static string UnableSaveXMLOverwriteFail(string path)
                {
                    return UNABLE_SAVE_XML + path + UNABLE_SAVE_XML_OVERWRITE_FAIL;
                }

                public static string UnableSaveXML(string path)
                {
                    return UNABLE_SAVE_XML + path + ".";
                }

               
            }
        }
    }
}
