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
        private readonly Dictionary<string, SymbolVariable> symbolLookup;

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
    }
}
