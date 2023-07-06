using System.Net;
using System.Text.Json;
using TranslationSystem.Domain;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Services;

public class DefinitionService : IDefinitionService
{
    private readonly IHttpClientFactory httpClientFactory;

    public DefinitionService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetDefinitionAsync(string word)
    {
        var client = httpClientFactory.CreateClient("definitions");
        var response = await client.GetAsync($"/api/v2/entries/en/{word}");
        if(response.StatusCode == HttpStatusCode.NotFound)
                return "";
        if(!response.IsSuccessStatusCode)     
            throw new WebException(response.ReasonPhrase);
        var json = await response.Content.ReadAsStringAsync();
        var words = JsonSerializer.Deserialize<GetWordDefinitionDto[]>(json
            , new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        return words[0].Meanings[0].Definitions[0].Definition;
    }
}
