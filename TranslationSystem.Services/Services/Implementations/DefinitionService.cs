using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TranslationSystem.Domain;
using TranslationSystem.Domain.Models;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Services;

public class DefinitionService : IDefinitionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly MerriamWebsterApi _merriamWebsterApi;

    public DefinitionService(IHttpClientFactory httpClientFactory
        ,IOptions<MerriamWebsterApi> options)
    {
        _httpClientFactory = httpClientFactory;
        _merriamWebsterApi = options.Value;
    }

    public async Task<string> GetDefinitionAsync(string word)
    {
        var client = _httpClientFactory.CreateClient("definitions");
        var response = await client.GetAsync($"/api/v3/references/thesaurus/json/{word}?key={_merriamWebsterApi.ApiKey}");
        if(!response.IsSuccessStatusCode)
            throw new WebException(response.ReasonPhrase);
        var json = await response.Content.ReadAsStringAsync();
        var words = JsonSerializer.Deserialize<GetWordDefinitionDto[]>(json
            , new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        return words.Length != 0 ? string.Join("; ",words.SelectMany(x => x.ShortDef)): "";
    }
}
