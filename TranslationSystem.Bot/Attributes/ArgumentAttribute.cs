namespace TranslationSystem.Bot.Attributes;

public class ArgumentAttribute:Attribute
{
    public int Position { get; }

    public ArgumentAttribute(int position)
    {
        Position = position;
    }
}

