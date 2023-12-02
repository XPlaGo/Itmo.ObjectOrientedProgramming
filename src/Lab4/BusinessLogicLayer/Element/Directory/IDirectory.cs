using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.File;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;

public interface IDirectory : IElement
{
    public IList<IDirectory> Directories { get; }
    public IList<IFile> Files { get; }
}