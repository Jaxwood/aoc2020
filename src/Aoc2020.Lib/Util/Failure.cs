namespace Aoc2020.Lib.Util
{
    public class Failure<T> : Result<T>
    {
        public Failure()
        {
            this.Value = default(T);
        }

        public T Value { get; private set; }
    }
}