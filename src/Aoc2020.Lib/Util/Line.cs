namespace Aoc2020.Lib.Util
{
    public class Line
    {
        public Line(string raw, int lineNum)
        {
            Raw = raw;
            LineNum = lineNum;
        }

        public string Raw { get; }
        public int LineNum { get; }
    }
}