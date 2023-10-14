using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.DeflectorsProtection;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Deflectors;

public class DeflectorDispatcher<TProtectionResult> : IDeflectorsVisitor<IImpedimentVisitor<TProtectionResult>>
{
    public DeflectorDispatcher(Func<IDeflector, IImpediment, TProtectionResult> protect)
    {
        UseFirstClassDeflector = new DeflectorProtectionBuilder<TProtectionResult, FirstClassDeflector>(this, protect);
        UseSecondClassDeflector = new DeflectorProtectionBuilder<TProtectionResult, SecondClassDeflector>(this, protect);
        UseThirdClassDeflector = new DeflectorProtectionBuilder<TProtectionResult, ThirdClassDeflector>(this, protect);
        UsePhotonDeflector = new DeflectorProtectionBuilder<TProtectionResult, PhotonDeflector>(this, protect);
        UseNoneDeflector = new DeflectorProtectionBuilder<TProtectionResult, NoneDeflector>(this, protect);
    }

    public DeflectorProtectionBuilder<TProtectionResult, FirstClassDeflector> UseFirstClassDeflector { get; }
    public DeflectorProtectionBuilder<TProtectionResult, SecondClassDeflector> UseSecondClassDeflector { get; }
    public DeflectorProtectionBuilder<TProtectionResult, ThirdClassDeflector> UseThirdClassDeflector { get; }
    public DeflectorProtectionBuilder<TProtectionResult, PhotonDeflector> UsePhotonDeflector { get; }
    public DeflectorProtectionBuilder<TProtectionResult, NoneDeflector> UseNoneDeflector { get; }

    public IImpedimentVisitor<TProtectionResult> Visit(FirstClassDeflector deflector)
    {
        return UseFirstClassDeflector.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(SecondClassDeflector deflector)
    {
        return UseSecondClassDeflector.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(ThirdClassDeflector deflector)
    {
        return UseThirdClassDeflector.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(PhotonDeflector deflector)
    {
        return UsePhotonDeflector.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(NoneDeflector deflector)
    {
        return UseNoneDeflector.Deflector(deflector);
    }
}