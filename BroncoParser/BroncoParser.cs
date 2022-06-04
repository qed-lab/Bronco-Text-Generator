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
        /*
        public static ISymbol ParseString(string input)
        {
            return Parse(Bag, ref input);
        }
        */

        public static void Test()
        {
            string input = 
@"this is line 1
the other line
";

            ISymbol root = null;

            Terminal.Put(root)
            (input);

            Console.WriteLine(root.Flatten().Value);
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

        /*
         * Actual parser from here on.
         */

        public static Parser<ISymbol> NonTerminal = (string input) =>
        {
            string reference = null;

            var result =
            Char('<')
            .Then(Char(nameChars).Many().String().Put(reference))
            .Then(Char('>'))
            (input);

            return Result<ISymbol>(() => GetReference(reference), result);
        };

        public static Parser<ISymbol> Terminal = (string input) =>
        {
            string text = null;

            var result =
            AnyChar
            .Until(NonTerminal.Or(NewLine))
            .String().Put(text)
            (input);

            return Result<ISymbol>(() => new Terminal(text), result);
        };


        public static Parser<ISymbol> SymbolList = (string input) =>
        {
            var result =
            NonTerminal.Or<ISymbol>(Terminal)
            .Until(NewLine)
            (input);

            return Result<ISymbol>(() => new SymbolList(result.Value), result);
        };

        public static Parser<string> BagTitle = (string input) =>
        {
            string title = null;

            var result = 
            Char('=')
            .Then(Char(nameChars).Many().String().Put(title))
            .Then(Char('='))
            (input);

            return Result(() => title, result);
        };

        public static Parser<ISymbol> Bag = (string input) =>
        {
            string title = null;
            IEnumerable<MetaData<ISymbol>> items = null;

            var result =
            BagTitle.Put(title)
            .Then(NewLine)
            .Then(
                SymbolList.Map(s => new MetaData<ISymbol>(s)).Many().Put(items))
            (input);

            return Result<ISymbol>(() => SetReference(title, new Bag(items)), result);
        };

        public static Parser<IEnumerable<ISymbol>> Generator = (string input) =>
        {
            return Bag.Many()(input);
        };
    }
}