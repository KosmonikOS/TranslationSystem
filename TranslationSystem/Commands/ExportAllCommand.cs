using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem;
[Command("exportall")]
public class ExportAllCommand: CommandHandler
{
    private readonly IWordsRepository _wordsRepository;

    public ExportAllCommand(IWordsRepository wordsRepository)
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
        var words = await _wordsRepository.GetUserWords(userId)
            .Project(project).ToListAsync();
        await client.SendTextMessageAsync(message.Chat.Id,FormatWordsHelper.FormatExportingMessage(words));
    }
}
