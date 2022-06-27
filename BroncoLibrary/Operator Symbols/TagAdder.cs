using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class TagAdder : DynamicSymbol
    {
        public TagAdder() 
        {
            AddEvaluation<MetaData, MetaData>(Add);
        }

        public ISymbol Add(MetaData addTo, MetaData addFrom)
        {
            foreach (var tag in addFrom.Tags)
                addTo.Tags.AddTag(tag.Key, tag.Value);

            return addFrom;
        }
    }
}
