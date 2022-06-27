using Antlr4.Runtime;
using BroncoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoParserANTLR
{
    public static class BroncoParser
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
            ExplicitBroncoGrammarLexer speakLexer = new(input);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            ExplicitBroncoGrammarParser parser = new ExplicitBroncoGrammarParser(commonTokenStream);
            parser.AddErrorListener(new ExceptionErrorListner());

            BroncoExplicitVisitor visitor = new();
            var symbol = (ISymbol)visitor.Visit(parser.file());
            return symbol;
        }
    }
}
