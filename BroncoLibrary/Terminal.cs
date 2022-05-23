using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class Terminal : Symbol, ITerminal
    {
        private string _stringValue;

        public string Value => _stringValue;

        public Terminal(string value)
        {
            _stringValue = value;

            addEvaluation(Evaluate);
        }

        public Terminal Evaluate()
        {
            return this;
        }
    }
}
