using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Diagnostics;
using Telegram.Bot.Types.Enums;

namespace Telegram.NextBot.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class TestAnalyzer : DiagnosticAnalyzer
    {
        private object DebugLock = new object();

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [
            new DiagnosticDescriptor("TG0001", "Title1", "MessageFormat1", "Usage", DiagnosticSeverity.Error, true),
            new DiagnosticDescriptor("TG0002", "Title2", "MessageFormat2", "Usage", DiagnosticSeverity.Error, true)
        ];

        public override void Initialize(AnalysisContext context)
        {
            ILogger<TestAnalyzer> logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<TestAnalyzer>();
            // Attaching debugger
            //DebugTools.AttachStudioDebugger();

            // Configguring analyzers context
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

            // Registerring actions
            //context.RegisterSyntaxNodeAction(TestAnalyze, SyntaxKind.ClassDeclaration);
            //context.RegisterSyntaxNodeAction(CollectFilterAttributesDeclarations, SyntaxKind.ClassDeclaration);
        }

        private void CollectFilterAttributesDeclarations(SyntaxNodeAnalysisContext context)
        {
            lock (DebugLock)
            {
                ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
                Debug.WriteLine("\n" + classDeclaration.Identifier.ToString());

                if (classDeclaration.BaseList == null)
                {
                    Debug.WriteLine("No BaseList");
                    return;
                }

                Debug.WriteLine("\n", classDeclaration.Members.Select(m => m.ToFullString()));
                foreach (BaseTypeSyntax baseType in classDeclaration.BaseList.Types)
                {
                    if (!baseType.IsHandlerDeclaration())
                        continue;
                }
            }
        }

        private void TestAnalyze(SyntaxNodeAnalysisContext context)
        {
            lock (DebugLock)
            {
                ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
                Debug.WriteLine(classDeclaration.Identifier.ToString());

                IEnumerable<AttributeSyntax> attributes = classDeclaration.AttributeLists.SelectMany(l => l.Attributes);
                if (attributes.Any())
                {
                    IEnumerable<AttributeSyntax> handlerAttributes = attributes.Where(a => a.IsHandlerAttribute());
                    if (!handlerAttributes.Any())
                    {
                        return;
                    }

                    if (handlerAttributes.Count() > 1)
                    {
                        return;
                    }

                    UpdateType expectingUpdateTypeHandling = UpdateType.Unknown;
                    AttributeSyntax attributeSyntax = handlerAttributes.ElementAt(0);

                    //attributeSyntax.ArgumentList.Arguments

                    //IEnumerable<AttributeSyntax> filterAttributes = attributes.Where(a => a.IsFilterAttribute());
                }



                //if (classDeclaration.AttributeLists.Any() && )

                if (classDeclaration.AttributeLists.Any())
                {
                    var list = classDeclaration.AttributeLists.SelectMany(a => a.Attributes);
                    Debug.WriteLine(string.Join(";", list));
                }

                if (classDeclaration.BaseList != null)
                {
                    foreach (BaseTypeSyntax baseType in classDeclaration.BaseList.Types)
                    {
                        Debug.WriteLine(baseType.ToFullString());
                    }
                }
            }
        }
    }
}
