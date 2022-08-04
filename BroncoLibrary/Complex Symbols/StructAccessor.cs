using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class StructAccessor : ISymbol
    {
        private ISymbol _origin;
        private string[] _fields;

        public StructAccessor(ISymbol origin, string[] fields)
        {
            _origin = origin;
            _fields = fields;
        }

        public ISymbol Evaluate()
        {
            UserVariable current = _origin.FlattenTo<UserVariable>();

            foreach(var field in _fields)
            {
                if(!current.IsSet)
                {
                    current.SetPointer(new SymbolStruct());
                }

                SymbolStruct currentStruct = ((ISymbol) current).FlattenTo<SymbolStruct>();
                current = currentStruct.GetField(field); 
            }

            return current;
        }
    }
}
