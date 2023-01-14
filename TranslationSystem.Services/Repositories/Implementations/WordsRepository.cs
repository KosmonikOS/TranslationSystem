using MongoDB.Driver;
using TranslationSystem.Data.Contexts;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem.Services.Repositories.Implementations;

public class WordsRepository : IWordsRepository
{
    private readonly ApplicationContext _context;

    public WordsRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IAsyncCursor<Word> GetUserWordsAsync(string userId)
    {
        var filter = Builders<Word>.Filter.Eq(x => x.UserId, userId);
        return _context.Words.FindSync(filter);
    }

    public async Task AddWordAsync(Word word)
    {
        await _context.Words.InsertOneAsync(word);
    }

    public async Task DeleteWordAsync(string word, string userId)
    {
        var filter = Builders<Word>.Filter.And(
            Builders<Word>.Filter.Eq(x => x.Content, word),
            Builders<Word>.Filter.Eq(x => x.UserId, userId));
        await _context.Words.DeleteOneAsync(filter);
    }
}

