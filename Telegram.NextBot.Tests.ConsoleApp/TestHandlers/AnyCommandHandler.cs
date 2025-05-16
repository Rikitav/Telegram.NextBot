using Telegram.Bot.Types;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [CommandHandler]
    public class AnyCommandHandler : CommandHandler
    {
        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            await Reply("I received something with command '" + Input.Text + "'", cancellationToken: cancellation);
        }
    }
}
