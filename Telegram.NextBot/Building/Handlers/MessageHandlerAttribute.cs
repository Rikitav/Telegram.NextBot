using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Handlers
{
    public class MessageHandlerAttribute(int concurrency = 0) : PollingHandlerAttribute<MessageHandler>(UpdateType.Message, concurrency)
    {
        public override bool CanPass(FilterExecutionContext<Update> context) => true;
    }
}
