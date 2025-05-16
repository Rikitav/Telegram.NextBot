using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [MessageHandler, OnChat(ChatType.Supergroup), Text(TextOperation.Equals, "hello")]
    public class GroupMessageHandler : MessageHandler
    {
        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            await Client.SendMessage(Input.Chat, "\"" + (Input.Text ?? "<NULL>") + "\" from group chat!", cancellationToken: cancellation);
        }
    }
}
