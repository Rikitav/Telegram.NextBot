using Telegram.Bot;

namespace Telegram.NextBot.Building.Handlers
{
    public abstract class CommandHandler : MessageHandler
    {
        private string[]? _cmdArgsSplit;

        protected string ReceivedCommand
        {
            get => ExtraData.GetDataValue<string>(nameof(ReceivedCommand)) ?? string.Empty;
        }

        protected string[] CommandArguments
        {
            get
            {
                if (Input.Text is not { Length: > 0 })
                    return [];

                return _cmdArgsSplit ??= Input.Text.Split([" "], StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
            }
        }
    }
}
