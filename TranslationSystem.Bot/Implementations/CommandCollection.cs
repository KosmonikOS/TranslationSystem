using System.Reflection;
using TranslationSystem.Bot.Abstractions;
using TranslationSystem.Bot.Attributes;

namespace TranslationSystem.Bot.Implementations;

internal class CommandCollection : ICommandCollection
{
    private Dictionary<string, Type> _commands = new();
    public bool TryAdd(Type type)
    {
        var command = type.GetCustomAttribute<CommandAttribute>();
        if (command is null) return false;
        _commands.Add(command.Command, type);
        return true;
    }

    public Type? GetType(string command) => _commands.GetValueOrDefault(command);
}
