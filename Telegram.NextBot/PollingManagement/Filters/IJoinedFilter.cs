namespace Telegram.NextBot.PollingManagement.Filters
{
    public interface IPollingFilter<T> : IFilter<T> where T : class
    {
        public IFilter<T>[] Filters { get; }
    }
}
