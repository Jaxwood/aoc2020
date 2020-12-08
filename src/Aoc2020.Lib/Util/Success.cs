namespace Aoc2020.Lib.Util
{
    public class Success<T> : Result<T>
    {
        public Success(T result)
        {
            this.Value = result;
        }

        public T Value { get; private set; }
    }
}