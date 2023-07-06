using Microsoft.Extensions.DependencyInjection;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Host;

var host = HostBuilder.BuildHost();

var client = host.Services.GetRequiredService<ITelegramBotClientWithCommands>();

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

var exitEvent = new ManualResetEvent(false);

// Capture termination signals to gracefully stop the execution
AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
{
    cancellationTokenSource.Cancel();
    exitEvent.Set();
};

await client.StartHandlingCommandsAsync(host.Services, cancellationToken);

exitEvent.WaitOne();