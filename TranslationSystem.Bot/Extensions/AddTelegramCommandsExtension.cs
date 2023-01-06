using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Implementations;

namespace TranslationSystem.Bot.Extensions;

public static class AddTelegramCommandsExtension
{
    //All commands need to inherit CommandHandler and be decorated with CommandAttribute
    public static IServiceCollection AddTelegramCommands(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
            assemblies = new Assembly[] { Assembly.GetExecutingAssembly() };
        var types = assemblies.SelectMany(a =>
            a.GetTypes().Where(t => typeof(CommandHandler).IsAssignableFrom(t)
                                      && !t.IsAbstract && !t.IsInterface)).ToList();
        var commands = new CommandCollection();
        foreach (var command in types)
        {
            if (commands.TryAdd(command))
                services.AddTransient(command);
        }

        services.AddSingleton<ICommandCollection>(commands);
        return services;
    }
}

