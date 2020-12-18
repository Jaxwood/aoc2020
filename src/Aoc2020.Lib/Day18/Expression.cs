namespace Aoc2020.Lib.Day18
{
    public record Expression(Token Token, long Value = default);

    public enum Token
    {
        Open = 0,
        Close = 1,
        Add = 2,
        Multiply = 3,
        Number = 4,
    }
}
