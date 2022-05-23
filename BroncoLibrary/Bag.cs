using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Bag : Symbol, ICollection<MetaData<Symbol>>
    {
        private List<MetaData<Symbol>> _symbols;
        private Random _random;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public Bag()
        {
            _symbols = new List<MetaData<Symbol>>();
            _random = new Random();

            addEvaluation(Evaluate);
        }

        public Symbol Evaluate()
        {
            (MetaData<Symbol>, double) best = (null, -double.MaxValue);

            foreach(var symbol in _symbols)
            {
                double rolledWeight = _random.NextDouble()*symbol.Weight;
                if (rolledWeight > best.Item2)
                    best = (symbol, rolledWeight);
            }

            return best.Item1.Evaluate(EmptyArgs);
        }

        public void Add(MetaData<Symbol> item) => _symbols.Add(item);

        public void Clear() => _symbols.Clear();

        public bool Contains(MetaData<Symbol> item) => _symbols.Contains(item);

        public void CopyTo(MetaData<Symbol>[] array, int arrayIndex) => _symbols.CopyTo(array, arrayIndex);

        public bool Remove(MetaData<Symbol> item) => _symbols.Remove(item);

        public IEnumerator<MetaData<Symbol>> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();
    }
}
