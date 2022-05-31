using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolList : ISymbol
    {
        private List<ISymbol> _symbols;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public SymbolList()
        {
            _symbols = new List<ISymbol>();
        }

        public SymbolList(IEnumerable<ISymbol> symbols)
        {
            _symbols = new List<ISymbol>(symbols);
        }

        public ISymbol Evaluate()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var symbol in _symbols)
            {
                sb.Append(symbol.Flatten().Value);
            }

            return new Terminal(sb.ToString());
        }

        public void Add(ISymbol item) => _symbols.Add(item);
    }
}
