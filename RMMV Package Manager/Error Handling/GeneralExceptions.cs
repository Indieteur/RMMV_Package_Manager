using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RMMV_PackMan
{

    public class UnableToDeleteDirectoryException : FileSystemException
    {
        public UnableToDeleteDirectoryException(string message, string path) : base(message, path)
        {
        }
    } 

    public class UnableToCreateDirectoryException : FileSystemException
    {
        public UnableToCreateDirectoryException(string message, string path) : base(message, path)
        {
        }
    }

    public class FileSystemException : Exception
    {
        public string Path { get; protected set; }
        public FileSystemException(string message, string path) : base(message)
        {
            Path = path;
        }
    }

    public class UnwrappedException : Exception
    {
        public UnwrappedException()
        {

        }
        public UnwrappedException(string message) : base(message)
        {
            
        }
    }

    public class NullGlobalPackagesException : Exception
    {
        public NullGlobalPackagesException()
        {
            
        }
        public NullGlobalPackagesException(string message) : base(message)
        {

        }
    }

    public class EmptyPackageException : Exception
    {
        public EmptyPackageException() : base ("Package doesn't contain any assets.")
        {

        }

        public EmptyPackageException(string message) : base(message)
        {

        }
    }

    public class PackageNotFoundException : Exception
    {
        public bool GlobalPackage { get; private set; }
        public PackageNotFoundException(bool globalPackage)
        {
            GlobalPackage = globalPackage;
        }
        public PackageNotFoundException(bool globalPackage, string message) : base(message)
        {
            GlobalPackage = globalPackage;
        }
    }

    public class FileNotFoundExceptionWPath : Exception
    {
        public string Path { get; private set; }
        public FileNotFoundExceptionWPath(string path)
        {
            Path = path;
        }
        public FileNotFoundExceptionWPath(string path, string message) : base(message)
        {
            Path = path;
        }
    }

    public class InvalidArgumentException: Exception
    {
        public Type ArgumentType { get; private set; }
        public string ArgumentName { get; private set; }
        public InvalidArgumentException(Type argumentType, string argumentName)
        {
            ArgumentType = argumentType;
            ArgumentName = argumentName;
        }
        public InvalidArgumentException(Type argumentType, string argumentName, string message) : base (message)
        {
            ArgumentType = argumentType;
            ArgumentName = argumentName;
        }
    }

    public class PackageAlreadyExistsException : Exception
    {
        public PackageAlreadyExistsException(string message) : base(message)
        {

        }
    }

    public class NullProjectException : Exception
    {
        public NullProjectException(string message) : base(message)
        {

        }
    }

    public class InstalledPackageNotFoundException : Exception
    {
        public InstalledPackageNotFoundException(string message) : base(message)
        {

        }
    }
}
