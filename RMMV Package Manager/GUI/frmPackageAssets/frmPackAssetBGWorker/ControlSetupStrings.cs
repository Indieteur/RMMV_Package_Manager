using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMMV_PackMan.GUI
{
    public static partial class frmPackAssetBGWorker
    {

        public static ObjectAndIntCollection FindObjectAndIntItemWithIntAndSub(this IEnumerable<ObjectAndIntCollection> collection, int highInt, int lowInt)
        {
            if (collection == null)
                return null;
            foreach (ObjectAndIntCollection obj in collection)
            {
                if (obj.IntegerCollection == null || obj.SubIntegerCollection == null)
                    continue;
                if (obj.IntegerCollection.Contains(highInt) && obj.SubIntegerCollection.Contains(lowInt))
                    return obj;
            }
            return null;
        }

        public static ObjectAndIntCollection FindObjectAndIntItemWithInt(this IEnumerable<ObjectAndIntCollection> collection, int intToFind)
        {
            if (collection == null)
                return null;
            foreach (ObjectAndIntCollection obj in collection)
            {
                if (obj.IntegerCollection == null)
                    continue;
                if (obj.IntegerCollection.Contains(intToFind))
                    return obj;
            }
            return null;
        }

        public static ObjectAndIntCollection FindObjectAndIntItemWithInt(this IEnumerable<ObjectAndIntCollection> collection, int intIndex, int intToFind)
        {
            if (collection == null)
                return null;
            foreach (ObjectAndIntCollection obj in collection)
            {
                if (obj.IntegerCollection == null)
                    continue;
                if (obj.IntegerCollection[intIndex] == intToFind)
                    return obj;
            }
            return null;
        }

        public static ObjectAndIntCollection FindObjectAndIntItemWithInt(this ComboBox.ObjectCollection collection, int intIndex, int intToFind)
        {
            if (collection == null)
                return null;
            foreach (object obj in collection)
            {
                ObjectAndIntCollection castedValue = obj as ObjectAndIntCollection;
                if (castedValue.IntegerCollection[intIndex] == intToFind)
                    return castedValue;
            }
            return null;
        }

        public static class ConstantSetup
        {
            public static List<ObjectAndIntCollection> ControlSetupList { get; internal set; }

            public static ObjectAndIntCollection[] CollectionTypeNormal { get; internal set; }
            public static ObjectAndIntCollection[] CollectionTypeGenerator { get; internal set; }
            public static ObjectAndIntCollection[] Gender { get; internal set; }
            public static ObjectAndIntCollection[] T1_AudioFileType { get; internal set; }
            public static ObjectAndIntCollection[] T1_CharacterFileType { get; internal set; }
            public static ObjectAndIntCollection[] T1_TilesetFileType { get; internal set; }
            public static ObjectAndIntCollection[] T1_MovieFileType { get; internal set; }
            public static ObjectAndIntCollection[] T2_AtlasType { get; internal set; }
            public static ObjectAndIntCollection[] T1_GenFileType { get; internal set; }
            
        }

        public static void PreloadAssetFormData()
        {
            ConstantSetup.CollectionTypeNormal = new ObjectAndIntCollection[] 
            {
                new ObjectAndIntCollection("Audio - Background Music", 2), 
                new ObjectAndIntCollection("Audio - Background Sound", 4),
                new ObjectAndIntCollection("Audio - Musical Effect", 8),
                new ObjectAndIntCollection("Audio - Sound Effect", 16),
                new ObjectAndIntCollection("System Data", 131072),
                new ObjectAndIntCollection("Image - Character", 256),
                new ObjectAndIntCollection("Image - Animations", 32),
                new ObjectAndIntCollection("Image - Battle Backgrounds Floor", 64),
                new ObjectAndIntCollection("Image - Battle Backgrounds Top", 128),
                new ObjectAndIntCollection("Image - Parallax", 512),
                new ObjectAndIntCollection("Image - Pictures", 1024),
                new ObjectAndIntCollection("Image - System", 2048),
                new ObjectAndIntCollection("Image - Title Background", 8192),
                new ObjectAndIntCollection("Image - Title Border", 16384),
                new ObjectAndIntCollection("Image - Tileset", 4096),
                new ObjectAndIntCollection("Video - Movie", 65536),
                new ObjectAndIntCollection("Plugins", 32768),
                new ObjectAndIntCollection("Generator Parts", 1),
              
            };

            ConstantSetup.CollectionTypeGenerator = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("Generator Part - Accessory A", 0),
                new ObjectAndIntCollection("Generator Part - Accessory B", 1),
                new ObjectAndIntCollection("Generator Part - Beast Ears", 2),
                new ObjectAndIntCollection("Generator Part - Beard", 3),
                new ObjectAndIntCollection("Generator Part - Body", 4),
                new ObjectAndIntCollection("Generator Part - Cloak", 5),
                new ObjectAndIntCollection("Generator Part - Clothing", 6),
                new ObjectAndIntCollection("Generator Part - Ears", 7),
                new ObjectAndIntCollection("Generator Part - Eyebrows", 8),
                new ObjectAndIntCollection("Generator Part - Eyes", 9),
                new ObjectAndIntCollection("Generator Part - Face", 10),
                new ObjectAndIntCollection("Generator Part - Facial Mark", 11),
                new ObjectAndIntCollection("Generator Part - Front Hair", 12),
                new ObjectAndIntCollection("Generator Part - Glasses", 13),
                new ObjectAndIntCollection("Generator Part - Mouth", 14),
                new ObjectAndIntCollection("Generator Part - Nose", 15),
                new ObjectAndIntCollection("Generator Part - Rear Hair", 16),
                new ObjectAndIntCollection("Generator Part - Tail", 17),
                new ObjectAndIntCollection("Generator Part - Wing", 18)
            };

            ConstantSetup.Gender = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("Male", 1),
                new ObjectAndIntCollection("Female", 2),
                new ObjectAndIntCollection("Kid", 4)
            };

            ConstantSetup.T1_AudioFileType = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("M4A File", 0),
                new ObjectAndIntCollection("OGG File", 1),
            };

            ConstantSetup.T1_CharacterFileType = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("Character", 0),
                new ObjectAndIntCollection("Enemy", 1),
                new ObjectAndIntCollection("Face", 2),
                new ObjectAndIntCollection("SV_Actor", 3),
                new ObjectAndIntCollection("SV_Enemy", 4)
            };

            ConstantSetup.T1_TilesetFileType = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("TXT File", 1),
                new ObjectAndIntCollection("PNG File", 2)
            };

            ConstantSetup.T2_AtlasType = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("A1 - Animation", 1),
                new ObjectAndIntCollection("A2 - Ground", 2),
                new ObjectAndIntCollection("A3 - Building", 3),
                new ObjectAndIntCollection("A4 - Walls", 4),
                new ObjectAndIntCollection("A5 - Normal", 5),
                new ObjectAndIntCollection("B", 6),
                new ObjectAndIntCollection("C", 7),
                new ObjectAndIntCollection("D", 8),
                new ObjectAndIntCollection("E", 9),
                new ObjectAndIntCollection("Not Specified", 0)
            };

            ConstantSetup.T1_MovieFileType = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("MP4 File", 0),
                new ObjectAndIntCollection("WEBM File", 1)
            };

            ConstantSetup.T1_GenFileType = new ObjectAndIntCollection[]
            {
                new ObjectAndIntCollection("Face", 1),
                new ObjectAndIntCollection("SV", 2),
                new ObjectAndIntCollection("SV (C File)", 3),
                new ObjectAndIntCollection("TV", 4),
                new ObjectAndIntCollection("TV (C File)", 5),
                new ObjectAndIntCollection("TVD", 6),
                new ObjectAndIntCollection("TVD (C File)", 7),
                new ObjectAndIntCollection("Var (Icon)", 8),
            };

            ConstantSetup.ControlSetupList = new List<ObjectAndIntCollection>();

            //Collection - Audio, Characters, Tileset, Movies and Gen Parts
            ControlSetup tSetup = new ControlSetup();
            tSetup.CollectionTypeItems = ConstantSetup.CollectionTypeNormal;        
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] { 2, 4, 8, 16, 256, 4096, 65536, 1 }, 0));

            //Collection - SingleFileCollections (image)
            tSetup = new ControlSetup();
            tSetup.CollectionTypeItems = ConstantSetup.CollectionTypeNormal;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.IMAGE_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] {  32, 64, 128, 512, 1024, 2048, 8192, 16384 }, 0));

            //Collection - System Data
            tSetup = new ControlSetup();
            tSetup.CollectionTypeItems = ConstantSetup.CollectionTypeNormal;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.SYSTEM_DATA_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] { 131072 }, 0));

            //Collection - Plugins
            tSetup = new ControlSetup();
            tSetup.CollectionTypeItems = ConstantSetup.CollectionTypeNormal;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.PLUGIN_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] { 32768 }, 0));

            //GeneratorPartGroup
            tSetup = new ControlSetup();
            tSetup.CollectionTypeItems = ConstantSetup.CollectionTypeGenerator;
            tSetup.CollectionTypeEnabled = true;
            tSetup.GenderEnabled = true;
            tSetup.GenderItems = ConstantSetup.Gender;
            tSetup.GroupNameReadOnly = false;
            tSetup.GroupRevertEnabled = true;
            tSetup.GroupSaveEnabled = true;
            tSetup.btnRemoveGroupEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.IMAGE_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 1));


            // AudioGroup
            tSetup = new ControlSetup();
            tSetup.CollectionTypeEnabled = false;
            tSetup.GroupNameReadOnly = false;
            tSetup.GroupRevertEnabled = true;
            tSetup.GroupSaveEnabled = true;
            tSetup.btnRemoveGroupEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.AUDIO_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 2));

            // CharacterGroup
            tSetup = new ControlSetup();
            tSetup.CollectionTypeEnabled = false;
            tSetup.GroupNameReadOnly = false;
            tSetup.GroupRevertEnabled = true;
            tSetup.GroupSaveEnabled = true;
            tSetup.btnRemoveGroupEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.IMAGE_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 3));

            // MovieGroup
            tSetup = new ControlSetup();
            tSetup.CollectionTypeEnabled = false;
            tSetup.GroupNameReadOnly = false;
            tSetup.GroupRevertEnabled = true;
            tSetup.GroupSaveEnabled = true;
            tSetup.btnRemoveGroupEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.VIDEO_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 4));

            // TilesetGroup
            tSetup = new ControlSetup();
            tSetup.CollectionTypeEnabled = false;
            tSetup.GroupNameReadOnly = false;
            tSetup.GroupRevertEnabled = true;
            tSetup.GroupSaveEnabled = true;
            tSetup.btnRemoveGroupEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.TILESET_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 5));

            //RMAudioFile,
            tSetup = new ControlSetup();
            tSetup.FileInfoRevertEnabled = true;
            tSetup.FileInfoSaveEnabled = true;
            tSetup.Type1Enabled = true;
            tSetup.Type1Items = ConstantSetup.T1_AudioFileType;
            tSetup.Type1Text = "File Type:";
            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.AUDIO_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 6));

            //RMGenFile
            tSetup = new ControlSetup();
            tSetup.FileInfoRevertEnabled = true;
            tSetup.FileInfoSaveEnabled = true;
            tSetup.IsAGeneratorFile = true;

            tSetup.Type1Enabled = true;
            tSetup.Type1Items = ConstantSetup.T1_GenFileType;
            tSetup.Type1Text = "Type:";

            tSetup.Type2Style = ComboBoxStyle.Simple;
            tSetup.Type2Text = "High Order:";
            tSetup.Type2Enabled = true;

            tSetup.Type3Style = ComboBoxStyle.Simple;
            tSetup.Type3Text = "Low Order:";
            tSetup.Type3Enabled = true;

            tSetup.Type4Style = ComboBoxStyle.Simple;
            tSetup.Type4Text = "Colour";
            tSetup.Type4Enabled = true;

            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.IMAGE_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 7));

            //RMCharImageFile,
            tSetup = new ControlSetup();
            tSetup.FileInfoRevertEnabled = true;
            tSetup.FileInfoSaveEnabled = true;
            tSetup.Type1Enabled = true;
            tSetup.Type1Items = ConstantSetup.T1_CharacterFileType;
            tSetup.Type1Text = "Type:";
            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.IMAGE_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 8));

            //RMTilesetFile
            tSetup = new ControlSetup();
            tSetup.FileInfoRevertEnabled = true;
            tSetup.FileInfoSaveEnabled = true;

            tSetup.Type1Enabled = true;
            tSetup.Type1Items = ConstantSetup.T1_TilesetFileType;
            tSetup.Type1Text = "File Type:";

            tSetup.Type2Enabled = true;
            tSetup.Type2Items = ConstantSetup.T2_AtlasType;
            tSetup.Type2Text = "Atlas Type:";

            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.TILESET_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 9));

            //RMMovieFile,
            tSetup = new ControlSetup();
            tSetup.FileInfoRevertEnabled = true;
            tSetup.FileInfoSaveEnabled = true;
            tSetup.Type1Enabled = true;
            tSetup.Type1Items = ConstantSetup.T1_MovieFileType;
            tSetup.Type1Text = "File Type:";
            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.VIDEO_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, 10));

            //RMSingleFile (image)
            tSetup = new ControlSetup();
            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.IMAGE_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] { 32, 64, 128, 512, 1024, 2048, 8192, 16384 }, 11));

            //RMSingleFile (system data)
            tSetup = new ControlSetup();
            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.SYSTEM_DATA_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] { 131072 }, 11));

            //RMSingleFile (plugin)
            tSetup = new ControlSetup();
            tSetup.btnOpenFileEnabled = true;
            tSetup.btnRemoveFileEnabled = true;
            tSetup.btnAddFileEnabled = true;
            tSetup.openFileFilter = FileDialogFilters.PLUGIN_FORMAT;
            ConstantSetup.ControlSetupList.Add(new ObjectAndIntCollection(tSetup, new int[] { 32768 }, 11));



        }
        public class ControlSetup
        {
            static object[] emptyObjectList = new object[0];
            public bool GroupNameReadOnly { get; set; } = true;
            public bool CollectionTypeEnabled { get; set; } = false;
            public bool GenderEnabled { get; set; } = false;
            public bool GroupSaveEnabled { get; set; } = false;
            public bool GroupRevertEnabled { get; set; } = false;
            public bool Type1Enabled { get; set; } = false;
            public string Type1Text { get; set; } = "Type 1:";
            public bool Type2Enabled { get; set; } = false;
            public string Type2Text { get; set; } = "Type 2:";
            public bool Type3Enabled { get; set; } = false;
            public string Type3Text { get; set; } = "Type 3:";
            public bool Type4Enabled { get; set; } = false;
            public string Type4Text { get; set; } = "Type 4:";
            public bool FileInfoSaveEnabled { get; set; } = false;
            public bool FileInfoRevertEnabled { get; set; } = false;
            public ComboBoxStyle Type2Style { get; set; } = ComboBoxStyle.DropDownList;
            public ComboBoxStyle Type3Style { get; set; } = ComboBoxStyle.DropDownList;
            public ComboBoxStyle Type4Style { get; set; } = ComboBoxStyle.DropDownList;
            public object[] Type1Items { get; set; } = emptyObjectList;
            public object[] Type2Items { get; set; } = emptyObjectList;
            public object[] Type3Items { get; set; } = emptyObjectList;
            public object[] Type4Items { get; set; } = emptyObjectList;
            public bool IsAGeneratorFile { get; set; } = false;
            public object[] CollectionTypeItems { get; set; } = emptyObjectList;
            public object[] GenderItems { get; set; } = emptyObjectList;
            public bool btnOpenFileEnabled { get; set; } = false;
            public bool btnRemoveFileEnabled { get; set; } = false;
            public bool btnRemoveGroupEnabled { get; set; } = false;
            public bool btnAddFileEnabled { get; set; } = false;
            public string openFileFilter { get; set; } = string.Empty;
        }
    }

    public class ObjectAndIntCollection
    {
        public object Object { get; set; }
        public int[] IntegerCollection { get; set; }
        public int[] SubIntegerCollection { get; set; }
        public ObjectAndIntCollection(object _object, params int[] intCollection)
        {
            Object = _object;
            IntegerCollection = intCollection;
        }

        public ObjectAndIntCollection(object _object, int[] lowInt, params int[] highInt)
        {
            Object = _object;
            SubIntegerCollection = lowInt;
            IntegerCollection = highInt;
        }
        public override string ToString()
        {
            return Object.ToString();
        }
    }

  


}
