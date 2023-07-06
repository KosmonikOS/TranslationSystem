using System.Net;
using System.Text;
using System.Text.Json;
using TranslationSystem.Domain;
using TranslationSystem.Services.Services.Abstractions;

namespace TranslationSystem.Services;

public class TranslationService : ITranslationService
{
    private readonly IHttpClientFactory httpClientFactory;

    public TranslationService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetTranslationAsync(string word)
    {
        var clinet = httpClientFactory.CreateClient("translations");
        var content = new StringContent(@"{
            ""text"": ""{" + word + @"}"",
            ""source"": ""eng_Latn"",
            ""target"": ""rus_Cyrl""
        }", Encoding.UTF8, "application/json");
        var response = await clinet.PostAsync("/v1/nllb-200-3-3b/translation", content);
        if(!response.IsSuccessStatusCode)
            throw new WebException(response.ReasonPhrase);
        var json = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<GetWordTranslationDto>(json, new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });
        return dto.TranslationText;
    }

}
