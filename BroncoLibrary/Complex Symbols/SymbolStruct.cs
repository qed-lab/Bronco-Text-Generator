using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class SymbolStruct : DynamicSymbol
    {
        private IList<UserVariable> _fields = new List<UserVariable>();
        private IDictionary<string, int> _fieldLookup = new Dictionary<string, int>();

        public UserVariable GetField(int index)
            => _fields[index];

        public UserVariable GetField(string id)
            => _fields[GetFieldIndex(id)];

        public int GetFieldIndex(string id)
        {
            int index;

            if(_fieldLookup.TryGetValue(id, out index))
                return index;

            _fields.Add(new UserVariable(id));
            index = _fields.Count - 1;
            _fieldLookup.Add(id, index);
            return index;
        }
    }
}
