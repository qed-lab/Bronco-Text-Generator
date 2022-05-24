using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolList : Symbol, ICollection<Symbol>
    {
        private List<Symbol> _symbols;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public SymbolList()
        {
            _symbols = new List<Symbol>();

            addEvaluation(Evaluate);
        }

        public Symbol Evaluate()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var symbol in _symbols)
            {
                sb.Append(symbol.Flatten(EmptyArgs));
            }

            return new Terminal(sb.ToString());
        }

        public void Add(Symbol item) => _symbols.Add(item);

        public void Clear() => _symbols.Clear();

        public bool Contains(Symbol item) => _symbols.Contains(item);

        public void CopyTo(Symbol[] array, int arrayIndex) => _symbols.CopyTo(array, arrayIndex);

        public bool Remove(Symbol item) => _symbols.Remove(item);

        public IEnumerator<Symbol> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();
    }
}
