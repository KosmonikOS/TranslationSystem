using MongoDB.Driver;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Services.Repositories.Abstractions;

public interface IWordsRepository
{
    public IFindFluent<Word,Word> GetUserWords(long userId,string? word = null);
    public Task AddWordAsync(Word word);
    public Task DeleteWordAsync(string word, long userId);
}

