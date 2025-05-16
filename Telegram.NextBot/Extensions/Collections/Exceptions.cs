using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.NextBot.Extensions.Collections
{
    public class CollectionFrozenException : Exception
    {
        public CollectionFrozenException()
            : base("Can't change a frozen collection.") { }
    }
}
