using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithHdd
{
    public IWithSsdOrPcCase WithHdd(HddDocument hddDocument);
}