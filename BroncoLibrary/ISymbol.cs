namespace BroncoLibrary
{
    public interface ISymbol
    {
        public ISymbol Evaluate();

        public string GetString()
        {
            return Evaluate().GetString();
        }
    }
}