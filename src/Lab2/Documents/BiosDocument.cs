using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class BiosDocument : IDocument
{
    public BiosDocument(
        DocumentId id,
        string type,
        string version,
        IReadOnlyList<DocumentRef<CpuDocument>> cpuSupportedList)
    {
        Id = id;
        Type = type;
        Version = version;
        CpuSupportedList = cpuSupportedList;
    }

    public DocumentId Id { get; set; }
    public string Type { get; set; }
    public string Version { get; set; }
    public IReadOnlyList<DocumentRef<CpuDocument>> CpuSupportedList { get; set; }

    internal class BiosDocumentBuilder
    {
        private DocumentId? Id { get; set; }
        private string? Type { get; set; }
        private string? Version { get; set; }
        private IReadOnlyList<DocumentRef<CpuDocument>>? CpuSupportedList { get; set; }

        public BiosDocumentBuilder WithId(DocumentId id)
        {
            Id = id;
            return this;
        }

        public BiosDocumentBuilder WithType(string type)
        {
            Type = type;
            return this;
        }

        public BiosDocumentBuilder WithVersion(string version)
        {
            Version = version;
            return this;
        }

        public BiosDocumentBuilder WithCpuSupportedList(IReadOnlyList<DocumentRef<CpuDocument>> cpuSupportedList)
        {
            CpuSupportedList = cpuSupportedList;
            return this;
        }

        public BiosDocument Build()
        {
            if (Id is null) throw new BuilderNullComponentException(nameof(Id));
            if (Type is null) throw new BuilderNullComponentException(nameof(Type));
            if (Version is null) throw new BuilderNullComponentException(nameof(Version));
            if (CpuSupportedList is null) throw new BuilderNullComponentException(nameof(CpuSupportedList));

            return new BiosDocument(
                Id,
                Type,
                Version,
                CpuSupportedList);
        }
    }
}