﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolVariable : ISymbol
    {
        private ISymbol _currentSymbol = null;

        public SymbolVariable() { }

        public SymbolVariable(ISymbol symbol)
            => SetToFlatten(symbol);

        public ISymbol Evaluate()
            => _currentSymbol;

        public ISymbol Argue(ISymbol[]  args)
            => _currentSymbol.Argue(args);

        public void SetToFlatten(ISymbol value)
            => _currentSymbol = value.Flatten();

        public void Set(ISymbol value)
            => _currentSymbol = value;
    }
}
