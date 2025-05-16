using Microsoft.CodeAnalysis;

namespace Telegram.NextBot.Analyzers.Diagnostics
{
    internal static class Diagnoses
    {
        private const string Category = "Usage";

        public static DiagnosticDescriptor TooManyHandlerAttributes = new DiagnosticDescriptor("TG0001", string.Empty, "The class cannot have several \"Update Handling\" attributes", Category, DiagnosticSeverity.Error, true);
        public static DiagnosticDescriptor MustImplementHandlerInterface = new DiagnosticDescriptor("TG0002", string.Empty, "The class must implement the {0} interface", Category, DiagnosticSeverity.Error, true);
    }
}
