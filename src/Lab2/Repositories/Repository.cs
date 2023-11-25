using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Repository.Driver;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class Repository<TDocument> : IRepository<TDocument>
    where TDocument : IDocument
{
    private readonly IRepositoryDriver<TDocument> _driver;
    private readonly IList<TDocument> _data;

    public Repository(IRepositoryDriver<TDocument> driver)
    {
        _driver = driver;
        _data = _driver.GetAll();
    }

    public TDocument GetById(DocumentId id)
    {
        TDocument document =
            (from doc in _data
                where doc.Id.Equals(id)
                select doc).First();

        return document;
    }

    public TDocument GetByRef(DocumentRef reference)
    {
        TDocument document =
            (from doc in _data
                where doc.Id.Equals(reference.Id)
                select doc).First();

        return document;
    }

    public void Add(TDocument document)
    {
        _data.Add(document);
    }

    public void Backup()
    {
        _driver.SaveAll(_data);
    }
}