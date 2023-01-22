using MongoDB.Driver;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Services.Repositories.Abstractions;

public interface IWordsRepository
{
    public IAsyncCursor<Word> GetUserWordsAsync(long userId,string? word);
    public Task AddWordAsync(Word word);
    public Task DeleteWordAsync(string word, long userId);
}

