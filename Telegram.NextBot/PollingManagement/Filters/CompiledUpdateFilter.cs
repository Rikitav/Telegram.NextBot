using Telegram.Bot.Types;

namespace Telegram.NextBot.PollingManagement.Filters
{
    public sealed class CompiledPollingFilter : Filter<Update>
    {
        private readonly Func<Update, object?> _getFilterringTarget;
        private readonly Func<FilterExecutionContext<Update>, object, bool> _filterAction;

        private CompiledPollingFilter(Func<FilterExecutionContext<Update>, object, bool> filterAction, Func<Update, object?> getFilterringTarget)
        {
            _filterAction = filterAction ?? throw new ArgumentNullException("CompiledPollingFilter was initialized incorrectly! \"" + nameof(filterAction) + "\" is null.");
            _getFilterringTarget = getFilterringTarget;
        }

        private static bool CanPassInternal<T>(IList<IFilter<T>> filters, FilterExecutionContext<Update> updateContext, object filterringTarget) where T : class
        {
            FilterExecutionContext<T> context = updateContext.CreateChild((T)filterringTarget);
            return filters.All(f => f.CanPass(context));
        }

        public static CompiledPollingFilter Compile<T>(IList<IFilter<T>> filters, Func<Update, object?> getFilterringTarget) where T : class
        {
            return new CompiledPollingFilter(
                (context, filterringTarget) => CanPassInternal(filters, context, filterringTarget),
                getFilterringTarget);
        }

        public override bool CanPass(FilterExecutionContext<Update> context)
        {
            try
            {
                object? filterringTarget = _getFilterringTarget.Invoke(context.Input);
                if (filterringTarget == null)
                    return false;

                return _filterAction.Invoke(context, filterringTarget);
            }
            catch
            {
                return false;
            }
        }
    }
}
