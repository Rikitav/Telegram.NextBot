using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.Building.Attributes
{
    public class OnChatAttribute : PollingFilterAttribute<Chat>
    {
        public override UpdateType[] AllowedTypes =>
        [
            UpdateType.Message,
            UpdateType.EditedMessage,
            UpdateType.ChannelPost,
            UpdateType.EditedChannelPost,
            UpdateType.BusinessMessage,
            UpdateType.EditedBusinessMessage,
            UpdateType.MessageReaction,
            UpdateType.MessageReactionCount,
            UpdateType.PollAnswer,
            UpdateType.MyChatMember,
            UpdateType.ChatMember,
            UpdateType.ChatJoinRequest,
            UpdateType.ChatBoost,
            UpdateType.RemovedChatBoost
        ];

        public OnChatAttribute(ChatType chatType) : base(new ChatFilter(chatType)) { }
        public OnChatAttribute(string firstName) : base(new ChatFilter(firstName)) { }
        public OnChatAttribute(long id) : base(new ChatFilter(id)) { }
        public OnChatAttribute(ChatType chatType, string firstName) : base(new ChatFilter(chatType, firstName)) { }
        public OnChatAttribute(ChatType chatType, long id) : base(new ChatFilter(chatType, id)) { }
        public OnChatAttribute(Chat chat) : base(new ChatFilter(chat)) { }

        public override Chat? GetFilterringTarget(Update update)
        {
            return update switch
            {
                { Message: { } message }                    => message.Chat,
                { EditedMessage: { } message }              => message.Chat,
                { ChannelPost: { } message }                => message.Chat,
                { EditedChannelPost: { } message }          => message.Chat,
                { BusinessMessage: { } message }            => message.Chat,
                { EditedBusinessMessage: { } message }      => message.Chat,
                { MessageReaction: { } reaction }           => reaction.Chat,
                { MessageReactionCount: { } reactionCount } => reactionCount.Chat,
                { PollAnswer: { } pollAnswer }              => pollAnswer.VoterChat,
                { MyChatMember: { } chatMemberUpdated }     => chatMemberUpdated.Chat,
                { ChatMember: { } chatMember }              => chatMember.Chat,
                { ChatJoinRequest: { } chatMember }         => chatMember.Chat,
                { ChatBoost: { } chatBoost }                => chatBoost.Chat,
                { RemovedChatBoost: { } removedChatBoost }  => removedChatBoost.Chat,
                _ => null
            };
        }
    }
}
