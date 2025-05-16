using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.NextBot.Hosting;
using Telegram.NextBot.Hosting.DefaultServices;

namespace Telegram.NextBot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramWebhook(this IServiceCollection services)
        {
            services.AddHttpClient<TelegramBotClient>("tgwebhook").RemoveAllLoggers().AddTypedClient(TypedTelegramBotClientFactory);
            return services;
        }

        public static IServiceCollection AddTelegramReceiver(this IServiceCollection services)
        {
            services.AddHttpClient<TelegramBotClient>("tgreceiver").RemoveAllLoggers().AddTypedClient(TypedTelegramBotClientFactory);
            services.AddSingleton<NextBotUpdateHandler>();
            services.AddHostedService<HostedUpdateReceiver>();
            return services;
        }

        private static TelegramBotClient TypedTelegramBotClientFactory(HttpClient httpClient, IServiceProvider provider)
            => new TelegramBotClient(provider.GetRequiredService<TelegramBotOptions>().ClientOptions, httpClient);
    }
}
