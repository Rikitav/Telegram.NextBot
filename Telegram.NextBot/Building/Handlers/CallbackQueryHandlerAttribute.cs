using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Handlers
{
    public sealed class CallbackQueryHandlerAttribute : PollingHandlerAttribute<CallbackQueryHandler>
    {
        private CallbackQueryHandlerAttribute()
            : base(UpdateType.CallbackQuery) { }

        public override bool CanPass(FilterExecutionContext<Update> context) => true;
    }
}
