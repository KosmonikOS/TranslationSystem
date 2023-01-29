using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Constants;
using TranslationSystem.Services.Repositories.Abstractions;

namespace   TranslationSystem.Commands;

[Command("next")]
public class NextWordCommand : CommandHandler
{
    private readonly IQuizRepository _quizRepository;
    private readonly IWordsRepository _wordsRepository;

    public NextWordCommand(IQuizRepository quizRepository,IWordsRepository wordsRepository)
    {
        _quizRepository = quizRepository;
        _wordsRepository = wordsRepository;
    }
    public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var userId = message.From.Id;
        var nextWordId = (await _quizRepository.GetUserQuiz(userId).SingleOrDefaultAsync())?.NextWordId;
        if(nextWordId is null)
        {
            nextWordId = await _quizRepository.AddUserQuizAsync(userId);
            if(nextWordId is null)
            {
                await client.SendTextMessageAsync(message.Chat.Id,Messages.NoWordsAddedMessage);
                return;
            }
        }
        var word = await _wordsRepository.GetWordById(nextWordId).Project(x => x.Content).FirstOrDefaultAsync();


        var markup = new InlineKeyboardMarkup(new[] 
        { 
            InlineKeyboardButton.WithCallbackData("Show", $"showbyid {nextWordId}"),
            InlineKeyboardButton.WithCallbackData("Next", "next")
         });

        await client.SendTextMessageAsync(message.Chat.Id,$"Следующее слово: {word}",replyMarkup:markup);
        await _quizRepository.SetNextWordAsync(userId,nextWordId);
    }
}