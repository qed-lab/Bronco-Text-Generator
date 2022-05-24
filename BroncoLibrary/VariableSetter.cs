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
            addEvaluation<SymbolVariable, ISymbol>(SetToFlatten);
        }

        public ISymbol SetToFlatten(SymbolVariable var, ISymbol value)
        {
            var.SetToFlatten(value);

            return var;
        }
    }
}
