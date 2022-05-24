using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolList : ISymbol, ICollection<ISymbol>
    {
        private List<ISymbol> _symbols;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public SymbolList()
        {
            _symbols = new List<ISymbol>();
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

        public void Clear() => _symbols.Clear();

        public bool Contains(ISymbol item) => _symbols.Contains(item);

        public void CopyTo(ISymbol[] array, int arrayIndex) => _symbols.CopyTo(array, arrayIndex);

        public bool Remove(ISymbol item) => _symbols.Remove(item);

        public IEnumerator<ISymbol> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();
    }
}
