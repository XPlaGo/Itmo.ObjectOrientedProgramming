using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public interface IRepository<out TDocument>
    where TDocument : IDocument
{
    public TDocument GetById(DocumentId id);
    public TDocument GetByRef(DocumentRef reference);
}