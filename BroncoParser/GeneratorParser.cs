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
        private static string varChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
        private static readonly Dictionary<string, SymbolVariable> _symbolReferences = new Dictionary<string, SymbolVariable>();

        public static ISymbol ParseString(string input)
        {
            SetReference("setter", new VariableSetter());

            Generator(input);

            var local = _symbolReferences;

            return GetReference("start");
        }

        public static SymbolVariable GetReference(string key)
        {
            SymbolVariable reference;
            if(!_symbolReferences.TryGetValue(key, out reference))
            {
                reference = new SymbolVariable();
                _symbolReferences.Add(key, reference);
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

        public static Parser<string> VarName =
            Char(varChars)
            .Many()
            .AtLeastOne()
            .String();

        public static Parser<ISymbol> NonTerminal = (string input) =>
        {
            string reference = null;
            SymbolVariable[] args = null;

            var result =
            Char('<')
            .Then(
                VarName
                .Do(s => reference = s)
                .Trim()
                .Then(
                    Char('|')
                    .Then(
                        VarName
                        .Trim()
                        .Map(s => GetReference(s))
                        .Split(
                            Char(',')
                        )
                        .Do(s => args = s.ToArray())
                    )
                    .Optional()
                )
            )
            .Then(Char('>'))
            (input);

            if(args == null)
                return Result<ISymbol>(() => GetReference(reference), result);
            else
                return Result<ISymbol>(() => GetReference(reference).Argue(args), result);
        };

        public static Parser<ISymbol> Terminal = (string input) =>
        {
            string text = null;

            var result =
            AnyChar
            .Until(NonTerminal.Or(NewLine))
            .AtLeastOne()
            .String().Do((s) => text = s)
            (input);

            return Result<ISymbol>(() => new Terminal(text), result);
        };


        public static Parser<ISymbol> SymbolList = (string input) =>
        {
            var result =
            (NonTerminal.Or<ISymbol>(Terminal))
            .Until(NewLine)
            .AtLeastOne()
            (input);

            return Result<ISymbol>(() => new SymbolList(result.Value), result);
        };

        public static Parser<string> BagTitle = (string input) =>
        {
            string title = null;

            var result = 
            Char('=')
            .Then(
                VarName
                .Do((s) => title = s)
                .Trim())
            .Then(Char('='))
            (input);

            return Result(() => title, result);
        };

        public static Parser<ISymbol> Bag = (string input) =>
        {
            string title = null;
            IList<MetaData<ISymbol>> items = null;

            var result =
            BagTitle.Do((s) => title = s)
            .Trim()
            .Then(NewLine)
            .Then(
                SymbolList
                .Map(s => new MetaData<ISymbol>(s))
                .Split(NewLine)
                .Do(s => items = s))
            (input);

            return Result<ISymbol>(() => SetReference(title, new Bag(items)), result);
        };

        public static Parser<IList<ISymbol>> Generator = (string input) =>
        {
            return WhiteSpace.Many().Then(Bag.Split(
                NewLine
                .ThenConsume(
                    WhiteSpace
                    .Many())))
            (input);
        };
    }
}