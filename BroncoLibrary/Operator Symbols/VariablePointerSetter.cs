using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class VariablePointerSetter : DynamicSymbol
    {
        public VariablePointerSetter() 
        {
            AddEvaluation<SymbolVariable, ISymbol>(SetPointer);
        }

        public ISymbol SetPointer(SymbolVariable var, ISymbol value)
        {
            var.SetPointer(value);

            return var;
        }
    }
}
