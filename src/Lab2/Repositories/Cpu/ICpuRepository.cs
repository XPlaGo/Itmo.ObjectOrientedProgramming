using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.Cpu;

public interface ICpuRepository
{
    public CpuDocument? GetById(DocumentId id);
}