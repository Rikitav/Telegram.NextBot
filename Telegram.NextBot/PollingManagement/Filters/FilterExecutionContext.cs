using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Extensions.Collections;

namespace Telegram.NextBot.PollingManagement.Filters
{
    public class FilterExecutionContext<T>(UpdateType updateType, T input, HandlerDataDictionary data) where T : class
    {
        public HandlerDataDictionary Data { get; } = data;
        public UpdateType Type { get; } = updateType;
        public T Input { get; } = input;

        public FilterExecutionContext(UpdateType updateType, T input)
            : this(updateType, input, new HandlerDataDictionary()) { }

        public FilterExecutionContext<C> CreateChild<C>(C input) where C : class
            => new FilterExecutionContext<C>(Type, input, Data);
    }
}
