using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Telegram.NextBot.Extensions
{
    public static class UpdateTypeExtensions
    {
        public static readonly UpdateType[] MessageTypes =
        [
            UpdateType.Message,
            UpdateType.EditedMessage,
            UpdateType.BusinessMessage,
            UpdateType.EditedBusinessMessage,
            UpdateType.ChannelPost,
            UpdateType.EditedChannelPost
        ];

        public static bool IsUpdateObjectAllowed<T>(this UpdateType[] allowedTypes) where T : class
        {
            return allowedTypes.Any(t => t.IsValidUpdateObject<T>());
        }

        public static bool IsValidUpdateObject<T>(this UpdateType updateType) where T : class
        {
            return typeof(T).Equals(updateType.ReflectUpdateObject());
        }

        public static Type? ReflectUpdateObject(this UpdateType updateType)
        {
            return updateType switch
            {
                UpdateType.Message or UpdateType.EditedMessage or UpdateType.BusinessMessage or UpdateType.EditedBusinessMessage or UpdateType.ChannelPost or UpdateType.EditedChannelPost => typeof(Message),
                UpdateType.MyChatMember => typeof(ChatMemberUpdated),
                UpdateType.ChatMember => typeof(ChatMemberUpdated),
                UpdateType.InlineQuery => typeof(InlineQuery),
                UpdateType.ChosenInlineResult => typeof(ChosenInlineResult),
                UpdateType.CallbackQuery => typeof(CallbackQuery),
                UpdateType.ShippingQuery => typeof(ShippingQuery),
                UpdateType.PreCheckoutQuery => typeof(PreCheckoutQuery),
                UpdateType.Poll => typeof(Poll),
                UpdateType.PollAnswer => typeof(PollAnswer),
                UpdateType.ChatJoinRequest => typeof(ChatJoinRequest),
                UpdateType.MessageReaction => typeof(MessageReactionUpdated),
                UpdateType.MessageReactionCount => typeof(MessageReactionCountUpdated),
                UpdateType.ChatBoost => typeof(ChatBoostUpdated),
                UpdateType.RemovedChatBoost => typeof(ChatBoostRemoved),
                UpdateType.BusinessConnection => typeof(BusinessConnection),
                UpdateType.DeletedBusinessMessages => typeof(BusinessMessagesDeleted),
                UpdateType.PurchasedPaidMedia => typeof(PaidMediaPurchased),
                _ or UpdateType.Unknown => null
            };
        }

        public static UpdateType[] OnlyMessageTypes(this UpdateType[] types)
        {
            return types.Intersect(MessageTypes).ToArray();
        }
    }
}
