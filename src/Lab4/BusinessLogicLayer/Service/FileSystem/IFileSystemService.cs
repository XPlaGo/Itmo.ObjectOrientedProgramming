using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Service.FileSystem;

public interface IFileSystemService<out TResponse>
{
    public IDirectory Connect();
    public void Disconnect();
    public TResponse Copy(IPath fromPath, IPath toPath, IDirectory currentDirectory, IDirectory rootDirectory);
    public TResponse Delete(IPath path, IDirectory currentDirectory, IDirectory rootDirectory);
    public TResponse Move(IPath fromPath, IPath toPath, IDirectory currentDirectory, IDirectory rootDirectory);
    public TResponse Rename(IPath path, string newName, string newFormat, IDirectory currentDirectory, IDirectory rootDirectory);
    public TResponse Show(IPath path, IDirectory currentDirectory, IDirectory rootDirectory);
    public IDirectory GoToPath(IPath path, IDirectory currentDirectory, IDirectory rootDirectory);
    public string List(int depth, IDirectory currentDirectory);
}