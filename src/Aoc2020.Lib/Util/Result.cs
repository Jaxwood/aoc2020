namespace Aoc2020.Lib.Util
{
    public interface Result<T>
    {
        T Value { get; }
    }
}