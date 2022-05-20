using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Bag : IArgumentSymbol, ICollection<IMetaSymbol>
    {
        private List<IMetaSymbol> _symbols;
        private Random _random;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public Bag()
        {
            _symbols = new List<IMetaSymbol>();
            _random = new Random();
        }

        public ISymbol Evaluate(ICollection<ISymbol> arguments)
        {
            (IMetaSymbol, double) best = (null, -double.MaxValue);

            foreach(var symbol in _symbols)
            {
                double rolledWeight = _random.NextDouble()*symbol.Data.Weight;
                if (rolledWeight > best.Item2)
                    best = (symbol, rolledWeight);
            }

            return best.Item1.Evaluate();
        }

        public void Add(IMetaSymbol item) => _symbols.Add(item);

        public void Clear() => _symbols.Clear();

        public bool Contains(IMetaSymbol item) => _symbols.Contains(item);

        public void CopyTo(IMetaSymbol[] array, int arrayIndex) => _symbols.CopyTo(array, arrayIndex);

        public bool Remove(IMetaSymbol item) => _symbols.Remove(item);

        public IEnumerator<IMetaSymbol> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();
    }
}
