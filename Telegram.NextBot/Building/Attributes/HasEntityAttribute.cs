using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.Building.Attributes
{
    public class HasEntityAttribute : PollingFilterAttribute<Message>
    {
        public override UpdateType[] AllowedTypes =>
        [
            UpdateType.Message
        ];

        public HasEntityAttribute(MessageEntityType entityType)
            : base(new HasEntityFilter(entityType)) { }

        public override Message? GetFilterringTarget(Update update)
            => update.Message;
    }
}
