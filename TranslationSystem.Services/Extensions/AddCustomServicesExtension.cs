using Microsoft.Extensions.DependencyInjection;
using TranslationSystem.Services.Services.Abstractions;
using TranslationSystem.Services.Services.Implementations;

namespace TranslationSystem.Services.Extensions
{
    public static class AddCustomServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IDefinitionService, DefinitionService>();
            services.AddScoped<ITranslationService, TranslationService>();
            return services;
        }
    }
}
