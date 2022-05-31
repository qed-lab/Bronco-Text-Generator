using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class BoolSymbol : ITerminal
    {
        public bool State { get; set; } = false;
        public string Value => State.ToString();
        
        public BoolSymbol(bool state)
            => State = state;
    }
}
