using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions.Collections;

namespace Telegram.NextBot.PollingManagement.Handlers
{
    public class BranchingHandlerContainer : IHandlerContainer
    {
        public Update HandlingUpdate { get; private set; }

        public HandlerDataDictionary ExtraData { get; private set; }

        public ITelegramBotClient Client { get; private set; }
    }
}
