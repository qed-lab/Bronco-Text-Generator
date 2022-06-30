using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public interface ITerminal : ISymbol
    {
        public string Value { get; }

        ISymbol ISymbol.Evaluate()
        {
            return this;
        }

        public String ToString()
        {
            return Value;
        }
    }
}
