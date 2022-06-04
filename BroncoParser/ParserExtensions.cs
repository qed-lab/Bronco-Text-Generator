using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParser
{
    public static class ParseExtensions
    {
        public static Parser<T> Put<T>(this Parser<T> parser, T ?output)
        {
            return (input) =>
            {
                var result = parser(input);

                output = result.Value;

                return result;
            };
        }

        public static Parser<T> Or<T>(this Parser<T> parser1, Parser<T> parser2)
        {
            return (input) =>
            {
                var result = parser1(input);

                if (!result.Success)
                    result = parser2(input);

                return result;
            };
        }

        public static Parser<object> Or<T, U>(this Parser<T> parser1, Parser<U> parser2)
        {
            return (input) =>
            {
                Result result = parser1(input);

                if (!result.Success)
                    result = parser2(input);

                return new Result<Object>();
            };
        }

        public static Parser<(T, U)> Then<T, U>(this Parser<T> parser1, Parser<U> parser2)
        {
            return (input) =>
            {
                var result1 = parser1(input);

                if (result1.Success)
                {
                    var result2 = parser2(result1.Remainder);

                    if (result2.Success) return new Result<(T, U)>((result1.Value, result2.Value), result2.Remainder);
                }

                return new Result<(T, U)>();
            };
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

        public static Parser<U> Map<T, U>(this Parser<T> parser, Func<T, U> map)
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
