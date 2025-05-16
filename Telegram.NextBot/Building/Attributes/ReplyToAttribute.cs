using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.Building.Attributes
{
    public class ReplyToAttribute : PollingFilterAttribute<Message>
    {
        public override UpdateType[] AllowedTypes =>
        [
            UpdateType.Message
        ];

        public ReplyToAttribute(ReplyType replyType)
            : base(new ReplyToFilter(replyType)) { }

        public ReplyToAttribute(User repliedUser)
            : base(new ReplyToFilter(repliedUser)) { }

        public override Message? GetFilterringTarget(Update update)
            => update.Message;
    }
}
