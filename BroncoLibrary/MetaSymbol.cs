﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public abstract class MetaSymbol : ISymbol
    {
        private static readonly string weightKey = "weight";
        private static readonly Func<double> weightFallBack = () => 1.0;
        private static readonly string tagsKey = "tags";
        private static readonly Func<ICollection<string>> tagsFallBack = () => new List<string> { };

        private Dictionary<object, object> _metaData;

        public double Weight
        {
            get
            {
                return GetMetaData<double>(weightKey, weightFallBack);
            }

            private set
            {
                SetMetaData(weightKey, value);
            }
        }

        public MetaSymbol()
        {
            _metaData = new Dictionary<object, object>();
        }

        public T GetMetaData<T>(object key)
        {
            return (T) _metaData[key];
        }

        public T GetMetaData<T>(object key, Func<T> fallBack)
        {
            T value = GetMetaData<T>(key);
            if (value != null) return value;

            T fallBackValue = fallBack.Invoke();
            SetMetaData(key, fallBackValue);

            return fallBackValue;
        }

        public void SetMetaData(object key, object value)
        {
            _metaData[key] = value;
        }

        public abstract ISymbol Evaluate();
    }
}
