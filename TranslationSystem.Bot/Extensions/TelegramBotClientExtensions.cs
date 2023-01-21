using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TranslationSystem.Bot.Extensions;

public static class TelegramBotClientExtensions
{
    public static async Task SendValidationalErrorAsync(this ITelegramBotClient client, ChatId chatId, IEnumerable<string> errors)
    {
        await client.SendTextMessageAsync(chatId, BuildErrorMessage(errors));
    }

    private static string BuildErrorMessage(IEnumerable<string> errors)
    {
        var sb = new StringBuilder("Некорректные аргументы:\n");

        foreach (var error in errors)
        {
            sb.Append(error);
            sb.Append('\n');
        }

        return sb.ToString();
    }
}

