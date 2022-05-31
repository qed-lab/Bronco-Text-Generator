namespace BroncoParser
{
    public class Result<T>
    {
        public T Value { get; private set; }

        public bool Success { get; private set; }

        public string Remainder { get; private set; }

        public Result(T value, string remainder)
        {
            Value = value;
            Success = true;
            Remainder = remainder;
        }

        public Result()
        {
            Success = false;
        }
    }
}