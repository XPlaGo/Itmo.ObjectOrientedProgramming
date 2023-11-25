using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithMotherboard
{
    public IWithCpu WithMotherboard(MotherboardDocument motherboardDocument);
}