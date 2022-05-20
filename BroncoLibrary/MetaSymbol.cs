using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public abstract class MetaSymbol : ISymbol
    {
        private Dictionary<object, object> _metaData;

        public MetaSymbol()
        {
            _metaData = new Dictionary<object, object>();
        }

        public T getMetaData<T>(object key)
        {
            return (T) _metaData[key];
        }

        public string getMetaData(string key)
        {
            return (string)_metaData[key];
        }

        public void setMetaData(object key, object value)
        {
            _metaData[key] = value;
        }

        public abstract ISymbol Evaluate();
    }
}
