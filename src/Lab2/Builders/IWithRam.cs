using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithRam
{
    public IWithBios WithRam(RamDocument ramDocument);
}