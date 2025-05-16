using Telegram.Bot.Types;
using Telegram.NextBot.Extensions.Collections;

namespace Telegram.NextBot.Hosting.DefaultServices
{
    public class HostDataContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public User BotUser { get; internal set; } = null!;

        public HandlerDataDictionary ToHandlerData()
            => new HandlerDataDictionary(new Dictionary<string, object?>()
            {
                { nameof(BotUser), BotUser }
            });
    }
}
