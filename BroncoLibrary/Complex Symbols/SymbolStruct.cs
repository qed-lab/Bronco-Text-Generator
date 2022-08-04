using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolStruct : DynamicSymbol
    {
        private IDictionary<string, UserVariable> _fieldLookup = new Dictionary<string, UserVariable>();

        public UserVariable GetField(string id)
        {
            UserVariable value;

            if(_fieldLookup.TryGetValue(id, out value))
                return value;

            value = new UserVariable(id);
            _fieldLookup.Add(id, value);
            return value;
        }
    }
}
