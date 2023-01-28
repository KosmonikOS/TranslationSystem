using System.Text;
using TranslationSystem.Domain.Dtos;

namespace TranslationSystem.Helpers;

public static class PrintWordsHelper
{
    private const string NotFoundMessage = "Слово не найдено";
    public static string FormWordsListMessage(List<ShowWordDto> words)
    {
        if(words.Count == 0) return NotFoundMessage;
        var sb = new StringBuilder("Список ваших слов:");
        for(var i = 0;i < words.Count;i++){
            sb.Append($"{i+1}{words[i].Word}({words[i].Translation})\n{words[i].Definition}\n");
        }
        return sb.ToString();
    }
}