using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Constants;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Domain.Validators;
using TranslationSystem.Helpers;
using TranslationSystem.Services.Repositories.Abstractions;

namespace TranslationSystem.Commands;

[Command("showbyid")]
public class ShowWordByIdCommand : CommandHandler
{
    private readonly IWordsRepository _wordsRepository;
    private readonly GetWordByIdDtoValidator _validator;

    public ShowWordByIdCommand(IWordsRepository wordsRepository,GetWordByIdDtoValidator validator)
    {
        _wordsRepository = wordsRepository;
        _validator = validator;
    }
    public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var model = GetModelFromArguments<GetWordByIdDto>(message);
        var validationResult = await _validator.ValidateAsync(model);
        if(!validationResult.IsValid){
            await client.SendTextMessageAsync(message.Chat.Id,Messages.UnknownErrorMessage);
            return;
        }
        var project = Builders<Word>.Projection.Expression<ShowWordDto>(x => new ShowWordDto(){
            Word = x.Content,
            Definition = x.Definition,
            Translation = x.Translation
        });
        var word = await _wordsRepository.GetWordById(model.WordId).Project(project).FirstOrDefaultAsync();
        await client.SendTextMessageAsync(message.Chat.Id,PrintWordsHelper.FormWordMessage(word));
    }
}