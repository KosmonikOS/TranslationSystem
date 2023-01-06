namespace TranslationSystem.Bot.Abstractions;
internal interface ICommandCollection
{
    public bool TryAdd(Type type);
    public Type? GetType(string command);
}

