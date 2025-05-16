using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Immutable;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.NextBot.Building.Attributes;
using Telegram.NextBot.Building.Handlers;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Tests.ConsoleApp.TestHandlers
{
    [CommandHandler, CommandAllias("script")]
    public class ScriptCommandHandler : CommandHandler
    {
        private static readonly MetadataReference[] scriptReferences = Directory.GetFiles(Environment.CurrentDirectory, "*.dll").Select(dll => MetadataReference.CreateFromFile(dll)).ToArray();
        private static readonly string[] scriptImports = ["Telegram.Bot.Types", "Telegram.Bot.Types.Enums", "Telegram.Bot"];
        private static readonly ScriptOptions scriptOptions = ScriptOptions.Default.AddImports(scriptImports).AddReferences(scriptReferences);

        private const string scriptInitCode = "System.Console.SetOut(OutputWriter);";

        public override async Task Execute(AbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            if (Input.Text == null)
            {
                return;
            }

            if (Input is not { Entities.Length: > 0 })
            {
                return;
            }

            MessageEntity? codeEntity = Input.Entities.FirstOrDefault(entity => entity.Type == MessageEntityType.Pre);
            if (codeEntity == null)
            {
                return;
            }

            if (codeEntity.Language != "csharp")
            {
                return;
            }

            string codeSubstring = Input.Text.Substring(codeEntity.Offset, codeEntity.Length);
            Script<object> script = CSharpScript.Create<object>(scriptInitCode, scriptOptions, typeof(ScriptGlobals)).ContinueWith(codeSubstring);

            ImmutableArray<Diagnostic> diagnostics = script.Compile(cancellation);
            if (diagnostics.Any())
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Diagnostic diagnostic in diagnostics)
                {
                    string diagnosticStr = diagnostic.ToString();
                    if (stringBuilder.Length + diagnosticStr.Length > 1000)
                    {
                        await Responce(stringBuilder.ToString(), cancellationToken: cancellation);
                        stringBuilder.Clear();
                    }

                    stringBuilder.AppendLine(diagnosticStr);
                }

                await Responce(stringBuilder.ToString(), cancellationToken: cancellation);
            }
            else
            {
                StringBuilder outputBuider = new StringBuilder();
                TextWriter outputWriter = new StringWriter(outputBuider);
                ScriptGlobals scriptGlobals = new ScriptGlobals(outputWriter, Client);

                ScriptState<object> state = await script.RunAsync(scriptGlobals, cancellation);
                if (state.Exception != null)
                {
                    await Responce(state.Exception.ToString());
                    return;
                }

                await Responce(outputBuider.ToString());
            }
        }

        private class ScriptGlobals(TextWriter outputWriter, ITelegramBotClient tgBotClient)
        {
            public ITelegramBotClient TgBotClient { get; } = tgBotClient;
            public TextWriter OutputWriter { get; } = outputWriter;
        }
    }
}
