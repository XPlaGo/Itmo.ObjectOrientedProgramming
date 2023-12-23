using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithBios
{
    public IWithXmp WithBios(BiosDocument biosDocument);
}