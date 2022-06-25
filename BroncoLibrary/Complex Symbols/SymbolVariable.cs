using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolVariable : ISymbol
    {
        private class ArgReference : ISymbol
        {
            private ISymbol _symbol;
            private ISymbol[] _args;

            public ArgReference(ISymbol[] args)
            {
                _symbol = null;
                _args = args;
            }

            public ISymbol Evaluate()
                => _symbol.Evaluate();

            public void Update(ISymbol symbol)
                => _symbol = symbol.Argue(_args);
        }

        private IList<ArgReference> _argReferences = new List<ArgReference>();
        private ISymbol _currentSymbol = null;

        public SymbolVariable() { }

        public SymbolVariable(ISymbol symbol)
            => SetPointer(symbol);

        public ISymbol Evaluate()
            => _currentSymbol;

        public ISymbol Argue(ISymbol[]  args)
        {
            ArgReference argRef = new ArgReference(args);
            _argReferences.Add(argRef);

            if(_currentSymbol != null) argRef.Update(_currentSymbol);

            return argRef;
        }

        public void Set(ISymbol value)
            => SetSymbol(value.Flatten());

        public void SetPointer(ISymbol value)
            => SetSymbol(value);

        public void SetSymbol(ISymbol symbol)
        {
            _currentSymbol = symbol;
            foreach(var argRef in _argReferences)
                argRef.Update(symbol);
        }
    }
}
