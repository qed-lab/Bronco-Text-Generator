using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Add : DynamicSymbol
    {
        public Add()
            => AddEvaluation<FloatSymbol, FloatSymbol>(AddTo);

        public ISymbol AddTo(FloatSymbol symbol1, FloatSymbol symbol2)
            => new FloatSymbol(symbol1.FloatValue + symbol2.FloatValue);
    }

    public class Subtract : DynamicSymbol
    {
        public Subtract()
            => AddEvaluation<FloatSymbol, FloatSymbol>(SubTo);

        public ISymbol SubTo(FloatSymbol symbol1, FloatSymbol symbol2)
            => new FloatSymbol(symbol1.FloatValue - symbol2.FloatValue);
    }

    public class Multiply : DynamicSymbol
    {
        public Multiply()
            => AddEvaluation<FloatSymbol, FloatSymbol>(Mult);

        public ISymbol Mult(FloatSymbol symbol1, FloatSymbol symbol2)
            => new FloatSymbol(symbol1.FloatValue * symbol2.FloatValue);
    }

    public class Round : DynamicSymbol
    {
        public Round()
            => AddEvaluation<FloatSymbol>(RoundFloat);

        public ISymbol RoundFloat(FloatSymbol symbol1)
            => new FloatSymbol((float) Math.Round(symbol1.FloatValue));
    }
}
