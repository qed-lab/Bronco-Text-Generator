using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParserANTLR
{
    internal class ParseReferences
    {
        public struct SymbolReference : ISymbol
        {
            private string _id;
            private ParseReferences _references;
            private ISymbol? _symbol = null;
            private IList<ArgReference> _argReferences = new List<ArgReference>();

            public SymbolReference(string id, ParseReferences references)
            {
                _id = id;
                _references = references;
                references._symbolLookup.Add(id, this);
            }

            public ISymbol Evaluate()
                => _symbol.Evaluate();

            public ISymbol Argue(ISymbol[] args)
            {
                var arg = new ArgReference(args);
                _argReferences.Add(arg);
                return arg;
            }

            public void Update(ISymbol symbol)
            {
                _symbol = symbol;
                foreach (var arg in _argReferences)
                    arg.Update(symbol);
            }
        }

        public struct ArgReference : ISymbol
        {
            private ISymbol? _symbol;
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

        private readonly Dictionary<string, SymbolReference> _symbolLookup = new();

        public bool hasReference(string id)
            => _symbolLookup.ContainsKey(id);

        public ISymbol GetReference(string id)
        {
            SymbolReference symbol;
            if (!_symbolLookup.TryGetValue(id, out symbol))
                symbol = new SymbolReference(id, this);

            return symbol;
        }

        public void SetReference(string id, ISymbol value)
        {
            SymbolReference symbol = (SymbolReference) GetReference(id);

            symbol.Update(value);
        }

        public void Clear()
        {
            _symbolLookup.Clear();
        }
    }
}
