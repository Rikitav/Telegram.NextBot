using Telegram.Bot.Types;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public enum ReplyType
    {
        Myself,
        Message
    }

    public class ReplyToFilter : Filter<Message>
    {
        private readonly ReplyType _replyType;
        private readonly User? replyedUser;

        public ReplyToFilter(User user)
        {
            replyedUser = user;
        }

        public ReplyToFilter(ReplyType replyType)
        {
            _replyType = replyType;
        }

        public override bool CanPass(FilterExecutionContext<Message> context)
        {
            switch (_replyType)
            {
                case ReplyType.Myself:
                    {
                        if (context.Input is not { ReplyToMessage.From.Id: { } id })
                            return false;

                        User? botUser = context.Data.GetDataValue<User>("BotUser");
                        if (botUser == null)
                            return false;

                        return botUser.Id == id;
                    }

                case ReplyType.Message:
                    {
                        if (context.Input is not { ReplyToMessage: { } replyTo })
                            return false;

                        context.Data.SetDataValue("repliedMessage", replyTo);
                        return true;
                    }

                default:
                    {
                        if (replyedUser != null)
                        {
                            if (context.Input is not { ReplyToMessage.From: { } user })
                                return false;

                            if (user != replyedUser)
                                return false;

                            context.Data.SetDataValue("repliedUser", replyedUser);
                            return true;
                        }

                        return false;
                    }
            }
        }
    }
}
