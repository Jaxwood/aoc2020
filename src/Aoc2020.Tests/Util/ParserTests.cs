using Aoc2020.Lib.Util;
using System.Collections.Generic;
using Xunit;

namespace Aoc2020.Tests.Util
{
    public class ParserTests
    {
        [Fact]
        public void CanParseAFile()
        {
            var sut = new Parser("filetoparse.txt");
            var actual = sut.Parse(new LineFactory());
            var expected = new List<LineStub>()
            {
                new LineStub
                {
                    Left = "foo",
                    Right = "bar",
                    Number = 3,
                },
                new LineStub
                {
                    Left = "baz",
                    Right = "qux",
                    Number = -1,
                }
            };
            Assert.Equal(expected, actual);
        }

    }
}
