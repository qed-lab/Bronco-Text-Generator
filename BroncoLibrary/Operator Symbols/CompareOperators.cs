using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class LessThan : DynamicSymbol
    {
        public LessThan()
            => AddEvaluation<FloatSymbol, FloatSymbol>(IsLessThan);

        public ISymbol IsLessThan(FloatSymbol symbol1, FloatSymbol symbol2)
            => new BoolSymbol(symbol1.FloatValue < symbol2.FloatValue);
    }

    public class GreaterThan : DynamicSymbol
    {
        public GreaterThan()
            => AddEvaluation<FloatSymbol, FloatSymbol>(IsGreaterThan);

        public ISymbol IsGreaterThan(FloatSymbol symbol1, FloatSymbol symbol2)
            => new BoolSymbol(symbol1.FloatValue > symbol2.FloatValue);
    }

    public class Equals : DynamicSymbol
    {
        public Equals()
            => AddEvaluation<FloatSymbol, FloatSymbol>(IsEqual);

        public ISymbol IsEqual(FloatSymbol symbol1, FloatSymbol symbol2)
            => new BoolSymbol(symbol1.FloatValue == symbol2.FloatValue);
    }
}
