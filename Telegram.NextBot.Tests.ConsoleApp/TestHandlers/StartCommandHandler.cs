using Telegram.Bot;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.Building.Attributes;
using Telegram.Bot.Types;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [CommandHandler, CommandAllias("start", "hello")]
    public class StartCommandHandler : CommandHandler
    {
        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            await Responce("Start command hit", cancellationToken: cancellation);
        }
    }
}
