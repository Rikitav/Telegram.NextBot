using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions.DependencyInjection;

namespace Telegram.NextBot.Hosting.DefaultServices
{
    public class NextBotUpdateHandler : IUpdateHandler
    {
        private readonly ILogger<NextBotUpdateHandler> _logger;
        private readonly TelegramBotOptions _options;

        private HandlerProvider _handlerProvider = null!;

        public NextBotUpdateHandler(TelegramBotOptions options, ILogger<NextBotUpdateHandler> logger)
        {
            _options = options;
            _logger = logger;
        }

        public void PostInitilize(HandlerProvider handlers)
        {
            _handlerProvider = handlers;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogError(exception, "Recieved an Exception");
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Recieved an Update of type {0}", update.Type);
            
            UpdateHandlerSession session = new UpdateHandlerSession(botClient, update, _options, cancellationToken);
            DescribedHandler[] handlers = _handlerProvider.GetHandlers(update).ToArray();

            switch (_options.HandlerParserOptions)
            {
                case HandlerParserOptions.ExecuteFirstFound:
                    {
                        await handlers[0].Execute(session, cancellationToken);
                        break;
                    }

                case HandlerParserOptions.ExecuteParallel:
                    {
                        await Task.WhenAll(handlers.Select(handler => handler.Execute(session, cancellationToken)));
                        break;
                    }
            }
        }
    }
}
