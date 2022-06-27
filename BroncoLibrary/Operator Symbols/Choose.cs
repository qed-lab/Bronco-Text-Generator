using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Choose : DynamicSymbol
    {
        private static readonly Random _random = new Random();

        public Choose()
        {
            AddEvaluation(Choice);
        }

        public ISymbol Choice(ISymbol[] args)
            => args[_random.Next(args.Length)];
    }
}
