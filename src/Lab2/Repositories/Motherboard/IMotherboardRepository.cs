using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.Motherboard;

public interface IMotherboardRepository
{
    public MotherboardDocument? GetById(DocumentId id);
}