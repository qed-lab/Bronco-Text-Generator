using BroncoLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoTest
{
    public class P : ICollection<object>
    {
        private List<Object> _items;
        
        public int Count => throw new InvalidOperationException();
        public bool IsReadOnly => throw new InvalidOperationException();

        public P()
        {
            _items = new List<object>();
        }

        public void Add(object item)
        {
            _items.Add(item);
        }

        public static explicit operator Bag(P p)
        {
            Bag bag = new Bag();

            foreach (var item in p._items)
            {
                switch(item)
                {
                    case "THIS":
                        bag.Add(new MetaData<ISymbol>(bag));
                        break;
                    case String s:
                        bag.Add(new MetaData<ISymbol>(new Terminal(s)));
                        break;
                    case DynamicSymbol s:
                        bag.Add(new MetaData<ISymbol>(s));
                        break;
                    case Tuple<String, double> t:
                        bag.Add(new MetaData<ISymbol>(new Terminal(t.Item1), t.Item2));
                        break;
                    case Tuple<DynamicSymbol, double> t:
                        bag.Add(new MetaData<ISymbol>(t.Item1, t.Item2));
                        break;
                }
            }

            return bag;
        }

        public static explicit operator SymbolList(P o)
        {
            SymbolList bag = new SymbolList();

            foreach (var item in o._items)
            {
                switch (item)
                {
                    case "THIS":
                        bag.Add(bag);
                        break;
                    case String s:
                        bag.Add(new Terminal(s));
                        break;
                    case ISymbol s:
                        bag.Add(s);
                        break;
                }
            }

            return bag;
        }

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(object item)
        {
            throw new InvalidOperationException();
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            throw new InvalidOperationException();
        }

        public IEnumerator<object> GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        public bool Remove(object item)
        {
            throw new InvalidOperationException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException();
        }
    }
}
