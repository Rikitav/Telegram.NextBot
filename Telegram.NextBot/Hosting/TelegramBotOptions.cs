using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Telegram.NextBot.Hosting
{
    public enum HandlerParserOptions
    {
        ExecuteFirstFound,
        ExecuteParallel
    }

    public class TelegramBotOptions
    {
        public HandlerParserOptions HandlerParserOptions { get; set; } = HandlerParserOptions.ExecuteFirstFound;
        public TelegramBotClientOptions ClientOptions { get; set; } = null!;
        public ReceiverOptions ReceiverOptions { get; set; } = null!;
    }
}
