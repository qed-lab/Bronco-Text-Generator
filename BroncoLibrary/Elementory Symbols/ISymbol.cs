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
            => FlattenTo(this, type);

        public ITerminal Flatten()
            => FlattenTo<ITerminal>();

        public string ToString()
            => Flatten().Value;

        public static ISymbol FlattenTo(ISymbol toFlat, Type type)
        {
            //TODO: MetaData has gotten progessivly more and more janky. First thing to fix on refactor
            MetaData metaData = null;
            ISymbol current = toFlat;

            while (!type.IsAssignableFrom(current.GetType()))
            {
                if (current is ITerminal) throw new Exception("Symbol cannot be flattened to desired type");
                if(metaData == null && current is MetaData) metaData = (MetaData)current;

                current = current.Evaluate();
            }

            if(metaData != null)
            {
                if (current is ITerminal)
                    return new TerminalMetaData((ITerminal)current, metaData);
                else
                    return new MetaData(current, metaData);
            }

            return current;
        }
    }
}
