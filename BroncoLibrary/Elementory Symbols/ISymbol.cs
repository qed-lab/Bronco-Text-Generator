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

        public ISymbol Evaluate();

        public SymbolVariable GetArgument(int index) 
            => throw new ArgumentException("This symbol does not have any arguments");

        public ISymbol Argue(ISymbol[] args)
            => throw new ArgumentException("This symbol does not accept any arguments");

        public T FlattenTo<T>() where T : ISymbol
            => (T) FlattenTo(typeof(T));
        
        public ISymbol FlattenTo(Type type)
        {
            ISymbol current = this;

            while (!type.IsAssignableFrom(current.GetType()))
            {
                if (current is ITerminal) throw new Exception("Symbol cannot be flattened to desired type");

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
