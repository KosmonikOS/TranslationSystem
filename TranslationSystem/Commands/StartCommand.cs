using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;
using TranslationSystem.Constants;

namespace TranslationSystem.Commands;

[Command("start")]
public class StartCommand : CommandHandler
{
    public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        await client.SendTextMessageAsync(chatId, Messages.HelloMessage);
    }
}

