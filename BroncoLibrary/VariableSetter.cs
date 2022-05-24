using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class VariableSetter : Symbol
    {
        public VariableSetter() 
        {
            addEvaluation<SymbolVariable, Symbol>(Set);
        }

        public Symbol Set(SymbolVariable var, Symbol value)
        {
            var.Set(value);

            return var;
        }
    }
}
