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

        public static ISymbol Parse(Stream input)
            => Parse(new AntlrInputStream(input));

        public static ISymbol Parse(string input)
            => Parse(new AntlrInputStream(input));

        public static ISymbol Parse(AntlrInputStream input)
        {
            BroncoLexer lexer = new(input);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            BroncoParser parser = new BroncoParser(commonTokenStream);
            parser.AddErrorListener(new ExceptionErrorListner());

            BroncoVisitor visitor = new();
            var symbol = (ISymbol)visitor.Visit(parser.file());
            return symbol;
        }
    }
}
