using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class TagMatcher : DynamicSymbol
    {
        public TagMatcher()
            => AddEvaluation<MetaData, MetaData>(TagMatch);

        public ISymbol TagMatch(MetaData s1, MetaData s2)
            => new FloatSymbol(s1.Tags.CompareTags(s2.Tags));
    }
}
