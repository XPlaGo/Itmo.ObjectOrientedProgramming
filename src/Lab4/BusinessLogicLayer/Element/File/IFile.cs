namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.File;

public interface IFile : IElement
{
    public string Format { get; set; }
    public string Content { get; set; }
}