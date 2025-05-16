using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.Extensions.DependencyInjection
{
    public readonly struct DescriptorIndexer(int concurrency, int priority) : IComparable<DescriptorIndexer>
    {
        public readonly int Concurrency = concurrency;
        public readonly int Priority = priority;

        public DescriptorIndexer(PollingHandlerAttributeBase pollingHandler)
            : this(pollingHandler.Concurrency, pollingHandler.Priority) { }

        public DescriptorIndexer UpdatePriority(int priority)
            => new DescriptorIndexer(Concurrency, priority);

        public int CompareTo(DescriptorIndexer other)
        {
            int concurrencyCmp = Concurrency.CompareTo(other.Concurrency);
            return concurrencyCmp != 0 ? concurrencyCmp : Priority.CompareTo(other.Priority);
        }
    }
}
