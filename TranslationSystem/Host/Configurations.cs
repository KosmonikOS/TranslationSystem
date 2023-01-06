using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace TranslationSystem.Host;

internal static class Configurations
{
    public static void ConfigureServices(HostBuilderContext context,IServiceCollection services)
    {
        //Register services here
    }

    public static void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
    {
        //Add configuration sources here
        builder.AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddJsonFile("configuration.json",true);
    }
}
