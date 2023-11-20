using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Dto;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Database;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.Motherboard;

public class ListMotherboardRepository : IMotherboardRepository
{
    private List<MotherboardDocument> _data = new();

    public void InitFromJson(FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);

        try
        {
            string jsonData = File.ReadAllText(file.ToString()
                                               ?? throw new DatabaseFileNotFoundException(nameof(file)));
            List<MotherboardDto> data = JsonSerializer.Deserialize<List<MotherboardDto>>(jsonData)
                                         ?? throw new DatabaseDeserializeException(nameof(file));
            _data = data.Select(item => item.ToMotherboardDocument()).ToList();
        }
        catch (FileNotFoundException)
        {
            throw new DatabaseFileNotFoundException(nameof(file));
        }
    }

    public MotherboardDocument? GetById(DocumentId id)
    {
        MotherboardDocument? document =
            (from doc in _data
                where doc.Id.Equals(id)
                select doc).FirstOrDefault();

        return document;
    }

    public MotherboardDocument? UpdateById(DocumentId id, MotherboardDocument newDocument)
    {
        ArgumentNullException.ThrowIfNull(newDocument);

        MotherboardDocument? document =
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