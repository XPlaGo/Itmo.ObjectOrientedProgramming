using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.DeflectorProtectionResult;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Factories;

public class DeflectorDispatcherFactory
{
    public IDeflectorsVisitor<IImpedimentVisitor<DeflectorProtectionResult>> CreateDispatcher()
    {
        return new DeflectorDispatcher<DeflectorProtectionResult>(Protect)
            .UseFirstClassDeflector
                .FromAntimatterFlares(Protect)
                .FromCosmoWhales(Protect)
                .Confirm()
            .UseSecondClassDeflector
                .FromAntimatterFlares(Protect)
                .FromCosmoWhales(Protect)
                .Confirm()
            .UseThirdClassDeflector
                .FromAntimatterFlares(Protect)
                .FromCosmoWhales(Protect)
                .Confirm()
            .UsePhotonDeflector
                .FromAntimatterFlares(Protect)
                .Confirm();
    }

    private DeflectorProtectionResult Protect(IDeflector deflector, IImpediment impediment)
    {
        if (deflector.DeflectorPoints > impediment.DamagePoints)
        {
            deflector.DeflectorPoints -= impediment.DamagePoints;
            impediment.DamagePoints = 0;
            return DeflectorProtectionResult.Withstood;
        }

        impediment.DamagePoints -= deflector.DeflectorPoints;
        deflector.DeflectorPoints = 0;
        return DeflectorProtectionResult.Destroyed;
    }

    private DeflectorProtectionResult Protect(PhotonDeflector deflector, AntimatterFlares impediment)
    {
        if (deflector.PhotonPoints > impediment.DamagePoints)
        {
            deflector.PhotonPoints -= impediment.DamagePoints;
            impediment.DamagePoints = 0;
            return DeflectorProtectionResult.Withstood;
        }

        impediment.DamagePoints -= deflector.PhotonPoints;
        deflector.PhotonPoints = 0;
        return DeflectorProtectionResult.Destroyed;
    }

    private DeflectorProtectionResult Protect(IDeflector deflector, AntimatterFlares impediment)
    {
        return DeflectorProtectionResult.HadNoEffect;
    }

    private DeflectorProtectionResult Protect(IDeflector deflector, CosmoWhales impediment)
    {
        if (impediment.DamagePoints == 0) return DeflectorProtectionResult.Withstood;

        impediment.DamagePoints = 0;
        deflector.DeflectorPoints = 0;
        return DeflectorProtectionResult.Destroyed;
    }
}