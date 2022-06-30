using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class BagAdder : DynamicSymbol
    {
        public BagAdder() 
        {
            AddEvaluation<Bag, MetaData>(Add);
            AddEvaluation<Bag, MetaData, ISymbol>(Add);
        }

        public ISymbol Add(Bag addTo, MetaData item)
        {
            addTo.Add(item);

            return item;
        }

        public ISymbol Add(Bag addTo, MetaData item, ISymbol condition)
        {
            addTo.Add(item, condition);

            return item;
        }
    }
}
