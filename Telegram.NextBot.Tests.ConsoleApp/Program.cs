using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Hosting;
using Telegram.NextBot.Tests.ConsoleApp.TestHandlers;

namespace Telegram.NextBot.Tests.ConsoleApp
{
    internal class Program
    {
        public static void Main()
        {
            TelegramBotHostBuilder builder = TelegramBotHost.CreateBuilder();

            builder.Configure(options =>
            {
                IConfigurationSection botHostSection = builder.Configuration.GetSection("TelegramBot");
                string botToken = botHostSection.GetValue<string>("Token") ?? throw new ArgumentNullException();

                options.HandlerParserOptions = HandlerParserOptions.ExecuteParallel;
                options.ClientOptions = new TelegramBotClientOptions(botToken);
            });

            builder.Services
                .AddTelegramReceiver();

            builder.Handlers
                .AddHandler<AnyMessageHandler>()
                .AddHandler<AnyCommandHandler>()
                .AddHandler<PrivateMessageHandler>()
                .AddHandler<GroupMessageHandler>()
                .AddHandler<StartCommandHandler>()
                .AddHandler<ArgsCommandHandler>()
                .AddHandler<EnumMessageInfoCommandHandler>()
                .AddHandler<ScriptCommandHandler>()
                .AddHandler<SexHandler>();

            TelegramBotHost telegramBot = builder.Build();
            telegramBot.Run();
        }
    }
}
