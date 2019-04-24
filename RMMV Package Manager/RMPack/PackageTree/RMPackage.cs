using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public class RMPackage : RMPackObject
    {
        protected string _md5OfUID, _uID;

        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }



        public RMPackLic License { get; set; }


        public RMCollectionType ContentsSummary
        {
            get
            {
                return Pacman.Helper.RetrieveCollectionSummary(this);
            }
        }
        public List<RMCollection> Collections { get; set; } = new List<RMCollection>();
        public bool Implicit { get; set; } = false; //For root package only. Will be ignored for sub packages.
        public bool Installed { get; set; } = false;
        public string XMLLocation { get; protected set; }
        public override InstallStatus InstallationStatus { get => GetInstallStatusByListCheck(Collections); set => throw new NotImplementedException(); }

        public string XMLDirectory
        {
            get
            {
                if (XMLLocation == null)
                    return null;
                return Path.GetDirectoryName(XMLLocation);
            }
        }

        public string UniqueID
        {
            get
            {
                return _uID;
            }
            set
            {
                _md5OfUID = null;
                _uID = value;
            }
        }

        public string UniqueIDInMD5
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_md5OfUID))
                {
                    if (UniqueID == null)
                        return null;
                    else
                        _md5OfUID = Helper.CreateMD5FromString(UniqueID);
                }
                return _md5OfUID;
            }
        }


       

        public RMPackage()
        {

        }

        public RMPackage(string XMLPath, string _namespace, out LogDataList log, string ImplicitInitDir = null, bool NoAssetProbing = false, bool skipGenParts = false)
        {

            Init(XMLPath, _namespace, out log, ImplicitInitDir, NoAssetProbing, skipGenParts);
        }
        
        protected void Init(string XMLPath, string _namespace, out LogDataList log, string ImplicitInitDir = null, bool NoAssetProbing = false, bool skipGenParts = false)
        {
            log = new LogDataList();
            log.WriteInformationLog(LoggerMessages.RMPackage.Info.READING_XML + XMLPath, _namespace);
            XMLLocation = XMLPath;

            XDocument doc = XDocument.Load(XMLPath);
            XElement elementChecked = doc.Root;
            if (doc.Root == null)
            {
                try
                {
                    throw new NoPackageFoundException(XMLPath + ExceptionMessages.RMPackage.NOT_VALID_PACK_FILE);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex.Message, _namespace, ex);
                    throw;
                }
            }


            IEnumerable<XAttribute> attributes = elementChecked.Attributes();

            if (attributes == null || attributes.Count() == 0)
            {
                try
                {
                    throw new PackageAttributeORFieldNotFoundException(ExceptionMessages.RMPackage.NoName(XMLPath), PackageAttributeORFieldNotFoundException.WhichAttribute.Name);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex.Message, _namespace, ex);
                    throw;
                }
            }

            foreach (XAttribute xat in attributes)
            {
                if (xat.Name.LocalName == RMPConstants.ATTR_NAME)
                    Name = xat.Value;
                else if (xat.Name.LocalName == RMPConstants.ATTR_AUTH)
                    Author = xat.Value;
                else if (xat.Name.LocalName == RMPConstants.ATTR_VERSION)
                    Version = xat.Value;
                else if (xat.Name.LocalName == RMPConstants.ATTR_IMPLICIT)
                    Implicit = Pacman.Helper.RetrieveBoolValue(xat.Value);
                else if (xat.Name.LocalName == RMPConstants.ATTR_LOADED)
                    Installed = Pacman.Helper.RetrieveBoolValue(xat.Value);

            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                try
                {
                    throw new PackageAttributeORFieldNotFoundException(ExceptionMessages.RMPackage.NoName(XMLPath), PackageAttributeORFieldNotFoundException.WhichAttribute.Name);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex.Message, _namespace, ex);
                    throw;
                }
            }
                

            IEnumerable<XElement> elements = elementChecked.Elements();

            if (elements == null || elements.Count() == 0)
            {
                try
                {
                    throw new PackageAttributeORFieldNotFoundException(ExceptionMessages.RMPackage.NoLic(XMLPath), PackageAttributeORFieldNotFoundException.WhichAttribute.License);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex.Message, _namespace, ex);
                    throw;
                }
            }
        


            foreach (XElement elem in elements)
            {
                if (elem.Name == RMPConstants.FIELD_DESC)
                    Description = elem.Value.ParseXMLString();
                else if (elem.Name.LocalName == RMPConstants.FIELD_URL)
                {
                    URL = elem.Value;
                    if (!URL.IsAValidURL())
                        log.WriteWarningLog(LoggerMessages.RMPackage.Warning.InvalidPackURL(Name), _namespace);
                }
                else if (elem.Name.LocalName == RMPConstants.FIELD_LICENSE)
                        License = new RMPackLic(elem, this);
                   
                else if (elem.Name.LocalName == RMPConstants.FIELD_UID)
                    UniqueID = elem.Value.ToLower();
                else
                {
                    RMCollection rmc = RMCollection.TryParse(elem, this, skipGenParts);
                    if (rmc != null)
                        Collections.Add(rmc);
                }
            }
            if (string.IsNullOrWhiteSpace(UniqueID))
            {
                try
                {
                    throw new PackageAttributeORFieldNotFoundException(ExceptionMessages.RMPackage.NoUID(XMLPath), PackageAttributeORFieldNotFoundException.WhichAttribute.UniqueID);
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex.Message, _namespace, ex);
                    throw;
                }
            }

            LogDataList outputLogAutoRetrieve = null;
            if (Implicit && !NoAssetProbing)
                if (string.IsNullOrWhiteSpace(ImplicitInitDir))
                    AutoRetrieveContents(_namespace, out outputLogAutoRetrieve, skipGenParts);
                else
                    AutoRetrieveContents(ImplicitInitDir, _namespace, out outputLogAutoRetrieve, skipGenParts);

            log.AppendLogs(outputLogAutoRetrieve);

            log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrieveDone(Name, XMLPath), _namespace);


        }

        public override XElement ToXMLElement()
        {
            return ToXMLElement(!Implicit);
        }

        public XElement ToXMLElement(bool @explicit)
        {
            XElement newElem = new XElement(RMPConstants.FIELD_PACKAGE);
            newElem.SetAttributeValue(RMPConstants.ATTR_NAME, Name);
            newElem.SetElementValue(RMPConstants.FIELD_UID, UniqueID);
            if (!string.IsNullOrWhiteSpace(Author))
                newElem.SetAttributeValue(RMPConstants.ATTR_AUTH, Author);
            if (!string.IsNullOrWhiteSpace(Version))
                newElem.SetAttributeValue(RMPConstants.ATTR_VERSION, Version);
            if (Implicit)
                newElem.SetAttributeValue(RMPConstants.ATTR_IMPLICIT, "Y");
            if (License != null)
                newElem.Add(License.ToXElement());
            if (!string.IsNullOrWhiteSpace(URL))
                newElem.SetElementValue(RMPConstants.FIELD_URL, URL);
            if (!string.IsNullOrWhiteSpace(Description))
                newElem.SetElementValue(RMPConstants.FIELD_DESC, Description.ToXMLString());
            if (Installed)
                newElem.SetAttributeValue(RMPConstants.ATTR_LOADED, "Y");


            if (@explicit && Collections != null && Collections.Count > 0)
            {
                foreach (RMCollection collection in Collections)
                {

                    if (!collection.IsCollectionEmpty())
                        newElem.Add(collection.ToXMLElement());
                }
            }

            return newElem;
        }




        void AutoRetrieveContents(string _namespace, out LogDataList log, bool skipGenParts = false)
        {
            AutoRetrieveContents(Path.GetDirectoryName(XMLLocation), _namespace, out log, skipGenParts);
        }

        public void AutoRetrieveContents(string contentDirectory, string _namespace, out LogDataList log, bool skipGenParts = false)
        {
            log = new LogDataList();
            log.WriteInformationLog(LoggerMessages.RMPackage.Info.RetrieveAuto(Name, XMLLocation), _namespace);
            RMPackage thisPackage = this;
            LogDataList outLog = null;
            RMImplicit.RetrievePackFromDir(contentDirectory, _namespace, true, out outLog, ref thisPackage, skipGenParts);
            log.AppendLogs(outLog);
        }

        public  enum SaveToFileMode
        {
            Auto,
            ImplicitAssetInfo,
            ExplicitAssetInfo
        }

        public void SaveToFile(string path, string _namespace, SaveToFileMode mode = SaveToFileMode.Auto, bool Overwrite = true, WriteAllTextLogMessages logMessage = null)
        {
            if (!Overwrite && File.Exists(path))
            {
                try
                {
                    throw new FileLoadException(path + ExceptionMessages.General.ALREADY_EXISTS);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(LoggerMessages.RMPackage.Error.UnableSaveXMLOverwriteFail(path), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                    throw;
                }
            }
            

            Exception outEx;

            string toWrite = null;

            try
            {
                switch (mode)
                {
                    case SaveToFileMode.Auto:
                        toWrite = ToString();
                        break;
                    case SaveToFileMode.ExplicitAssetInfo:
                        toWrite = ToString(true);
                        break;
                    default:
                        toWrite = ToString(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.RMPackage.Error.UnableSaveXML(path), _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                throw ex;
            }
            if (Helper.WriteAllTextSafely(path, toWrite, _namespace, out outEx, logMessage) !=  WriteFileResult.Success)
                throw outEx;
            XMLLocation = path;
        }

        public List<RMPackFile> RetrieveAllFiles()
        {
            if (Collections == null || Collections.Count == 0)
                return null;
            List<RMPackFile> files = new List<RMPackFile>();
            if (Collections != null)
            {
                foreach (RMCollection collection in Collections)
                {
                    List<RMPackFile> retrievedFiles = collection.RetrieveAllFiles();
                    if (retrievedFiles != null && retrievedFiles.Count > 0)
                        files.AddRange(retrievedFiles);
                }
            }
            return files;
        }


        public void SetInstalledPropertyAll(InstallStatus status)
        {
            if (Collections == null || Collections.Count == 0)
                return;
            if (Collections != null)
                foreach (RMCollection collection in Collections)
                    collection.SetInstalledPropertyAll(status);
        }

        public RMGenFile FindGenFileWithPath(string path)
        {
            if (Collections != null)
            {
                foreach (RMCollection collection in Collections)
                {
                    RMGeneratorCollection genCollection = collection as RMGeneratorCollection;
                    if (genCollection != null)
                    {
                        RMGenFile genFile = genCollection.FindFileWithPath(path);
                        if (genFile != null)
                            return genFile;
                        genCollection = null;
                    }
                }
            }
            return null;
        }

        public override RMPackObject Clone()
        {
            RMPackage clone = new RMPackage();
            clone.Author = Author;
            if (Collections != null)
            {
                foreach (RMCollection collection in Collections)
                {
                    RMAudioCollection rma = collection as RMAudioCollection;
                    if (rma != null)
                    {
                        clone.Collections.Add(rma.Clone(clone));
                        continue;
                    }

                    RMDataCollection rmdc = collection as RMDataCollection;
                    if (rmdc != null)
                    {
                        clone.Collections.Add(rmdc.Clone(clone));
                        continue;
                    }

                    RMGeneratorCollection rmgen = collection as RMGeneratorCollection;
                    if (rmgen != null)
                    {
                        clone.Collections.Add(rmgen.Clone(clone));
                        continue;
                    }

                    RMAnimationCollection rmanim = collection as RMAnimationCollection;
                    if (rmanim != null)
                    {
                        clone.Collections.Add(rmanim.Clone(clone));
                        continue;
                    }

                    RMBattleBacks1_Collection rmbb1 = collection as RMBattleBacks1_Collection;
                    if (rmbb1 != null)
                    {
                        clone.Collections.Add(rmbb1.Clone(clone));
                        continue;
                    }

                    RMBattleBacks2_Collection rmbb2 = collection as RMBattleBacks2_Collection;
                    if (rmbb2 != null)
                    {
                        clone.Collections.Add(rmbb2.Clone(clone));
                        continue;
                    }

                    RMCharImageCollection rmchar = collection as RMCharImageCollection;
                    if (rmchar != null)
                    {
                        clone.Collections.Add(rmchar.Clone(clone));
                        continue;
                    }

                    RMParallaxCollection rmpar = collection as RMParallaxCollection;
                    if (rmpar != null)
                    {
                        clone.Collections.Add(rmpar.Clone(clone));
                        continue;
                    }

                    RMPictureCollection rmpic = collection as RMPictureCollection;
                    if (rmpic != null)
                    {
                        clone.Collections.Add(rmpic.Clone(clone));
                        continue;
                    }

                    RMSysImageCollection rmsys = collection as RMSysImageCollection;
                    if (rmsys != null)
                    {
                        clone.Collections.Add(rmsys.Clone(clone));
                        continue;
                    }

                    RMTilesetCollection rmtile = collection as RMTilesetCollection;
                    if (rmtile != null)
                    {
                        clone.Collections.Add(rmtile.Clone(clone));
                        continue;
                    }

                    RMTitles1_Collection rmtt1 = collection as RMTitles1_Collection;
                    if (rmtt1 != null)
                    {
                        clone.Collections.Add(rmtt1.Clone(clone));
                        continue;
                    }

                    RMTitles2_Collection rmtt2 = collection as RMTitles2_Collection;
                    if (rmtt2 != null)
                    {
                        clone.Collections.Add(rmtt2.Clone(clone));
                        continue;
                    }

                    RMMovieCollection rmmovie = collection as RMMovieCollection;
                    if (rmmovie != null)
                    {
                        clone.Collections.Add(rmmovie.Clone(clone));
                        continue;
                    }

                    RMPluginsCollection rmplug = collection as RMPluginsCollection;
                    if (rmplug != null)
                    {
                        clone.Collections.Add(rmplug.Clone(clone));
                        continue;
                    }
                }
            }
            clone.Description = Description;
            clone.Implicit = Implicit;
            clone.Installed = Installed;
            if (License != null)
                clone.License = License.Clone();
            clone.Name = Name;
            clone.UniqueID = UniqueID;
            clone.URL = URL;
            clone.Version = Version;
            clone.XMLLocation = XMLLocation;
            clone._md5OfUID = _md5OfUID;
            clone._uID = _uID;
            return clone;
        }

        public RMCollection CreateCollectionOfType(RMCollectionType collType, string _namespace, bool checkForExistence = false)
        {
            if (Collections == null)
                Collections = new List<RMCollection>();
            if (checkForExistence)
            {
                foreach (RMCollection collection in Collections)
                {
                    if (collection.GetRMCollectionType() == collType)
                    {
                        try
                        {
                            throw new CollectionAlreadyExistsException(ExceptionMessages.RMPackage.CollAlreadyExistsType(collType.ToLoggerPluralString()));
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(LoggerMessages.RMPackage.Error.CREATE_COLL_FAILED + collType.ToLoggerPluralString() + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                            throw;
                        }
                    }
                }
            }

            RMCollection newCollection = null;
            try
            {
                newCollection = collType.ToNewCollection(this);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(LoggerMessages.RMPackage.Error.CREATE_COLL_FAILED + collType.ToLoggerPluralString() + ".", _namespace, ex, BasicDebugLogger.DebugErrorType.Error);
                throw;
            }

            Collections.Add(newCollection);
            return newCollection;
        }


        public List<BoolAndRMFile> CheckFileExistences(string rootDir)
        {
            List<BoolAndRMFile> boolAndRMFileList = new List<BoolAndRMFile>();
            if (Collections != null && Collections.Count != 0)
            {
                foreach (RMCollection collection in Collections)
                {
                    List<BoolAndRMFile> outRes = collection.CheckFileExistences(rootDir);
                    if (outRes != null)
                        boolAndRMFileList.AddRange(outRes);
                }
            }

            return boolAndRMFileList;
        }

        public string TrimPrefixCommonPathOfFiles(out List<RMPackFile> retrievedFiles, bool includeLicenseFile)
        {
            retrievedFiles = RetrieveAllFiles();
            if (retrievedFiles == null)
                return null;

            List<string> pathsOnlyArray = retrievedFiles.RetrievePathsOnly(RMPackExtensions.RetrievePathMode.IgnoreNonRootedPath);

            if (pathsOnlyArray == null)
                return null;

            bool isAValidLicenseFileForTrim = includeLicenseFile && License != null && License.LicenseSource == RMPackLic.Source.File && !string.IsNullOrWhiteSpace(License.Data) && Path.IsPathRooted(License.Data);

            if (isAValidLicenseFileForTrim)
                pathsOnlyArray.Add(License.Data);


            if (pathsOnlyArray == null)
                return null;

            string commonPath = Helper.GetCommonPath(true, pathsOnlyArray);
            if (string.IsNullOrWhiteSpace(commonPath))
                return null;

            foreach (RMPackFile file in retrievedFiles)
            {
                if (string.IsNullOrWhiteSpace(file.Path) || file.NonRootedPath)
                    continue;

                string loweredPath = file.Path.ToLower();
                if (loweredPath.StartsWith(commonPath))
                    file.Path = Helper.GetRelativePath(file.Path, commonPath);
                //if (loweredPath.StartsWith(commonPath))
                //{
                //    file.Path = Helper.GetRelativePath(file.Path, commonPath);
                //    file.NonRootedPath = false;
                //}
            }

            if (isAValidLicenseFileForTrim)
            {
                string loweredPath = License.Data.ToLower();
                if (loweredPath.StartsWith(commonPath))
                    License.Data = Helper.GetRelativePath(License.Data, commonPath);
            }

            return commonPath;
        }

        public void RemoveGeneratorCollections()
        {
            if (Collections == null)
                return;

            List<RMGeneratorCollection> collectionsRetrieved = new List<RMGeneratorCollection>();
            foreach (RMCollection collection in Collections)
            {
                if (collection is RMGeneratorCollection)
                    collectionsRetrieved.Add(collection as RMGeneratorCollection);
            }

            foreach (RMGeneratorCollection collection in collectionsRetrieved)
            {
                Collections.Remove(collection);
            }
        }
        public string ToString(bool @explicit)
        {
            return ToXMLElement(@explicit).ToString();
        }
    }



}
