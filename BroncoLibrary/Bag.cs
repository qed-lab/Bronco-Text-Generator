using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Bag : DynamicSymbol, ICollection<(MetaData<ISymbol>, BoolSymbol)>
    {
        private List<(MetaData<ISymbol>, BoolSymbol)> _symbols;
        private Random _random;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public Bag()
        {
            _symbols = new();
            _random = new();

            AddEvaluation(Pick);
            //AddEvaluation<MetaData<ISymbol>>(Pick);
        }

        //TODO make picking better: more robust tag filtering system
        public ISymbol Pick(ISymbol[] args)
        {
            (MetaData<ISymbol>, double) best = (null, -double.MaxValue);

            foreach (var symbol in _symbols)
            {
                double rolledWeight = _random.NextDouble() * symbol.Item1.Weight;
                if (rolledWeight > best.Item2)
                    best = (symbol.Item1, rolledWeight);
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

        public void Add((MetaData<ISymbol>, BoolSymbol) item) => _symbols.Add(item);

        public void Clear() => _symbols.Clear();

        public bool Contains((MetaData<ISymbol>, BoolSymbol) item) => _symbols.Contains(item);

        public void CopyTo((MetaData<ISymbol>, BoolSymbol)[] array, int arrayIndex) => _symbols.CopyTo(array, arrayIndex);

        public bool Remove((MetaData<ISymbol>, BoolSymbol) item) => _symbols.Remove(item);

        public IEnumerator<(MetaData<ISymbol>, BoolSymbol)> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();
    }
}
