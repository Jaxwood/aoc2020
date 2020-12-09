using Aoc2020.Lib.Day09;
using Aoc2020.Lib.Util;
using Aoc2020.Tests.Day01;
using System;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day09
{
    public class Day09Tests
    {
        [Theory]
        [InlineData("Day09/Example1.txt", 5, 127)]
        [InlineData("Day09/Input.txt", 25, 104054607)]
        public void Part1(string filepath, int preamble, int expected)
        {
            var parser = new Parser(filepath);
            var lines = parser.Parse<long>(new XMAXFactory());
            var sut = new XMASEncrypter(lines.ToArray(), preamble);
            var actual = sut.Cypher();
            Assert.Equal(expected, actual.Value);
        }
    }

    internal class XMAXFactory : IParseFactory<long>
    {
        public long Create(Line line)
        {
            return Convert.ToInt64(line.Raw);
        }
    }
}
