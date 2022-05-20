using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class MetaTerminal : Terminal, IMetaSymbol
    {

        public MetaData Data { get; set; }

        public MetaTerminal(string value) : base(value)
        {
            Data = new MetaData();
        }
    }
}
