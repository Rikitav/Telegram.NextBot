using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Text;
using Telegram.NextBot.Analyzers.Diagnostics;

namespace Telegram.NextBot.Analyzers
{
    /*
    [Generator(LanguageNames.CSharp)]
    public class AttributedClassRegisterer : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
#if DEBUG
            //if (!Debugger.IsAttached)
                //Debugger.Launch();
#endif
            foreach (string attribute in AnalyzerNamesHelper.UpdateHandlerAttributes)
            {
                context.RegisterImplementationSourceOutput(context.CompilationProvider, (ctx, comp) =>
                {

                });

                IncrementalValuesProvider<ClassDefinitionInfo> classDeclarations = context.SyntaxProvider.ForAttributeWithMetadataName(
                    AnalyzerNamesHelper.AspectDeclareNamespace + attribute,
                    (node, _) => node is ClassDeclarationSyntax,
                    (ctx, _) => GetSemanticTargetForGeneration(ctx));

                context.RegisterImplementationSourceOutput(classDeclarations.Collect(), (ctx, classes) => Execute(ctx, classes, attribute));
            }
        }

        private ClassDefinitionInfo GetSemanticTargetForGeneration(GeneratorAttributeSyntaxContext context)
        {
            ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.TargetNode;

            ISymbol? symbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
            if (symbol == null || symbol is not INamedTypeSymbol typeSymbol)
                throw new Exception("Failed to get named token of class \"" + classDeclaration.Identifier.ToString() + "\"");

            return new ClassDefinitionInfo(classDeclaration, typeSymbol);
        }

        private async void Execute(SourceProductionContext context, ImmutableArray<ClassDefinitionInfo> classes, string attributeName)
        {
            await Task.Yield();

            string interfaceName = AnalyzerNamesHelper.GetInterface(attributeName);
            string instancesClassName = attributeName + "Instances";
            StringBuilder sourceBuilder = new StringBuilder();

            sourceBuilder.AppendLine("using Telegram.NextBot.PollingManagement.Handlers;\n");
            sourceBuilder.AppendLine("namespace Telegram.NextBot.Analyzers.Instances");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine("\tpublic static partial class " + instancesClassName);
            sourceBuilder.AppendLine("\t{");

            foreach (ClassDefinitionInfo classDefinitionInfo in classes)
            {
                if (!ValidateClassDeclaration(context, classDefinitionInfo, attributeName))
                    continue;

                string classFielldFormat = string.Format("\t\tpublic static {0} {1} = new {2}();",
                    interfaceName,
                    classDefinitionInfo.DeclarationSyntax.Identifier.ToString(),
                    classDefinitionInfo.TypeSymbol.ToString());

                sourceBuilder.AppendLine(classFielldFormat);
            }

            sourceBuilder.AppendLine("\t}");
            sourceBuilder.AppendLine("}");

            context.AddSource(instancesClassName + ".cs", sourceBuilder.ToString());
        }

        private bool ValidateClassDeclaration(SourceProductionContext context, ClassDefinitionInfo classDefinitionInfo, string attributeName)
        {
            // Getting attribute list os class definition
            IEnumerable<string> classAttributeList = classDefinitionInfo.DeclarationSyntax.AttributeLists
                .SelectMany(x => x.Attributes.Select(a => a.Name.ToString()))
                .Intersect(AnalyzerNamesHelper.AttributeNamesList);

            // Checking if class definition has more than one update handler attributes
            if (classAttributeList.Count() > 1)
                classDefinitionInfo.DeclarationSyntax.DiagnoseWith(Diagnoses.TooManyHandlerAttributes).Report(context);

            string attributesInterface = AnalyzerNamesHelper.GetInterface(attributeName);
            IEnumerable<string> classInterfacesList = classDefinitionInfo.TypeSymbol.AllInterfaces.Select(i => i.Name);

            if (!classInterfacesList.Contains(attributesInterface))
                classDefinitionInfo.DeclarationSyntax.DiagnoseWith(Diagnoses.MustImplementHandlerInterface, false, attributesInterface).Report(context);

            return !classDefinitionInfo.DeclarationSyntax.AnyDiagnoses();
        }

        internal class ClassDefinitionInfo(ClassDeclarationSyntax declarationSyntax, INamedTypeSymbol typeSymbol)
        {
            public readonly ClassDeclarationSyntax DeclarationSyntax = declarationSyntax;
            public readonly INamedTypeSymbol TypeSymbol = typeSymbol;
        }
    }
    */
}
