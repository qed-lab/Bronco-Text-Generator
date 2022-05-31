using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParser
{
    public delegate Result<T> Parser<T>(string input);

    public static class ParserCombinators
    {
        public static Parser<T> Or<T>(this Parser<T> parser1, Parser<T> parser2)
        {
            return (input) =>
            {
                Result<T> result = parser1(input);

                if (!result.Success)
                    result = parser2(input);

                return result;
            };
        }

        public static Parser<IEnumerable<T>> Then<T>(this Parser<T> parser1, Parser<T> parser2)
        {
            return (input) =>
            {
                Result<T> result1 = parser1(input);
                if (!result1.Success) return new Result<IEnumerable<T>>();

                Result<T> result2 = parser2(result1.Remainder);
                if (!result2.Success) return new Result<IEnumerable<T>>();

                return new Result<IEnumerable<T>>(new List<T>() { result1.Value, result2.Value }, result2.Remainder);
            };
        }
    }
}
