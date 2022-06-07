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

                if(result.Success)
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

        public static Parser<U> Then<T, U>(this Parser<T> parser1, Parser<U> parser2)
        {
            return (input) =>
            {
                var result1 = parser1(input);

                if (result1.Success)
                {
                    var result2 = parser2(result1.Remainder);

                    if (result2.Success) return new Result<U>(result2.Value, result2.Remainder);
                }

                return new Result<U>();
            };
        }

        public static Parser<T> ThenConsume<T, U>(this Parser<T> parser1, Parser<U> parser2)
        {
            return (input) =>
            {
                var result1 = parser1(input);

                if (result1.Success)
                {
                    var result2 = parser2(result1.Remainder);

                    if (result2.Success) return new Result<T>(result1.Value, result2.Remainder);
                }

                return new Result<T>();
            };
        }

        public static Parser<T> Trim<T>(this Parser<T> parser)
        {
            return BParse.WhiteSpace.Many().Then(parser).ThenConsume(BParse.InlineWhiteSpace.Many());
        }

        public static Parser<IList<T>> Until<T, U>(this Parser<T> parser, Parser<U> until)
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

                return new Result<IList<T>>(results, parsed);
            };
        }

        public static Parser<IList<T>> Many<T>(this Parser<T> parser)
        {
            return Until(parser, parser.Not());
        }

        public static Parser<U> UntilParse<T, U>(this Parser<T> parser, Parser<U> until)
        {
            return parser.Until(until).Then(until);
        }

        public static Parser<IList<T>> AtLeastOne<T>(this Parser<IList<T>> parser)
        {
            return (input) =>
            {
                var result = parser(input);

                if (result.Success && result.Value.Count == 0) return new Result<IList<T>>();

                return result;
            };
        }

        public static Parser<IList<T>> Add<T>(this Parser<IList<T>> parser1, Parser<T> parser2)
        {
            return (input) =>
            {
                var result1 = parser1(input);

                if(result1.Success)
                {
                    var result2 = parser2(result1.Remainder);
                    return BParse.Result<IList<T>>(() => {
                        result1.Value.Add(result2.Value);
                        return result1.Value;
                        }, result2);
                }

                return new Result<IList<T>>();
            };
        }

        public static Parser<IList<T>> AddOptional<T>(this Parser<IList<T>> parser1, Parser<T> parser2)
        {
            return (input) =>
            {
                var result1 = parser1.Add(parser2)(input);

                if (result1.Success) return result1;

                return parser1(input);
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

        public static Parser<string> String<T>(this Parser<IList<T>> parser)
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

                if (!result.Success) return new Result<U>();

                return new Result<U>(map(result.Value), result.Remainder);
            };
        }

        public static Parser<U> MapParse<T, U>(this Parser<T> parser, Func<T, Result<U>> map)
        {
            return (input) =>
            {
                var result = parser.Map(map)(input);

                if(!result.Success || !result.Value.Success) return new Result<U>();

                return new Result<U>(result.Value.Value, result.Remainder);
            };
        }

        public static Parser<T> Not<T>(this Parser<T> parser)
        {
            return (input) =>
            {
                var result = parser(input);

                return result.Success ? new Result<T>() : new Result<T>(default(T), input);
            };
        }

        public static Parser<IList<T>> Split<T, U>(this Parser<T> parser, Parser<U> split)
        {
            return split.Optional().Then(
                parser
                .ThenConsume(split)
                .Many()
                .AddOptional(parser)
                );
        }
    }
}
