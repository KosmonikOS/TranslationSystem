using Microsoft.Extensions.Hosting;

namespace TranslationSystem.Host;

internal static class HostBuilder
{
    public static IHost BuildHost()
    {
        var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(ServiceConfiguration.ConfigureServices);
        return host.Build();
    }
}

