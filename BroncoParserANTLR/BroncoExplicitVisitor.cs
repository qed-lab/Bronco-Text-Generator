using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParserANTLR
{
    internal class BroncoExplicitVisitor : ExplicitBroncoGrammarBaseVisitor<object>
    {
        private readonly Dictionary<string, SymbolVariable> symbolLookup = new();

        private SymbolVariable GetReference(string id)
        {
            SymbolVariable symbol;
            if(symbolLookup.TryGetValue(id, out symbol))
                return symbol;
            symbol = new SymbolVariable();
            symbolLookup.Add(id, symbol);
            return symbol;
        }

        private void SetReference(string id, ISymbol value)
        {
            SymbolVariable symbol;
            if (!symbolLookup.TryGetValue(id, out symbol))
            {
                symbol = new SymbolVariable();
                symbolLookup.Add(id, symbol);
            }

            symbol.SetPointer(value);
        }

        public override object VisitBag([NotNull] ExplicitBroncoGrammarParser.BagContext context)
        {
            string title = (string)Visit(context.bag_title());
            
            IList<(MetaData<ISymbol>, ISymbol)> bagItems = new List<(MetaData<ISymbol>, ISymbol)>();

            foreach(var itemContext in context.bag_item())
            {
                bagItems.Add(((MetaData<ISymbol>, ISymbol)) Visit(itemContext));
            }



            return new Bag(bagItems);
        }

        public override object VisitBag_title([NotNull] ExplicitBroncoGrammarParser.Bag_titleContext context)
        {
            return context.ID().GetText();
        }

        public override object VisitBag_item([NotNull] ExplicitBroncoGrammarParser.Bag_itemContext context)
        {
            ISymbol symbol = (ISymbol)Visit(context.symbol());
            ISymbol condition = (ISymbol)Visit(context.symbol_ref());

            if(!(symbol is MetaData<ISymbol>)) symbol = new MetaData<ISymbol>(symbol);
            if (condition == null) condition = new BoolSymbol(true);

            return (symbol, condition);
        }

        public override object VisitSymbol([NotNull] ExplicitBroncoGrammarParser.SymbolContext context)
        {
            IList<ISymbol> symbols = new List<ISymbol>();

            foreach (var symbol in context.symbol_list_item())
            {
                symbols.Add((ISymbol) Visit(symbol));
            }
        }

        public override object VisitSymbol_list_item([NotNull] ExplicitBroncoGrammarParser.Symbol_list_itemContext context)
        {
            var symbol = context.TERMINAL();
            if (symbol != null) return (ISymbol)new Terminal(symbol.GetText());
            return (ISymbol) Visit(context.symbol_ref());
        }

        public override object VisitSymbol_ref([NotNull] ExplicitBroncoGrammarParser.Symbol_refContext context)
        {
            var symbol = context.ID();
            if (symbol != null) return GetReference(symbol.GetText());
            return (ISymbol)Visit(context.symbol_call());
        }

        public override object VisitSymbol_call([NotNull] ExplicitBroncoGrammarParser.Symbol_callContext context)
        {
            ISymbol callee = GetReference(context.ID().GetText());

            var argsContext = context.symbol_call_args();
            if (argsContext != null)
            {
                IEnumerable<ISymbol> args = (IEnumerable<ISymbol>)Visit(argsContext);
                if (args.Count() > 0) callee = callee.Argue(args.ToArray());
            }

            return callee;
        }

        public override object VisitSymbol_call_args([NotNull] ExplicitBroncoGrammarParser.Symbol_call_argsContext context)
        {
            IList<ISymbol> args = new List<ISymbol>();

            foreach (var arg in context.symbol_ref())
                args.Add((ISymbol)Visit(arg));

            return args;
        }
    }
}
