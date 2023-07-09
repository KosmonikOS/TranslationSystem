using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TranslationSystem.Host;

internal static class HostBuilder
{
    public static IHost BuildHost()
    {
        var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureLogging(Configurations.ConfigureLogging)
            .ConfigureServices(Configurations.ConfigureServices)
            .ConfigureAppConfiguration(Configurations.ConfigureConfiguration);
        return host.Build();
    }
}

