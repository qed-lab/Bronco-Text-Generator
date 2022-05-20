using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    internal class SymbolList : MetaSymbol
    {
        private List<ISymbol> _symbols;

        public override ISymbol Evaluate()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var symbol in _symbols)
            {
                sb.Append(symbol.GetString());
            }

            return new Terminal(sb.ToString());
        }
    }
}
