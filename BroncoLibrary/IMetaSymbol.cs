using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public interface IMetaSymbol : ISymbol
    {
        public MetaData Data { get; set; }

        public abstract ISymbol Evaluate();
    }
}
