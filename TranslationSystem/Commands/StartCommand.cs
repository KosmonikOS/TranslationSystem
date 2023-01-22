using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;

namespace TranslationSystem.Commands;

[Command("start")]
public class StartCommand : CommandHandler
{
    private const string HelloMessage = "Данный бот раздработан для улучшения навыков английского языка \n" +
                                        "Вам доступны следующие команды:\n" +
                                        "1. /add (Желаемое слово) - добовляет новое слово\n" +
                                        "2. /showall - показать все доступные слова\n" +
                                        "3. /show (Желаемое слово) - показать конкретное слово\n";
    public override async Task HandleAsync(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        await client.SendTextMessageAsync(chatId, HelloMessage);
    }
}

