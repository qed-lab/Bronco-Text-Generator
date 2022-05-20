using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    internal class SymbolList : MetaSymbol, IEnumerable<ISymbol>
    {
        private List<ISymbol> _symbols;

        public SymbolList()
        {
            _symbols = new List<ISymbol>();
        }

        public override ISymbol Evaluate()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var symbol in _symbols)
            {
                sb.Append(symbol.GetString());
            }

            return new Terminal(sb.ToString());
        }

        public void Add(ISymbol item)
        {
            _symbols.Add(item);
        }

        public IEnumerator<ISymbol> GetEnumerator()
        {
            return _symbols.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
