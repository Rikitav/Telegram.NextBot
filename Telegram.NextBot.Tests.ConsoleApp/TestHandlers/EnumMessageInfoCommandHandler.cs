using System.Reflection;
using Telegram.Bot.Types;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Filters;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [CommandHandler, CommandAllias("info"), ReplyTo(ReplyType.Message)]
    public class EnumMessageInfoCommandHandler : CommandHandler
    {
        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            foreach (PropertyInfo prop in Input.EnumerateObjectProperties())
            {
                object? propValue = prop.GetValue(Input);
                if (propValue == null)
                    continue;

                string propFormat = string.Format("Name: {0},\nType: {1},\nValue: {2}.", prop.Name, prop.PropertyType, propValue.ToString());
                await Responce(propFormat, cancellationToken: cancellation);
            }
        }
    }
}
