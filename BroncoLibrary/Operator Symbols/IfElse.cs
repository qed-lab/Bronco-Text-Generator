using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class IfElse : DynamicSymbol
    {
        private static readonly Random _random = new Random();

        public IfElse()
        {
            AddEvaluation<BoolSymbol, ISymbol, ISymbol>(Condition);
        }

        public ISymbol Condition(BoolSymbol con, ISymbol symbol1,  ISymbol symbol2)
        {
            if(con.State)
                return symbol1;

            return symbol2;
        }
    }
}
