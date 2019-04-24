using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMCharacterExt
    {
         public static RMCharImageFile.ImageTypes ParseString(this RMCharImageFile.ImageTypes type, string str)
        {
            switch (str)
            {
                case RMPConstants.CharacterImageType.CHARACTER:
                    return RMCharImageFile.ImageTypes.Character;
                case RMPConstants.CharacterImageType.ENEMY:
                    return RMCharImageFile.ImageTypes.Enemy;
                case RMPConstants.CharacterImageType.FACE:
                    return RMCharImageFile.ImageTypes.Face;
                case RMPConstants.CharacterImageType.SV_ACTOR:
                    return RMCharImageFile.ImageTypes.SV_Actor;
                case RMPConstants.CharacterImageType.SV_ENEMY:
                    return RMCharImageFile.ImageTypes.SV_Enemy;
                default:
                    return RMCharImageFile.ImageTypes.None;
            }
        }

        public static string ToXMLString(this RMCharImageFile.ImageTypes type)
        {
            switch (type)
            {
                case RMCharImageFile.ImageTypes.Character:
                    return RMPConstants.CharacterImageType.CHARACTER;
                case RMCharImageFile.ImageTypes.Enemy:
                    return RMPConstants.CharacterImageType.ENEMY;
                case RMCharImageFile.ImageTypes.Face:
                    return RMPConstants.CharacterImageType.FACE;
                case RMCharImageFile.ImageTypes.SV_Actor:
                    return RMPConstants.CharacterImageType.SV_ACTOR;
                case RMCharImageFile.ImageTypes.SV_Enemy:
                    return RMPConstants.CharacterImageType.SV_ENEMY;
                default:
                    return string.Empty;
            }
        }

        public static string ToDirectoryName(this RMCharImageFile.ImageTypes type, bool fullPath = true)
        {
            switch (type)
            {
                case RMCharImageFile.ImageTypes.Character:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.CHARACTERS;
                    else 
                        return DirectoryNames.ProjectFiles.Image.CHARACTERS; 
                case RMCharImageFile.ImageTypes.Enemy:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.ENEMIES;
                    else
                        return DirectoryNames.ProjectFiles.Image.ENEMIES;
                case RMCharImageFile.ImageTypes.Face:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.FACES;
                    else
                        return DirectoryNames.ProjectFiles.Image.FACES;
                case RMCharImageFile.ImageTypes.SV_Actor:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.SV_ACTORS;
                    else
                        return DirectoryNames.ProjectFiles.Image.SV_ACTORS;
                case RMCharImageFile.ImageTypes.SV_Enemy:
                    if (fullPath)
                        return DirectoryNames.ProjectFiles.Image.ROOT + "\\" + DirectoryNames.ProjectFiles.Image.SV_ENEMIES;
                    else
                        return DirectoryNames.ProjectFiles.Image.SV_ENEMIES;
                default:
                    return string.Empty;
            }
        }
    }
}
