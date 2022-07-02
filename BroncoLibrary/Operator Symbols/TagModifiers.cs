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

    public class TagRemover : DynamicSymbol
    {
        public TagRemover()
        {
            AddEvaluation<MetaData, MetaData>(Remove);
        }

        public ISymbol Remove(MetaData remove, MetaData removeFrom)
        {
            foreach (var tag in remove.Tags)
                if (removeFrom.Tags.Contains(tag)) removeFrom.Tags.Tags.Remove(tag.Key);

            return removeFrom;
        }
    }
}
