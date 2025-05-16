namespace Telegram.NextBot.PollingManagement.Filters
{
    public class JoinedFilter<T>(params IFilter<T>[] filters) : Filter<T>, IFilter<T> where T : class
    {
        public IFilter<T>[] Filters { get; } = filters;

        public override bool CanPass(FilterExecutionContext<T> context)
        {
            foreach (IFilter<T> filter in Filters)
            {
                if (!filter.CanPass(context))
                    return false;
            }

            return true;
        }
    }

    public class AndFilter<T>(IFilter<T> leftFilter, IFilter<T> rightFilter) : JoinedFilter<T>(leftFilter, rightFilter) where T : class
    {
        public override bool CanPass(FilterExecutionContext<T> context)
            => Filters[0].CanPass(context) && Filters[1].CanPass(context);
    }

    public class OrFilter<T>(IFilter<T> leftFilter, IFilter<T> rightFilter) : JoinedFilter<T>(leftFilter, rightFilter) where T : class
    {
        public override bool CanPass(FilterExecutionContext<T> context)
            => Filters[0].CanPass(context) || Filters[1].CanPass(context);
    }
}
