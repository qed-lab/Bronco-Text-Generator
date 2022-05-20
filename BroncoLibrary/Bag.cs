using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    internal class Bag : IArgumentSymbol
    {
        private List<MetaSymbol> _symbols;
        private Random _random;

        public ISymbol Evaluate(ICollection<ISymbol> arguments)
        {
            (MetaSymbol, double) best = (null, -double.MaxValue);

            foreach(var symbol in _symbols)
            {
                double rolledWeight = _random.NextDouble()* symbol.Weight;
                if (rolledWeight > best.Item2)
                    best = (symbol, rolledWeight);
            }

            return best.Item1;
        }
    }
}
