namespace TranslationSystem.Bot.Abstractions;

public interface ITelegramBotClientWithCommands
{
    public Task StartHandlingCommandsAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken,string? errorMessage);
}

