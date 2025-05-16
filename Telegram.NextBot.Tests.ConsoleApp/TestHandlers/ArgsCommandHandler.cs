using Telegram.Bot.Types;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [CommandHandler, CommandAllias("args")]
    public class ArgsCommandHandler : CommandHandler
    {
        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            string replyFormat = string.Format("Received command with arguments : {0}", string.Join(", ", CommandArguments));
            await Reply(replyFormat, cancellationToken: cancellation);
        }
    }
}
