using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class PcCaseDocument : IDocument
{
    public PcCaseDocument(DocumentId id, float maxVideoCardHeight, float maxVideoCardWidth, IReadOnlyList<string> supportedMotherboardFormFactors)
    {
        Id = id;
        MaxVideoCardHeight = maxVideoCardHeight;
        MaxVideoCardWidth = maxVideoCardWidth;
        SupportedMotherboardFormFactors = supportedMotherboardFormFactors;
    }

    public DocumentId Id { get; init; }
    public float MaxVideoCardHeight { get; init; }
    public float MaxVideoCardWidth { get; init; }
    public IReadOnlyList<string> SupportedMotherboardFormFactors { get; init; }

    public static PcCaseDocument CopyWith(
        PcCaseDocument other,
        DocumentId? id = null,
        float? maxVideoCardHeight = null,
        float? maxVideoCardWidth = null,
        IReadOnlyList<string>? supportedVideoCardsFormFactors = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new PcCaseDocument(
            id ?? other.Id,
            maxVideoCardHeight ?? other.MaxVideoCardHeight,
            maxVideoCardWidth ?? other.MaxVideoCardWidth,
            supportedVideoCardsFormFactors ?? other.SupportedMotherboardFormFactors);
    }
}