using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [MessageHandler, HasEntity(MessageEntityType.Mention), Text(TextOperation.StartWith, "@"), Text(TextOperation.Contains, "секс")]
    public class SexHandler : MessageHandler
    {
        private const string SexStickerFileid = "CAACAgIAAxkBAAIBNmgmLnaSFNoIwv3VxQiYQ0jVAXWZAAJWLAACCC9gShXOyZwiVBvHNgQ";

        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            await Client.SendSticker(Input.Chat, InputFile.FromString(SexStickerFileid), Input, cancellationToken: cancellation);
        }
    }
}
