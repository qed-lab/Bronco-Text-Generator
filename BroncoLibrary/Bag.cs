﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Bag : DynamicSymbol, ICollection<MetaData<ISymbol>>
    {
        private List<MetaData<ISymbol>> _symbols;
        private Random _random;

        public int Count => _symbols.Count;
        public bool IsReadOnly => true;

        public Bag()
        {
            _symbols = new List<MetaData<ISymbol>>();
            _random = new Random();

            addEvaluation(Pick);
        }

        public ISymbol Pick()
        {
            (MetaData<ISymbol>, double) best = (null, -double.MaxValue);

            foreach(var symbol in _symbols)
            {
                double rolledWeight = _random.NextDouble()*symbol.Weight;
                if (rolledWeight > best.Item2)
                    best = (symbol, rolledWeight);
            }

            return best.Item1.Evaluate();
        }

        public void Add(MetaData<ISymbol> item) => _symbols.Add(item);

        public void Clear() => _symbols.Clear();

        public bool Contains(MetaData<ISymbol> item) => _symbols.Contains(item);

        public void CopyTo(MetaData<ISymbol>[] array, int arrayIndex) => _symbols.CopyTo(array, arrayIndex);

        public bool Remove(MetaData<ISymbol> item) => _symbols.Remove(item);

        public IEnumerator<MetaData<ISymbol>> GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _symbols.GetEnumerator();
    }
}
