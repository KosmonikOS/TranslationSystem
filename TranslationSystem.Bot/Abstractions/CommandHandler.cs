using System.Reflection;
using Telegram.Bot.Types;
using TranslationSystem.Bot.Attributes;

namespace TranslationSystem.Bot.Abstractions;
public abstract class CommandHandler
{
    public abstract Task HandleAsync(Message message);

    public T GetCommandArguments<T>(Message message)
    {
        var type = typeof(T);
        var dto = Activator.CreateInstance(type);
        var properties = type.GetProperties();
        TryAssignUserId(message, properties, dto);
        TryAssignProperties(message, properties, dto);
        return (T)dto;
    }

    private string[] GetCommandArgumentsInternal(string command)
        => command.Substring(command.IndexOf(' ') + 1).Split();

    private void TryAssignUserId<T>(Message message, PropertyInfo[] properties, T dto)
    {
        var userIdInfo = properties.SingleOrDefault(x => x.GetCustomAttribute<UserIdAttribute>() is not null);
        if (userIdInfo is not null)
        {
            userIdInfo.SetValue(dto, message.From.Id);
        }
    }

    private void TryAssignProperties<T>(Message message, PropertyInfo[] properties, T dto)
    {
        var arguments = GetCommandArgumentsInternal(message.Text);
        foreach (var map in properties.Select(x => new { Property = x, Attribute = x.GetCustomAttribute<ArgumentAttribute>() }))
        {
            if (map.Attribute is null) continue;
            if (TryConvert(arguments[map.Attribute.Position - 1], map.Property.PropertyType, out object? value))
                map.Property.SetValue(dto, value);
        }
    }

    private bool TryConvert(string from, Type to, out object? convert)
    {
        convert = default;
        switch (Type.GetTypeCode(to))
        {
            case TypeCode.Int32:
                int intValue;
                if (int.TryParse(from, out intValue))
                    convert = intValue;
                break;
            case TypeCode.Int64:
                int longValue;
                if (int.TryParse(from, out longValue))
                    convert = longValue;
                break;
            case TypeCode.Double:
                double doubleValue;
                if (double.TryParse(from, out doubleValue))
                    convert = doubleValue;
                break;
            case TypeCode.Decimal:
                decimal decimalValue;
                if (decimal.TryParse(from, out decimalValue))
                    convert = decimalValue;
                break;
            case TypeCode.String or TypeCode.Char:
                convert = from;
                break;
        }

        return convert is not null;
    }

}

