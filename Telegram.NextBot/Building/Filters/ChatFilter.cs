using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public class ChatFilter : Filter<Chat>
    {
        public const string ChatDataKeyName = nameof(ChatFilter);
        private readonly Func<Chat, bool> VerifyChat;

        public ChatFilter(ChatType type)
        {
            VerifyChat = (checkChat) =>
                checkChat.Type == type;
        }

        public ChatFilter(string firstName)
        {
            VerifyChat = (checkChat) =>
                checkChat.FirstName != null
                && checkChat.FirstName.Equals(firstName);
        }

        public ChatFilter(long id)
        {
            VerifyChat = (checkChat) =>
                checkChat.Id == id;
        }

        public ChatFilter(ChatType type, string firstName)
        {
            VerifyChat = (checkChat) =>
                checkChat.Type == type
                && checkChat.FirstName != null
                && checkChat.FirstName.Equals(firstName);
        }

        public ChatFilter(ChatType type, long id)
        {
            VerifyChat = (checkChat) =>
                checkChat.Type == type
                && checkChat.Id == id;
        }

        public ChatFilter(Chat chat)
        {
            VerifyChat = (checkChat) =>
                checkChat == chat;
        }

        public override bool CanPass(FilterExecutionContext<Chat> context)
        {
            if (!VerifyChat.Invoke(context.Input))
                return false;

            context.Data.SetDataValue(ChatDataKeyName, context.Input);
            return true;
        }
    }
}
