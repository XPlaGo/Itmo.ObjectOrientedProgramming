using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Repository.Driver;

public interface IRepositoryDriver<TDocument>
    where TDocument : IDocument
{
    public IList<TDocument> GetAll();
    public void SaveAll(IList<TDocument> data);
}