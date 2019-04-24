using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class Extensions
    {
        static char[] CachedInvalidFilenameChars { get; set; } = Path.GetInvalidFileNameChars();
        public static bool HasInvalidFileNameCharacters(this string filename)
        {
            return (!string.IsNullOrWhiteSpace(filename) && filename.IndexOfAny(CachedInvalidFilenameChars) >= 0);
        }
        public static bool IsAValidURL(this string stringToCheck)
        {
            if (string.IsNullOrWhiteSpace(stringToCheck))
                return false;
            Uri result;
            return (Uri.TryCreate(stringToCheck, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps));
        }

        public static void AddSafely(this List<InstalledPackage> installedPackages, InstalledPackage elementToAdd)
        {
            if (installedPackages == null)
                installedPackages = new List<InstalledPackage>();
            if (elementToAdd != null)
                installedPackages.Add(elementToAdd);
        }

        public static string ParseXMLString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length <= 1)
                return str;

            char[] charArrayOfStr = str.ToCharArray();

            StringBuilder sb = new StringBuilder(charArrayOfStr.Length);

            for (int i = 0; i < charArrayOfStr.Length; ++i)
            {
                char curChar = charArrayOfStr[i];
                bool validNextChar = (i < charArrayOfStr.Length - 1);
                char nextChar = (validNextChar) ? charArrayOfStr[i + 1] : 'A';

                if (curChar == '\\' && validNextChar)
                {
                    if (nextChar == 'r')
                    {
                        //skip
                    }
                    else if (nextChar == 'n')
                        sb.Append(Environment.NewLine);
                    else
                        sb.Append(nextChar);
                    ++i;

                }
                else
                    sb.Append(curChar);
            }

            return sb.ToString();
        }

        public static string ToXMLString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length <= 1)
                return str;

            char[] charArrayOfStr = str.ToCharArray();

            StringBuilder sb = new StringBuilder(charArrayOfStr.Length);
            for (int i = 0; i < charArrayOfStr.Length; ++i)
            {
                char curChar = charArrayOfStr[i];

                if (curChar == '\r')
                    continue;
                else if (curChar == '\n')
                    sb.Append("\\n");
                else if (curChar == '\\')
                    sb.Append("\\\\");
                else
                    sb.Append(curChar);
            }

            return sb.ToString();
        }

        public static bool HasNonExistingFile(this IEnumerable<BoolAndRMFile> listOfBoolAndRM)
        {
            if (listOfBoolAndRM == null || listOfBoolAndRM.Count() == 0)
                return false;

            foreach (BoolAndRMFile boolAndRMFile in listOfBoolAndRM)
                if (!boolAndRMFile.Boolean)
                    return true;
            return false;
        }

        public static LogDataList MakeLogSet(this IEnumerable<BoolAndRMFile> listOfBoolAndRM, string prefixStr, string _namespace, string rootDir = null)
        {
            if (listOfBoolAndRM == null || listOfBoolAndRM.Count() == 0)
                return null;
            LogDataList log = new LogDataList();
            string resultingText = (string.IsNullOrWhiteSpace(prefixStr)) ? "" : prefixStr;
            resultingText += LoggerMessages.Extension.MISS_FILE;
            if (!string.IsNullOrWhiteSpace(rootDir))
                resultingText += rootDir + "\\";
            foreach (BoolAndRMFile boolAndRMFile in listOfBoolAndRM)
                if (!boolAndRMFile.Boolean)
                    log.WriteErrorLog(resultingText + boolAndRMFile.PackFile.Path + LoggerMessages.Extension.MISS_FILE_1, _namespace, null);
            return log;
        }

        public static IntAndString GetItemWithMaxIntAndOrStrLen(this IEnumerable<IntAndString> list)
        {
            if (list == null)
                return null;
            IntAndString maxVal = null;
            foreach (IntAndString item in list)
            {
                if (maxVal == null)
                    maxVal = item;
                else
                {
                    if (item.Integer > maxVal.Integer)
                    {
                        maxVal = item;
                    }
                    else if (item.Integer == maxVal.Integer && item.String.Length > maxVal.String.Length)
                    {
                        maxVal = item;
                    }
                }
            }
            if (maxVal != null)
                return maxVal;
            else
                return null;
        }
    }
}
