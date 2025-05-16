using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Handlers
{
    public class CommandHandlerAttribute() : PollingHandlerAttribute<CommandHandler>(UpdateType.Message, 1)
    {
        public override bool CanPass(FilterExecutionContext<Update> context)
        {
            if (context.Input.Message is not { Entities.Length: > 0, Text.Length: > 0 } message)
                return false;

            MessageEntity commandEntity = message.Entities[0];
            if (commandEntity.Type != MessageEntityType.BotCommand)
                return false;

            string commandSubstring = message.Text.Substring(commandEntity.Offset + 1, commandEntity.Length - 1);
            context.Data.SetDataValue("ReceivedCommand", commandSubstring);
            return true;
        }
    }
}
