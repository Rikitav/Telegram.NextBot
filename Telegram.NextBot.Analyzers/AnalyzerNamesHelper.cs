using Microsoft.CodeAnalysis.CSharp.Syntax;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Handlers;

namespace Telegram.NextBot.Analyzers
{
    internal static class AnalyzerNamesHelper
    {
        public static readonly string AspectDeclareNamespace = "Telegram.NextBot.PollingManagement.Attributes.";

        public static IEnumerable<string> AttributeNamesList
        {
            get => UpdateHandlerAttributes.Concat(UpdateHandlerShortAttributes);
        }

        public static readonly string[] UpdateHandlerAttributes =
        [
            "UpdateHandlerAttribute",
            "BotCommandHandlerAttribute",
            "MessageHandlerAttribute",
            "CallbackQueryHandlerAttribute"
        ];

        public static readonly string[] UpdateHandlerShortAttributes =
        [
            "MessageHandler",
            "CallbackQueryHandler"
        ];

        public static string GetInterface(string attributeName)
        {
            return attributeName.Replace("HandlerAttribute", string.Empty) switch
            {
                "Update" => "IGeneralUpdateHandler",
                "BotCommand" => "IBotCommandHandler",
                "CallbackQuery" => "ICallbackQueryHandler",
                "Message" => "IMessageHandler",
                _ => string.Empty,
            };
        }

        public static UpdateType GetHandlerAttributeUpdateType(this AttributeSyntax attributeSyntax)
        {
            string attrName = attributeSyntax.Name.ToString();
            //if (!attrName.EndsWith("Attribute"))
            
            switch (attrName)
            {
                case nameof(MessageHandlerAttribute):
                    {

                        break;
                    }
            }

            return UpdateType.Unknown;
        }

        public static bool IsHandlerAttribute(this AttributeSyntax attributeSyntax)
        {
            return false;
        }

        public static bool IsFilterAttribute(this AttributeSyntax attributeSyntax)
        {
            return false;
        }

        public static bool IsHandlerDeclaration(this BaseTypeSyntax baseTypeSyntax)
        {
            return true;
        }
    }
}
