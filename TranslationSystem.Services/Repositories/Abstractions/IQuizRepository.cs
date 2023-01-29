using MongoDB.Driver;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Services.Repositories.Abstractions;

public interface IQuizRepository
{
    public IFindFluent<UserQuiz,UserQuiz> GetUserQuiz(long userId);
    public Task<string?> AddUserQuizAsync(long userId);
    public Task SetNextWordAsync(long userId,string prevId);
}