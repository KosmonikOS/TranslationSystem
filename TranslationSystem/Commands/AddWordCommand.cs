using FluentValidation;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Bot.Extensions;
using TranslationSystem.Constants;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Helpers;
using TranslationSystem.Services.Repositories.Abstractions;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Commands
{
    [Command("add")] //Should contain word:string in arguments
    public class AddWordCommand : CommandHandler
    {
        private readonly IValidator<AddWordDto> _validator;
        private readonly IDefinitionService _definitionService;
        private readonly IWordsRepository _wordsRepository;

        public AddWordCommand(IValidator<AddWordDto> validator
            , IDefinitionService definitionService, IWordsRepository wordsRepository)
        {
            _validator = validator;
            _definitionService = definitionService;
            _wordsRepository = wordsRepository;
        }
        public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            var model = GetModelFromArguments<AddWordDto>(message);
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                await client.SendValidationalErrorAsync(chatId, validationResult.Errors.Select(x => x.ErrorMessage));
                return;
            }

            var definition = await _definitionService.GetDefinitionAsync(model.Word);
            var word = new Word()
            {
                Content = model.Word,
                Definition = definition,
                Translation = "",
                UserId = model.UserId
            };

            await _wordsRepository.AddWordAsync(word);

            await client.SendTextMessageAsync(chatId, Messages.AddedMessage 
                + PrintWordsHelper.FormWordMessage(new ShowWordDto{
                    Word = model.Word,
                    Translation = "",
                    Definition = definition
                }),cancellationToken:cancellationToken);
        }
    }
}
