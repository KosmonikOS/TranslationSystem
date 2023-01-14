using MongoDB.Driver;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Services.Repositories.Abstractions;

public interface IWordsRepository
{
    public IAsyncCursor<Word> GetUserWordsAsync(string userId);
    public Task AddWordAsync(Word word);
    public Task DeleteWordAsync(string word, string userId);
}

