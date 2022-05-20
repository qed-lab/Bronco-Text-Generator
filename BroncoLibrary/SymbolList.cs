using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolList : IMetaSymbol
    {
        private List<ISymbol> _symbols;

        public MetaData Data { get; set; }

        public SymbolList()
        {
            _symbols = new List<ISymbol>();
            Data = new MetaData();
        }

        public ISymbol Evaluate()
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
    }
}
