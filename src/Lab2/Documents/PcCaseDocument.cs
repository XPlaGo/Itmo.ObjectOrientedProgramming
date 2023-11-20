using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class PcCaseDocument : IDocument
{
    public PcCaseDocument(DocumentId id, float maxVideoCardHeight, float maxVideoCardWidth, IReadOnlyList<string> supportedVideoCardsFormFactors)
    {
        Id = id;
        MaxVideoCardHeight = maxVideoCardHeight;
        MaxVideoCardWidth = maxVideoCardWidth;
        SupportedVideoCardsFormFactors = supportedVideoCardsFormFactors;
    }

    public DocumentId Id { get; set; }
    public float MaxVideoCardHeight { get; set; }
    public float MaxVideoCardWidth { get; set; }
    public IReadOnlyList<string> SupportedVideoCardsFormFactors { get; set; }
}