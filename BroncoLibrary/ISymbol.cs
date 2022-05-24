using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public interface ISymbol
    {
        public ISymbol Evaluate();

        public ISymbol Argue(ISymbol[] args)
        {
            throw new ArgumentException("This symbol does not accept any arguments");
        }

        public ITerminal Flatten()
        {
            ISymbol current = this;

            while(!(current is ITerminal))
            {
                current = current.Evaluate();
            }

            return (ITerminal) current;
        }
    }
}
