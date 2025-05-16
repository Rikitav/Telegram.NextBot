using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Extensions.DependencyInjection;

namespace Telegram.NextBot.Extensions.Collections
{
    public sealed class HandlerDescriptorList : IEnumerable<HandlerDescriptor>
    {
        private readonly SortedList<DescriptorIndexer, HandlerDescriptor> _innerColletion;

        public bool IsReadOnly
        {
            get;
            private set;
        }

        public UpdateType UpdateType
        {
            get;
            private set;
        }

        public HandlerDescriptorList(UpdateType updateType)
        {
            _innerColletion = [];
            IsReadOnly = false;
            UpdateType = updateType;
        }

        public void Add(HandlerDescriptor descriptor)
        {
            if (IsReadOnly)
                throw new CollectionFrozenException();

            if (descriptor.UpdateType != UpdateType)
                throw new InvalidOperationException();

            while (_innerColletion.TryGetValue(descriptor.Indexer, out HandlerDescriptor conflictDescriptor))
            {
                int newPriority = conflictDescriptor.Indexer.Priority + (descriptor.Filters.Any() ? 1 : -1);
                descriptor.UpdatePriority(newPriority);
            }

            _innerColletion.Add(descriptor.Indexer, descriptor);
        }

        public bool ContainsKey(DescriptorIndexer indexer)
        {
            return _innerColletion.ContainsKey(indexer);
        }

        public void Freeze()
        {
            IsReadOnly = true;
        }

        public IEnumerator<HandlerDescriptor> GetEnumerator()
        {
            return _innerColletion.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerColletion.Values.GetEnumerator();
        }
    }
}
