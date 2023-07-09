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

    public TelegramBotClientWithCommands(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task StartHandlingCommandsAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        _serviceProvider = serviceProvider;
        _cancellationToken = cancellationToken;
        _logger = serviceProvider.GetRequiredService<ILogger<TelegramBotClientWithCommands>>();
        await _client.DeleteWebhookAsync(cancellationToken: _cancellationToken);
        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandleErrorAsync,
            cancellationToken: _cancellationToken);
    }
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
        if (update.Type is not (UpdateType.CallbackQuery or UpdateType.Message))
            return;

        var message = update.Type is UpdateType.Message ? update.Message: update.CallbackQuery.ToMessage();

        using var scope = _serviceProvider.CreateScope();

        var handler = scope.ServiceProvider.ResolveCommandHandler(message);
        if(handler is not null)
            await handler.HandleAsync(message,_client,_cancellationToken);
        }
        catch(Exception ex)
        {
            await HandleErrorAsync(_client,ex,cancellationToken);
        }
    }
    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception.ToString());
        return Task.CompletedTask;
    }
}

