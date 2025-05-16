using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.NextBot.Hosting.DefaultServices
{
    public class UpdateHandlerSession(ITelegramBotClient botClient, Update update, TelegramBotOptions options, CancellationToken cancellationToken)
    {
        /// <summary>
        /// 
        /// </summary>
        public Update HandlingUpdate { get; private set; } = update;

        /// <summary>
        /// 
        /// </summary>
        public ITelegramBotClient BotClient { get; private set; } = botClient;

        /// <summary>
        /// 
        /// </summary>
        public CancellationToken CancellationToken { get; private set; } = cancellationToken;

        /// <summary>
        /// 
        /// </summary>
        public TelegramBotOptions Options { get; private set; } = options;
    }
}
