using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public enum TextOperation
    {
        Equals,
        Contains,
        StartWith,
        EndsWith,
        NotNull
    }

    public class TextFilter(TextOperation operation, string content) : Filter<string>
    {
        private readonly string Context = content;
        private readonly TextOperation Operation = operation;

        public override bool CanPass(FilterExecutionContext<string> context)
        {
            switch (Operation)
            {
                case TextOperation.Equals:
                    return context.Input.Equals(Context);

                case TextOperation.Contains:
                    return context.Input.Contains(Context);

                case TextOperation.NotNull:
                    return !string.IsNullOrEmpty(context.Input);

                case TextOperation.StartWith:
                    return context.Input.StartsWith(Context);

                case TextOperation.EndsWith:
                    return context.Input.EndsWith(Context);
                
                default:
                    return false;
            }
        }
    }
}
