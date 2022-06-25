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

        private List<(MetaData<ISymbol> symbol, ISymbol condition)> _items;
        private Random _random;

        public int ArgumentCount { get; private set; }

        public Bag(int argCount)
        {
            ArgumentCount = argCount;
            _items = new();
            _random = new();
            AddEvaluation(Pick);
        }

        public Bag(int argCount, IEnumerable<MetaData<ISymbol>> items) : this(argCount)
        {
            foreach (var item in items)
                _items.Add((item, new BoolSymbol(true)));
        }

        public Bag(int argCount, IEnumerable<(MetaData<ISymbol>, ISymbol)> items) : this(argCount)
        {
            foreach(var item in items)
                _items.Add(item);
        }

        public ISymbol Pick(ISymbol[] args)
        {
            (MetaData<ISymbol> symbol, double weight) best = (null, -double.MaxValue);

            foreach (var item in _items)
            {
                double rolledWeight = _random.NextDouble() * item.symbol.Weight;
                FloatSymbol condition = item.condition.FlattenTo<FloatSymbol>();
                rolledWeight *= condition.FloatValue;
                if (rolledWeight > best.weight)
                    best = (item.symbol, rolledWeight);
            }

            return best.symbol;
        }

        public void Add((MetaData<ISymbol>, ISymbol) item)
            => Add(item.Item1, item.Item2);

        public void Add(MetaData<ISymbol> symbol, ISymbol condition) 
            => _items.Add((symbol, condition));

        public void Add(MetaData<ISymbol> symbol)
        {
            var condition = ArgumentCount == 0 ? 
                new BoolSymbol(true) : tagMatcher.Argue(new ISymbol[]{ symbol, GetArgument(0)});

            _items.Add((symbol, condition));
        }
    }
}
