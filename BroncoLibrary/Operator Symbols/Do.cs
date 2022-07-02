using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Do : DynamicSymbol
    {
        public Do()
            => AddEvaluation(DoSymbols);

        public ISymbol DoSymbols(ISymbol[] symbols)
        {
            foreach(var symbol in symbols)
                symbol.Flatten();
            return new EmptySymbol();
        }
    }

    public class DoYield : DynamicSymbol
    {
        public DoYield()
            => AddEvaluation(DoSymbols);

        public ISymbol DoSymbols(ISymbol[] symbols)
        {
            for(int i = 0; i < symbols.Length-1; i++)
                symbols[i].Flatten();

            return symbols[symbols.Length - 1];
        }
    }
}
