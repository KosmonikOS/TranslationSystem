﻿using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;

namespace TranslationSystem.Bot.Extensions;

public static class ResolveCommandHandlerExtension
{
    public static CommandHandler? ResolveCommandHandler(this IServiceProvider provider, Message message)
    {
        var command = message.Text.Substring(1, message.Text.IndexOf(' ') > 0 ? message.Text.IndexOf(' ') - 1 : message.Text.Length - 1);
        var commands = provider.GetRequiredService<ICommandCollection>();
        var handlerType = commands.GetType(command);
        if(handlerType is null)
            return null;
        return (CommandHandler)provider.GetRequiredService(handlerType);
    }
}

