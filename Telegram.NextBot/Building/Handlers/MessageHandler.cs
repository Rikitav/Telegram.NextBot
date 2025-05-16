using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Building.Handlers
{
    public abstract class MessageHandler : AbstractHandler<Message>
    {
        public override UpdateType HandlingUpdateType => UpdateType.Message;

        protected async Task Reply(
            string text,
            ParseMode parseMode = ParseMode.None,
            ReplyMarkup? replyMarkup = null,
            LinkPreviewOptions? linkPreviewOptions = null,
            int? messageThreadId = null,
            IEnumerable<MessageEntity>? entities = null,
            bool disableNotification = false,
            bool protectContent = false,
            string? messageEffectId = null,
            string? businessConnectionId = null,
            bool allowPaidBroadcast = false,
            CancellationToken cancellationToken = default(CancellationToken))
            => await Client.SendMessage(
                Input.Chat, text, parseMode, Input,
                replyMarkup, linkPreviewOptions,
                messageThreadId, entities,
                disableNotification, protectContent,
                messageEffectId, businessConnectionId,
                allowPaidBroadcast, cancellationToken);

        protected async Task Responce(
            string text,
            ParseMode parseMode = ParseMode.None,
            ReplyParameters? replyParameters = null,
            ReplyMarkup? replyMarkup = null,
            LinkPreviewOptions? linkPreviewOptions = null,
            int? messageThreadId = null,
            IEnumerable<MessageEntity>? entities = null,
            bool disableNotification = false,
            bool protectContent = false,
            string? messageEffectId = null,
            string? businessConnectionId = null,
            bool allowPaidBroadcast = false,
            CancellationToken cancellationToken = default(CancellationToken))
            => await Client.SendMessage(
                Input.Chat, text, parseMode, replyParameters,
                replyMarkup, linkPreviewOptions,
                messageThreadId, entities,
                disableNotification, protectContent,
                messageEffectId, businessConnectionId,
                allowPaidBroadcast, cancellationToken);
    }
}
