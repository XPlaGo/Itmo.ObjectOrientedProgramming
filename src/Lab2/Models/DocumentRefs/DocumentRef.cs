using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

public class DocumentRef
{
    public DocumentRef(DocumentId id)
    {
        Id = id;
    }

    public DocumentId Id { get; set; }
}