using MongoDB.Driver;
using TranslationSystem.Data.Abstractions;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Data.Contexts;

public class ApplicationContext : MongoDbContext
{
    public ApplicationContext(MongoClient client, string dataBaseName)
        : base(client, dataBaseName) { }

}

