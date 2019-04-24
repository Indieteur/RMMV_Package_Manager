using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.SimpleDatabaseFormat;

namespace RMMV_PackMan
{
    static partial class GeneratorPartsManager
    {
        public static class Precision
        {
            static List<Config> PartsWithNonDefaultFloor = new List<Config>();
            static List<Config> SpecialNumberedParts = new List<Config>();

            public static void LoadPartsWithNonDefaultFloorSDF(string path)
            {
                SimpleDatabase sdf = new SimpleDatabase(path);
                PartsWithNonDefaultFloor.Clear();
                if (sdf.SimpleDataList != null)
                {
                    foreach(SimpleDatabaseTokenList tokenList in sdf.SimpleDataList)
                    {
                        PartsWithNonDefaultFloor.Add(new Config(tokenList));
                    }
                }
            }

            public static void LoadSpecialNumberedPartsSDF(string path)
            {
                SimpleDatabase sdf = new SimpleDatabase(path);
                SpecialNumberedParts.Clear();
                if (sdf.SimpleDataList != null)
                {
                    foreach (SimpleDatabaseTokenList tokenList in sdf.SimpleDataList)
                    {
                        SpecialNumberedParts.Add(new Config(tokenList));
                    }
                }
            }

            public static int GetPositionFloorOfPart(RMGenPart.eGender gender, RMGenPart.GenPartType partType)
            {
                Config cfg = Config.RetrieveConfigForPartFromList(PartsWithNonDefaultFloor, gender, partType);
                if (cfg != null)
                    return cfg.Integer;
                return 1;
            }

            public static bool ShouldSkipPosition(RMGenPart.eGender gender, RMGenPart.GenPartType partType, int position)
            {
                if (Config.RetrieveConfigForPartFromList(SpecialNumberedParts, gender, partType, position) != null)
                    return true;
                return false;
            }
            public static int GetNextVacantPosition(RMGenPart.eGender gender, RMGenPart.GenPartType partType, int curPosition)
            {
                string pathToVar = PMFileSystem.Generator.Variation.Root + "\\" + gender.GetContainingDirectoryName();
                string partToCount = pathToVar + "\\" + RMPConstants.GenFileNamePrefixANDSuffix.VARIATION + "_" + partType.ToParsableXMLString();


                Config cfgWithLowestInt = Config.GetConfigFileWithLowestInt(SpecialNumberedParts, gender, partType, curPosition);
                bool nextIntAlreadyChecked = false;
                int nextInt = curPosition + 1;
                if (cfgWithLowestInt == null || cfgWithLowestInt.Integer > nextInt)
                    nextIntAlreadyChecked = true;

                string partToCheck = partToCount + RMGenFile.FormatPosition(nextInt) + RMPConstants.GenFileNamePrefixANDSuffix.PNG;
                if (File.Exists(partToCheck))
                    return GetNextVacantPosition(gender, partType, nextInt);
                
                if (!nextIntAlreadyChecked && ShouldSkipPosition(gender, partType, nextInt))
                    return GetNextVacantPosition(gender, partType, nextInt);

                return nextInt;
            }

            public static int GetNextIntPosition(RMGenPart.eGender gender, RMGenPart.GenPartType partType, int curPosition)
            {
                Config cfgWithLowestInt = Config.GetConfigFileWithLowestInt(SpecialNumberedParts, gender, partType, curPosition);
                int nextInt = curPosition + 1;
                if (cfgWithLowestInt == null || cfgWithLowestInt.Integer > nextInt)
                    return nextInt;

                if (ShouldSkipPosition(gender, partType, nextInt))
                    return GetNextIntPosition(gender, partType, nextInt);

                return nextInt;
            }

            class Config
            {
                public RMGenPart.eGender Gender;
                public RMGenPart.GenPartType Part;
                public int Integer = 0;
                public Config(SimpleDatabaseTokenList tokenList)
                {
                    if (tokenList.Tokens.Count >= 3)
                    {
                        Gender = Gender.ParseXMLString(tokenList.Tokens[0]);
                        Part = Part.ParseXMLString(tokenList.Tokens[1]);
                        int.TryParse( tokenList.Tokens[2], out Integer);
                    }
                }
                internal static Config RetrieveConfigForPartFromList(IEnumerable<Config> ListOfConfigs, RMGenPart.eGender Gender, RMGenPart.GenPartType partType)
                {
                    if (ListOfConfigs == null)
                        return null;
                    foreach (Config cfg in ListOfConfigs)
                    {
                        if (cfg.Gender == Gender && cfg.Part == partType)
                            return cfg;
                    }
                    return null;
                }
                internal static Config RetrieveConfigForPartFromList(IEnumerable<Config> ListOfConfigs, RMGenPart.eGender Gender, RMGenPart.GenPartType partType, int position)
                {
                    if (ListOfConfigs == null)
                        return null;
                    foreach (Config cfg in ListOfConfigs)
                    {
                        if (cfg.Gender == Gender && cfg.Part == partType && cfg.Integer == position)
                            return cfg;
                    }
                    return null;
                }
                internal static Config GetConfigFileWithLowestInt(IEnumerable<Config> ListOfConfigs, RMGenPart.eGender Gender, RMGenPart.GenPartType partType, int floor)
                {
                    if (ListOfConfigs == null)
                        return null;

                    Config currentLowestCFG = null;
                    foreach (Config cfg in ListOfConfigs)
                    {
                        if (cfg.Gender == Gender && cfg.Part == partType)
                        {
                            if (currentLowestCFG == null  && cfg.Integer > floor)
                                currentLowestCFG = cfg;
                            else if (currentLowestCFG != null && currentLowestCFG.Integer > cfg.Integer && cfg.Integer > floor)
                                currentLowestCFG = cfg;
                        }
                    }
                    return currentLowestCFG;
                }
            }
           
        }

        
    }
}
