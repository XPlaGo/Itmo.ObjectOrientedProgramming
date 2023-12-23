using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithWifiAdapter
{
    public IBuild WithWifiAdapter(WifiAdapterDocument wifiAdapter);
}