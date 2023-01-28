using Microsoft.Extensions.DependencyInjection;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Host;

var host = HostBuilder.BuildHost();

var clinet = host.Services.GetRequiredService<ITelegramBotClientWithCommands>();

var cancellationToken = new CancellationTokenSource();

await clinet.StartHandlingCommandsAsync(host.Services,cancellationToken.Token);

Console.ReadLine();

cancellationToken.Cancel();