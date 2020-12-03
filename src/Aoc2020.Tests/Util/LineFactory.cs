using Aoc2020.Lib.Util;
using System;

namespace Aoc2020.Tests.Util
{
    public class LineFactory : IParseFactory<LineStub>
    {
        public LineStub Create(Line line)
        {
            var parts = line.Raw.Split('=', ';');
            return new LineStub
            {
                Left = parts[0],
                Right = parts[1],
                Number = Convert.ToInt32(parts[2]),
            };
        }
    }
}
