using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Terminal : ISymbol
    {
        public string Value { get; private set; }

        public Terminal(string value)
        {
            Value = value;
        }

        public ISymbol Evaluate()
        {
            return this;
        }

        public string GetString()
        {
            return Value;
        }
    }
}
