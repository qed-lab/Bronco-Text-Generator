using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class MetaData<T> : DynamicSymbol, ISymbol where T : ISymbol
    {
        private static readonly string weightKey = "weight";
        private static readonly Func<double> weightFallBack = () => 1.0;
        private static readonly string tagsKey = "tags";
        private static readonly Func<TagSet> tagsFallBack = () => new TagSet();

        private Dictionary<object, object> _metaData;

        public T Symbol { get; set; }
        public double Weight
        {
            get
            {
                return GetMetaData<double>(weightKey, weightFallBack);
            }

            set
            {
                SetMetaData(weightKey, value);
            }
        }

        public TagSet Tags
        {
            get
            {
                return GetMetaData<TagSet>(tagsKey, tagsFallBack);
            }

            private set
            {
                SetMetaData(tagsKey, value);
            }
        }

        public MetaData(T symbol, ICollection<string> tags) : this(symbol)
        {
            foreach (var tag in tags)
                Tags.AddTag(tag);
        }

        public MetaData(T symbol, double weight) : this(symbol)
        {
            Weight = weight;
        }

        public MetaData(T symbol)
        {
            Symbol = symbol;
            _metaData = new Dictionary<object, object>();

            AddEvaluation(GetSymbol);
        }

        public ISymbol GetSymbol()
        {
            return Symbol;
        }

        public TData GetMetaData<TData>(object key)
        {
            return (TData)_metaData[key];
        }

        public TData GetMetaData<TData>(object key, Func<TData> fallBack)
        {
            if(!_metaData.ContainsKey(key))
            {
                TData fallBackValue = fallBack.Invoke();
                SetMetaData(key, fallBackValue);

                return fallBackValue;
            }

            return GetMetaData<TData>(key);
        }

        public void SetMetaData(object key, object value)
        {
            _metaData[key] = value;
        }

        ISymbol ISymbol.FlattenTo(Type type)
        {
            ISymbol symbol = this;
            return new MetaData<ISymbol>(ISymbol.FlattenTo(this, type));
        }
    }
}
