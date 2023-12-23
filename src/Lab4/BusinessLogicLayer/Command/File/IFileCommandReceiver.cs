using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;

public interface IFileCommandReceiver<out TResponse>
{
    public TResponse Copy(IPath fromPath, IPath toPath);
    public TResponse Delete(IPath path);
    public TResponse Move(IPath fromPath, IPath toPath);
    public TResponse Rename(IPath path, string newName, string newFormat);
    public TResponse Show(IPath path);
}