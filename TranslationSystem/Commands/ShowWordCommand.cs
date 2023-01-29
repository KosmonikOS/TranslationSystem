using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Bot.Extensions;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Domain.Validators;
using TranslationSystem.Helpers;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem.Commands;

[Command("show")]
public class ShowWordCommand : CommandHandler
{
    private readonly IWordsRepository _wordsRepository;
    private readonly GetWordDtoValidator _validator;

    public ShowWordCommand(IWordsRepository wordsRepository,GetWordDtoValidator validator)
    {
        _wordsRepository = wordsRepository;
        _validator = validator;
    }
    public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        var model = GetModelFromArguments<GetWordDto>(message);
        var validationResult = await _validator.ValidateAsync(model);
        if(!validationResult.IsValid)
        {
            await client.SendValidationalErrorAsync(chatId,validationResult.Errors.Select(x => x.ErrorMessage));
            return;
        }
        var project = Builders<Word>.Projection.Expression<ShowWordDto>(x => new ShowWordDto(){
            Word = x.Content,
            Definition = x.Definition,
            Translation = x.Translation
        });
        var word = await _wordsRepository.GetUserWords(model.UserId,model.Word)
            .Project(project).FirstOrDefaultAsync();
        await client.SendTextMessageAsync(message.Chat.Id,PrintWordsHelper.FormWordMessage(word));
    }
}