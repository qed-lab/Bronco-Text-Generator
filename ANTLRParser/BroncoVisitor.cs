using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoTextParser
{
    public class BroncoVisitor : BroncoParserBaseVisitor<object>
    {
        private readonly Dictionary<string, SymbolVariable> symbolLookup = new();
        private readonly Dictionary<string, SymbolVariable> localLookup = new();

        public BroncoVisitor()
        {
            SetReference("set", new VariableSetter());
            SetReference("addTag", new TagAdder());
            SetReference("matchTag", new TagMatcher());
            SetReference("choose", new Choose());
            SetReference("setPointer", new VariablePointerSetter());
            SetReference("if", new IfElse());
            SetReference("gt", new GreaterThan());
            SetReference("lt", new LessThan());
            SetReference("equal", new Equals());
            SetReference("add", new Add());
            SetReference("mult", new Multiply());
            SetReference("random", new RandomNumber());
            SetReference("round", new Round());
        }

        public BroncoVisitor(IDictionary<string, ISymbol> startingGlobals)
        {
            foreach (var symbol in startingGlobals)
                symbolLookup.Add(symbol.Key, new SymbolVariable(symbol.Key, symbol.Value));
        }

        private SymbolVariable GetReference(string id)
        {
            SymbolVariable symbol;
            if (localLookup.TryGetValue(id, out symbol))
                return symbol;
            if (symbolLookup.TryGetValue(id, out symbol))
                return symbol;
            symbol = new SymbolVariable(id);
            symbolLookup.Add(id, symbol);
            return symbol;
        }

        private void SetReference(string id, ISymbol value)
        {
            SymbolVariable symbol;
            if (!symbolLookup.TryGetValue(id, out symbol))
            {
                symbol = new SymbolVariable(id);
                symbolLookup.Add(id, symbol);
            }

            symbol.SetPointer(value);
        }

        private void SetLocalReference(string id, ISymbol value)
        {
            localLookup.Add(id, new SymbolVariable(id, value));
        }

        public override object VisitFile([NotNull] BroncoParser.FileContext context)
        {
            foreach (var bag in context.bag())
            {
                Visit(bag);
            }

            return GetReference("start");
        }

        public override object VisitBag([NotNull] BroncoParser.BagContext context)
        {
            (string, IList<string>) title = ((string, IList<string>)) Visit(context.bag_title());
            var bag = new Bag(title.Item2.Count());

            for (int i = 0; i < title.Item2.Count(); i++)
                SetLocalReference(title.Item2[i], bag.GetArgument(i));

            foreach(var itemContext in context.bag_item())
            {
                var item = Visit(itemContext);

                if(item is (MetaData, ISymbol))
                    bag.Add(((MetaData, ISymbol)) item);
                else
                    bag.Add((MetaData) item);
            }

            localLookup.Clear();

            SetReference(title.Item1, bag);
            return bag;
        }

        public override object VisitBag_title([NotNull] BroncoParser.Bag_titleContext context)
        {
            string title = context.TITLE().GetText();
            title = title.Substring(1, title.Length-1);
            var argContext = context.bag_title_args();
            IList<string> args = argContext != null ? (IList<string>) Visit(argContext) : new List<string>();

            return (title, args);
        }

        public override object VisitBag_title_args([NotNull] BroncoParser.Bag_title_argsContext context)
        {
            IList<string> args = new List<string>();

            foreach (var arg in context.TITLE_ID())
                args.Add(arg.GetText());

            return args;
        }

        public override object VisitBag_item([NotNull] BroncoParser.Bag_itemContext context)
        {
            ISymbol symbol = (ISymbol)Visit(context.symbol());
            ISymbol condition;

            if(!(symbol is MetaData)) symbol = new MetaData(symbol);
            if (context.symbol_ref() == null) condition = null;
            else condition = (ISymbol)Visit(context.symbol_ref());

            return condition != null ? ((MetaData)symbol, condition) : (MetaData)symbol;
        }

        public override object VisitSymbol([NotNull] BroncoParser.SymbolContext context)
        {
            IList<ISymbol> symbols = new List<ISymbol>();

            foreach (var symbol in context.symbol_list_item())
                symbols.Add((ISymbol) Visit(symbol));

            ISymbol ret = symbols.Count() != 1 ? new SymbolList(symbols) : symbols[0];

            var metaDataContext = context.meta_data();
            if (metaDataContext != null)
            {
                MetaData metaDataRet = new(ret);
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


        public override object VisitSymbol_list_item([NotNull] BroncoParser.Symbol_list_itemContext context)
        {
            var symbol = context.TERMINAL();
            if (symbol != null)
            {
                return (ISymbol)new Terminal(symbol.GetText());
            }
            return (ISymbol)Visit(context.symbol_insert());
        }

        public override object VisitSymbol_insert([NotNull] BroncoParser.Symbol_insertContext context)
        {
            var symbolRef = context.symbol_ref();
            if (symbolRef != null) return (ISymbol) Visit(symbolRef);
            return (ISymbol) Visit(context.symbol_call_inner());
        }

        public override object VisitMeta_data([NotNull] BroncoParser.Meta_dataContext context)
        {
            var meta = context.META_TAG();
            string text;
            if (meta != null)
            {
                text = meta.GetText();
                int colIndex = text.IndexOf(':');
                string tagName;
                float weight;
                if (colIndex != -1)
                {
                    tagName = text.Substring(1, colIndex - 1);
                    weight = float.Parse(text.Substring(colIndex + 1, text.Length - colIndex));
                } else
                {
                    tagName = text.Substring(1, text.Length - 1);
                    weight = 1;
                }

                return (tagName, weight);
              
            }

            text = context.META_WEIGHT().GetText();
            return float.Parse(text.Substring(1, text.Length - 1));
        }

        public override object VisitSymbol_ref([NotNull] BroncoParser.Symbol_refContext context)
        {
            var id = context.IDENTIFIER();
            if (id != null) return GetReference(id.GetText());

            var call = context.symbol_call();
            if (call != null) return (ISymbol) Visit(call);

            var num = context.NUMBER();
            if (num != null) return new FloatSymbol(float.Parse(num.GetText()));

            var boolean = context.BOOL_LITERAL();
            if (boolean != null) return new BoolSymbol(bool.Parse(boolean.GetText()));

            return Visit(context.symbol());
        }

        public override object VisitSymbol_call([NotNull] BroncoParser.Symbol_callContext context)
        {
            return Visit(context.symbol_call_inner());
        }

        public override object VisitSymbol_call_inner([NotNull] BroncoParser.Symbol_call_innerContext context)
        {
            ISymbol callee = GetReference(context.IDENTIFIER().GetText());

            var argsContext = context.symbol_call_args();
            if (argsContext != null)
            {
                IEnumerable<ISymbol> args = (IEnumerable<ISymbol>)Visit(argsContext);
                if (args.Count() > 0) callee = callee.Argue(args.ToArray());
            }

            return callee;
        }

        public override object VisitSymbol_call_args([NotNull] BroncoParser.Symbol_call_argsContext context)
        {
            IList<ISymbol> args = new List<ISymbol>();

            foreach (var arg in context.symbol_ref())
                args.Add((ISymbol)Visit(arg));

            return args;
        }
    }
}
