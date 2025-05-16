using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public class MessageOfTypeFilter(MessageType type) : Filter<Message>
    {
        public override bool CanPass(FilterExecutionContext<Message> context)
            => context.Input.Type == type;
    }

    public class HasTextFilter() : Filter<Message>
    {
        public override bool CanPass(FilterExecutionContext<Message> context)
            => !string.IsNullOrEmpty(context.Input.Text);
    }

    public class TextContatinsFilter(string content) : Filter<string>
    {
        public override bool CanPass(FilterExecutionContext<string> context)
            => context.Input != null && context.Input.Contains(content);
    }

    public class MessageRegexFilter : RegexFilterBase<Message>
    {
        public MessageRegexFilter(Func<Message, string?> getString, string pattern, RegexOptions regexOptions = default, TimeSpan matchTimeout = default)
            : base(getString, pattern, regexOptions, matchTimeout) { }

        public MessageRegexFilter(Func<Message, string?> getString, Regex regex)
            : base(getString, regex) { }
    }

    public class MessageHasEntity : Filter<Message>
    {
        private readonly Func<Message, MessageEntity, bool> VerifyEntity;

        public MessageHasEntity(MessageEntityType type)
        {
            VerifyEntity = (message, entity) =>
                entity.Type == type;
        }

        public MessageHasEntity(MessageEntityType type, int offset, int length)
        {
            VerifyEntity = (message, entity) =>
                entity.Type == type
                && entity.Offset == offset
                && entity.Length == length;
        }

        public MessageHasEntity(MessageEntityType type, string content)
        {
            VerifyEntity = (message, entity) =>
                entity.Type == type
                && message.Text != null
                && message.Text.Substring(entity.Offset, entity.Length).Equals(content);
        }

        public MessageHasEntity(MessageEntityType type, string content, StringComparison stringComparison)
        {
            VerifyEntity = (message, entity) =>
                entity.Type == type
                && message.Text != null
                && message.Text.Substring(entity.Offset, entity.Length).Equals(content, stringComparison);
        }

        public override bool CanPass(FilterExecutionContext<Message> context)
        {
            if (context.Input.Entities == null)
                return false;

            foreach (MessageEntity entity in context.Input.Entities)
            {
                if (VerifyEntity.Invoke(context.Input, entity))
                    return true;
            }

            return false;
        }
    }
}
