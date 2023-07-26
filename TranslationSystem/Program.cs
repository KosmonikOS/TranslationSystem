using Microsoft.Extensions.DependencyInjection;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Constants;
using TranslationSystem.Host;

var host = HostBuilder.BuildHost();

var client = host.Services.GetRequiredService<ITelegramBotClientWithCommands>();

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

var tcs = new TaskCompletionSource();

// Capture termination signals to gracefully stop the execution
AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
{
    cancellationTokenSource.Cancel();
    tcs.SetResult();
};

await client.StartHandlingCommandsAsync(host.Services
    , cancellationToken,Messages.UnknownErrorMessage);

await tcs.Task;