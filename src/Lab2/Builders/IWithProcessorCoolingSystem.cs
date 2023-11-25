using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithProcessorCoolingSystem
{
    public IWithRam WithProcessorCoolingSystem(ProcessorCoolingSystemDocument processorCoolingSystemDocument);
}