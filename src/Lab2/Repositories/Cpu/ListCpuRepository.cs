using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Dto;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Database;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.Cpu;

public class ListCpuRepository : ICpuRepository
{
    private List<CpuDocument> _data = new();

    public void InitFromJson(FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);

        try
        {
            string jsonData = File.ReadAllText(file.ToString()
                                               ?? throw new DatabaseFileNotFoundException(nameof(file)));
            List<CpuDto>? data = JsonSerializer.Deserialize<List<CpuDto>>(jsonData)
                                 ?? throw new DatabaseDeserializeException(nameof(file));
            _data = data.Select(item => item.ToCpuDocument()).ToList();
        }
        catch (FileNotFoundException)
        {
            throw new DatabaseFileNotFoundException(nameof(file));
        }
    }

    public CpuDocument? GetById(DocumentId id)
    {
        CpuDocument? document =
            (from doc in _data
                where doc.Id.Equals(id)
                select doc).FirstOrDefault();

        return document;
    }

    public CpuDocument? UpdateById(DocumentId id, CpuDocument newDocument)
    {
        ArgumentNullException.ThrowIfNull(newDocument);

        CpuDocument? document =
            (from doc in _data
                where doc.Id.Equals(id)
                select doc).FirstOrDefault();

        if (document is not null)
        {
            document.Id = newDocument.Id;
        }

        return document;
    }
}