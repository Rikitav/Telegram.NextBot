using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.NextBot.PollingManagement.Filters
{
    public class PollingFilterBuilder<T> where T : class
    {
        protected readonly List<IFilter<T>> Filters = [];

        public PollingFilterBuilder<T> AddFilter(IFilter<T> nodeFilter)
        {
            Filters.Add(nodeFilter);
            return this;
        }

        internal CompiledPollingFilter Compile(Func<Update, T?> getFilterringTarget)
        {
            return CompiledPollingFilter.Compile(Filters, getFilterringTarget);
        }
    }
}
