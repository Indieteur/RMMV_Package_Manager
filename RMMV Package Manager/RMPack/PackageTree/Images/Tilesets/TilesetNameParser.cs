using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    struct TilesetTypeAndName
    {
        public string internalName;
        public string Name;
        public RMTilesetFile.eAtlasType atlasType;
        public TilesetTypeAndName(string fileName)
        {
            atlasType = RMTilesetFile.eAtlasType.NotSpecified;
            string upperCaseName = fileName.ToUpper();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                internalName = null;
                Name = null;
            }
            char[] arrayOfChar = upperCaseName.ToCharArray();
            StringBuilder sb = new StringBuilder();
            bool hasRetrievedAtlasType = false;
            int endOfName = arrayOfChar.Length;
            for (int i = arrayOfChar.Length - 1; i >= 0; i--)
            {
                char workChar = arrayOfChar[i];
                if (!hasRetrievedAtlasType && workChar == '_')
                {
                    atlasType = atlasType.ParseString(Reverse(sb.ToString()));
                    hasRetrievedAtlasType = true;
                    endOfName = i;
                    sb = new StringBuilder();
                    continue;
                }
                sb.Append(workChar);
            }
            internalName = Reverse(sb.ToString()).ToLower();

            Name = fileName.Substring(0, endOfName);
        }

        static string Reverse(string str) //https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
