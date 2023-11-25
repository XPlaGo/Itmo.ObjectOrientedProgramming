using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithSsd
{
    public IWithHddOrPcCase WithSsd(SsdDocument ssdDocument);
}