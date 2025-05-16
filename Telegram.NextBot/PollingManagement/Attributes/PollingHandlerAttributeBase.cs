using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Extensions.DependencyInjection;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.PollingManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class PollingHandlerAttributeBase : Attribute, IFilter<Update>
    {
        public readonly Type ExpectingHandlerType;
        public readonly UpdateType UpdateType;
        public readonly int Concurrency;

        public int Priority
        {
            get;
            set;
        }
        
        protected internal PollingHandlerAttributeBase(Type expectingHandlerType, UpdateType updateType, int concurrency = 0)
        {
            if (expectingHandlerType == null)
                throw new ArgumentNullException(nameof(expectingHandlerType));
            
            if (!expectingHandlerType.IsHandlerType())
                throw new ArgumentException(nameof(expectingHandlerType));

            if (updateType == UpdateType.Unknown)
                throw new Exception();

            ExpectingHandlerType = expectingHandlerType;
            UpdateType = updateType;
            Concurrency = concurrency;
        }

        internal DescriptorIndexer GetIndexer()
            => new DescriptorIndexer(this);

        public abstract bool CanPass(FilterExecutionContext<Update> context);
    }
}
