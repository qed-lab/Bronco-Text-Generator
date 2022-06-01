using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BroncoLibrary;

namespace BroncoParser
{
    public class GeneratorParser : BParse
    {
        private static string nameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
        private static readonly Dictionary<string, SymbolVariable> SymbolReferences = new Dictionary<string, SymbolVariable>();

        public static ISymbol ParseString(string input)
        {
            return Parse(Bag, ref input);
        }

        public static void Test()
        {
            string input = 
@"this is line 1
the other line
";
            //var output = Parse(AnyChar()
            //    .Until(String(Environment.NewLine)), ref input);

            var output = Parse(Terminal, ref input);

            Console.WriteLine(output);
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

        public static Parser<ISymbol> NonTerminal = (string input) =>
        {
            Parse(Char('<'), ref input);
            string reference = Parse(Char(nameChars).Many().String(), ref input);
            Parse(Char('>'), ref input);

            return GetReference(reference).Result<ISymbol>(input);
        };

        public static Parser<ISymbol> Terminal = (string input) =>
        {
            string text = Parse(AnyChar().Until(NonTerminal.Or(Char(Environment.NewLine))).String(), ref input);

            return new Terminal(text).Result<ISymbol>(input);
        };


        public static Parser<ISymbol> SymbolList = (string input) =>
        {
            return new SymbolList(Parse(NonTerminal.Or(Terminal)
                .Until(String(Environment.NewLine)), ref input)).Result<ISymbol>(input);
        };

        public static Parser<string> BagTitle = (string input) =>
        {
            Parse(Char('='), ref input);
            string title = Parse(Char(nameChars).Many().String(), ref input);
            Parse(Char('='), ref input);

            return title.Result(input);
        };

        public static Parser<ISymbol> Bag = (string input) =>
        {
            string title = Parse(BagTitle, ref input);
            Parse(String(Environment.NewLine), ref input);
            IEnumerable<MetaData<ISymbol>> items = Parse(SymbolList.Map(s => new MetaData<ISymbol>(s)).Many(), ref input);

            return SetReference(title, new Bag(items)).Result<ISymbol>(input);
        };

        public static Parser<ISymbol> Generator = (string input) =>
        {
            Parse(Bag.Many(), ref input);

            return GetReference("start").Result<ISymbol>(input);
        };
    }
}