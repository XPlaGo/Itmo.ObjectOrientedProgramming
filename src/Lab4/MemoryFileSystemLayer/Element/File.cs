using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.File;

namespace Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Element;

public class File : IFile
{
    public File(string name, string format, string content)
    {
        Name = name;
        Format = format;
        Content = content;
    }

    public string Name { get; set; }
    public string Format { get; set; }
    public string Content { get; set; }
}