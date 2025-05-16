using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Building.Handlers
{
    public abstract class CallbackQueryHandler : AbstractHandler<CallbackQuery>
    {
        public override UpdateType HandlingUpdateType => UpdateType.CallbackQuery;

        protected string TypeData
        {
            get => Input switch
            {
                { Data: { } data } => data,
                { ChatInstance: { } chatInstance } => chatInstance,
                { GameShortName: { } gameShortName } => gameShortName
            };
        }
    }
}
