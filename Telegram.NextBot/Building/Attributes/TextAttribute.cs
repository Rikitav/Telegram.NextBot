using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.Building.Attributes
{
    public class TextAttribute : PollingFilterAttribute<string>
    {
        public override UpdateType[] AllowedTypes =>
        [
            UpdateType.Message,
            UpdateType.EditedMessage,
            UpdateType.ChannelPost,
            UpdateType.EditedChannelPost,
            UpdateType.BusinessMessage,
            UpdateType.EditedBusinessMessage
        ];

        public TextAttribute(TextOperation operation, string content)
            : base(new TextFilter(operation, content)) { }

        public override string? GetFilterringTarget(Update update)
        {
            return update switch
            {
                { Message: { } message }               => message.Text,
                { EditedMessage: { } message }         => message.Text,
                { ChannelPost: { } message }           => message.Text,
                { EditedChannelPost: { } message }     => message.Text,
                { BusinessMessage: { } message }       => message.Text,
                { EditedBusinessMessage: { } message } => message.Text,
                _ => null
            };
        }
    }
}
