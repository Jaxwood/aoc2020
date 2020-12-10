using Aoc2020.Lib.Day10;
using Aoc2020.Lib.Util;
using Aoc2020.Tests.Day01;
using Xunit;

namespace Aoc2020.Tests.Day10
{
    public class Day10Tests
    {
        [Theory]
        [InlineData("Day10/Example1.txt", 35)]
        [InlineData("Day10/Example2.txt", 220)]
        [InlineData("Day10/Input.txt", 2201)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var lines = parser.Parse(new IdentityFactory());
            var sut = new JoltAdapter(lines);
            var actual = sut.Chain();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day10/Example1.txt", 8)]
        [InlineData("Day10/Example2.txt", 19208)]
        [InlineData("Day10/Input.txt", 169255295254528)]
        public void Part2(string filepath, long expected)
        {
            var parser = new Parser(filepath);
            var lines = parser.Parse(new IdentityFactory());
            var sut = new JoltAdapter(lines);
            var actual = sut.Pathways();
            Assert.Equal(expected, actual);
        }
    }
}
