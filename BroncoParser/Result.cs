using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParser
{
    public class Result
    {
        protected object _value = null;
        public object Value { get { return _value; } set { _value = value; } }
        public bool Success { get; private set; }
        public string Remainder { get; private set; } = "";

        public Result(object value, string remainder)
        {
            Value = value;
            Remainder = remainder;
            Success = true;
        }

        public Result() { Success = false; }
    }

    public class Result<T> : Result
    {
        public new T Value { get { return (T)_value; } set { _value = (T)value; } }

        public Result(T value, string remainder) : base(value, remainder) { }

        public Result() : base() { }
    }
}
