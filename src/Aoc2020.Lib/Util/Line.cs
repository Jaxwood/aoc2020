namespace Aoc2020.Lib.Util
{
    public class Line
    {
        public Line(string raw, int lineNum, bool lastLine)
        {
            Raw = raw;
            LineNum = lineNum;
            LastLine = lastLine;
        }

        public string Raw { get; }
        public int LineNum { get; }
        public bool LastLine { get; }
    }
}