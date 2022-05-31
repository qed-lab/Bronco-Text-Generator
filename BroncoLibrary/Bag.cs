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
        private List<(MetaData<ISymbol>, ISymbol)> _symbols;
        private Random _random;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public Bag()
        {
        }

        public Bag(IEnumerable<MetaData<ISymbol>> items)
        {
            _symbols = new();
            _random = new();

            foreach (var item in items)
                _symbols.Add((item, new BoolSymbol(true)));

            AddEvaluation(Pick);
        }

        //TODO make arguments actually do something
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

        public void Add(MetaData<ISymbol> symbol, ISymbol condition) => _symbols.Add((symbol, condition));

        public void Add(MetaData<ISymbol> symbol) => Add(symbol, new BoolSymbol(true));
    }
}
