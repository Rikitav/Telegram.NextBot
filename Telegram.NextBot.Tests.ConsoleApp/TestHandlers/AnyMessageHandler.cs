using Telegram.Bot.Types;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [MessageHandler, Text(TextOperation.Equals, "any", Modifiers = FilterModifier.Inverse)]
    public class AnyMessageHandler : MessageHandler
    {
        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            await Reply("I received a Message", cancellationToken: cancellation);
        }
    }
}
