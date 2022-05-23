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

        public SymbolVariable(Symbol symbol)
        {
            _currentSymbol = symbol;

            addEvaluation(Get);
            addEvaluation<Symbol>(Set);
        }

        public Symbol Get()
        {
            return _currentSymbol;
        }

        public Symbol Set(Symbol value)
        {
            _currentSymbol = value;

            return _currentSymbol;
        }
    }
}
