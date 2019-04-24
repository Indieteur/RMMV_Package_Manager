using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMMovieExt
    {
        public static RMMovieFile.FileType ParseString(this RMMovieFile.FileType type, string strToParse)
        {
            switch (strToParse)
            {
                case RMPConstants.MovieFileType.MP4:
                    return RMMovieFile.FileType.mp4;
                case RMPConstants.MovieFileType.WEBM:
                    return RMMovieFile.FileType.webm;
                default:
                    return RMMovieFile.FileType.none;
            }
        }
        public static string ToXMLString(this RMMovieFile.FileType fileType)
        {
            switch (fileType)
            {
                case RMMovieFile.FileType.mp4:
                    return RMPConstants.MovieFileType.MP4;
                case RMMovieFile.FileType.webm:
                    return RMPConstants.MovieFileType.WEBM;
                default:
                    return string.Empty;
            }
        }
    }
}
