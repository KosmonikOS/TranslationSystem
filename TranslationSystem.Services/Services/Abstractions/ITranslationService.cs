namespace TranslationSystem.Services.Services.Abstractions;
public interface ITranslationService
{
    public Task<string> GetTranslationAsync(string word);
}

