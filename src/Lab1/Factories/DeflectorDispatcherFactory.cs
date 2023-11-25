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
                .Confirm()
            .UseNoneDeflector
                .FromMeteorites(ProtectNone)
                .FromAntimatterFlares(ProtectNone)
                .FromCosmoWhales(ProtectNone)
                .Confirm();
    }

    private DeflectorProtectionResult ProtectNone(NoneDeflector deflector, IImpediment impediment)
    {
        return DeflectorProtectionResult.None;
    }

    private DeflectorProtectionResult Protect(IDeflector deflector, IImpediment impediment)
    {
        if (deflector.DeflectorPoints > impediment.DamagePoints)
        {
            deflector.DecreaseDeflectorPoints(impediment.DamagePoints);
            impediment.DecreasePoints(impediment.DamagePoints);
            return DeflectorProtectionResult.Withstood;
        }

        impediment.DecreasePoints(deflector.DeflectorPoints);
        deflector.DecreaseDeflectorPoints(deflector.DeflectorPoints);
        return DeflectorProtectionResult.Destroyed;
    }

    private DeflectorProtectionResult Protect(PhotonDeflector deflector, AntimatterFlares impediment)
    {
        if (deflector.PhotonPoints > impediment.DamagePoints)
        {
            deflector.DecreasePhotonPoints(impediment.DamagePoints);
            impediment.DecreasePoints(impediment.DamagePoints);
            return DeflectorProtectionResult.Withstood;
        }

        impediment.DecreasePoints(deflector.PhotonPoints);
        deflector.DecreasePhotonPoints(deflector.PhotonPoints);
        return DeflectorProtectionResult.Destroyed;
    }

    private DeflectorProtectionResult Protect(IDeflector deflector, AntimatterFlares impediment)
    {
        return DeflectorProtectionResult.HadNoEffect;
    }

    private DeflectorProtectionResult Protect(IDeflector deflector, CosmoWhales impediment)
    {
        if (impediment.DamagePoints == 0) return DeflectorProtectionResult.Withstood;

        impediment.DecreasePoints(impediment.DamagePoints);
        deflector.DecreaseDeflectorPoints(deflector.DeflectorPoints);
        return DeflectorProtectionResult.Destroyed;
    }
}