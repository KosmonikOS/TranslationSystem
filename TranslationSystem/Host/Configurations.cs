using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TranslationSystem.Data.Contexts;
using TranslationSystem.Data.Extensions;

namespace TranslationSystem.Host;

internal static class Configurations
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var mongoDb = context.Configuration.GetSection("MongoDb");
        //Register services here
        services.AddMongoDbContext<ApplicationContext>(options =>
        {
            options.ConnectionString = mongoDb["ConnectionString"];
            options.DatabaseName = mongoDb["DatabaseName"];
        });
    }

    public static void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
    {
        //Add configuration sources here
        builder.AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddJsonFile("configuration.json", true);
    }
}
