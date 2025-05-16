using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Telegram.NextBot.Hosting.DefaultServices
{
    public class HostedUpdateReceiver(TelegramBotClient botClient, NextBotUpdateHandler updateHandler, TelegramBotOptions options) : BackgroundService
    {
        private readonly ITelegramBotClient _client = botClient;
        private readonly ReceiverOptions _receiverOptions = options.ReceiverOptions;
        private readonly NextBotUpdateHandler _updateHandler = updateHandler;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DefaultUpdateReceiver updateReceiver = new DefaultUpdateReceiver(_client, _receiverOptions);
            await updateReceiver.ReceiveAsync(_updateHandler, stoppingToken).ConfigureAwait(false);
        }
    }
}
