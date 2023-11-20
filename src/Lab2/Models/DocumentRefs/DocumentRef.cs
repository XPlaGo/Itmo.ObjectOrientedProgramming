using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

public class DocumentRef<T>
    where T : IDocument
{
    public DocumentRef(DocumentId documentId)
    {
        Id = documentId;
    }

    public DocumentId Id { get; set; }
}