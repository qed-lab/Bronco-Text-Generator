using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BroncoLibrary;
using Sprache;

namespace BroncoParser
{
    internal static class BroncoParser
    {
        private static string nameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";

        public static readonly Dictionary<String, ISymbol> SymbolReferences = new Dictionary<string, ISymbol>();

        public static readonly Parser<ISymbol> NonTerminal =
            from openBrace in Parse.Char('<').Once()
            from reference in Parse.Chars(nameChars).Many().Text()
            from closeBrace in Parse.Char('>').Once()
            select SymbolReferences[reference];

        public static readonly Parser<Terminal> Terminal =
            from text in
            (from not in Parse.Not(NonTerminal)
             from c in Parse.CharExcept('\n')
             select c).Many().Text()
            select new Terminal(text);

        public static readonly Parser<string> bagTitle =
            from openBrace in Parse.Char('<').Once()
            from title in Parse.Chars(nameChars).Many().Text()
            from closeBrace in Parse.Char('>').Once()
            select title;
    }
}
