using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Connection;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Tree;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;

public interface ICommandReceiver<out TResponse> : IFileCommandReceiver<TResponse>, ITreeReceiver, IConnectionCommandReceiver { }