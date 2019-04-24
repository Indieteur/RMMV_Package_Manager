using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMGenExtension
    {

       public static string ToLogString(this RMGenFile.GenFileFields whichFields)
        {
           
            if (whichFields == RMGenFile.GenFileFields.None)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool hasStarted = false;
            if (whichFields.HasFlag(RMGenFile.GenFileFields.BaseOrder))
            {
                sb.Append(RMPConstants.GenFileFields.BASE_ORDER);
                hasStarted = true;
            }

            if (whichFields.HasFlag(RMGenFile.GenFileFields.Colour))
            {
                if (hasStarted)
                    sb.Append(", ");
                sb.Append(RMPConstants.GenFileFields.COLOUR);
                hasStarted = true;
            }

            if (whichFields.HasFlag(RMGenFile.GenFileFields.GenFileType))
            {
                if (hasStarted)
                    sb.Append(", ");
                sb.Append(RMPConstants.GenFileFields.GEN_FILE_TYPE);
                hasStarted = true;
            }

            if (whichFields.HasFlag(RMGenFile.GenFileFields.GenPartType))
            {
                if (hasStarted)
                    sb.Append(", ");
                sb.Append(RMPConstants.GenFileFields.GEN_PART_TYPE);
                hasStarted = true;
            }

            if (whichFields.HasFlag(RMGenFile.GenFileFields.Order))
            {
                if (hasStarted)
                    sb.Append(", ");
                sb.Append(RMPConstants.GenFileFields.ORDER);
                hasStarted = true;
            }

            if (whichFields.HasFlag(RMGenFile.GenFileFields.Position))
            {
                if (hasStarted)
                    sb.Append(", ");
                sb.Append(RMPConstants.GenFileFields.POSITION);
                hasStarted = true;
            }
            return sb.ToString();
        }

        public static  RMGenPart.GenPartType ParseXMLString (this RMGenPart.GenPartType partType, string stringToParse)
        {
            switch (stringToParse)
            {
                case RMPConstants.GenPartsType.BEARD:
                    return RMGenPart.GenPartType.Beard;
                case RMPConstants.GenPartsType.ACCESSORY_A:
                    return RMGenPart.GenPartType.Accessory_A;
                case RMPConstants.GenPartsType.ACCESSORY_B:
                    return RMGenPart.GenPartType.Accessory_B;
                case RMPConstants.GenPartsType.BEAST_EARS:
                    return RMGenPart.GenPartType.Beast_Ears;
                case RMPConstants.GenPartsType.BODY:
                    return RMGenPart.GenPartType.Body;
                case RMPConstants.GenPartsType.CLOAK:
                    return RMGenPart.GenPartType.Cloak;
                case RMPConstants.GenPartsType.CLOTHING:
                    return RMGenPart.GenPartType.Clothing;
                case RMPConstants.GenPartsType.EARS:
                    return RMGenPart.GenPartType.Ears;
                case RMPConstants.GenPartsType.EYEBROWS:
                    return RMGenPart.GenPartType.Eyebrows;
                case RMPConstants.GenPartsType.EYES:
                    return RMGenPart.GenPartType.Eyes;
                case RMPConstants.GenPartsType.FACE:
                    return RMGenPart.GenPartType.Face;
                case RMPConstants.GenPartsType.FACIAL_MARK:
                    return RMGenPart.GenPartType.Facial_Mark;
                case RMPConstants.GenPartsType.FRONT_HAIR:
                    return RMGenPart.GenPartType.Front_Hair;
                case RMPConstants.GenPartsType.GLASSES:
                    return RMGenPart.GenPartType.Glasses;
                case RMPConstants.GenPartsType.MOUTH:
                    return RMGenPart.GenPartType.Mouth;
                case RMPConstants.GenPartsType.NOSE:
                    return RMGenPart.GenPartType.Nose;
                case RMPConstants.GenPartsType.REAR_HAIR:
                    return RMGenPart.GenPartType.Rear_Hair;
                case RMPConstants.GenPartsType.TAIL:
                    return RMGenPart.GenPartType.Tail;
                case RMPConstants.GenPartsType.WING:
                    return RMGenPart.GenPartType.Wing;
            }
            return RMGenPart.GenPartType.None;
        }

        public static RMGenPart.eGender ParseXMLString(this RMGenPart.eGender gender, string strToParse)
        {
            string[] genders = strToParse.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (genders.Length == 0)
                return RMGenPart.eGender.None;
            RMGenPart.eGender returnVal = RMGenPart.eGender.None;
            for (int i = 0; i < genders.Length; i++)
            {
                string tempString = genders[i].Trim();
                if (tempString == RMPConstants.GenGenders.FEMALE)
                    returnVal = returnVal | RMGenPart.eGender.Female;
                else if (tempString == RMPConstants.GenGenders.KID)
                    returnVal = returnVal | RMGenPart.eGender.Kid;
                else if (tempString == RMPConstants.GenGenders.MALE)
                    returnVal = returnVal | RMGenPart.eGender.Male;
            }
            return returnVal;

        }

        public static string ToProperName(this RMGenPart.GenPartType genPartType)
        {
            switch (genPartType)
            {
                case RMGenPart.GenPartType.Accessory_A:
                    return RMPConstants.GenPartsTypeProperName.ACCESSORY_A;
                case RMGenPart.GenPartType.Accessory_B:
                    return RMPConstants.GenPartsTypeProperName.ACCESSORY_B;
                case RMGenPart.GenPartType.Beard:
                    return RMPConstants.GenPartsTypeProperName.BEARD;
                case RMGenPart.GenPartType.Beast_Ears:
                    return RMPConstants.GenPartsTypeProperName.BEAST_EARS;
                case RMGenPart.GenPartType.Body:
                    return RMPConstants.GenPartsTypeProperName.BODY;
                case RMGenPart.GenPartType.Cloak:
                    return RMPConstants.GenPartsTypeProperName.CLOAK;
                case RMGenPart.GenPartType.Clothing:
                    return RMPConstants.GenPartsTypeProperName.CLOTHING;
                case RMGenPart.GenPartType.Ears:
                    return RMPConstants.GenPartsTypeProperName.EARS;
                case RMGenPart.GenPartType.Eyebrows:
                    return RMPConstants.GenPartsTypeProperName.EYEBROWS;
                case RMGenPart.GenPartType.Eyes:
                    return RMPConstants.GenPartsTypeProperName.EYES;
                case RMGenPart.GenPartType.Face:
                    return RMPConstants.GenPartsTypeProperName.FACE;
                case RMGenPart.GenPartType.Facial_Mark:
                    return RMPConstants.GenPartsTypeProperName.FACIAL_MARK;
                case RMGenPart.GenPartType.Front_Hair:
                    return RMPConstants.GenPartsTypeProperName.FRONT_HAIR;
                case RMGenPart.GenPartType.Glasses:
                    return RMPConstants.GenPartsTypeProperName.GLASSES;
                case RMGenPart.GenPartType.Mouth:
                    return RMPConstants.GenPartsTypeProperName.MOUTH;
                case RMGenPart.GenPartType.Nose:
                    return RMPConstants.GenPartsTypeProperName.NOSE;
                case RMGenPart.GenPartType.Rear_Hair:
                    return RMPConstants.GenPartsTypeProperName.REAR_HAIR;
                case RMGenPart.GenPartType.Tail:
                    return RMPConstants.GenPartsTypeProperName.TAIL;
                case RMGenPart.GenPartType.Wing:
                    return RMPConstants.GenPartsTypeProperName.WING;
                default:
                    return string.Empty;

            }
        }

        public static string ToParsableXMLString(this RMGenPart.GenPartType genPartType)
        {
            switch (genPartType)
            {
                case RMGenPart.GenPartType.Accessory_A:
                    return RMPConstants.GenPartsType.ACCESSORY_A;
                case RMGenPart.GenPartType.Accessory_B:
                    return RMPConstants.GenPartsType.ACCESSORY_B;
                case RMGenPart.GenPartType.Beard:
                    return RMPConstants.GenPartsType.BEARD;
                case RMGenPart.GenPartType.Beast_Ears:
                    return RMPConstants.GenPartsType.BEAST_EARS;
                case RMGenPart.GenPartType.Body:
                    return RMPConstants.GenPartsType.BODY;
                case RMGenPart.GenPartType.Cloak:
                    return RMPConstants.GenPartsType.CLOAK;
                case RMGenPart.GenPartType.Clothing:
                    return RMPConstants.GenPartsType.CLOTHING;
                case RMGenPart.GenPartType.Ears:
                    return RMPConstants.GenPartsType.EARS;
                case RMGenPart.GenPartType.Eyebrows:
                    return RMPConstants.GenPartsType.EYEBROWS;
                case RMGenPart.GenPartType.Eyes:
                    return RMPConstants.GenPartsType.EYES;
                case RMGenPart.GenPartType.Face:
                    return RMPConstants.GenPartsType.FACE;
                case RMGenPart.GenPartType.Facial_Mark:
                    return RMPConstants.GenPartsType.FACIAL_MARK;
                case RMGenPart.GenPartType.Front_Hair:
                    return RMPConstants.GenPartsType.FRONT_HAIR;
                case RMGenPart.GenPartType.Glasses:
                    return RMPConstants.GenPartsType.GLASSES;
                case RMGenPart.GenPartType.Mouth:
                    return RMPConstants.GenPartsType.MOUTH;
                case RMGenPart.GenPartType.Nose:
                    return RMPConstants.GenPartsType.NOSE;
                case RMGenPart.GenPartType.Rear_Hair:
                    return RMPConstants.GenPartsType.REAR_HAIR;
                case RMGenPart.GenPartType.Tail:
                    return RMPConstants.GenPartsType.TAIL;
                case RMGenPart.GenPartType.Wing:
                    return RMPConstants.GenPartsType.WING;
                default:
                    return string.Empty;

            }
        }

        public static RMGenPart.GenPartType ParseLowerCaseString(this RMGenPart.GenPartType part, string strToCheck)
        {
            switch (strToCheck.ToLower())
            {
                case RMPConstants.GenPartsTypeFileName.ACCESSORY_A:
                    return RMGenPart.GenPartType.Accessory_A;
                case RMPConstants.GenPartsTypeFileName.ACCESSORY_B:
                    return RMGenPart.GenPartType.Accessory_B;
                case RMPConstants.GenPartsTypeFileName.BEARD:
                    return RMGenPart.GenPartType.Beard;
                case RMPConstants.GenPartsTypeFileName.BEAST_EARS:
                    return RMGenPart.GenPartType.Beast_Ears;
                case RMPConstants.GenPartsTypeFileName.BODY:
                    return RMGenPart.GenPartType.Body;
                case RMPConstants.GenPartsTypeFileName.CLOAK:
                    return RMGenPart.GenPartType.Cloak;
                case RMPConstants.GenPartsTypeFileName.CLOTHING:
                    return RMGenPart.GenPartType.Clothing;
                case RMPConstants.GenPartsTypeFileName.EARS:
                    return RMGenPart.GenPartType.Ears;
                case RMPConstants.GenPartsTypeFileName.EYEBROWS:
                    return RMGenPart.GenPartType.Eyebrows;
                case RMPConstants.GenPartsTypeFileName.EYES:
                    return RMGenPart.GenPartType.Eyes;
                case RMPConstants.GenPartsTypeFileName.FACE:
                    return RMGenPart.GenPartType.Face;
                case RMPConstants.GenPartsTypeFileName.FACIAL_MARK:
                    return RMGenPart.GenPartType.Facial_Mark;
                case RMPConstants.GenPartsTypeFileName.FRONT_HAIR:
                    return RMGenPart.GenPartType.Front_Hair;
                case RMPConstants.GenPartsTypeFileName.GLASSES:
                    return RMGenPart.GenPartType.Glasses;
                case RMPConstants.GenPartsTypeFileName.MOUTH:
                    return RMGenPart.GenPartType.Mouth;
                case RMPConstants.GenPartsTypeFileName.NOSE:
                    return RMGenPart.GenPartType.Nose;
                case RMPConstants.GenPartsTypeFileName.REAR_HAIR:
                    return RMGenPart.GenPartType.Rear_Hair;
                case RMPConstants.GenPartsTypeFileName.TAIL:
                    return RMGenPart.GenPartType.Tail;
                case RMPConstants.GenPartsTypeFileName.WING:
                    return RMGenPart.GenPartType.Wing;
                default:
                    return RMGenPart.GenPartType.None;
            }
        }


        public static bool IsACFile(this RMGenFile.GenFileType fileType)
        {
            if (fileType == RMGenFile.GenFileType.SV_C || fileType == RMGenFile.GenFileType.TVD_C || fileType == RMGenFile.GenFileType.TV_C)
                return true;
            return false;
        }

        public static string ToFilePrefix(this RMGenFile.GenFileType fileType)
        {
            switch (fileType)
            {
                case RMGenFile.GenFileType.Face:
                    return RMPConstants.GenFileNamePrefixANDSuffix.FACE;
                case RMGenFile.GenFileType.SV:
                    return RMPConstants.GenFileNamePrefixANDSuffix.SV;
                case RMGenFile.GenFileType.SV_C:
                    return RMPConstants.GenFileNamePrefixANDSuffix.SV;
                case RMGenFile.GenFileType.TV:
                    return RMPConstants.GenFileNamePrefixANDSuffix.TV;
                case RMGenFile.GenFileType.TVD:
                    return RMPConstants.GenFileNamePrefixANDSuffix.TVD;
                case RMGenFile.GenFileType.TVD_C:
                    return RMPConstants.GenFileNamePrefixANDSuffix.TVD;
                case RMGenFile.GenFileType.TV_C:
                    return RMPConstants.GenFileNamePrefixANDSuffix.TV;
                case RMGenFile.GenFileType.Var:
                    return RMPConstants.GenFileNamePrefixANDSuffix.VARIATION;
                default:
                    return string.Empty;

            }
        }

        public static string ToParsableString(this RMGenFile.GenFileType fileType)
        {
            switch (fileType)
            {
                case RMGenFile.GenFileType.Face:
                    return RMPConstants.GenFileTypes.FACE;
                case RMGenFile.GenFileType.SV:
                    return RMPConstants.GenFileTypes.SV;
                case RMGenFile.GenFileType.SV_C:
                    return RMPConstants.GenFileTypes.SV_C;
                case RMGenFile.GenFileType.TV:
                    return RMPConstants.GenFileTypes.TV;
                case RMGenFile.GenFileType.TVD:
                    return RMPConstants.GenFileTypes.TVD;
                case RMGenFile.GenFileType.TVD_C:
                    return RMPConstants.GenFileTypes.TVD_C;
                case RMGenFile.GenFileType.TV_C:
                    return RMPConstants.GenFileTypes.TV_C;
                case RMGenFile.GenFileType.Var:
                    return RMPConstants.GenFileTypes.VARIATION;
                default:
                    return string.Empty;

            }
        }

        public static string GetContainingDirectoryName(this RMGenFile.GenFileType fileType)
        {
            switch (fileType)
            {
                case RMGenFile.GenFileType.Face:
                    return DirectoryNames.Generator.FACE;
                case RMGenFile.GenFileType.SV:
                    return DirectoryNames.Generator.SV;
                case RMGenFile.GenFileType.SV_C:
                    return DirectoryNames.Generator.SV;
                case RMGenFile.GenFileType.TV:
                    return DirectoryNames.Generator.TV;
                case RMGenFile.GenFileType.TVD:
                    return DirectoryNames.Generator.TVD;
                case RMGenFile.GenFileType.TVD_C:
                    return DirectoryNames.Generator.TVD;
                case RMGenFile.GenFileType.TV_C:
                    return DirectoryNames.Generator.TV;
                case RMGenFile.GenFileType.Var:
                    return DirectoryNames.Generator.VARIATION;
                default:
                    return string.Empty;

            }
        }

        public static string ToParsableXMLString(this RMGenPart.eGender genders)
        {
            StringBuilder sb = new StringBuilder();
            bool alreadyStarted = false;
            if (genders != RMGenPart.eGender.None)
            {
                if (genders.HasFlag(RMGenPart.eGender.Female))
                {
                    sb.Append(RMPConstants.GenGenders.FEMALE);
                    alreadyStarted = true;
                }
                if (genders.HasFlag(RMGenPart.eGender.Male))
                {
                    if (alreadyStarted)
                        sb.Append("|" + RMPConstants.GenGenders.MALE);
                    else
                    {
                        sb.Append(RMPConstants.GenGenders.MALE);
                        alreadyStarted = true;
                    }
                }
                if (genders.HasFlag(RMGenPart.eGender.Kid))
                {
                    if (alreadyStarted)
                        sb.Append("|" + RMPConstants.GenGenders.KID);
                    else
                    {
                        sb.Append(RMPConstants.GenGenders.KID);
                        alreadyStarted = true;
                    }

                }
            }
            return sb.ToString();

        }

        public static string ToProperName(this RMGenPart.eGender genders)
        {
            switch (genders)
            {
                case RMGenPart.eGender.Female:
                    return RMPConstants.GenGendersProperName.FEMALE;
                case RMGenPart.eGender.Kid:
                    return RMPConstants.GenGendersProperName.KID;
                case RMGenPart.eGender.Male:
                    return RMPConstants.GenGendersProperName.MALE;
                default:
                    return string.Empty;
            }
        }

        public static string GetContainingDirectoryName(this RMGenPart.eGender genders)
        {
            switch (genders)
            {
                case RMGenPart.eGender.Female:
                    return DirectoryNames.Generator.FEMALE;
                case RMGenPart.eGender.Kid:
                    return DirectoryNames.Generator.KID;
                case RMGenPart.eGender.Male:
                    return DirectoryNames.Generator.MALE;
                default:
                    return string.Empty;
            }
        }
    }
}
