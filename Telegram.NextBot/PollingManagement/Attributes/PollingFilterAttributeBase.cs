using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.PollingManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class PollingFilterAttributeBase : Attribute
    {
        private CompiledPollingFilter? _compiledFilter;

        public FilterModifier Modifiers
        {
            get;
            set;
        }

        public abstract UpdateType[] AllowedTypes
        {
            get;
        }

        internal CompiledPollingFilter CompiledFilter
        {
            get => _compiledFilter ?? throw new ArgumentNullException(nameof(CompiledFilter));
            set => _compiledFilter = value ?? throw new ArgumentNullException(nameof(CompiledFilter));
        }

        protected internal PollingFilterAttributeBase()
        {
            if (!AllowedTypes.Any())
                throw new ArgumentException();
        }
    }
}
