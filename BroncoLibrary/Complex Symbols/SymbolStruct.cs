using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolStruct : DynamicSymbol
    {
        private IList<SymbolVariable> _fields = new List<SymbolVariable>();
        private IDictionary<string, int> _fieldLookup = new Dictionary<string, int>();

        public SymbolVariable GetField(int index)
            => _fields[index];

        public SymbolVariable GetField(string id)
            => _fields[GetFieldIndex(id)];

        public int GetFieldIndex(string id)
        {
            int index;

            if(_fieldLookup.TryGetValue(id, out index))
                return index;

            _fields.Add(new SymbolVariable(id));
            index = _fields.Count - 1;
            _fieldLookup.Add(id, index);
            return index;
        }
    }
}
