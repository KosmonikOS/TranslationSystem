using Telegram.Bot.Types;

namespace TranslationSystem.Bot.Extensions;

public static class CallbackQueryExtensions
{
    //Conversion to handle callback queries with command handlers
    public static Message ToMessage(this CallbackQuery callback) 
        => new Message(){
            Text = $"/{callback.Data}",
            From = callback.From,
            Chat = new Chat(){
                Id = callback.From.Id
            }
        };
}