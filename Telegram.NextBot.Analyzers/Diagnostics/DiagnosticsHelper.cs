using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Telegram.NextBot.Analyzers.Diagnostics
{
    internal static class DiagnosticsHelper
    {
        private static readonly object LockObj = new object();
        private static readonly Dictionary<ClassDeclarationSyntax, List<DiagnosticDescriptor>> diagnosedClassDefinitionsHash = [];

        public static bool AnyDiagnoses(this ClassDeclarationSyntax classDeclaration)
        {
            lock (LockObj)
            {
                // Checking if this class declaration have "diagnoses" list
                if (!diagnosedClassDefinitionsHash.TryGetValue(classDeclaration, out List<DiagnosticDescriptor> diagnoses))
                    return false;

                // Checking if given class declaration has any "diagnoses"
                return diagnoses.Any();
            }
        }

        public static Diagnostic? DiagnoseWith(this ClassDeclarationSyntax classDeclaration, DiagnosticDescriptor descriptor, bool allowMultiplyDiagnoses = true, params object[]? args)
        {
            lock (LockObj)
            {
                // Checking if this class declaration already have "diagnoses" list
                if (!diagnosedClassDefinitionsHash.TryGetValue(classDeclaration, out List<DiagnosticDescriptor>? diagnoses))
                {
                    // Declaring so, if doesn't
                    diagnosedClassDefinitionsHash.Add(classDeclaration, diagnoses = []);
                }
                else if (!allowMultiplyDiagnoses && diagnoses.Contains(descriptor)) // Checking if given class declaration is already "diagnosed"
                {
                    // Dont diagnose if already
                    return null;
                }

                // Getting location of identifier (name) of class
                Location location = Location.Create(classDeclaration.SyntaxTree, classDeclaration.Identifier.Span);

                // Creating diagnostic instance
                diagnoses.Add(descriptor);
                return Diagnostic.Create(descriptor, location, args);
            }
        }

        public static void Report(this Diagnostic? target, SourceProductionContext context)
        {
            if (target == null)
                return;

            context.ReportDiagnostic(target);
        }
    }
}
