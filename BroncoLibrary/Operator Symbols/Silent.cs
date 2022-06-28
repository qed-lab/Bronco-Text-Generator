using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Silent : DynamicSymbol
    {
        public Silent()
            => AddEvaluation<ISymbol>(BeSilent);

        public ISymbol BeSilent(ISymbol symbol)
        {
            symbol.Evaluate();
            return new EmptySymbol();
        }
    }
}
