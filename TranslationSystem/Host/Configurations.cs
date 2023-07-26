using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using FluentValidation;
using OpenAI.GPT3.Extensions;
using TranslationSystem.Bot.Extensions;
using TranslationSystem.Data.Contexts;
using TranslationSystem.Data.Extensions;
using TranslationSystem.Domain.Validators;
using TranslationSystem.Services.Extensions;
using Microsoft.Extensions.Logging;
using TranslationSystem.Domain.Models;

namespace TranslationSystem.Host;

internal static class Configurations
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var mongoDb = context.Configuration.GetSection("MongoDb");
        var openAi = context.Configuration.GetSection("OpenAi");
        var telegram = context.Configuration.GetSection("Telegram");
        var nlpcloud = context.Configuration.GetSection("NLPCloud");
        var merriamWebster = context.Configuration.GetSection("MerriamWebster");
        //Register options
        services.Configure<MerriamWebsterApi>(merriamWebster);
        //Register services here
        services.AddMongoDbContext<ApplicationContext>(options =>
        {
            options.ConnectionString = mongoDb["ConnectionString"];
            options.DatabaseName = mongoDb["DatabaseName"];
        });
        services.AddOpenAIService(options =>
        {
            options.ApiKey = openAi["ApiKey"];
        });
        services.AddHttpClient("definitions",(serviceProvider, httpClient) =>
            httpClient.BaseAddress = new Uri("https://dictionaryapi.com"));
        services.AddHttpClient("translations",httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://api.nlpcloud.io");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {nlpcloud["Token"]}");
        });
        services.AddTelegramClientWithCommands(telegram["ApiToken"],Assembly.GetExecutingAssembly());
        services.AddCustomServices();
        services.AddCustomRepositories();
        services.AddValidatorsFromAssemblyContaining<AddWordDtoValidator>();
    }

    public static void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
    {
        //Add configuration sources here
        builder.AddJsonFile("configuration.json", true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());
    }

    public static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.AddConsole();        
    }
}