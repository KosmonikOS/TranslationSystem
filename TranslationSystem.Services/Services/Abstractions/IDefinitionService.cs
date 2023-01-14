namespace TranslationSystem.Services.Services.Abstractions;

public interface IDefinitionService
{
    public Task<string> GetDefinitionAsync(string word);
}

