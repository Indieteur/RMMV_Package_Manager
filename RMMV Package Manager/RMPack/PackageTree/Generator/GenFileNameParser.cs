using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public class TempGenFileNameParsed
    {

        public RMGenPart.GenPartType Part = RMGenPart.GenPartType.None;
        public int BaseOrder = 0;
        public int Position = 0;
        public int Order = 0;
        public int Colour = 0;
        public bool IsAcFile = false;

        RMGenFile.FileNameInfo curPos;
        StringBuilder curString;
        public TempGenFileNameParsed(string fileName, int startAt)
        {
            curPos = RMGenFile.FileNameInfo.Type;
            char[] characters = fileName.ToCharArray();
            curString = new StringBuilder();
            for (int i = startAt; i < characters.Length; ++i)
            {
                char charCache = characters[i];
                if (charCache == RMPConstants.GenFileNamePrefixANDSuffix.SEPARATOR)
                {
                    DoParse();
                    if (curPos == RMGenFile.FileNameInfo.Type)
                        curPos = RMGenFile.FileNameInfo.Position;
                    continue;

                }
                else if (charCache == '.')
                {
                    DoParse();
                    break;
                }
                else if (curPos == RMGenFile.FileNameInfo.Type && char.IsNumber(charCache))
                {
                    DoParse();
                    curPos = RMGenFile.FileNameInfo.BaseOrder;
                }
                else if (curPos == RMGenFile.FileNameInfo.Position && charCache == RMPConstants.GenFileNamePrefixANDSuffix.POSITION)
                    continue;
                else if (curPos == RMGenFile.FileNameInfo.Waiting)
                {
                    if (charCache == RMPConstants.GenFileNamePrefixANDSuffix.COLOUR)
                    {
                        curPos = RMGenFile.FileNameInfo.Colour;
                        continue;
                    }
                    else if (charCache == RMPConstants.GenFileNamePrefixANDSuffix.ORDER)
                    {
                        curPos = RMGenFile.FileNameInfo.Order;
                        continue;
                    }
                }
                curString.Append(charCache);
            }
        }

        void DoParse()
        {
            string tString = curString.ToString();
            if (curPos == RMGenFile.FileNameInfo.Type)
                Part = Part.ParseLowerCaseString(tString);
            else if (curPos == RMGenFile.FileNameInfo.BaseOrder)
            {
                int tInt;
                if (int.TryParse(tString, out tInt))
                    BaseOrder = tInt;
                curPos = RMGenFile.FileNameInfo.Position;
            }
            else if (curPos == RMGenFile.FileNameInfo.Position)
            {
                int tInt;
                if (int.TryParse(tString, out tInt))
                    Position = tInt;
                curPos = RMGenFile.FileNameInfo.Waiting;
            }
            else if (curPos == RMGenFile.FileNameInfo.Order)
            {
                int tInt;
                if (int.TryParse(tString, out tInt))
                    Order = tInt;
                else
                    IsAcFile = true;
                curPos = RMGenFile.FileNameInfo.Waiting;
            }
            else if (curPos == RMGenFile.FileNameInfo.Colour)
            {
                int tInt;
                if (int.TryParse(tString, out tInt))
                    Colour = tInt;
                curPos = RMGenFile.FileNameInfo.Waiting;
            }
            curString = new StringBuilder();
        }

        public static RMGenFile.GenFileType RetrieveType(string fileName, out int startPosOfParse)
        {
            if (fileName.StartsWith(RMPConstants.GenFileNamePrefixANDSuffix.FACE_LOWER + "_"))
            {
                startPosOfParse = 3;
                return RMGenFile.GenFileType.Face;
            }
            else if (fileName.StartsWith(RMPConstants.GenFileNamePrefixANDSuffix.SV_LOWER + "_"))
            {
                startPosOfParse = 3;
                return RMGenFile.GenFileType.SV;
            }
            else if (fileName.StartsWith(RMPConstants.GenFileNamePrefixANDSuffix.TVD_LOWER + "_"))
            {
                startPosOfParse = 3;
                return RMGenFile.GenFileType.TVD;
            }
            else if (fileName.StartsWith(RMPConstants.GenFileNamePrefixANDSuffix.TV_LOWER + "_"))
            {
                startPosOfParse = 3;
                return RMGenFile.GenFileType.TV;
            }
            else if (fileName.StartsWith(RMPConstants.GenFileNamePrefixANDSuffix.VARIATION + "_"))
            {
                startPosOfParse = 5;
                return RMGenFile.GenFileType.Var;
            }
            startPosOfParse = 0;
            return RMGenFile.GenFileType.None;
        }

    }
}
