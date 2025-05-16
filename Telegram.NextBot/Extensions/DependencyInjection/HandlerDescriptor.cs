using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Extensions.DependencyInjection
{
    public class HandlerDescriptor
    {
        public Type HandlerType
        {
            get;
            private set;
        }

        public UpdateType UpdateType
        {
            get;
            private set;
        }

        public DescriptorIndexer Indexer
        {
            get;
            private set;
        }

        public IFilter<Update>[] Filters
        {
            get;
            private set;
        }

        public IFilter<Update> ValidateFilter
        {
            get;
            private set;
        }

        public HandlerDescriptor(Type handlerType, PollingHandlerAttributeBase pollingHandlerAttribute, IFilter<Update>[] filters)
        {
            HandlerType = handlerType ?? throw new ArgumentNullException(nameof(handlerType));
            if (pollingHandlerAttribute.ExpectingHandlerType != null && pollingHandlerAttribute.ExpectingHandlerType != handlerType.BaseType)
                throw new ArgumentException();

            UpdateType = pollingHandlerAttribute.UpdateType;
            Indexer = pollingHandlerAttribute.GetIndexer();
            ValidateFilter = pollingHandlerAttribute;
            Filters = filters;
        }

        public bool RunFilters(FilterExecutionContext<Update> filterExecutionContext)
        {
            // Running through all of descriptors filters
            foreach (IFilter<Update> filter in Filters)
            {
                // Executing filter
                if (!filter.CanPass(filterExecutionContext))
                    return false;
            }

            // All passed
            return true;
        }

        public void UpdatePriority(int newPriority)
        {
            Indexer = Indexer.UpdatePriority(newPriority);
        }
    }
}
