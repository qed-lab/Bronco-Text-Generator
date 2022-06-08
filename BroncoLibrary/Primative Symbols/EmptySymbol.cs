using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class EmptySymbol : ITerminal
    {
        public string Value { get => ""; }

        public ISymbol Evaluate()
        {
            return this;
        }
    }
}
