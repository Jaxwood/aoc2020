using Aoc2020.Lib.Util;
using System;

namespace Aoc2020.Tests.Util
{
    public class LineFactory : IParseFactory<Line>
    {
        public Line Create(string raw)
        {
            var parts = raw.Split('=', ';');
            return new Line
            {
                Left = parts[0],
                Right = parts[1],
                Number = Convert.ToInt32(parts[2]),
            };
        }
    }
}
