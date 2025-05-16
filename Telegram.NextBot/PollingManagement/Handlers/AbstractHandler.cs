using Telegram.Bot;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Extensions.Collections;
using Telegram.NextBot.Hosting.DefaultServices;

namespace Telegram.NextBot.PollingManagement.Handlers
{
    public abstract class AbstractHandler<T> : PollingHandlerBase where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        protected AbstractHandlerContainer<T> Container { get; private set; } = default!;

        /// <summary>
        /// 
        /// </summary>
        protected ITelegramBotClient Client => Container.Client;

        /// <summary>
        /// 
        /// </summary>
        protected T Input => Container.ActualUpdate;

        /// <summary>
        /// 
        /// </summary>
        protected HandlerDataDictionary ExtraData => Container.ExtraData;

        protected AbstractHandler()
        {
            if (!HandlingUpdateType.IsValidUpdateObject<T>())
                throw new Exception();
        }

        internal override IHandlerContainer CreateContainer(UpdateHandlerSession session, HandlerDataDictionary data)
        {
            return new AbstractHandlerContainer<T>(session.HandlingUpdate, session.BotClient, data);
        }

        internal override async Task ExecuteInternal(IHandlerContainer container, CancellationToken cancellationToken)
        {
            Container = (AbstractHandlerContainer<T>)container;
            await Execute(Container, cancellationToken);
        }

        public abstract Task Execute(AbstractHandlerContainer<T> container, CancellationToken cancellation);
    }
}
