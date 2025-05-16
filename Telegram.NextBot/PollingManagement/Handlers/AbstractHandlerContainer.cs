using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Extensions.Collections;

namespace Telegram.NextBot.PollingManagement.Handlers
{
    public class AbstractHandlerContainer<T>(Update handlingUpdate, ITelegramBotClient client, HandlerDataDictionary data) : IHandlerContainer where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public Update HandlingUpdate { get; private set; } = handlingUpdate;

        /// <summary>
        /// 
        /// </summary>
        public T ActualUpdate { get; private set; } = handlingUpdate.GetActualUpdateObject<T>();

        /// <summary>
        /// 
        /// </summary>
        public HandlerDataDictionary ExtraData { get; private set; } = data;

        /// <summary>
        /// 
        /// </summary>
        public ITelegramBotClient Client { get; private set; } = client;
    }
}
