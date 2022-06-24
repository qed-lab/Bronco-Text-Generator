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

        public void Clear()
            => throw new NotImplementedException("This type only implements ICollection for the collection initializer");


        public bool Contains(ISymbol item)
            => throw new NotImplementedException("This type only implements ICollection for the collection initializer");


        public void CopyTo(ISymbol[] array, int arrayIndex)
            => throw new NotImplementedException("This type only implements ICollection for the collection initializer");

        public bool Remove(ISymbol item)
            => throw new NotImplementedException("This type only implements ICollection for the collection initializer");

        public IEnumerator<ISymbol> GetEnumerator()
            => throw new NotImplementedException("This type only implements ICollection for the collection initializer");

        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException("This type only implements ICollection for the collection initializer");
    }
}
