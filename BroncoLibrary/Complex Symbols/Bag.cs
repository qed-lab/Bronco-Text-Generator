using BroncoLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Bag : DynamicSymbol
    {
        private static readonly TagMatcher tagMatcher = new TagMatcher();

        private List<(MetaData symbol, ISymbol condition)> _items;
        private Random _random;

        public ISymbol DefaultCondition { get; set; }
        public SymbolVariable CurrentItem { get; private set; } = new SymbolVariable("item");
        public int ArgumentCount { get; private set; }

        public Bag(int argCount, ISymbol condition)
        {
            ArgumentCount = argCount;
            DefaultCondition = condition;
            _items = new();
            _random = new();
            AddEvaluation(Pick);
        }

        public Bag(int argCount) : this(argCount, new BoolSymbol(true))
        {
        }

        public Bag(int argCount, IEnumerable<MetaData> items) : this(argCount)
        {
            foreach (var item in items)
                _items.Add((item, new BoolSymbol(true)));
        }

        public Bag(int argCount, IEnumerable<(MetaData, ISymbol)> items) : this(argCount)
        {
            foreach(var item in items)
                _items.Add(item);
        }

        public ISymbol Pick(ISymbol[] args)
        {
            (MetaData symbol, double weight) best = (null, -double.MaxValue);

            foreach (var item in _items)
            {
                CurrentItem.SetPointer(item.symbol);
                double rolledWeight = _random.NextDouble() * item.symbol.Weight;
                FloatSymbol condition = item.condition.FlattenTo<FloatSymbol>();
                rolledWeight *= condition.FloatValue;
                if (rolledWeight > best.weight)
                    best = (item.symbol, rolledWeight);
            }
            /*
            Console.Write("Picked ");
            foreach (var tag in best.symbol.Tags)
                Console.Write($"{tag.Key} ");
            Console.WriteLine();
            */
            return best.symbol;
        }

        public void Add((MetaData, ISymbol) item)
            => Add(item.Item1, item.Item2);

        public void Add(MetaData symbol, ISymbol condition) 
            => _items.Add((symbol, condition));

        public void Add(MetaData symbol)
            => _items.Add((symbol, DefaultCondition));
    }
}
