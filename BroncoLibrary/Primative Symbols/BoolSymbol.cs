using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class BoolSymbol : FloatSymbol
    {
        private bool _state = false;
        private float _value = 0.0f;

        public bool State { get => _state; 
            set
            {
                _state = value;
                _value = _state ? 1.0f : 0.0f;
            } }

        public override float FloatValue
        {
            get => _value; 
            set
            {
                _state = value > 0.0f;
                _value = _state ? 1.0f : 0.0f;
            }
        }

        public override string Value => State.ToString();
        
        public BoolSymbol(bool state)
            => State = state;
    }
}
