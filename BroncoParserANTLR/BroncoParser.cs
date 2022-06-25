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
        public static ISymbol Parse(string input)
            => Parse(new AntlrInputStream(input));

        public static ISymbol Parse(AntlrInputStream input)
        {
            ExplicitBroncoGrammarLexer speakLexer = new(input);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            ExplicitBroncoGrammarParser parser = new ExplicitBroncoGrammarParser(commonTokenStream);

            BroncoExplicitVisitor visitor = new();
            return (ISymbol)visitor.Visit(parser.file());
        }
    }
}
