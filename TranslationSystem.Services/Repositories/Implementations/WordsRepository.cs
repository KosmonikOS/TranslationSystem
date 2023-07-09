using MongoDB.Driver;
using TranslationSystem.Data.Contexts;
using TranslationSystem.Domain.Dtos;
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

    public IFindFluent<Word, Word> GetWordById(string id)
    {
        var filter = Builders<Word>.Filter.Eq(x => x.Id,id);
        return GetWordsInternal(filter);
    }

    public IFindFluent<Word,Word> GetUserWords(long userId,string? word = null)
    {
        var filter = Builders<Word>.Filter.Eq(x => x.UserId, userId);
        if (word is not null)
            filter &= Builders<Word>.Filter.Eq(x => x.Content, word);
        return GetWordsInternal(filter);
    }

    public async Task<List<T>> GetAndMarkUserNewWordsAsync<T>(long userId
        ,ProjectionDefinition<Word,T> project)
    {
        using var session = await _context.CreateSessionAsync();
        try
        {
            session.StartTransaction();
            var filter = Builders<Word>.Filter.Eq(x => x.UserId, userId) 
            & Builders<Word>.Filter.Where(x => !x.IsAlreadyExported);
            var words = await GetWordsInternal(filter).Project(project).ToListAsync();
            var update = Builders<Word>.Update.Set(x => x.IsAlreadyExported,true);
            await _context.Words.UpdateManyAsync(filter, update);
            await session.CommitTransactionAsync();
            return words;
        }
        catch (MongoException)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public Task AddWordAsync(Word word)
    {
         return _context.Words.InsertOneAsync(word);
    }

    public Task DeleteWordAsync(string word, long userId)
    {
        var filter = Builders<Word>.Filter.Eq(x => x.Content, word) 
        & Builders<Word>.Filter.Eq(x => x.UserId, userId);
        return _context.Words.DeleteOneAsync(filter);
    }

    private IFindFluent<Word,Word> GetWordsInternal(FilterDefinition<Word> filter) => _context.Words.Find(filter);
}

