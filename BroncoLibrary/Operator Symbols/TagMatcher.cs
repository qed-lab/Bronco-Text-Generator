using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary.Operator_Symbols
{
    public class TagMatcher : DynamicSymbol
    {
        public TagMatcher()
            => AddEvaluation<MetaData<ISymbol>, MetaData<ISymbol>>(TagMatch);

        public ISymbol TagMatch(MetaData<ISymbol> s1, MetaData<ISymbol> s2)
            => new FloatSymbol(s1.Tags.CompareTags(s2.Tags));
    }
}
