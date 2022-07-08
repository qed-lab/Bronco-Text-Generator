using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    static class Util
    {
        public static bool Vowel(char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }
    }

    public class Capitalize : DynamicSymbol
    {
        public Capitalize()
            => AddEvaluation<ITerminal>(Cap);

        public ISymbol Cap(ITerminal input)
            => new Terminal(input.Value[0].ToString().ToUpper() + input.Value.Substring(1));
    }

    public class Ana : DynamicSymbol
    {
        public Ana()
            => AddEvaluation<ITerminal>(A);

        public ISymbol A(ITerminal input)
        {
            string text = input.Value.ToLower();

            if(text.Length > 0)
            {
                if(text.Length > 2 && text[0] == 'u' && text[2] == 'i')
                    return new Terminal("a " + input.Value);

                if(Util.Vowel(text[0]))
                    return new Terminal("an " + input.Value);
            }

            return new Terminal("an " + input.Value);
        }
    }

    public class Plural : DynamicSymbol
    {
        public Plural()
            => AddEvaluation<ITerminal>(S);

        public ISymbol S(ITerminal input)
        {
            string text = input.Value.ToLower();

            if (text.Length > 0)
            {
                char c = text[text.Length - 1];
                char c2 = text[text.Length - 2];

                if (c == 's' || c == 'x' || c == 'z' || 
                    ((c2 == 'c' || c2 == 's') && c == 'h'))
                    return new Terminal(input.Value + "es");

                if (c == 'y' && !Util.Vowel(c2))
                    return new Terminal(input.Value.Substring(0, text.Length - 1) + "ies");
            }

            return new Terminal(input.Value + "s");
        }
    }

    public class Ed : DynamicSymbol
    {
        public Ed()
            => AddEvaluation<ITerminal>(Eded);

        public ISymbol Eded(ITerminal input)
        {
            string text = input.Value.ToLower();

            if (text.Length > 0)
            {
                char c = text[text.Length - 1];

                if (c == 'e')
                    return new Terminal(input.Value + "d");

                if (c == 'y' && !Util.Vowel(text[text.Length - 2]))
                    return new Terminal(input.Value.Substring(0, text.Length - 1) + "ied");
            }

            return new Terminal(input.Value + "ed");
        }
    }
}
