using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class UserVariable : SymbolVariable
    {
        public UserVariable(string name) : base(name) { }

        public UserVariable(string name, ISymbol symbol) : base(name, symbol) { }
    }
}
