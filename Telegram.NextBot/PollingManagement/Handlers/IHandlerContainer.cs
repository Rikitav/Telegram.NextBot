using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions.Collections;

namespace Telegram.NextBot.PollingManagement.Handlers
{
    public interface IHandlerContainer
    {
        public Update HandlingUpdate { get; }
        public HandlerDataDictionary ExtraData { get; }
        public ITelegramBotClient Client { get; }
    }
}
