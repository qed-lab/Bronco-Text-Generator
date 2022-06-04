using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace BroncoParser
{
    public static class ParseExtensions
    {
        public static Parser<T> Do<T>(this Parser<T> parser, Action<T> action)
        {
            return (input) =>
            {
                var result = parser(input);

                action(result.Value);

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


                return BParse.Result<object>(() => result.Value, result);
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

        public static Parser<T> ThenConsume<T, U>(this Parser<T> parser1, Parser<U> parser2)
        {
            return (input) =>
            {
                var result = parser1.Then(parser2)(input);
                return BParse.Result(() => result.Value.Item1, result);
            };
        }

        public static Parser<IEnumerable<T>> Until<T, U>(this Parser<T> parser, Parser<U> until)
        {
            return (input) =>
            {
                List<T> results = new List<T>();

                var parsed = input;
                var parse = parser(parsed);
                var terminate = until(parsed);

                while (!terminate.Success && parse.Success)
                {
                    results.Add(parse.Value);
                    parsed = parse.Remainder;
                    parse = parser(parsed);
                    terminate = until(parsed);
                }

                return results.Count == 0 ? 
                new Result<IEnumerable<T>>() : new Result<IEnumerable<T>>(results, parsed);
            };
        }

        public static Parser<IEnumerable<T>> Many<T>(this Parser<T> parser)
        {
            return Until(parser, parser.Not());
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
                var result = parser(input);

                if (result.Success) return new Result<U>(map(result.Value), result.Remainder);

                return new Result<U>();
            };
        }

        public static Parser<bool> Not<T>(this Parser<T> parser)
        {
            return (input) =>
            {
                var result = parser(input);

                return result.Success ? new Result<bool>() : new Result<bool>(true, input);
            };
        }
    }
}
