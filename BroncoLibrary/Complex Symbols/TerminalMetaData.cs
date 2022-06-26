using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class TerminalMetaData : MetaData, ITerminal
    {
        public string Value => ((ITerminal) Symbol).Value;

        public TerminalMetaData(ITerminal symbol) : base(symbol)
        {
        }

        public TerminalMetaData(ITerminal symbol, MetaData metaDataBase) : base(symbol, metaDataBase)
        {
        }

        public TerminalMetaData(ITerminal symbol, ICollection<string> tags) : base(symbol, tags)
        {
        }

        public TerminalMetaData(ITerminal symbol, double weight) : base(symbol, weight)
        {
        }
    }
}
