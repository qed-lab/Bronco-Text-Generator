using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class And : DynamicSymbol
    {
        public And()
            => AddEvaluation<BoolSymbol, BoolSymbol>(IsAnd);

        public ISymbol IsAnd(BoolSymbol symbol1, BoolSymbol symbol2)
            => new BoolSymbol(symbol1.State && symbol2.State);
    }

    public class Or : DynamicSymbol
    {
        public Or()
            => AddEvaluation<BoolSymbol, BoolSymbol>(IsOr);

        public ISymbol IsOr(BoolSymbol symbol1, BoolSymbol symbol2)
            => new BoolSymbol(symbol1.State || symbol2.State);
    }

    public class Not : DynamicSymbol
    {
        public Not()
            => AddEvaluation<BoolSymbol>(IsNot);

        public ISymbol IsNot(BoolSymbol symbol1)
            => new BoolSymbol(!symbol1.State);
    }
}
