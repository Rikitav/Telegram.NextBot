using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Extensions.DependencyInjection;
using Telegram.NextBot.Hosting.DefaultServices;

namespace Telegram.NextBot.Hosting
{
    public class TelegramBotHost : IHost
    {
        private readonly IHost _host;
        private readonly HandlerProvider _handlerProvider;
        private readonly TelegramBotOptions _options;
        private readonly ILogger<TelegramBotHost> _logger;
        private bool _disposed = false;

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        public IServiceProvider Services => _host.Services;

        /// <summary>
        /// 
        /// </summary>
        public HandlerProvider Handlers => _handlerProvider;

        /// <summary>
        /// 
        /// </summary>
        public TelegramBotOptions Options => _options;

        /// <summary>
        /// 
        /// </summary>
        public ILogger<TelegramBotHost> Logger => _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHost"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal TelegramBotHost(IHost host, HandlerProvider handlers)
        {
            _host = host;
            _handlerProvider = handlers;
            _logger = Services.GetRequiredService<ILogger<TelegramBotHost>>();
            _options = Services.GetRequiredService<TelegramBotOptions>();

            Services.GetRequiredService<NextBotUpdateHandler>().PostInitilize(_handlerProvider);

            HostDataContainer dataContainer = Services.GetRequiredService<HostDataContainer>();
            User botUser = Services.GetRequiredService<TelegramBotClient>().GetMe().Result;

            dataContainer.BotUser = botUser;
        }

        public static TelegramBotHostBuilder CreateBuilder()
        {
            TelegramBotHostBuilder builder = new TelegramBotHostBuilder();
            builder.ConfigureTelegramBotDefaults();
            return builder;
        }

        /// <summary>
        /// Starts the host.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await _host.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Stops the host.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await _host.StopAsync(cancellationToken);
        }

        /// <summary>
        /// Disposes the host.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            GC.SuppressFinalize(this);
            _host.Dispose();
            _disposed = true;
        }
    }
}
