using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslationSystem.Domain.Dtos;

namespace TranslationSystem.Helpers;

public static class PrintWordsHelper
{
    public static async Task SendPrintedWordsAsync(List<ShowWordDto> words,ITelegramBotClient client,ChatId chatId)
    {
        var sb = new StringBuilder("Список ваших слов:");
        for(var i = 0;i < words.Count;i++){
            sb.Append($"{i+1}{words[i].Word}({words[i].Translation})\n{words[i].Definition}\n");
        }
        await client.SendTextMessageAsync(chatId,sb.ToString());
    }
}