using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    internal class FloatSymbol : ITerminal
    {
        public float FloatValue { get; set; }

        public string Value { get => FloatValue.ToString(); }

        public FloatSymbol(float value)
        {
            FloatValue = value;
        }
    }
}
