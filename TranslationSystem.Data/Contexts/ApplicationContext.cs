using MongoDB.Driver;
using TranslationSystem.Data.Abstractions;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Data.Contexts;

public class ApplicationContext : MongoDbContext
{
    private readonly MongoClient _client;
    public ApplicationContext(MongoClient client, string dataBaseName)
        : base(client, dataBaseName)
    {
        _client = client;
    }

    public IMongoCollection<Word> Words => Database.GetCollection<Word>("words");
    public IMongoCollection<UserQuiz> UserQuiz => Database.GetCollection<UserQuiz>("user_quiz");
    public Task<IClientSessionHandle> CreateSessionAsync() => _client.StartSessionAsync();
}

