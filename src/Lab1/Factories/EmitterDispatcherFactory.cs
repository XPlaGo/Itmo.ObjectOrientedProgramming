using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.EmitterProtectionResult;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;

namespace Itmo.ObjectOrientedProgramming.Lab1.Factories;

public class EmitterDispatcherFactory
{
    public IEmitterVisitor<IImpedimentVisitor<EmitterProtectionResult>> CreateDispatcher()
    {
        return new EmitterDispatcher<EmitterProtectionResult>(Protect)
            .UseAntiNeutrinoEmitter
                .FromCosmoWhales(Protect)
                .Confirm();
    }

    private static EmitterProtectionResult Protect(IEmitter emitter, IImpediment impediment)
    {
        return EmitterProtectionResult.HadNoEffect;
    }

    private EmitterProtectionResult Protect(AntiNeutrinoEmitter emitter, CosmoWhales impediment)
    {
        impediment.DecreasePoints(impediment.DamagePoints);
        return EmitterProtectionResult.HadEffect;
    }
}