using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoTextParser
{
    public class BroncoVisitor : BroncoParserBaseVisitor<object>
    {
        private readonly Dictionary<string, ISymbol> _staticLookup = new();
        private readonly Dictionary<string, ISymbol> _localLookup = new();
        private readonly SymbolStruct _globals = new();

        public BroncoVisitor(IEnumerable<KeyValuePair<string, ISymbol>> startingReferences) : this()
        {
            foreach (var reference in startingReferences)
                _staticLookup.Add(reference.Key, reference.Value);
        }

        public BroncoVisitor()
        {
            _staticLookup.Add("set", new VariableSetter());
            _staticLookup.Add("addTag", new TagAdder());
            _staticLookup.Add("removeTag", new TagRemover());
            _staticLookup.Add("choose", new Choose());
            _staticLookup.Add("setPointer", new VariablePointerSetter());
            _staticLookup.Add("if", new IfElse());
            _staticLookup.Add("gt", new GreaterThan());
            _staticLookup.Add("lt", new LessThan());
            _staticLookup.Add("equal", new Equals());
            _staticLookup.Add("add", new Add());
            _staticLookup.Add("sub", new Subtract());
            _staticLookup.Add("mult", new Multiply());
            _staticLookup.Add("random", new RandomFloat());
            _staticLookup.Add("randomI", new RandomInt());
            _staticLookup.Add("round", new Round());
            _staticLookup.Add("and", new And());
            _staticLookup.Add("or", new Or());
            _staticLookup.Add("not", new Not());
            _staticLookup.Add("do", new Do());
            _staticLookup.Add("doYield", new DoYield());
            _staticLookup.Add("addToBag", new BagAdder());
            _staticLookup.Add("tagMult", new TagMult());
            _staticLookup.Add("tagContains", new TagContains());
            _staticLookup.Add("tagNoOverlap", new TagNoOverlap());
            _staticLookup.Add("tagOverlap", new TagOverlap());
            _staticLookup.Add("cap", new Capitalize());
            _staticLookup.Add("a", new Ana());
            _staticLookup.Add("s", new Plural());
            _staticLookup.Add("ed", new Ed());
        }

        public BroncoVisitor(IDictionary<string, ISymbol> startingGlobals)
        {
            foreach (var symbol in startingGlobals)
                _staticLookup.Add(symbol.Key, symbol.Value);
        }

        public IEnumerable<KeyValuePair<string, ISymbol>> GetReferences()
        {
            return _staticLookup;
        }

        private void SetVariable(string id, ISymbol value)
            => _globals.GetField(id).SetPointer(value);

        private ISymbol GetReference(string id)
        {
            ISymbol reference;

            if (_localLookup.TryGetValue(id, out reference)) return reference;

            if (_staticLookup.TryGetValue(id, out reference)) return reference;

            return _globals.GetField(id);
        }

        private void SetStatic(string id, ISymbol symbol)
            => _staticLookup.Add(id, symbol);

        private void SetLocal(string id, ISymbol symbol)
            => _localLookup.Add(id, symbol);

        private void ClearLocals()
            => _localLookup.Clear();

        public override object VisitFile([NotNull] BroncoParser.FileContext context)
        {
            foreach (var bag in context.bag())
            {
                Visit(bag);
            }

            return GetReference("start");
        }

        //TODO: This thing is a beast, need to rethink it, but not now...
        public override object VisitBag([NotNull] BroncoParser.BagContext context)
        {
            var argContext = context.bag_title_args();
            IList<string> args = argContext != null ? (IList<string>)Visit(argContext) : new List<string>();

            Bag bag = new Bag(args.Count());

            string title = context.TITLE().GetText();
            title = title.Substring(1, title.Length - 1);

            SetVariable(title, bag);

            for (int i = 0; i < args.Count(); i++)
                SetLocal(args[i], bag.GetArgument(i));
            SetLocal("item", bag.CurrentItem);

            var conditionContext = context.bag_default_condition();
            if (conditionContext != null) bag.DefaultCondition = (ISymbol)Visit(conditionContext);

            foreach(var itemContext in context.bag_item())
            {
                var item = Visit(itemContext);

                if(item is (MetaData, ISymbol))
                    bag.Add(((MetaData, ISymbol)) item);
                else
                    bag.Add((MetaData) item);
            }

            ClearLocals();

            return bag;
        }

        public override object VisitBag_default_condition([NotNull] BroncoParser.Bag_default_conditionContext context)
        {
            return (ISymbol) Visit(context.symbol_ref());
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
                text = meta.GetText().Trim();

                int colIndex = text.IndexOf(':');
                string tagName;
                float weight;
                if (colIndex != -1)
                {
                    tagName = text.Substring(1, colIndex - 1);
                    weight = float.Parse(text.Substring(colIndex + 1, text.Length - colIndex - 1));
                } else
                {
                    tagName = text.Substring(1, text.Length - 1);
                    weight = 1;
                }

                return (tagName, weight);
              
            }

            text = context.META_WEIGHT().GetText().Trim();
            return float.Parse(text.Substring(1, text.Length - 1));
        }

        public override object VisitSymbol_ref([NotNull] BroncoParser.Symbol_refContext context)
        {
            var id = context.symbol_identifier();
            if (id != null) return Visit(id);

            var call = context.symbol_call();
            if (call != null) return (ISymbol) Visit(call);

            var num = context.NUMBER();
            if (num != null) return new FloatSymbol(float.Parse(num.GetText()));

            var boolean = context.BOOL_LITERAL();
            if (boolean != null) return new BoolSymbol(bool.Parse(boolean.GetText()));

            return Visit(context.symbol());
        }

        public override object VisitSymbol_identifier([NotNull] BroncoParser.Symbol_identifierContext context)
        {
            var ids = context.IDENTIFIER();

            ISymbol symbol = GetReference(ids[0].GetText());
            if (ids.Length == 1) return symbol;

            string[] fields = new string[ids.Length - 1];

            for(int i = 1; i < ids.Length; i++)
            {
                fields[i - 1] = ids[i].GetText();
            }

            return new StructAccessor(symbol, fields);
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
