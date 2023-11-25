using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

public interface IImpedimentVisitor<out T>
{
    T Visit(Meteorites impediment);
    T Visit(AntimatterFlares impediment);
    T Visit(CosmoWhales impediment);
}