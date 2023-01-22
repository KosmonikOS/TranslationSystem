using FluentValidation;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Bot.Extensions;
using TranslationSystem.Domain.Dtos;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Repositories.Abstractions;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Commands
{
    [Command("add")] //Should contain word:string in arguments
    public class AddWordCommand : CommandHandler
    {
        private readonly IValidator<AddWordDto> _validator;
        private readonly ITranslationService _translationService;
        private readonly IDefinitionService _definitionService;
        private readonly IWordsRepository _wordsRepository;
        private const string AddedMessage = "Слово успешно добавлено";

        public AddWordCommand(IValidator<AddWordDto> validator, ITranslationService translationService
            , IDefinitionService definitionService, IWordsRepository wordsRepository)
        {
            _validator = validator;
            _translationService = translationService;
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
            var translation = await _translationService.GetTranslationAsync(model.Word);
            var word = new Word()
            {
                Content = model.Word,
                Definition = definition,
                Translation = translation,
                UserId = model.UserId
            };

            await _wordsRepository.AddWordAsync(word);

            await client.SendTextMessageAsync(chatId, AddedMessage,cancellationToken:cancellationToken);
        }
    }
}
