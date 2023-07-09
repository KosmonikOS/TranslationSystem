using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem;
[Command("exportnew")]
public class ExportNewCommand: CommandHandler
{
    private readonly IWordsRepository _wordsRepository;

    public ExportNewCommand(IWordsRepository wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var userId = message.From.Id;
        var project = Builders<Word>.Projection.Expression<ShowWordDto>(x => new ShowWordDto(){
            Word = x.Content,
            Definition = x.Definition,
            Translation = x.Translation
        });
        var words = await _wordsRepository.GetAndMarkUserNewWordsAsync(userId, project);
        await client.SendTextMessageAsync(message.Chat.Id,FormatWordsHelper.FormatExportingMessage(words));
    }
}
