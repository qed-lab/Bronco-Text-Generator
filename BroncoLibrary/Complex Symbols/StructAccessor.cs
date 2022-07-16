using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class StructAccessor : ISymbol
    {
        private SymbolStruct _origin;
        private int[] _fields;

        public StructAccessor(SymbolStruct origin, int[] fields)
        {
            _origin = origin;
            _fields = fields;
        }

        public ISymbol Evaluate()
        {
            ISymbol current = _origin;

            foreach(var field in _fields)
            {
                SymbolStruct currentStruct = current.FlattenTo<SymbolStruct>();
                current = currentStruct.GetField(field); 
            }

            return current;
        }
    }
}
