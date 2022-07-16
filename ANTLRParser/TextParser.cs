using Antlr4.Runtime;
using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoTextParser
{
    public static class TextParser
    {
        private class ExceptionErrorListner : IAntlrErrorListener<object>
        {
            public void SyntaxError(TextWriter output, IRecognizer recognizer, object offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                throw new Exception($"Parser error at {line}:{charPositionInLine} `{msg}`");
            }
        }

        private static BroncoVisitor _lastVisitor;

        public static ISymbol Parse(Stream input)
            => Parse(new AntlrInputStream(input));

        public static ISymbol Parse(string input)
            => Parse(new AntlrInputStream(input));

        public static ISymbol Parse(AntlrInputStream input)
            => GetSymbol(input, new());

        public static ISymbol Parse(Stream input, IEnumerable<KeyValuePair<string, ISymbol>> startingReferences)
            => Parse(new AntlrInputStream(input), startingReferences);

        public static ISymbol Parse(string input, IEnumerable<KeyValuePair<string, ISymbol>> startingReferences)
            => Parse(new AntlrInputStream(input), startingReferences);

        public static ISymbol Parse(AntlrInputStream input, IEnumerable<KeyValuePair<string, ISymbol>> startingReferences)
            => GetSymbol(input, new(startingReferences));

        public static IEnumerable<KeyValuePair<string, ISymbol>> GetReferences()
        {
            return _lastVisitor.GetReferences();
        }

        private static ISymbol GetSymbol(AntlrInputStream input, BroncoVisitor visitor)
        {
            BroncoLexer lexer = new(input);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            BroncoParser parser = new BroncoParser(commonTokenStream);
            parser.AddErrorListener(new ExceptionErrorListner());

            _lastVisitor = visitor;

            return (ISymbol)visitor.Visit(parser.file());
        }
    }
}
