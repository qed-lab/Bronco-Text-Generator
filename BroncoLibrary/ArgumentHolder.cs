using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class ArgumentHolder : Symbol
    {
        public Symbol _symbol;
        private Symbol[] _arguments;

        public ArgumentHolder(Symbol symbol, Symbol[] arguments)
        {
            _symbol = symbol;
            _arguments = arguments;

            addEvaluation(Evaluate);
        }

        public Symbol Evaluate() => _symbol.Evaluate(_arguments);
    }
}
