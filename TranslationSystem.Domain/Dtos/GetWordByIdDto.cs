using TranslationSystem.Bot.Attributes;

namespace TranslationSystem.Domain.Dtos;

public class GetWordByIdDto
{
    [Argument(1)]
    public string WordId { get; set; }
}