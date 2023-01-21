using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Extensions;

namespace TranslationSystem.Bot.Implementations;

public class TelegramBotClientWithCommands : ITelegramBotClientWithCommands
{
    private IServiceProvider _serviceProvider;
    private ITelegramBotClient _client;
    private CancellationToken _cancellationToken;

    public TelegramBotClientWithCommands(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task StartHandlingCommandsAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        _serviceProvider = serviceProvider;
        _cancellationToken = cancellationToken;
        await _client.DeleteWebhookAsync(cancellationToken: _cancellationToken);
        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            cancellationToken: _cancellationToken);
    }
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } messageText } message)
            return;

        using var scope = _serviceProvider.CreateScope();

        await scope.ServiceProvider.ResolveCommandHandler(update.Message).HandleAsync(update.Message,_client,_cancellationToken);
    }
    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

