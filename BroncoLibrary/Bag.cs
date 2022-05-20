using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Bag : IArgumentSymbol
    {
        private List<IMetaSymbol> _symbols;
        private Random _random;

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

        public void Add(IMetaSymbol item)
        {
            _symbols.Add(item);
        }
    }
}
