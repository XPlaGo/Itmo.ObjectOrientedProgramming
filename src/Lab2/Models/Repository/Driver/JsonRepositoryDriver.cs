using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Database;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Repository.Driver;

public class JsonRepositoryDriver<TDocument> : IRepositoryDriver<TDocument>
    where TDocument : IDocument
{
    private readonly FileInfo _file;

    public JsonRepositoryDriver(FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);

        _file = file;
    }

    public IList<TDocument> GetAll()
    {
        try
        {
            string jsonData = File.ReadAllText(_file.ToString()
                                               ?? throw new DatabaseFileNotFoundException(nameof(_file)));
            return JsonSerializer.Deserialize<List<TDocument>>(jsonData)
                                     ?? throw new DatabaseDeserializeException(nameof(_file));
        }
        catch (FileNotFoundException)
        {
            throw new DatabaseFileNotFoundException(_file.FullName);
        }
    }

    public void SaveAll(IList<TDocument> data)
    {
        try
        {
            string jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(_file.ToString() ?? throw new DatabaseFileNotFoundException(nameof(_file)), jsonData);
        }
        catch (FileNotFoundException)
        {
            throw new DatabaseFileNotFoundException(_file.FullName);
        }
    }
}