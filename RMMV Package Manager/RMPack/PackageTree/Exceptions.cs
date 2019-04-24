using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMMV_PackMan
{
    public class NoPackageFoundException : Exception
    {
        public NoPackageFoundException(string message) : base(message)
        {

        }
    }
    public class PackageAttributeORFieldNotFoundException : Exception
    {
        public enum WhichAttribute
        {
            Name,
            License,
            UniqueID,
            Other
        }
        public WhichAttribute Attribute;
        public PackageAttributeORFieldNotFoundException (string message, WhichAttribute attr) : base(message)
        {
            Attribute = attr;
        }
    }


    public class InvalidPackageLicenseException : Exception
    {
        public enum WhichInvalid
        {
            NoSource,
            InvalidSourceType,
            InvalidLicenseURL,
            InvalidLicenseFile
        }
        public WhichInvalid TypeOfIssue;
        public RMPackage Package;
        public InvalidPackageLicenseException (string message, WhichInvalid whichInvalid, RMPackage package) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Package = package;
        }
    }
    public class InvalidGeneratorPartException : Exception
    {
        public enum WhichInvalid
        {
            NoGender,
            GenderInvalid
        }
        public WhichInvalid TypeOfIssue;
        public RMGeneratorCollection Generator;
        public InvalidGeneratorPartException(string message, WhichInvalid whichInvalid, RMGeneratorCollection generator) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Generator = generator;
        }
    }
    public class InvalidGeneratorPartFileException : Exception
    {
        public enum WhichInvalid
        {
            TypeInvalid,
            NoType,
            OrderInvalid,
            NoOrder,
            ColourInvalid,
            NoColour, 
            PathNotSet,
            InvalidBaseOrder,
            GenderInvalid,
            NoGender
        }
        public WhichInvalid TypeOfIssue;
        public RMGenPart Part;
        public InvalidGeneratorPartFileException(string message, WhichInvalid whichInvalid, RMGenPart part) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Part = part;
        }
    }

    public class ImplicitInvalidGeneratorPartException : Exception
    {
        public enum WhichInvalid
        {
            Type,
            Position,
            Colour,
            Order,
            BaseOrder
        }
        public WhichInvalid TypeOfIssue;
        public string Path;
        public ImplicitInvalidGeneratorPartException(string message, WhichInvalid whichInvalid, string path) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Path = path;
        }
    }

    public class InvalidAudioException : Exception
    {
        public enum WhichInvalid
        {
            InvalidName,
            NoName
        }
        public WhichInvalid TypeOfIssue;
        public RMAudioCollection Collection;
        public InvalidAudioException(string message, WhichInvalid whichInvalid, RMAudioCollection collection) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Collection = collection;
        }
    }
    public class InvalidAudioFileException : Exception
    {
        public enum WhichInvalid
        {
            InvalidType,
            InvalidPath,
            NoType
        }
        public WhichInvalid TypeOfIssue;
        public RMAudioGroup ParentAudio;
        public InvalidAudioFileException(string message, WhichInvalid whichInvalid, RMAudioGroup parentAudio) : base(message)
        {
            TypeOfIssue = whichInvalid;
            ParentAudio = parentAudio;
        }
    }

    public class InvalidMovieException : Exception
    {
        public enum WhichInvalid
        {
            InvalidName,
            NoName
        }
        public WhichInvalid TypeOfIssue;
        public RMMovieCollection Collection;
        public InvalidMovieException(string message, WhichInvalid whichInvalid, RMMovieCollection collection) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Collection = collection;
        }
    }
    public class InvalidMovieFileException : Exception
    {
        public enum WhichInvalid
        {
            InvalidType,
            NoType,
            NoPath
        }
        public WhichInvalid TypeOfIssue;
        public RMMovieGroup ParentAudio;
        public InvalidMovieFileException(string message, WhichInvalid whichInvalid, RMMovieGroup parentMovie) : base(message)
        {
            TypeOfIssue = whichInvalid;
            ParentAudio = parentMovie;
        }
    }

    public class InvalidSingleFileCollectionException : Exception
    {
        public enum WhichInvalid
        {
            InvalidName,
            NoType,
            NoName
        }
        public WhichInvalid TypeOfIssue;
        public RMPackage Package;
        public InvalidSingleFileCollectionException(string message, WhichInvalid whichInvalid, RMPackage package) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Package = package;
        }
    }
    public class InvalidCharacterImageNodeException : Exception
    {
        public enum WhichInvalid
        {
            InvalidName,
            NoName
        }
        public WhichInvalid TypeOfIssue;
        public RMCharImageCollection Parent;
        public InvalidCharacterImageNodeException(string message, WhichInvalid whichInvalid, RMCharImageCollection parent) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Parent = parent;
        }
    }
    public class InvalidCharacterImageFileException : Exception
    {
        public enum WhichInvalid
        {
            InvalidType,
            NoType
        }
        public WhichInvalid TypeOfIssue;
        public RMCharImageGroup Parent;
        public InvalidCharacterImageFileException(string message, WhichInvalid whichInvalid, RMCharImageGroup parent) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Parent = parent;
        }
    }
    public class InvalidTilesetFileException : Exception
    {
        public enum WhichInvalid
        {
            InvalidType,
            NoType,
            InvalidOrder,
            NoOrder,
            NoPath
        }
        public WhichInvalid TypeOfIssue;
        public RMTilesetGroup Parent;
        public InvalidTilesetFileException(string message, WhichInvalid whichInvalid, RMTilesetGroup parent) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Parent = parent;
        }
    }
    public class InvalidTilesetException : Exception
    {
        public enum WhichInvalid
        {
            InvalidName,
            AtlasType,
            FileType,
            NoName
        }
        public WhichInvalid TypeOfIssue;
        public RMTilesetCollection Parent;
        public InvalidTilesetException(string message, WhichInvalid whichInvalid, RMTilesetCollection parent) : base(message)
        {
            TypeOfIssue = whichInvalid;
            Parent = parent;
        }
    }

 

    public class InvalidAudioCollectionTypeException : Exception
    {
        public InvalidAudioCollectionTypeException(string message) : base(message)
        {

        }
    }

    public class InvalidSingleFileException : Exception
    {
        public InvalidSingleFileException(string message) : base(message)
        {

        }
    }

    public class InvalidCharacterFileException : Exception
    {
        public InvalidCharacterFileException(string message) : base(message)
        {

        }
    }

    public class CollectionAlreadyExistsException : Exception
    {
        public CollectionAlreadyExistsException()
        {

        }
        public CollectionAlreadyExistsException(string message) : base (message)
        {

        }
    }
   
}
