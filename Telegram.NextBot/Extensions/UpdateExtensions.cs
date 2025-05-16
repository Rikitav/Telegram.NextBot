using Telegram.Bot.Types;

namespace Telegram.NextBot.Extensions
{
    public static class UpdateExtensions
    {
        public static long? GetSenderId(this Update update) => update switch
        {
            { Message.From: { } from } => from.Id,
            { Message.SenderChat: { } chat } => chat.Id,
            { EditedMessage.From: { } from } => from.Id,
            { EditedMessage.SenderChat: { } chat } => chat.Id,
            { ChannelPost.From: { } from } => from.Id,
            { ChannelPost.SenderChat: { } chat } => chat.Id,
            { EditedChannelPost.From: { } from } => from.Id,
            { EditedChannelPost.SenderChat: { } chat } => chat.Id,
            { CallbackQuery.From: { } from } => from.Id,
            { InlineQuery.From: { } from } => from.Id,
            { PollAnswer.User: { } user } => user.Id,
            { PreCheckoutQuery.From: { } from } => from.Id,
            { ShippingQuery.From: { } from } => from.Id,
            { ChosenInlineResult.From: { } from } => from.Id,
            { ChatJoinRequest.From: { } from } => from.Id,
            { ChatMember.From: { } from } => from.Id,
            { MyChatMember.From: { } from } => from.Id,
            _ => null
        };

        public static long? GetChatId(this Update update) => update switch
        {
            { Message.Chat: { } chat } => chat.Id,
            { Message.SenderChat: { } chat } => chat.Id,
            { EditedMessage.Chat: { } chat } => chat.Id,
            { EditedMessage.SenderChat: { } chat } => chat.Id,
            { ChannelPost.Chat: { } chat } => chat.Id,
            { ChannelPost.SenderChat: { } chat } => chat.Id,
            { EditedChannelPost.Chat: { } chat } => chat.Id,
            { EditedChannelPost.SenderChat: { } chat } => chat.Id,
            { CallbackQuery.Message.Chat: { } chat } => chat.Id,
            { ChatJoinRequest.Chat: { } chat } => chat.Id,
            { ChatMember.Chat: { } chat } => chat.Id,
            { MyChatMember.Chat: { } chat } => chat.Id,
            _ => null
        };

        public static object? GetActualUpdateObject(this Update update) => update switch
        {
            { Message: { } message } => message,
            { EditedMessage: { } editedMessage } => editedMessage,
            { ChannelPost: { } channelPost } => channelPost,
            { EditedChannelPost: { } editedChannelPost } => editedChannelPost,
            { BusinessConnection: { } businessConnection } => businessConnection,
            { BusinessMessage: { } businessMessage } => businessMessage,
            { EditedBusinessMessage: { } editedBusinessMessage } => editedBusinessMessage,
            { DeletedBusinessMessages: { } deletedBusinessMessages } => deletedBusinessMessages,
            { MessageReaction: { } messageReaction } => messageReaction,
            { MessageReactionCount: { } messageReactionCount } => messageReactionCount,
            { InlineQuery: { } inlineQuery } => inlineQuery,
            { ChosenInlineResult: { } chosenInlineResult } => chosenInlineResult,
            { CallbackQuery: { } callbackQuery } => callbackQuery,
            { ShippingQuery: { } shippingQuery } => shippingQuery,
            { PreCheckoutQuery: { } preCheckoutQuery } => preCheckoutQuery,
            { PurchasedPaidMedia: { } purchasedPaidMedia } => purchasedPaidMedia,
            { Poll: { } poll } => poll,
            { PollAnswer: { } pollAnswer } => pollAnswer,
            { MyChatMember: { } myChatMember } => myChatMember,
            { ChatMember: { } chatMember } => chatMember,
            { ChatJoinRequest: { } chatJoinRequest } => chatJoinRequest,
            { ChatBoost: { } chatBoost } => chatBoost,
            { RemovedChatBoost: { } removedChatBoost } => removedChatBoost,
            _ => null
        };

        public static T GetActualUpdateObject<T>(this Update update)
        {
            object? actualUpdate = update.GetActualUpdateObject() ?? throw new Exception();
            if (actualUpdate is not T actualCasted)
                throw new Exception();

            return actualCasted;
        }
    }
}
