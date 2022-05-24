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
            addEvaluation<SymbolVariable, DynamicSymbol>(SetToFlatten);
        }

        public ISymbol SetToFlatten(SymbolVariable var, DynamicSymbol value)
        {
            var.SetToFlatten(value);

            return var;
        }
    }
}
