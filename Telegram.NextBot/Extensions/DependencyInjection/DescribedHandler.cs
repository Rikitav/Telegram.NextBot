using Telegram.Bot.Types;
using Telegram.NextBot.Hosting.DefaultServices;
using Telegram.NextBot.PollingManagement.Filters;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Extensions.DependencyInjection
{
    public class DescribedHandler
    {
        public PollingHandlerBase HandlerInstance
        {
            get;
            private set;
        }

        public FilterExecutionContext<Update> FilterContext
        {
            get;
            private set;
        }

        public DescribedHandler(PollingHandlerBase handlerInstance, FilterExecutionContext<Update> filterContext)
        {
            HandlerInstance = handlerInstance;
            FilterContext = filterContext;
        }

        public async Task Execute(UpdateHandlerSession session, CancellationToken cancellationToken)
        {
            IHandlerContainer container = HandlerInstance.CreateContainer(session, FilterContext.Data);
            await HandlerInstance.ExecuteInternal(container, cancellationToken);
        }
    }
}
