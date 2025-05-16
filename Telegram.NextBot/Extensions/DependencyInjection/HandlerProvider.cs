using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Extensions.Collections;
using Telegram.NextBot.Hosting;
using Telegram.NextBot.Hosting.DefaultServices;
using Telegram.NextBot.PollingManagement.Filters;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Extensions.DependencyInjection
{
    public class HandlerProvider
    {
        private readonly ReadOnlyDictionary<UpdateType, HandlerDescriptorList> _innerCollection;
        private readonly IServiceProvider _hostServiceProvider;
        private readonly TelegramBotOptions _options;

        public HandlerProvider(Dictionary<UpdateType, HandlerDescriptorList> services, IServiceProvider hostServiceProvider, TelegramBotOptions options)
        {
            services.Values.ForEach(list => list.Freeze());
            _innerCollection = new ReadOnlyDictionary<UpdateType, HandlerDescriptorList>(services);
            _hostServiceProvider = hostServiceProvider;
            _options = options;
        }

        internal IEnumerable<DescribedHandler> GetHandlers(Update update)
        {
            if (!_innerCollection.TryGetValue(update.Type, out HandlerDescriptorList descriptors))
                yield break;

            HostDataContainer hostData = _hostServiceProvider.GetRequiredService<HostDataContainer>();
            foreach (HandlerDescriptor descriptor in descriptors.Reverse())
            {
                FilterExecutionContext<Update> filterContext = new FilterExecutionContext<Update>(update.Type, update, hostData.ToHandlerData());
                if (!descriptor.ValidateFilter.CanPass(filterContext))
                    continue;

                if (!descriptor.RunFilters(filterContext))
                    continue;

                PollingHandlerBase handlerInstance = (PollingHandlerBase)_hostServiceProvider.GetRequiredService(descriptor.HandlerType);
                yield return new DescribedHandler(handlerInstance, filterContext);

                if (_options.HandlerParserOptions == HandlerParserOptions.ExecuteFirstFound)
                    break;
            }
        }
    }
}
