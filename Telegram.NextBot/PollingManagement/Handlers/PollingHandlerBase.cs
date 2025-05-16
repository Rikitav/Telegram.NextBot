using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Extensions.Collections;
using Telegram.NextBot.Hosting.DefaultServices;

namespace Telegram.NextBot.PollingManagement.Handlers
{
    public abstract class PollingHandlerBase
    {
        public abstract UpdateType HandlingUpdateType { get; } 

        internal abstract IHandlerContainer CreateContainer(UpdateHandlerSession session, HandlerDataDictionary data);
        internal abstract Task ExecuteInternal(IHandlerContainer container, CancellationToken cancellationToken);
    }
}
