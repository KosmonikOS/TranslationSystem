using Microsoft.Extensions.DependencyInjection;
using TranslationSystem.Services.Repositories.Abstractions;
using TranslationSystem.Services.Repositories.Implementations;

namespace TranslationSystem.Services.Extensions
{
    public static class AddCustomRepositoriesExtension
    {
        public static IServiceCollection AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWordsRepository, WordsRepository>();
            services.AddScoped<IQuizRepository,QuizRepository>();
            return services;
        }
    }
}
