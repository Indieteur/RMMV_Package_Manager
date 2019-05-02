using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Indieteur.BasicLoggingSystem;
using Indieteur.ChecksumZIP;
using System.Threading;

namespace RMMV_PackMan
{
    public class PackArchive
    {
        public InstalledPackage Parent { get; set; }
        public string ArchivePath { get => Parent.Directory + "\\" + Vars.INSTALLED_ARCH_FILENAME; }
        public PackArchive(InstalledPackage parent)
        {
            Parent = parent;
        }

    }

    public static class ArchiveManagement
    {
        public enum ChecksumStatus
        {
            NoStoredChecksum,
            MatchingChecksum,
            ChecksumMatchFailed
        }
        public static void CreateNewZip(string directoryOfFiles, string pathToCreateZip, string _namespace, bool useRootFolder = false, bool storeChecksumInArchive = true, bool overwrite = true)
        {

            Logger.WriteInformationLog(LoggerMessages.ArchiveManagement.Information.CreateZipStart(directoryOfFiles, pathToCreateZip), _namespace);
            try
            {
                ChecksumZIP.CreateArchiveFromDirectory(directoryOfFiles, pathToCreateZip, false, overwrite, CompressionLevel.Optimal, useRootFolder);
                if (storeChecksumInArchive)
                {
                    ChecksumZIP.AppendChecksumToArchive(pathToCreateZip, ChecksumZIPHelper.GetSuggestedBufferSize(pathToCreateZip, Properties.Settings.Default.MinBufferSize, Properties.Settings.Default.MaxBufferSize));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.ArchiveManagement.Error.CreateZipFailed(directoryOfFiles, pathToCreateZip), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                Exception outEx;
                if (File.Exists(pathToCreateZip))
                    Helper.DeleteFileSafely(pathToCreateZip, _namespace, out outEx, LoggerMessages.GeneralError.OVERWRITE_FILE_FAILED_ARG);
                throw;
            }

            Logger.WriteInformationLog(LoggerMessages.ArchiveManagement.Information.CreateZipSuccess(directoryOfFiles, pathToCreateZip), _namespace);

        }
        public static void ExtractZip(string zipFile, string dirToExtract, string _namespace, bool performChecksumMatching = true)
        {
            Logger.WriteInformationLog(LoggerMessages.ArchiveManagement.Information.ExtractZipStart(zipFile, dirToExtract), _namespace);
            try
            {
                ChecksumZIP.ExtractZip(zipFile, dirToExtract, performChecksumMatching, ChecksumZIPHelper.GetSuggestedBufferSize(zipFile, Properties.Settings.Default.MinBufferSize, Properties.Settings.Default.MaxBufferSize));
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.ArchiveManagement.Error.ExtractZipFailed(zipFile, dirToExtract), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                throw;
            }
            Logger.WriteInformationLog(LoggerMessages.ArchiveManagement.Information.ExtractZipSuccess(zipFile, dirToExtract), _namespace);
        }

        public static ChecksumStatus PerformArchiveChecksumCheck(string filePath)
        {
            if (!ChecksumZIP.ArchiveHasChecksumValue(filePath))
                return ChecksumStatus.NoStoredChecksum;
            if (!ChecksumZIP.ArchiveHasMatchingStoredChecksum(filePath, ChecksumZIPHelper.GetSuggestedBufferSize(filePath, Properties.Settings.Default.MinBufferSize, Properties.Settings.Default.MaxBufferSize)))
                return ChecksumStatus.ChecksumMatchFailed;
            return ChecksumStatus.MatchingChecksum;

        }
    }


    public class InstalledPackageArchiveNotFoundException : Exception
    {
        public string Directory;
        public InstalledPackageArchiveNotFoundException(string message, string directory) : base(message)
        {
            Directory = directory;
        }
    }
}
