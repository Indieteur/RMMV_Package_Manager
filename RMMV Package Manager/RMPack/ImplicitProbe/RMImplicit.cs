using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace RMMV_PackMan
{
    static partial class RMImplicit
    {
        public static void RetrievePackFromDir(string rootPath, string _namespace, bool trimRootPath, out LogDataList log, ref RMPackage package, bool skipGenParts = false)
        {
            log = new LogDataList();
            rootPath = rootPath.ToLower();

            string appendedPath = rootPath + "\\" + RMPConstants.LowCaseDirectoryNames.GENERATOR;



            LogDataList outLog = null;
            if (!skipGenParts && Directory.Exists(appendedPath))
            {
                RMCollection collectionToAdd = GeneratorProbe.RetrieveGeneratorCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, package);
                if (collectionToAdd != null)
                    package.Collections.Add(collectionToAdd);
                log.AppendLogs(outLog);
            }


            appendedPath = rootPath + "\\" + RMPConstants.LowCaseDirectoryNames.AUDIO;
            if (Directory.Exists(appendedPath))
            {
                string newRootPath = appendedPath;
                appendedPath += "\\" + RMPConstants.LowCaseDirectoryNames.AUDIO_BGM;
                if (Directory.Exists(appendedPath))
                {
                    RMCollection collectionToAdd = AudioProbe.RetrieveAudioCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMAudioCollection.AudioType.BGM, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.AUDIO_BGS;
                if (Directory.Exists(appendedPath))
                {
                    RMCollection collectionToAdd = AudioProbe.RetrieveAudioCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMAudioCollection.AudioType.BGS, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.AUDIO_ME;
                if (Directory.Exists(appendedPath))
                {
                    RMCollection collectionToAdd = AudioProbe.RetrieveAudioCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMAudioCollection.AudioType.ME, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.AUDIO_SE;
                if (Directory.Exists(appendedPath))
                {
                    RMCollection collectionToAdd = AudioProbe.RetrieveAudioCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMAudioCollection.AudioType.SE, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }
            }
            appendedPath = rootPath + "\\" + RMPConstants.LowCaseDirectoryNames.DATA;
            if (Directory.Exists(appendedPath))
            {
                RMCollection collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Data, package);
                if (collectionToAdd != null)
                    package.Collections.Add(collectionToAdd);
                log.AppendLogs(outLog);
            }
            appendedPath = rootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG;
            if (Directory.Exists(appendedPath))
            {

                RMCollection collectionToAdd = CharacterProbe.RetrieveCharacterImages(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, package);
                if (collectionToAdd != null)
                    package.Collections.Add(collectionToAdd);
                log.AppendLogs(outLog);

                string newRootPath = appendedPath;
                appendedPath += "\\" + RMPConstants.LowCaseDirectoryNames.IMG_ANIM;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Animation, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_BATTLEBACKS_1;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.BattleBacks_1, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_BATTLEBACKS_2;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.BattleBacks_2, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_PARALLAXES;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Parallax, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_PICTURES;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Pictures, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_SYSTEM;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.System, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_TITLES_1;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Titles_1, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_TITLES_2;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Titles_2, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }

                appendedPath = newRootPath + "\\" + RMPConstants.LowCaseDirectoryNames.IMG_TILESETS;
                if (Directory.Exists(appendedPath))
                {
                    collectionToAdd = TilesetProbe.RetrieveTilesetCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, package);
                    if (collectionToAdd != null)
                        package.Collections.Add(collectionToAdd);
                    log.AppendLogs(outLog);
                }
            }

            appendedPath = rootPath + "\\" + RMPConstants.LowCaseDirectoryNames.PLUGINS;
            if (Directory.Exists(appendedPath))
            {
                RMCollection collectionToAdd = SingleFileCollectionProbe.RetrieveSingleFileCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, RMSingleFileCollection.CollectionType.Plugins, package);
                if (collectionToAdd != null)
                    package.Collections.Add(collectionToAdd);
                log.AppendLogs(outLog);
            }

            appendedPath = rootPath + "\\" + RMPConstants.LowCaseDirectoryNames.MOVIES;
            if (Directory.Exists(appendedPath))
            {
                RMCollection collectionToAdd = MovieProbe.RetrieveMovieCollection(appendedPath.ToLower(), rootPath, _namespace, trimRootPath, out outLog, package);
                if (collectionToAdd != null)
                    package.Collections.Add(collectionToAdd);
                log.AppendLogs(outLog);
            }

        }


    }

}

