using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Reflection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Extensions.Collections;
using Telegram.NextBot.Hosting;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Filters;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Extensions.DependencyInjection
{
    public class HandlerCollection : IEnumerable<KeyValuePair<UpdateType, HandlerDescriptorList>>
    {
        private readonly Dictionary<UpdateType, HandlerDescriptorList> _innerCollection;
        private readonly IServiceCollection _hostServiceColletion;

        private readonly FilterModifier[] lastingModifiers = [FilterModifier.OrNext];

        public bool IsReadOnly
        {
            get;
            private set;
        }

        public IEnumerable<UpdateType> Keys
        {
            get => _innerCollection.Keys;
        }

        public IEnumerable<HandlerDescriptorList> Values
        {
            get => _innerCollection.Values;
        }

        public HandlerCollection(IServiceCollection hostServiceColletion)
        {
            _innerCollection = [];
            _hostServiceColletion = hostServiceColletion;
        }

        public HandlerProvider BuildHandlerProvider(IServiceProvider hostServiceProvider, TelegramBotOptions options)
        {
            return new HandlerProvider(_innerCollection, hostServiceProvider, options);
        }

        public HandlerCollection AddHandler<T>() where T : PollingHandlerBase
        {
            return AddHandler(typeof(T));
        }

        public HandlerCollection AddHandler(Type handlerType)
        {
            //
            if (!handlerType.IsHandlerType())
                throw new Exception();

            //
            PollingHandlerAttributeBase handlerAttribute = InspectHandletAttribute(handlerType);

            // Getting polling filter attribute
            IFilter<Update>[] filters = InspectFilterAttributes(handlerType, handlerAttribute.UpdateType).ToArray();

            //
            AddHandler(new HandlerDescriptor(handlerType, handlerAttribute, filters));
            return this;
        }

        public void AddHandler(HandlerDescriptor descriptor)
        {
            //
            if (!_innerCollection.TryGetValue(descriptor.UpdateType, out HandlerDescriptorList list))
            {
                list = new HandlerDescriptorList(descriptor.UpdateType);
                _innerCollection.Add(descriptor.UpdateType, list);
            }

            //
            list.Add(descriptor);
            _hostServiceColletion.AddScoped(descriptor.HandlerType);
        }

        private PollingHandlerAttributeBase InspectHandletAttribute(Type handlerType)
        {
            // Getting polling handler attribute
            PollingHandlerAttributeBase[] handlerAttrs = handlerType.GetCustomAttributes<PollingHandlerAttributeBase>().ToArray();

            //
            if (handlerAttrs.Length == 0)
                throw new Exception();

            //
            if (handlerAttrs.Length > 1)
                throw new Exception();

            //
            return handlerAttrs[0];
        }

        private IEnumerable<IFilter<Update>> InspectFilterAttributes(Type handlerType, UpdateType validUpdType)
        {
            //
            IEnumerable<PollingFilterAttributeBase> filters = handlerType.GetCustomAttributes<PollingFilterAttributeBase>();

            //
            IEnumerable<PollingFilterAttributeBase> invalidUpdTypeFilters = filters.Where(filterAttr => !filterAttr.AllowedTypes.Contains(validUpdType));
            if (invalidUpdTypeFilters.Any())
                throw new InvalidOperationException();

            FilterModifier? lastModifier = null;
            Filter<Update>? lastFilter = null;

            foreach (PollingFilterAttributeBase filterAttr in filters)
            {
                FilterModifier currentModifier = filterAttr.Modifiers;
                Filter<Update> currentFilter = filterAttr.CompiledFilter;

                if (filterAttr.Modifiers.HasFlag(FilterModifier.Inverse))
                {
                    currentFilter = currentFilter.Not();
                }

                if (lastModifier != null && lastFilter != null)
                {
                    if (lastModifier.Value.HasFlag(FilterModifier.OrNext))
                    {
                        currentFilter = lastFilter.Or(currentFilter);
                    }
                }

                if (lastingModifiers.Contains(filterAttr.Modifiers))
                {
                    lastFilter = currentFilter;
                    lastModifier = currentModifier;
                    continue;
                }
                else
                {
                    lastFilter = null;
                    lastModifier = null;
                    yield return currentFilter;
                }
            }
        }

        public IEnumerator<KeyValuePair<UpdateType, HandlerDescriptorList>> GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }
    }
}
