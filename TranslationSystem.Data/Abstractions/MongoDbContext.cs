using MongoDB.Driver;

namespace TranslationSystem.Data.Abstractions;

public abstract class MongoDbContext
{
    private readonly MongoClient _client;
    private readonly string _dataBaseName;
    public MongoDbContext(MongoClient client, string dataBaseName)
    {
        _client = client;
        _dataBaseName = dataBaseName;
    }

    public IMongoDatabase Database => _client.GetDatabase(_dataBaseName);
}

