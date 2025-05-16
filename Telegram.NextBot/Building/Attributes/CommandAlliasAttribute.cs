using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.Building.Attributes
{
    public class CommandAlliasAttribute : PollingFilterAttribute<Message>
    {
        public override UpdateType[] AllowedTypes => [UpdateType.Message];

        public CommandAlliasAttribute(params string[] alliases)
            : base(new CommandAlliasFilter(alliases)) { }

        public override Message? GetFilterringTarget(Update update) => update.Message;
    }
}
