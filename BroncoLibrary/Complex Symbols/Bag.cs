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
        private List<(MetaData<ISymbol> symbol, ISymbol condition)> _items;
        private Random _random;

        public int Count => _items.Count;
        public bool IsReadOnly => true;

        public Bag() : this(new List<MetaData<ISymbol>>())
        {
        }

        public Bag(IEnumerable<MetaData<ISymbol>> items)
        {
            _items = new();
            _random = new();

            foreach (var item in items)
                _items.Add((item, new BoolSymbol(true)));

            AddEvaluation(Pick);
        }

        //TODO make arguments actually do something
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

            return best.Item1;
        }

        private bool TagMatch(ISet<string> tags1, ISet<string> tags2)
        {
            foreach(string tag1 in tags1)
            {
                if (tags2.Contains(tag1)) return true;
            }

            return false;
        }

        public void Add(MetaData<ISymbol> symbol, ISymbol condition) => _items.Add((symbol, condition));

        public void Add(MetaData<ISymbol> symbol) => Add(symbol, new BoolSymbol(true));
    }
}
