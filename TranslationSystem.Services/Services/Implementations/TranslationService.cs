﻿using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using System.Net;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Services.Services.Implementations;

public class TranslationService:ITranslationService
{
    private readonly IOpenAIService _openAi;

    public TranslationService(IOpenAIService openAi)
    {
        _openAi = openAi;
    }
    public async Task<string> GetTranslationAsync(string word)
    {
        var request = new CompletionCreateRequest()
        {
            Prompt = $"Translate this into Russian:{word}",
            TopP = 1,
            FrequencyPenalty = 0,
            PresencePenalty = 0,
            BestOf = 1,
            Temperature = 0.3f
        };
        var response = await _openAi.Completions.CreateCompletion(request, Models.TextDavinciV3);
        if (!response.Successful)
            throw new WebException(response.Error.Message);
        return response.Choices[0].Text;
    }
}
