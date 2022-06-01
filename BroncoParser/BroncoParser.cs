using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BroncoLibrary;
using Sprache;

namespace BroncoParser
{
    public static class GeneratorParser
    {
        private static string nameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
        private static readonly Dictionary<string, SymbolVariable> SymbolReferences = new Dictionary<string, SymbolVariable>();

        public static ISymbol ParseString(string input)
        {
            return Bag.Parse(input);
        }

        public static SymbolVariable GetReference(string key)
        {
            SymbolVariable reference;
            if(!SymbolReferences.TryGetValue(key, out reference))
            {
                reference = new SymbolVariable();
                SymbolReferences.Add(key, reference);
            }

            return reference;
        }

        public static SymbolVariable SetReference(string key, ISymbol reference)
        {
            SymbolVariable item = GetReference(key);
            item.Set(reference);

            return item;
        }
 
        public static readonly Parser<ISymbol> NonTerminal =
            from openBrace in Parse.Char('<').Once()
            from reference in Parse.Chars(nameChars).Many().Text()
            from closeBrace in Parse.Char('>').Once()
            select GetReference(reference);

        public static readonly Parser<Terminal> Terminal =
            from text in Parse.AnyChar.Until(NonTerminal).Text()
            select new Terminal(text);

        public static readonly Parser<string> BagTitle =
            from openBrace in Parse.Char('=').Once()
            from title in Parse.Chars(nameChars).Many().Text()
            from closeBrace in Parse.Char('=').Once()
            select title;

        public static readonly Parser<SymbolList> SymbolList =
            from symbols in Parse.Or(Terminal, NonTerminal).Until(Parse.String(Environment.NewLine))
            select new SymbolList(symbols);

        public static readonly Parser<ISymbol> Bag =
            from title in BagTitle
            from br in Parse.String(Environment.NewLine)
            from items in SymbolList.Select(s => new MetaData<ISymbol>(s)).Many()
            select SetReference(title, new Bag(items));

        public static readonly Parser<ISymbol> Generator =
            from bags in Bag.Many()
            select GetReference("start");
    }
}
