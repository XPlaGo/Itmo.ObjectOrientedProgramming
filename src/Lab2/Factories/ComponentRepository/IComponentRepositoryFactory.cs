using System.IO;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;

namespace Itmo.ObjectOrientedProgramming.Lab2.Factories.ComponentRepository;

public interface IComponentRepositoryFactory
{
    public Repository<TDocument> CreateJsonDocumentRepository<TDocument>(FileInfo file)
        where TDocument : IDocument;
}