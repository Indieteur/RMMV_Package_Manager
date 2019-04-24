using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;
using Indieteur.SimpleDatabaseFormat;

namespace RMMV_PackMan
{
    static class PMFileSystem
    {
      

        public static string PackMan_ExecDir { get => Application.StartupPath; }
        public static string MV_Installation_Directory { get => Properties.Settings.Default.MVPath; set => Properties.Settings.Default.MVPath = value; }
        public static string PackMan_TempDir { get => PackMan_ExecDir + "\\" + Vars.TEMP_DIR_FOLDER; }
        public static string PackMan_TempRenumberDir { get => PackMan_TempDir + "\\" + Vars.TEMP_RENUMBER_DIR; }

        public static string LogUser_Dir { get => PackMan_ExecDir + "\\" + Vars.USER_LOG_FOLDER; }
        public static string LogDebug_Dir { get => PackMan_ExecDir + "\\" + Vars.DEBUG_LOG_FOLDER; }

        public static string Resource_Directory { get => PackMan_ExecDir + "\\" + Vars.RESOURCES_FOLDER; }
        public static string Template_BaseGenXML_Path { get => Resource_Directory + "\\" + Vars.DEFAULT_GENERATOR_FILES_PACKAGE_FILE; }
        public static string Template_DefProjFilesXML_Path { get => Resource_Directory + "\\" + Vars.DEFAULT_PROJECT_FILES_PACKAGE_FILE; }
        public static string SDF_GenFloor_Path { get => Resource_Directory + "\\" + Vars.SDF_GENFLOOR_FILENAME; }
        public static string SDF_GenSpecial_Path { get => Resource_Directory + "\\" + Vars.SDF_GENSPECIAL_FILENAME; }

        public static string PackMan_ManDir { get => MV_Installation_Directory + "\\" + Vars.PACKAGE_MANAGER_DIRECTORY; }

        public static string MV_NewData_Dir { get => MV_Installation_Directory + "\\" + Vars.NEWDATA_DIR; }
        public static string MV_NewDataMan_Dir { get => MV_NewData_Dir + "\\" + Vars.PACKAGE_MANAGER_DIRECTORY; }

        public static string PackMan_TempInstall { get => PackMan_TempDir + "\\" + Vars.TEMP_INSTALL_DIR; }

        public static string PackMan_TempBackupDir { get => PackMan_TempDir + "\\" + Vars.TEMP_BACKUP_DIR; }

        public static string PackMan_TempMakeDir { get => PackMan_TempDir + "\\" + Vars.TEMP_MAKE_ZIP_DIR; }

        


        public static void CheckBaseFiles(this frmMain form)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();

            if (!Directory.Exists(Resource_Directory))
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.RESOURCE_DIR_NOT_FOUND, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteErrorLog(LoggerMessages.RequiredData.Error.RESOURCEDIR_MISS + Resource_Directory + ".", _namespace, null, BasicDebugLogger.DebugErrorType.CriticalError, true);
                Vars.MustExitAtStart = true;
                Application.Exit();
                return;
            }
            if (!File.Exists(Template_BaseGenXML_Path))
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.BASEGEN_XML_MISS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteWarningLog(LoggerMessages.RequiredData.Warning.BASEGEN_XML_MISS + Template_BaseGenXML_Path + ".", _namespace);
            }
            if (!File.Exists(Template_DefProjFilesXML_Path))
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.DEFPROJFILE_XML_MISS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteWarningLog(LoggerMessages.RequiredData.Warning.BASENEWPROJ_XML_MISS + Template_DefProjFilesXML_Path + ".", _namespace);
            }

            if (!File.Exists(SDF_GenFloor_Path))
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.SDF_UNINSTALLGENFILES_MISS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteWarningLog(LoggerMessages.RequiredData.Warning.GENFLOOR_CFG_MISS + SDF_GenFloor_Path + ".", _namespace);
            }
            else
            {
                GeneratorPartsManager.Precision.LoadPartsWithNonDefaultFloorSDF(SDF_GenFloor_Path);
                Logger.WriteInformationLog(LoggerMessages.RequiredData.Information.GENFLOOR_LOADED + SDF_GenFloor_Path + ".", _namespace);
            }

            if (!File.Exists(SDF_GenSpecial_Path))
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.SDF_UNINSTALLGENFILES_MISS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteWarningLog(LoggerMessages.RequiredData.Warning.GENSPECIAL_CFG_MISS + SDF_GenSpecial_Path + ".", _namespace);
            }
            else
            {
                GeneratorPartsManager.Precision.LoadSpecialNumberedPartsSDF(SDF_GenSpecial_Path);
                Logger.WriteInformationLog(LoggerMessages.RequiredData.Information.GENSPECIAL_LOADED + SDF_GenSpecial_Path + ".", _namespace);
            }

            if (!Directory.Exists(PackMan_ManDir))
            {
                Logger.WriteWarningLog(LoggerMessages.RequiredData.Warning.MAN_DIR_NOT_FOUND + PackMan_ManDir + ".", _namespace);
                Exception ex;
                CreateFolderResult result = Helper.CreateFolderSafely(PackMan_ManDir, _namespace, out ex, new CreateFolderLogMessages(createFailed: LoggerMessages.RequiredData.Warning.ManDirUnableCreate));
                if (result == CreateFolderResult.UserCancelled)
                    Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.MAN_DIR_CREATE_NOTABLE, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                Logger.WriteInformationLog(LoggerMessages.RequiredData.Information.MAN_DIR_VERIFIED + PackMan_ManDir + ".", _namespace);

        }

        public static void CheckDefaultPackages(this frmMain form)
        {
            string _namespace = MethodBase.GetCurrentMethod().ToLogFormatFullName();
            if (PackageManagement.BaseGeneratorPackage == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.BASEGEN_NOT_PARSED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logger.WriteWarningLog(LoggerMessages.PackageManagement.DefaultPackages.Warning.GEN_PACK_NOT_FOUND, _namespace);
                if (!File.Exists(Template_BaseGenXML_Path))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.BASEGEN_XML_MISS_REQ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.DefaultPackages.Error.GEN_TEMPLATE_NOT_FOUND + Template_BaseGenXML_Path + ".", _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                }
                else
                {
                    LogDataList log = null;
                    try
                    {
                        PackageManagement.MakeInstalledPackageFileForDefPackage(Template_BaseGenXML_Path, _namespace, out log, MV_Installation_Directory);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.BASEGEN_UNABLE_INST_DATA, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.DefaultPackages.Error.ERR_OCCUR_GEN, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);

                    }
                    if (log != null && log.HasErrorsOrWarnings())
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        frmLogger loggerForm = new frmLogger(_logList: log);
                        loggerForm.StartPosition = FormStartPosition.CenterParent;
                        loggerForm.ShowDialog();
                    }
                }
            }
            else
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.DefaultPackages.Info.GEN_PACK_FOUND + PackageManagement.BaseGeneratorPackage.XMLPath + ".", _namespace);

            if (PackageManagement.DefaultProjectFilesPackage == null)
            {
                Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.DEFPROJFILE_NOT_PARSED, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logger.WriteWarningLog(LoggerMessages.PackageManagement.DefaultPackages.Warning.NEWDATA_PACK_NOT_FOUND, _namespace);
                if (!File.Exists(Template_BaseGenXML_Path))
                {
                    Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.DEFPROJFILE_XML_MISS_REQ, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteErrorLog(LoggerMessages.PackageManagement.DefaultPackages.Error.NEWDATA_TEMPLATE_NOT_FOUND + Template_DefProjFilesXML_Path + ".", _namespace, null, BasicDebugLogger.DebugErrorType.Error);
                }
                else
                {
                    LogDataList log = null;
                    try
                    {
                        PackageManagement.MakeInstalledPackageFileForDefPackage(Template_DefProjFilesXML_Path, _namespace, out log, MV_NewData_Dir);
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.PMFileSystem.NEWPROJ_UNABLE_INST_DATA, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteErrorLog(LoggerMessages.PackageManagement.DefaultPackages.Error.ERR_OCCUR_NEW_PROJ, _namespace, ex, BasicDebugLogger.DebugErrorType.Error);

                    }
                    if (log != null && log.HasErrorsOrWarnings())
                    {
                        Helper.ShowMessageBox(MessageBoxStrings.General.HAS_ERRORS_WARNINGS, MessageBoxStrings.MESSAGEBOX_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        frmLogger loggerForm = new frmLogger(_logList: log);
                        loggerForm.StartPosition = FormStartPosition.CenterParent;
                        loggerForm.ShowDialog();
                    }
                }
            }
            else
                Logger.WriteInformationLog(LoggerMessages.PackageManagement.DefaultPackages.Info.NEWDATA_PACK_FOUND + PackageManagement.DefaultProjectFilesPackage.XMLPath + ".", _namespace);
        }


        public static class Generator
        {

            public static string Root { get => MV_Installation_Directory + "\\" + DirectoryNames.Generator.ROOT; }
            public static class Face
            {
                public static string Root { get => Generator.Root + "\\" + DirectoryNames.Generator.FACE; }
                public static string MALE { get => Root + "\\" + DirectoryNames.Generator.MALE; }
                public static string FEMALE { get => Root + "\\" + DirectoryNames.Generator.FEMALE; }
                public static string KID { get => Root + "\\" + DirectoryNames.Generator.KID; }
            }

            public static class SV
            {
                public static string Root { get => Generator.Root + "\\" + DirectoryNames.Generator.SV; }
                public static string MALE { get => Root + "\\" + DirectoryNames.Generator.MALE; }
                public static string FEMALE { get => Root + "\\" + DirectoryNames.Generator.FEMALE; }
                public static string KID { get => Root + "\\" + DirectoryNames.Generator.KID; }
            }

            public static class TV
            {
                public static string Root { get => Generator.Root + "\\" + DirectoryNames.Generator.TV; }
                public static string MALE { get => Root + "\\" + DirectoryNames.Generator.MALE; }
                public static string FEMALE { get => Root + "\\" + DirectoryNames.Generator.FEMALE; }
                public static string KID { get => Root + "\\" + DirectoryNames.Generator.KID; }
            }

            public static class TVD
            {
                public static string Root { get => Generator.Root + "\\" + DirectoryNames.Generator.TVD; }
                public static string MALE { get => Root + "\\" + DirectoryNames.Generator.MALE; }
                public static string FEMALE { get => Root + "\\" + DirectoryNames.Generator.FEMALE; }
                public static string KID { get => Root + "\\" + DirectoryNames.Generator.KID; }
            }

            public static class Variation
            {
                public static string Root { get => Generator.Root + "\\" + DirectoryNames.Generator.VARIATION; }
                public static string MALE { get => Root + "\\" + DirectoryNames.Generator.MALE; }
                public static string FEMALE { get => Root + "\\" + DirectoryNames.Generator.FEMALE; }
                public static string KID { get => Root + "\\" + DirectoryNames.Generator.KID; }
            }
        }
    }
}
