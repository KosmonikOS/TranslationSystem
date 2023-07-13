using System.Text;
using TranslationSystem.Domain.Dtos;

namespace TranslationSystem;

public static class FormatWordsHelper
{
    public static string FormatExportingMessage(List<ShowWordDto> words)
    {
        var sb = new StringBuilder("Список слов для экспорта:\n");
        for(var i = 0;i < words.Count;i++){
            sb.Append($"{words[i].Word}, {words[i].Translation} {words[i].Definition}\n");
        }
        return sb.ToString();
    }
}
