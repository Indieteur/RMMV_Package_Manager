using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public static class RMImplicitExtension
    {
    

        internal static RMGenPart GetLowestPartByInternalPosition(this IEnumerable<RMGenPart> listOfPart, RMGenPart.GenPartType partType, RMGenPart.eGender gender)
        {
            if (listOfPart == null)
                return null;
            RMGenPart retVal = null;
            foreach (RMGenPart part in listOfPart)
                if (part.PartType == partType && part.Gender == gender)
                {
                    if (retVal == null)
                        retVal = part;
                    else if (part.implicitID < retVal.implicitID)
                        retVal = part;
                }
            
            return retVal;
        }

        internal static RMGenPart GetPartByInternalPosition(this IEnumerable<RMGenPart> listOfParts, RMGenPart.GenPartType partType, RMGenPart.eGender gender, int position)
        {
            if (listOfParts == null)
                return null;
            foreach(RMGenPart part in listOfParts)
                if (part.PartType == partType && part.Gender == gender && part.implicitID == position)
                    return part;
            
            return null;
        }
        internal static RMAudioGroup FindByInternalName(this IEnumerable<RMAudioGroup> listOfAudio, string name)
        {
            foreach(RMAudioGroup rma in listOfAudio)
                if (rma.internalName == name)
                    return rma;
            return null;
        }
        internal static RMMovieGroup FindByInternalName(this IEnumerable<RMMovieGroup> listOfMovies, string name)
        {
            foreach (RMMovieGroup rmv in listOfMovies)
                if (rmv.internalName == name)
                    return rmv;
            return null;
        }
        internal static RMCharImageGroup FindByInternalName(this IEnumerable<RMCharImageGroup> listOfNodes, string name)
        {
            foreach (RMCharImageGroup rci in listOfNodes)
                if (rci.internalName == name)
                    return rci;
            return null;
        }
        internal static RMTilesetGroup FindByInternalName(this IEnumerable<RMTilesetGroup> listOfTilesets, string name)
        {
            foreach (RMTilesetGroup tileset in listOfTilesets)
                if (tileset.internalName == name)
                    return tileset;
            return null;
        }
        internal static int CountPartOfType(this IEnumerable<RMGenPart> parts, RMGenPart.GenPartType partType, RMGenPart.eGender gender)
        {
            int counter = 0;
            foreach (RMGenPart part in parts)
                if (part.PartType == partType && part.Gender == gender)
                    ++counter;
            return counter;
        }



    }
}
