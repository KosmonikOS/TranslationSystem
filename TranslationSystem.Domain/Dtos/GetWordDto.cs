using TranslationSystem.Bot.Attributes;

namespace TranslationSystem.Domain.Dtos;

public class GetWordDto
{
    [Argument(1)]
    public string Word { get; set; }
    [UserId]
    public long UserId { get; set; }
}