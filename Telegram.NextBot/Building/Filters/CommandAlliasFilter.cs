using Telegram.Bot.Types;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public class CommandAlliasFilter(params string[] alliases) : Filter<Message>
    {
        public override bool CanPass(FilterExecutionContext<Message> context)
        {
            string? receiverCommand = context.Data.GetDataValue<string>("ReceivedCommand");
            if (receiverCommand == null)
                return false;

            return alliases.Contains(receiverCommand, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
