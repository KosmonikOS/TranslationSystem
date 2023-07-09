using MongoDB.Driver;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Services.Repositories.Abstractions;

public interface IWordsRepository
{
    public IFindFluent<Word,Word> GetWordById(string id);
    public IFindFluent<Word,Word> GetUserWords(long userId,string? word = null);
    public Task<List<T>> GetAndMarkUserNewWordsAsync<T>(long userId, ProjectionDefinition<Word, T> project);
    public Task AddWordAsync(Word word);
    public Task DeleteWordAsync(string word, long userId);
}

