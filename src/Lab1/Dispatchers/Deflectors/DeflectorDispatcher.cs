using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.DeflectorsProtection;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Deflectors;

public class DeflectorDispatcher<TProtectionResult> : IDeflectorsVisitor<IImpedimentVisitor<TProtectionResult>>
{
    private readonly DeflectorProtectionBuilder<TProtectionResult, FirstClassDeflector> _firstClassDeflectorBuilder;
    private readonly DeflectorProtectionBuilder<TProtectionResult, SecondClassDeflector> _secondClassDeflectorBuilder;
    private readonly DeflectorProtectionBuilder<TProtectionResult, ThirdClassDeflector> _thirdClassDeflectorBuilder;
    private readonly DeflectorProtectionBuilder<TProtectionResult, PhotonDeflector> _photonDeflectorBuilder;

    public DeflectorDispatcher(Func<IDeflector, IImpediment, TProtectionResult> protect)
    {
        _firstClassDeflectorBuilder = new DeflectorProtectionBuilder<TProtectionResult, FirstClassDeflector>(this, protect);
        _secondClassDeflectorBuilder = new DeflectorProtectionBuilder<TProtectionResult, SecondClassDeflector>(this, protect);
        _thirdClassDeflectorBuilder = new DeflectorProtectionBuilder<TProtectionResult, ThirdClassDeflector>(this, protect);
        _photonDeflectorBuilder = new DeflectorProtectionBuilder<TProtectionResult, PhotonDeflector>(this, protect);
    }

    public IDeflectorProtectionBuilder<TProtectionResult, FirstClassDeflector> UseFirstClassDeflector => _firstClassDeflectorBuilder;

    public IDeflectorProtectionBuilder<TProtectionResult, SecondClassDeflector> UseSecondClassDeflector => _secondClassDeflectorBuilder;

    public IDeflectorProtectionBuilder<TProtectionResult, ThirdClassDeflector> UseThirdClassDeflector => _thirdClassDeflectorBuilder;

    public IDeflectorProtectionBuilder<TProtectionResult, PhotonDeflector> UsePhotonDeflector => _photonDeflectorBuilder;

    public IImpedimentVisitor<TProtectionResult> Visit(FirstClassDeflector deflector)
    {
        return _firstClassDeflectorBuilder.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(SecondClassDeflector deflector)
    {
        return _secondClassDeflectorBuilder.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(ThirdClassDeflector deflector)
    {
        return _thirdClassDeflectorBuilder.Deflector(deflector);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(PhotonDeflector deflector)
    {
        return _photonDeflectorBuilder.Deflector(deflector);
    }
}