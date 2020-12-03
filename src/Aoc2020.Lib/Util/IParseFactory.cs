namespace Aoc2020.Lib.Util
{
    public interface IParseFactory<T>
    {
        T Create(Line line);
    }
}