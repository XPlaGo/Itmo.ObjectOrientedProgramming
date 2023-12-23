using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithPowerUnit
{
    public IWithWifiAdapterOrBuild WithPowerUnit(PowerUnitDocument powerUnitDocument);
}