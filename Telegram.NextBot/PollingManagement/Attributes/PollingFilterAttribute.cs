using Telegram.Bot.Types;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.PollingManagement.Attributes
{
    public abstract class PollingFilterAttribute<T> : PollingFilterAttributeBase where T : class
    {
        protected PollingFilterAttribute(Action<PollingFilterBuilder<T>> filterBuilderAction)
        {
            // Building filter
            PollingFilterBuilder<T> builder = new PollingFilterBuilder<T>();
            filterBuilderAction.Invoke(builder);
            CompiledFilter = builder.Compile(GetFilterringTarget);
        }

        protected PollingFilterAttribute(params IFilter<T>[] filters)
            : this(builder => Array.ForEach(filters, f => builder.AddFilter(f))) { } 

        public abstract T? GetFilterringTarget(Update update);
    }
}
