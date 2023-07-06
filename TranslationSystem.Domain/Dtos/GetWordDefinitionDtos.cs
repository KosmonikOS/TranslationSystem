namespace TranslationSystem.Domain;

public record DefinitionDto(string Definition);
public record MeaningsDto(DefinitionDto[] Definitions);
public record GetWordDefinitionDto(MeaningsDto[] Meanings);