using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.ArmorProtectionResult;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Factories;

public class ArmorDispatcherFactory
{
    public IArmorVisitor<IImpedimentVisitor<ArmorProtectionResult>> CreateDispatcher()
    {
        return new ArmorDispatcher<ArmorProtectionResult>(Protect)
            .UseFirstClassArmor
                .FromMeteorites(Protect)
                .FromAntimatterFlares(Protect)
                .FromCosmoWhales(Protect)
                .Confirm()
            .UseSecondClassArmor
                .FromMeteorites(Protect)
                .FromAntimatterFlares(Protect)
                .FromCosmoWhales(Protect)
                .Confirm()
            .UseThirdClassArmor
                .FromMeteorites(Protect)
                .FromAntimatterFlares(Protect)
                .FromCosmoWhales(Protect)
                .Confirm();
    }

    private static ArmorProtectionResult ProtectTemplate(IArmor armor, IImpediment impediment, double overallRatio)
    {
        double damage = overallRatio * impediment.DamagePoints;

        if (armor.ArmorPoints > damage)
        {
            armor.DecreaseArmorPoints(damage);
            impediment.DecreasePoints(impediment.DamagePoints);
            return ArmorProtectionResult.Withstood;
        }

        impediment.DecreasePoints(impediment.DamagePoints);
        armor.DecreaseArmorPoints(armor.ArmorPoints);
        return ArmorProtectionResult.Destroyed;
    }

    private static ArmorProtectionResult Protect(IArmor armor, CosmoWhales impediment)
    {
        if (impediment.DamagePoints == 0) return ArmorProtectionResult.Withstood;

        armor.DecreaseArmorPoints(armor.ArmorPoints);
        impediment.DecreasePoints(impediment.DamagePoints);
        return ArmorProtectionResult.Destroyed;
    }

    private static ArmorProtectionResult Protect(IArmor armor, AntimatterFlares impediment)
    {
        if (impediment.DamagePoints == 0) return ArmorProtectionResult.Withstood;

        armor.DecreaseArmorPoints(armor.ArmorPoints);
        impediment.DecreasePoints(impediment.DamagePoints);
        return ArmorProtectionResult.LossOfCrew;
    }

    private ArmorProtectionResult Protect(IArmor armor, IImpediment impediment)
    {
        return ProtectTemplate(armor, impediment, 1);
    }

    private ArmorProtectionResult Protect(IArmor armor, Meteorites impediment)
    {
        double overallRatio = impediment.OverallCharacteristics / armor.OverallCharacteristics;
        return ProtectTemplate(armor, impediment, overallRatio);
    }
}