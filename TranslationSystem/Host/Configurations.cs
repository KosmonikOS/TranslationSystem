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

namespace TranslationSystem.Host;

internal static class Configurations
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var mongoDb = context.Configuration.GetSection("MongoDb");
        var openAi = context.Configuration.GetSection("OpenAi");
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
        services.AddTelegramCommands(Assembly.GetExecutingAssembly());
        services.AddCustomServices();
        services.AddCustomRepositories();
        services.AddValidatorsFromAssemblyContaining<AddWordDtoValidator>();
    }

    public static void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
    {
        //Add configuration sources here
        builder.AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddJsonFile("configuration.json", true);
    }
}
