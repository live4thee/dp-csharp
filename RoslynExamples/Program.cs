// Code slightly modified from:
// https://github.com/dotnet/roslyn/blob/master/docs/samples/csharp-semantic.pdf

using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SemanticsCS
{
    class Program
    {
        static void Main(string[] args)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
@"using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        } 
    }
}");
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var compilation = CSharpCompilation.Create("HelloWorld")
                .AddReferences(MetadataReference.CreateFromAssembly(typeof(object).Assembly))
                .AddSyntaxTrees(tree);
            var model = compilation.GetSemanticModel(tree);

            ShowNamespaceMembers(model, root.Usings);
            ShowLiteralStrings(model, root);
        }

        static void ShowNamespaceMembers(SemanticModel model, SyntaxList<UsingDirectiveSyntax> usings)
        {
            foreach (var u in usings)
            {
                // binding the name of the using directives
                var nameInfo = model.GetSymbolInfo(u.Name);
                var systemSymbol = (INamespaceSymbol)nameInfo.Symbol;

                Console.WriteLine(string.Format(">>> Dumping namespace members for: {0} <<<", u.Name));
                foreach (var ns in systemSymbol.GetNamespaceMembers())
                {
                    Console.WriteLine(ns.Name);
                }
            }
        }

        static void ShowLiteralStrings(SemanticModel model, SyntaxNode root)
        {
            var lits = root.DescendantNodes()
                .OfType<LiteralExpressionSyntax>();

            foreach (var lit in lits)
            {
                if (lit.IsKind(SyntaxKind.StringLiteralExpression))
                {
                    var litInfo = model.GetTypeInfo(lit);
                    var s = string.Format("{0}: {1}", lit.GetText(), litInfo.Type);
                    Console.WriteLine(s);

                    // List public methods of 'string' class with returing type as 'string'
                    ShowMethods((INamedTypeSymbol)litInfo.Type);
                }
            }
        }

        static void ShowMethods(INamedTypeSymbol symbol)
        {
            foreach (var name in (from method in symbol.GetMembers().OfType<IMethodSymbol>()
                                  where method.ReturnType.Equals(symbol) &&
                                        method.DeclaredAccessibility == Accessibility.Public
                                  select method.Name).Distinct())
            {
                Console.WriteLine(name);
            }
        }
    }
}
