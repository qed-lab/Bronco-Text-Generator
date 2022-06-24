﻿using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParserANTLR
{
    public class BroncoExplicitVisitor : ExplicitBroncoGrammarBaseVisitor<object>
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

        public override object VisitFile([NotNull] ExplicitBroncoGrammarParser.FileContext context)
        {
            foreach (var bag in context.bag())
            {
                Visit(bag);
            }

            return GetReference("start");
        }

        public override object VisitBag([NotNull] ExplicitBroncoGrammarParser.BagContext context)
        {
            string title = (string)Visit(context.bag_title());
            
            IList<(MetaData<ISymbol>, ISymbol)> bagItems = new List<(MetaData<ISymbol>, ISymbol)>();

            foreach(var itemContext in context.bag_item())
            {
                bagItems.Add(((MetaData<ISymbol>, ISymbol)) Visit(itemContext));
            }

            var bag = new Bag(0, bagItems);
            SetReference(title, bag);
            return bag;
        }

        public override object VisitBag_title([NotNull] ExplicitBroncoGrammarParser.Bag_titleContext context)
        {
            return context.ID().GetText();
        }

        public override object VisitBag_item([NotNull] ExplicitBroncoGrammarParser.Bag_itemContext context)
        {
            ISymbol symbol = (ISymbol)Visit(context.symbol());
            ISymbol condition;

            if(!(symbol is MetaData<ISymbol>)) symbol = new MetaData<ISymbol>(symbol);
            if (context.symbol_ref() == null) condition = new BoolSymbol(true);
            else condition = (ISymbol)Visit(context.symbol_ref());

            return ((MetaData<ISymbol>) symbol, condition);
        }

        public override object VisitSymbol([NotNull] ExplicitBroncoGrammarParser.SymbolContext context)
        {
            IList<ISymbol> symbols = new List<ISymbol>();

            foreach (var symbol in context.symbol_list_item())
                symbols.Add((ISymbol) Visit(symbol));

            ISymbol ret = symbols.Count() != 1 ? new SymbolList(symbols) : symbols[0];

            var metaDataContext = context.meta_data();
            if (metaDataContext != null)
            {
                MetaData<ISymbol> metaDataRet = new(ret);
                foreach (var dataContext in metaDataContext)
                {
                    var data = Visit(dataContext);

                    if (data is float) metaDataRet.Weight = (float)data;
                    else
                    {
                        (string, float) tag = ((string, float)) data;
                        metaDataRet.Tags.AddTag(tag.Item1, tag.Item2);
                    }
                }

                ret = metaDataRet;
            }

            return ret;
        }

        public override object VisitMeta_data([NotNull] ExplicitBroncoGrammarParser.Meta_dataContext context)
        {
            var meta = context.meta_tag();
            if (meta != null) return Visit(meta);
            return Visit(context.meta_weight());
        }

        public override object VisitMeta_tag([NotNull] ExplicitBroncoGrammarParser.Meta_tagContext context)
        {
            var weightContext = context.FLOAT();
            float weight;
            if (weightContext == null) weight = 1;
            else weight = float.Parse(weightContext.GetText());

            return (context.ID().GetText(), weight);
        }

        public override object VisitMeta_weight([NotNull] ExplicitBroncoGrammarParser.Meta_weightContext context)
        {
            return float.Parse(context.FLOAT().GetText());
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
