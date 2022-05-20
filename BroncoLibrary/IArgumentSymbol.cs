using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    internal interface IArgumentSymbol : ISymbol
    {
        public ISymbol Evaluate(ICollection<ISymbol> arguments);

        ISymbol ISymbol.Evaluate()
        {
            return Evaluate(new ISymbol[0]);
        }
    }
}
