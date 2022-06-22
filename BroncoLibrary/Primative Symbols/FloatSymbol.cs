using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class FloatSymbol : ITerminal
    {
        public virtual float FloatValue { get; set; }

        public virtual string Value { get => FloatValue.ToString(); }

        public FloatSymbol()
            => FloatValue = 0.0f;

        public FloatSymbol(float value)
            => FloatValue = value;
    }
}
