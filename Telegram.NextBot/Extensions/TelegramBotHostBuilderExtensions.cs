using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Polling;
using Telegram.NextBot.Extensions.DependencyInjection;
using Telegram.NextBot.Hosting;
using Telegram.NextBot.Hosting.DefaultServices;

namespace Telegram.NextBot.Extensions
{
    public static class TelegramBotHostBuilderExtensions
    {
        /// <summary>
        /// Configures the default services required for the custom host
        /// </summary>
        /// <param name="builder">The host application builder</param>
        /// <returns>The host application builder</returns>
        public static TelegramBotHostBuilder ConfigureTelegramBotDefaults(this TelegramBotHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // Add required services
            builder.Services.AddOptions();
            
            // Add logging
            builder.Services.AddLogging(builder => builder.AddConsole());

            // Add configuration services
            builder.Services.AddSingleton(builder.Configuration);

            builder.Services.AddSingleton(new HostDataContainer());

            // Add host options
            builder.Services.Configure<HostOptions>(options =>
            {
                options.ShutdownTimeout = TimeSpan.FromSeconds(10);
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
            });

            return builder;
        }

        public static TelegramBotHostBuilder SetAllowedUpdates(this TelegramBotHostBuilder builder)
        {
            ReceiverOptions receiverOptions = builder.Options.ReceiverOptions;
            receiverOptions.AllowedUpdates = builder.Handlers.Keys.ToArray();
            return builder;
        }

        public static TelegramBotHostBuilder Configure(this TelegramBotHostBuilder builder, Action<TelegramBotOptions> configureAction)
        {
            configureAction.Invoke(builder.Options);

            if (builder.Options.ReceiverOptions == null)
                builder.Options.ReceiverOptions = new ReceiverOptions();

            if (builder.Options.ClientOptions == null)
                throw new ArgumentNullException();

            return builder;
        }
    }
}
