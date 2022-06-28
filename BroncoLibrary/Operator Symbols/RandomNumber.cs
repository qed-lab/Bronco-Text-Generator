using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class RandomNumber : DynamicSymbol
    {
        private static readonly Random _random = new Random();

        public RandomNumber()
        {
            AddEvaluation<FloatSymbol, FloatSymbol>(Rand);
            AddEvaluation<FloatSymbol>(Rand);
        }

        public ISymbol Rand(FloatSymbol lower, FloatSymbol upper)
        {
            float l = lower.FloatValue;
            float u = upper.FloatValue;

            return new FloatSymbol((float) _random.NextDouble()*(u - l) + l);
        }

        public ISymbol Rand(FloatSymbol upper)
            => new FloatSymbol((float)_random.NextDouble() * upper.FloatValue);
    }
}
