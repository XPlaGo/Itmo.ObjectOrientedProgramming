using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithCpu
{
    public IWithProcessorCoolingSystem WithCpu(CpuDocument cpuDocument);
}