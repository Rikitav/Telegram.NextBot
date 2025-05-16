namespace Telegram.NextBot.PollingManagement.Filters
{
    public interface IFilter<T> where T : class
    {
        public bool CanPass(FilterExecutionContext<T> context);
    }
}
