using MongoDB.Driver;
using TranslationSystem.Data.Contexts;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem.Services.Repositories.Implementations;

public class QuizRepository : IQuizRepository
{
    private readonly ApplicationContext _context;

    public QuizRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IFindFluent<UserQuiz, UserQuiz> GetUserQuiz(long userId)
    {
        var filter = Builders<UserQuiz>.Filter.Eq(x => x.UserId,userId);
        return _context.UserQuiz.Find(filter);
    }
    public async Task<string?> AddUserQuizAsync(long userId)
    {
        var firstWordId = await GetNextWordIdAsync(userId);
        if(firstWordId is null) return null;

        var userQuiz = new UserQuiz(){
          UserId = userId,
          NextWordId = firstWordId
        };
        await _context.UserQuiz.InsertOneAsync(userQuiz);
        return firstWordId;
    }
    public async Task SetNextWordAsync(long userId,string prevId)
    {
        var quizFilter = Builders<UserQuiz>.Filter.Eq(x => x.UserId,userId);
        var nextId = await GetNextWordIdAsync(userId,prevId);
        if(nextId is null)        
            nextId = await GetNextWordIdAsync(userId);
        var update = Builders<UserQuiz>.Update.Set(x => x.NextWordId,nextId);
        await _context.UserQuiz.UpdateOneAsync(quizFilter,update);
    }

    private async Task<string?> GetNextWordIdAsync(long userId,string? prev = null)
    {
        var filter = Builders<Word>.Filter.Eq(x => x.UserId,userId);
        if(prev is not null)
            filter = filter & Builders<Word>.Filter.Gt(x => x.Id,prev);
        return (await _context.Words.Find(filter).SortBy(x => x.Id).FirstOrDefaultAsync())?.Id;
    }
}