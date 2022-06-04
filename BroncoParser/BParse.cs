using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BroncoParser.BParse;

namespace BroncoParser
{
    public delegate Result<T> Parser<T>(string input);

    public class BParse
    {
        public static Result<T> Result<T>(Func<T> value, Result source)
        {
            if (!source.Success) return new Result<T>();

            return new Result<T>(value(), source.Remainder);
        }

        public static Parser<string> Char(char c)
        {
            return (input) =>
            {
                if (input.Length != 0 && input[0] == c)
                    return new Result<string>(c.ToString(), input.Substring(1));

                return new Result<string>();
            };
        }

        public static Parser<string> Char(string s)
        {
            return (input) =>
            {
                if (input.Length != 0 && s.Contains(input[0]))
                    return new Result<string>(input[0].ToString(), input.Substring(1));

                return new Result<string>();
            };
        }

        public static Parser<string> AnyChar =
            (input) => input.Length == 0 ? 
            new Result<string>() : 
            new Result<string>(input[0].ToString(), input.Substring(1));

        public static Parser<string> String(string s)
        {
            return (input) =>
            {
                if (input.StartsWith(s))
                    return new Result<string>(s, input.Substring(s.Length));

                return new Result<string>();
            };
        }

        public static Parser<string> NewLine = String(Environment.NewLine);

        public static Parser<string> EndOfInput = (string input) 
            => input.Length == 0 ? new Result<string>("", "") : new Result<string>();
    }
}
