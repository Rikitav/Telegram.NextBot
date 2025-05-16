using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Attributes;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Attributes
{
    public class BatchFilterAttribute : PollingFilterAttribute<Update>
    {
        public override UpdateType[] AllowedTypes => Update.AllTypes;

        public BatchFilterAttribute(params IFilter<Update>[] filters)
            : base(filters) { }

        public override Update? GetFilterringTarget(Update update)
            => update;
    }
}
