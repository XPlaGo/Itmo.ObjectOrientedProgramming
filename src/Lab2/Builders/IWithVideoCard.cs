using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithVideoCard
{
    public IWithOrSsdOrHddOrPcCase WithVideoCard(VideoCardDocument videoCardDocument);
}