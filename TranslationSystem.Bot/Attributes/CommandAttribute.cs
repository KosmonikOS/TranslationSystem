namespace TranslationSystem.Bot.Attributes;

public class CommandAttribute:Attribute
{
    public string Command { get; }

    public CommandAttribute(string command)
    {
        Command = command;
    }
}

