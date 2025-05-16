using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.NextBot.Analyzers.Additions
{
    internal class AttributeNameComparer : IEqualityComparer<string>
    {
        private readonly bool IsShortName;

        public AttributeNameComparer(AttributeSyntax attributeSyntax)
        {

        }

        public bool Equals(string x, string y) => throw new NotImplementedException();
        public int GetHashCode(string obj) => throw new NotImplementedException();
    }
}
