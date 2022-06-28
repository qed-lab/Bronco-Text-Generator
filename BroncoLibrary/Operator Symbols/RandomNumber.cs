using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class RandomFloat : DynamicSymbol
    {
        private static readonly Random _random = new Random();

        public RandomFloat()
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

    public class RandomInt : DynamicSymbol
    {
        private static readonly Random _random = new Random();

        public RandomInt()
        {
            AddEvaluation<FloatSymbol, FloatSymbol>(Rand);
            AddEvaluation<FloatSymbol>(Rand);
        }

        public ISymbol Rand(FloatSymbol lower, FloatSymbol upper)
        {
            float l = lower.FloatValue;
            float u = upper.FloatValue;

            return new FloatSymbol((float) Math.Round(_random.NextDouble() * (u - l) + l));
        }

        public ISymbol Rand(FloatSymbol upper)
            => new FloatSymbol((float)Math.Round(_random.NextDouble() * upper.FloatValue));
    }
}
