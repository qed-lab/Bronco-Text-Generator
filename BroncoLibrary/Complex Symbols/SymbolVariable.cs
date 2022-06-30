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
            {
                if (_symbol == null) throw new NullReferenceException("Variable is not set to a symbol");
                return _symbol.Argue(_args).Evaluate(); //TODO: Argue is handled inconcistantly, which results in inconsistancy here
            }

            public void Update(ISymbol symbol)
                => _symbol = symbol;

            public override string ToString()
            => $"Argued: {_symbol.ToString()}";
        }

        private IList<ArgReference> _argReferences = new List<ArgReference>();
        private ISymbol _currentSymbol = null;
        public string Name { get; private set; }

        public SymbolVariable(string name) 
        {
            Name = name;
        }

        public SymbolVariable(string name, ISymbol symbol) : this(name)
            => SetPointer(symbol);

        public ISymbol Evaluate()
        {
            if (_currentSymbol == null) throw new NullReferenceException($"Variable `{Name}` is not set to a symbol");
            return _currentSymbol;
        }

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

        private void SetSymbol(ISymbol symbol)
        {
            _currentSymbol = symbol;
            foreach(var argRef in _argReferences)
                argRef.Update(symbol);
        }

        public override string ToString()
            => $"{Name}: {_currentSymbol.ToString()}";
    }
}
