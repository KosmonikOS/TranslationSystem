using System.Net;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Services.Services.Implementations;

public class DefinitionService : IDefinitionService
{
    private readonly IOpenAIService _openAi;

    public DefinitionService(IOpenAIService openAi)
    {
        _openAi = openAi;
    }
    public async Task<string> GetDefinitionAsync(string word)
    {
        var request = new CompletionCreateRequest()
        {
            Prompt = $"Give me a short definition of word: {word}, in maximum 10 words",
            TopP = 1,
            FrequencyPenalty = 0,
            PresencePenalty = 0,
            BestOf = 1,
            Temperature = 0
        };
        var response = await _openAi.Completions.CreateCompletion(request, Models.TextDavinciV3);
        if (!response.Successful)
            throw new WebException(response.Error.Message);
        return response.Choices[0].Text;
    }
}

