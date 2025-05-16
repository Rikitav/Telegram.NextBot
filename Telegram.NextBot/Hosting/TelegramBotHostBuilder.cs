using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Extensions.DependencyInjection;

namespace Telegram.NextBot.Hosting
{
    public class TelegramBotHostBuilder : IHostApplicationBuilder
    {
        private readonly HostApplicationBuilder _hostApplicationBuilder;
        private readonly HandlerCollection _handlers;
        private readonly TelegramBotOptions _options;

        /// <summary>
        /// 
        /// </summary>
        public HandlerCollection Handlers => _handlers;

        /// <summary>
        /// 
        /// </summary>
        public TelegramBotOptions Options => _options;

        /// <inheritdoc/>
        public IServiceCollection Services => _hostApplicationBuilder.Services;

        /// <inheritdoc/>
        public IDictionary<object, object> Properties => ((IHostApplicationBuilder)_hostApplicationBuilder).Properties;

        /// <inheritdoc/>
        public IConfigurationManager Configuration => _hostApplicationBuilder.Configuration;

        /// <inheritdoc/>
        public IHostEnvironment Environment => _hostApplicationBuilder.Environment;

        /// <inheritdoc/>
        public ILoggingBuilder Logging => _hostApplicationBuilder.Logging;

        /// <inheritdoc/>
        public IMetricsBuilder Metrics => _hostApplicationBuilder.Metrics;

        /// <summary>
        /// Initializes a new instance of the <see cref="TelegramBotHostBuilder"/> class.
        /// </summary>
        internal TelegramBotHostBuilder(TelegramBotOptions? options = null)
        {
            // Inner builder
            _hostApplicationBuilder = new HostApplicationBuilder([]);
            _handlers = new HandlerCollection(Services);
            _options = options ?? new TelegramBotOptions();
        }

        /// <summary>
        /// Builds the host.
        /// </summary>
        /// <returns>A custom host instance.</returns>
        public TelegramBotHost Build()
        {
            Services.AddSingleton(Options);

            IHost buildedHost = _hostApplicationBuilder.Build();
            HandlerProvider handlers = Handlers.BuildHandlerProvider(buildedHost.Services, Options);

            return new TelegramBotHost(buildedHost, handlers);
        }

        public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
        {
            _hostApplicationBuilder.ConfigureContainer(factory, configure);
        }
    }
}
