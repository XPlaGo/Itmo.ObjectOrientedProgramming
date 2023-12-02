using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.File;

namespace Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Element;

public class Directory : IDirectory
{
    public Directory(string name, IList<IFile> files, IList<IDirectory> directories)
    {
        Name = name;
        Files = files;
        Directories = directories;
    }

    public string Name { get; set; }
    public IList<IDirectory> Directories { get; }
    public IList<IFile> Files { get; }
}