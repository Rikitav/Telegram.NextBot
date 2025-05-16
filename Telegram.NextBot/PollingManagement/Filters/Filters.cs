namespace Telegram.NextBot.PollingManagement.Filters
{
    public class Filter<T> : IFilter<T> where T : class
    {
        private readonly Func<FilterExecutionContext<T>, bool>? FilterFunc;

        public Filter()
            => FilterFunc = null;

        public Filter(Func<FilterExecutionContext<T>, bool> funcFilter)
            => FilterFunc = funcFilter;

        public virtual bool CanPass(FilterExecutionContext<T> context)
            => context.Input != null && FilterFunc != null && FilterFunc(context);

        public static Filter<T> If(Func<FilterExecutionContext<T>, bool> filter)
            => new Filter<T>(filter);

        public static AnyFilter<T> Any()
            => new AnyFilter<T>();

        public Filter<T> Not()
            => new Filter<T>(context => !CanPass(context));

        public AndFilter<T> And(IFilter<T> filter)
            => new AndFilter<T>(this, filter);

        public OrFilter<T> Or(IFilter<T> filter)
            => new OrFilter<T>(this, filter);
    }

    public class AnyFilter<T> : Filter<T> where T : class
    {
        public override bool CanPass(FilterExecutionContext<T> context) => true;
    }

    public class ReverseFilter<T>(IFilter<T> filter) : Filter<T> where T : class
    {
        public override bool CanPass(FilterExecutionContext<T> context) => !filter.CanPass(context);
    }

    internal class FallbackHandlerFilter<T> : IFilter<T> where T : class
    {
        public bool CanPass(FilterExecutionContext<T> context) => true;
    }
}
