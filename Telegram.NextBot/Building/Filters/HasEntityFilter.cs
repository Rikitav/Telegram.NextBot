using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public class HasEntityFilter : Filter<Message>
    {
        private readonly MessageEntityType? _entityType;

        public HasEntityFilter(MessageEntityType entityType)
        {
            _entityType = entityType;
        }

        public override bool CanPass(FilterExecutionContext<Message> context)
        {
            if (context.Input is not { Entities.Length: > 0 })
                return false;

            MessageEntity? messageEntity = FindEntity(context.Input.Entities);
            if (messageEntity != null)
            {
                context.Data.SetDataValue("hasEntity", messageEntity);
                return true;
            }

            return false;
        }

        private MessageEntity? FindEntity(MessageEntity[] entities)
        {
            if (_entityType != null)
            {
                return entities.FirstOrDefault(entity => entity.Type == _entityType);
            }

            return null;
        }
    }
}
