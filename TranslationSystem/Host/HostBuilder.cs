using Microsoft.Extensions.Hosting;

namespace TranslationSystem.Host;

internal static class HostBuilder
{
    public static IHost BuildHost()
    {
        var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(Configurations.ConfigureServices)
            .ConfigureAppConfiguration(Configurations.ConfigureConfiguration);
        return host.Build();
    }
}

