using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.File;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Exceptions.Path;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Service.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Exceptions;
using Path = Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.MemoryFilePath.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Service;

public class MemoryFileSystemService : IFileSystemService<string>
{
    private readonly IDirectory _rootDirectory;

    public MemoryFileSystemService(IDirectory rootDirectory)
    {
        _rootDirectory = rootDirectory;
    }

    public IDirectory Connect()
    {
        if (_rootDirectory is null) throw new RootDirectoryCannotBeNull(nameof(_rootDirectory));
        return _rootDirectory;
    }

    public void Disconnect() { }

    public string Copy(IPath fromPath, IPath toPath, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(fromPath);
        ArgumentNullException.ThrowIfNull(toPath);
        ArgumentNullException.ThrowIfNull(currentDirectory);
        ArgumentNullException.ThrowIfNull(rootDirectory);

        IFile file = GetFile(fromPath, currentDirectory, rootDirectory);
        IDirectory directory = GetDirectory(toPath, currentDirectory, rootDirectory);
        directory.Files.Add(file);

        return $"File copied from {fromPath.Data} to {toPath.Data}";
    }

    public string Delete(IPath path, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(currentDirectory);
        ArgumentNullException.ThrowIfNull(rootDirectory);

        int fileNameStart = path.Data.LastIndexOf("/", StringComparison.Ordinal);

        IDirectory parentDirectory = fileNameStart < 0
            ? rootDirectory
            : GetDirectory(new Path(path.Data[..fileNameStart]), currentDirectory, rootDirectory);

        string fileFullName = path.Data[(fileNameStart + 1)..];
        int dotPosition = fileFullName.LastIndexOf(".", StringComparison.Ordinal);
        string fileName = fileFullName[..dotPosition];
        string fileFormat = fileFullName[(dotPosition + 1)..];

        foreach (IFile file in parentDirectory.Files)
        {
            if (fileName.Equals(file.Name, StringComparison.Ordinal) &&
                fileFormat.Equals(file.Format, StringComparison.Ordinal))
            {
                parentDirectory.Files.Remove(file);
                return $"File {path} was deleted";
            }
        }

        throw new FileOrDirectoryNotExists(path.Data);
    }

    public string Move(IPath fromPath, IPath toPath, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(fromPath);
        ArgumentNullException.ThrowIfNull(currentDirectory);
        ArgumentNullException.ThrowIfNull(rootDirectory);

        int fileNameStart = fromPath.Data.LastIndexOf("/", StringComparison.Ordinal);

        IDirectory parentDirectory = fileNameStart < 0
            ? rootDirectory
            : GetDirectory(new Path(fromPath.Data[..fileNameStart]), currentDirectory, rootDirectory);

        string fileFullName = fromPath.Data[(fileNameStart + 1)..];
        int dotPosition = fileFullName.LastIndexOf(".", StringComparison.Ordinal);
        string fileName = fileFullName[..dotPosition];
        string fileFormat = fileFullName[(dotPosition + 1)..];

        foreach (IFile file in parentDirectory.Files)
        {
            if (fileName.Equals(file.Name, StringComparison.Ordinal) &&
                fileFormat.Equals(file.Format, StringComparison.Ordinal))
            {
                parentDirectory.Files.Remove(file);

                IDirectory toDirectory = GetDirectory(toPath, currentDirectory, rootDirectory);
                toDirectory.Files.Add(file);
                return $"File {fromPath} was moved to {toPath}";
            }
        }

        throw new FileOrDirectoryNotExists(fromPath.Data);
    }

    public string Rename(IPath path, string newName, string newFormat, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(newName);
        ArgumentNullException.ThrowIfNull(newFormat);
        ArgumentNullException.ThrowIfNull(currentDirectory);
        ArgumentNullException.ThrowIfNull(rootDirectory);

        IFile file = GetFile(path, currentDirectory, rootDirectory);
        file.Name = newName;
        file.Format = newFormat;

        return $"File {path.Data} was renamed to {newName}.{newFormat}";
    }

    public string Show(IPath path, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        return GetFile(path, currentDirectory, rootDirectory).Content;
    }

    public IDirectory GoToPath(IPath path, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        return GetDirectory(path, currentDirectory, rootDirectory);
    }

    public string List(int depth, IDirectory currentDirectory)
    {
        ArgumentNullException.ThrowIfNull(currentDirectory);
        return List(depth, currentDirectory, string.Empty);
    }

    private static string List(int depth, IDirectory currentDirectory, string prefix)
    {
        if (depth == 0)
        {
            return $"{prefix}\u25b2 {currentDirectory.Name}\n";
        }

        var ci = new CultureInfo("es-ES", true);
        var builder = new StringBuilder();
        builder.Append(ci, $"{prefix}\u25bc {currentDirectory.Name}\n");

        foreach (IFile file in currentDirectory.Files)
        {
            builder.Append(ci, $"{prefix}  {file.Name}.{file.Format}\n");
        }

        foreach (IDirectory directory in currentDirectory.Directories)
        {
            builder.Append(List(depth - 1, directory, $"{prefix}  "));
        }

        return builder.ToString();
    }

    private static IDirectory GetDirectoryFromDirectory(IDirectory directory, string name)
    {
        foreach (IDirectory dir in directory.Directories)
        {
            if (dir.Name.Equals(name, StringComparison.Ordinal))
                return dir;
        }

        throw new FileOrDirectoryNotExists(name);
    }

    private static IFile GetFileFromDirectory(IDirectory directory, string name, string format)
    {
        foreach (IFile file in directory.Files)
        {
            if (file.Name.Equals(name, StringComparison.Ordinal) && file.Format.Equals(format, StringComparison.Ordinal))
                return file;
        }

        throw new FileOrDirectoryNotExists(name);
    }

    private static IFile GetFileAbsolute(IPath path, IDirectory directory)
    {
        IList<string> parts = path.ToParts();
        IDirectory currentDirectory = directory;

        for (int i = 0; i < parts.Count - 1; i++)
        {
            currentDirectory = GetDirectoryFromDirectory(currentDirectory, parts[i]);
        }

        int dotPosition = parts[^1].LastIndexOf(".", StringComparison.Ordinal);
        if (dotPosition < 0) throw new FileOrDirectoryNotExists(parts[^1]);

        string fileName = parts[^1][..dotPosition];
        string fileFormat = parts[^1][(dotPosition + 1)..];

        return GetFileFromDirectory(currentDirectory, fileName, fileFormat);
    }

    private static IDirectory GetDirectoryAbsolute(IPath path, IDirectory directory)
    {
        IList<string> parts = path.ToParts();

        return parts.Aggregate(directory, GetDirectoryFromDirectory);
    }

    private static IFile GetFile(IPath path, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(path);

        if (path.IsAbsolute()) return GetFileAbsolute(path, rootDirectory);

        path = new Path(path.Data[1..]);
        if (path.IsAbsolute()) return GetFileAbsolute(path, currentDirectory);

        throw new FileOrDirectoryNotExists(path.Data);
    }

    private static IDirectory GetDirectory(IPath path, IDirectory currentDirectory, IDirectory rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(path);

        if (string.IsNullOrEmpty(path.Data)) return rootDirectory;

        if (path.IsAbsolute()) return GetDirectoryAbsolute(path, rootDirectory);

        path = new Path(path.Data[1..]);
        if (path.IsAbsolute()) return GetDirectoryAbsolute(path, currentDirectory);

        throw new FileOrDirectoryNotExists(path.Data);
    }
}