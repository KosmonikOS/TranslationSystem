using System.Text;
using System.Text.Json;

namespace TranslationSystem.Domain;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        var sb = new StringBuilder();
        sb.Append(char.ToLower(name[0]));
        for (int i = 1; i < name.Length; i++)
        {
            if(char.IsUpper(name[i]))
            {
                sb.Append('_');
                sb.Append(char.ToLower(name[i]));
            }
            else
            {
                sb.Append(char.ToLower(name[i]));
            }
        }
        return sb.ToString();
    }
}
