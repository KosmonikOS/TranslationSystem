using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Extensions;

namespace TranslationSystem.Bot.Implementations;

public class TelegramBotClientWithCommands : ITelegramBotClientWithCommands
{
    private IServiceProvider _serviceProvider;
    private readonly ITelegramBotClient _client;
    private ILogger<ITelegramBotClientWithCommands> _logger;
    private CancellationToken _cancellationToken;
    private string? _errorMessage;

    public TelegramBotClientWithCommands(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task StartHandlingCommandsAsync(IServiceProvider serviceProvider
    , CancellationToken cancellationToken, string? errorMessage)
    {
        _serviceProvider = serviceProvider;
        _cancellationToken = cancellationToken;
        _errorMessage = errorMessage;
        _logger = serviceProvider.GetRequiredService<ILogger<TelegramBotClientWithCommands>>();
        await _client.DeleteWebhookAsync(cancellationToken: _cancellationToken);
        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandleError,
            cancellationToken: _cancellationToken);
    }
    private async Task HandleUpdateAsync(ITelegramBotClient botClient
    , Update update, CancellationToken cancellationToken)
    {
        var message = update.Type is UpdateType.Message ? update.Message: update.CallbackQuery.ToMessage();
        try
        {
        if (update.Type is not (UpdateType.CallbackQuery or UpdateType.Message))
            return;

        using var scope = _serviceProvider.CreateScope();

        var handler = scope.ServiceProvider.ResolveCommandHandler(message);
        if(handler is not null)
            await handler.HandleAsync(message,_client,_cancellationToken);
        }
        catch(Exception ex)
        {
            var task = SendErrorMessageToUserAsync(message,_client,cancellationToken);
            await HandleError(_client,ex,cancellationToken);
            await task;
        }
    }
    private Task HandleError(ITelegramBotClient botClient
    , Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception.ToString());
        return Task.CompletedTask;
    }
    private async Task SendErrorMessageToUserAsync(Message message
    , ITelegramBotClient botClient,CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(_errorMessage))
            return;
        await botClient.SendTextMessageAsync(message.Chat.Id,_errorMessage
                                    , cancellationToken: cancellationToken);
    }
}

