using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Filters;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.PollingManagement.Attributes
{
    public abstract class PollingHandlerAttribute<T> : PollingHandlerAttributeBase, IFilter<Update> where T : PollingHandlerBase
    {
        protected PollingHandlerAttribute(UpdateType updateType, int concurrency = 0)
            : base(typeof(T), updateType, concurrency) { }
    }
}
