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
    public IFindFluent<Word,Word> GetUserWords(long userId,string? word = null)
    {
        var filter = Builders<Word>.Filter.Eq(x => x.UserId, userId);
        if (word is not null)
            filter = filter & Builders<Word>.Filter.Eq(x => x.Content, word);
        return _context.Words.Find(filter);
    }

    public async Task AddWordAsync(Word word)
    {
        await _context.Words.InsertOneAsync(word);
    }

    public async Task DeleteWordAsync(string word, long userId)
    {
        var filter = Builders<Word>.Filter.And(
            Builders<Word>.Filter.Eq(x => x.Content, word),
            Builders<Word>.Filter.Eq(x => x.UserId, userId));
        await _context.Words.DeleteOneAsync(filter);
    }
}

