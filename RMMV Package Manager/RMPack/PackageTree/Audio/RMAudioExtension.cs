using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMAudioExtension
    {
        public static RMCollectionType ToRMCollectionType(this RMAudioCollection.AudioType audioType)
        {
            switch (audioType)
            {
                case RMAudioCollection.AudioType.BGM:
                    return RMCollectionType.BGM;
                case RMAudioCollection.AudioType.BGS:
                    return RMCollectionType.BGS;
                case RMAudioCollection.AudioType.ME:
                    return RMCollectionType.ME;
                default:
                    return RMCollectionType.SE;

            }
            
        }

        public static string ToXMLString(this RMAudioCollection.AudioType audioType)
        {
            switch (audioType)
            {
                case RMAudioCollection.AudioType.BGM:
                    return RMPConstants.Collections.BGM;
                case RMAudioCollection.AudioType.BGS:
                    return RMPConstants.Collections.BGS;
                case RMAudioCollection.AudioType.ME:
                    return RMPConstants.Collections.ME;
                case RMAudioCollection.AudioType.SE:
                    return RMPConstants.Collections.SE;
                default:
                    return string.Empty;
            }
        }
        public static string ToDirectoryName(this RMAudioCollection.AudioType audioType)
        {
            switch (audioType)
            {
                case RMAudioCollection.AudioType.BGM:
                    return DirectoryNames.Audio.BGM;
                case RMAudioCollection.AudioType.BGS:
                    return DirectoryNames.Audio.BGS;
                case RMAudioCollection.AudioType.ME:
                    return DirectoryNames.Audio.ME;
                case RMAudioCollection.AudioType.SE:
                    return DirectoryNames.Audio.SE;
                default:
                    return string.Empty;
            }
        }

        public static RMAudioFile.FileType ParseString(this RMAudioFile.FileType fileType, string strToParse)
        {
            switch (strToParse)
            {
                case RMPConstants.AudioFileType.M4A:
                    return RMAudioFile.FileType.m4a;
                case RMPConstants.AudioFileType.MP3:
                    return RMAudioFile.FileType.mp3;
                case RMPConstants.AudioFileType.OGG:
                    return RMAudioFile.FileType.ogg;
                case RMPConstants.AudioFileType.WAV:
                    return RMAudioFile.FileType.wav;
                default:
                    return RMAudioFile.FileType.none;
            }
        }

        public static string ToExtensionString(this RMAudioFile.FileType fileType)
        {
            switch (fileType)
            {
                case RMAudioFile.FileType.m4a:
                    return RMPConstants.AudioFileType.M4A;
                case RMAudioFile.FileType.mp3:
                    return RMPConstants.AudioFileType.MP3;
                case RMAudioFile.FileType.ogg:
                    return RMPConstants.AudioFileType.OGG;
                case RMAudioFile.FileType.wav:
                    return RMPConstants.AudioFileType.WAV;
                default:
                    return string.Empty;
            }
        }
    }
}
