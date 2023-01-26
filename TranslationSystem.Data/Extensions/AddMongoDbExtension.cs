using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using TranslationSystem.Data.Options;

namespace TranslationSystem.Data.Extensions;

public static class AddMongoDbExtension
{
    public static IServiceCollection AddMongoDbContext<TContext>(this IServiceCollection services, Action<MongoDbContextOptions> configureDelegate)
    {
        var options = new MongoDbContextOptions();
        configureDelegate(options);
        Verify(options);
        var client = new MongoClient(options.ConnectionString);
        var isMongoAlive = client.GetDatabase(options.DatabaseName).RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
        if(!isMongoAlive)
            throw new MongoException("Cannot connect to the server");
        var activation = Activator.CreateInstance(typeof(TContext), args: new object[] { client, options.DatabaseName });
        if (activation is TContext context)
            services.AddSingleton(typeof(TContext), context);
        return services;
    }

    private static void Verify(MongoDbContextOptions options)
    {
        if (string.IsNullOrEmpty(options.ConnectionString))
            throw new ArgumentNullException("ConnectionString", "Database connection string is not provided");
        if (string.IsNullOrEmpty(options.DatabaseName))
            throw new ArgumentNullException("DatabaseName", "Database name is not provided");
    }
}

