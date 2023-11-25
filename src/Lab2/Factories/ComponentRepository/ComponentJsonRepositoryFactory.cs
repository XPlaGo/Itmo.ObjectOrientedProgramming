using System.IO;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Repository.Driver;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;

namespace Itmo.ObjectOrientedProgramming.Lab2.Factories.ComponentRepository;

public class ComponentJsonRepositoryFactory : IComponentRepositoryFactory
{
    public Repository<TDocument> CreateJsonDocumentRepository<TDocument>(FileInfo file)
    where TDocument : IDocument
    {
        return new Repository<TDocument>(new JsonRepositoryDriver<TDocument>(file));
    }
}