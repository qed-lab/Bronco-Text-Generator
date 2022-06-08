using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class VariableSetter : DynamicSymbol
    {
        public VariableSetter() 
        {
            AddEvaluation<SymbolVariable, ISymbol>(Set);
        }

        public ISymbol Set(SymbolVariable var, ISymbol value)
        {
            var.Set(value);

            return var;
        }
    }
}
