using Telegram.Bot.Types;

namespace TranslationSystem.Bot.Abstractions;
public abstract class CommandHandler
{
    public abstract void Handle(Message message);

    public string[] GetCommandArguments(Message message)
        => message.Text.Substring(message.Text.IndexOf(' ') + 1).Split();
}

