using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BroncoParser.BParse;

namespace BroncoParser
{
    public class BParse
    {
        public class Result<T>
        {
            public T Value { get; private set; } = default(T);
            public bool Success { get; private set; }
            public string Remainder { get; private set; } = "";

            public Result(T value, string remainder) 
            {
                Value = value; 
                Remainder = remainder;
                Success = true; 
            }

            public Result() { Success = false; }   
        }

        public delegate Result<T> Parser<T>(string input);

        public static T Parse<T>(Parser<T> parser, ref string input)
        {
            Result<T> result = parser(input);
            input = result.Remainder;
            return result.Value;
        }

        public static Parser<Char> Char(char c)
        {
            return (input) =>
            {
                if (input.Length != 0 && input[0] == c)
                    return new Result<char>(c, input.Substring(1));

                return new Result<char>();
            };
        }

        public static Parser<Char> Char(string s)
        {
            return (input) =>
            {
                if (input.Length != 0 && s.Contains(input[0]))
                    return new Result<char>(input[0], input.Substring(1));

                return new Result<char>();
            };
        }

        public static Parser<Char> AnyChar()
        {
            return (input) => input.Length == 0 ? new Result<char>() : new Result<char>(input[0], input.Substring(1));
        }

        public static Parser<string> String(string s)
        {
            return (input) =>
            {
                if (input.StartsWith(s))
                    return new Result<string>(s, input.Substring(s.Length));

                return new Result<string>();
            };
        }

        public static Parser<T> Or<T>(Parser<T> parser1, Parser<T> parser2)
        {
            return (input) =>
            {
                var result = parser1(input);

                if (!result.Success)
                    result = parser2(input);

                return result;
            };
        }
    }

    public static class ParseExtensions
    {
        public static Result<T> Result<T>(this T value, string remainder)
        {
            return new Result<T>(value, remainder);
        }

        public static Parser<IEnumerable<T>> Many<T>(this Parser<T> parser)
        {
            return (input) =>
            {
                List<T> results = new List<T>();
                var output = parser(input);
                var parsed = input;

                while (output.Success)
                {
                    results.Add(output.Value);
                    parsed = output.Remainder;
                    output = parser(parsed);
                }

                return new Result<IEnumerable<T>>(results, parsed);
            };
        }

        public static Parser<IEnumerable<T>> Until<T, U>(this Parser<T> parser, Parser<U> until)
        {
            return (input) =>
            {
                List<T> results = new List<T>();

                var parse = parser(input);
                var terminate = until(input);

                while (!terminate.Success && parse.Success)
                {
                    results.Add(parse.Value);
                    input = parse.Remainder;
                    parse = parser(input);
                    terminate = until(input);
                }

                return new Result<IEnumerable<T>>(results, input);
            };
        }

        public static Parser<T> Optional<T>(this Parser<T> parser)
        {
            return (input) =>
            {
                var output = parser(input);

                if (output.Success) return output;

                return new Result<T>(default(T), input);
            };
        }

        public static Parser<string> String<T>(this Parser<T> parser)
        {
            return (input) =>
            {
                var output = parser(input);

                if (output.Success) return new Result<string>(output.Value.ToString(), output.Remainder);

                return new Result<string>();
            };
        }

        public static Parser<string> String<T>(this Parser<IEnumerable<T>> parser)
        {
            return (input) =>
            {
                var output = parser(input);

                if (!output.Success) return new Result<string>();

                StringBuilder sb = new StringBuilder();
                foreach (var item in output.Value)
                    sb.Append(item.ToString());

                return new Result<string>(sb.ToString(), output.Remainder);
            };
        }

        public static Parser<U> Map<T,U>(this Parser<T> parser, Func<T,U> map)
        {
            return (input) =>
            {
                var output = parser(input);

                if (output.Success) return new Result<U>(map(output.Value), output.Remainder);

                return new Result<U>();
            };
        }
    }
}
