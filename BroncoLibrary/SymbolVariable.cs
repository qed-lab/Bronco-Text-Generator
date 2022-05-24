using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolVariable : Symbol
    {
        private Symbol _currentSymbol;

        public SymbolVariable() : this(null)
        {
        }

        public SymbolVariable(Symbol symbol)
        {
            _currentSymbol = symbol;
        }

        public new Symbol Evaluate(Symbol[] args)
        {
            return _currentSymbol.Evaluate(args);
        }

        public void Set(Symbol value)
        {
            _currentSymbol = value;
        }
    }
}
