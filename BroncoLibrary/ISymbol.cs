using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public interface ISymbol
    {
        public static readonly ISymbol[] EmptyArgs = new ISymbol[0];
        public static readonly SymbolVariable[] EmptyArgVars = new SymbolVariable[0];

        public SymbolVariable[] Arguments { get => EmptyArgVars; }

        public ISymbol Evaluate();

        public ISymbol Argue(ISymbol[] args)
        {
            throw new ArgumentException("This symbol does not accept any arguments");
        }

        public T FlattenTo<T>() where T : ISymbol
        {
            ISymbol current = this;

            while (!(current is T))
            {
                if (current is ITerminal) return default(T);

                current = current.Evaluate();
            }

            return (T)current;
        }

        public ISymbol FlattenTo(Type type)
        {
            ISymbol current = this;

            while (!type.IsAssignableFrom(current.GetType()))
            {
                if (current is ITerminal) return null;

                current = current.Evaluate();
            }

            return current;
        }

        public ITerminal Flatten()
        {
            return FlattenTo<ITerminal>();
        }
    }
}
