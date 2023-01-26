using System.Text;
using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem.Commands;

[Command("showall")]
public class ShowAllCommand : CommandHandler
{
    private readonly IWordsRepository _wordsRepository;

    public ShowAllCommand(IWordsRepository wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }
    public async override Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var userId = message.From.Id;
        var project = Builders<Word>.Projection.Expression<ShowWordDto>(x => new ShowWordDto(){
            Word = x.Content,
            Definition = x.Definition,
            Translation = x.Translation
        });
        var words = await _wordsRepository.GetUserWords(userId)
            .Project(project).ToListAsync();
        await PrintWords(words,client,message.Chat.Id);
    }

    private async Task PrintWords(List<ShowWordDto> words,ITelegramBotClient client,ChatId chatId)
    {
        var sb = new StringBuilder("Список ваших слов:");
        for(var i = 0;i < words.Count;i++){
            sb.Append($"{i+1}{words[i].Word}({words[i].Translation})\n{words[i].Definition}\n");
        }
        await client.SendTextMessageAsync(chatId,sb.ToString());
    }
}
