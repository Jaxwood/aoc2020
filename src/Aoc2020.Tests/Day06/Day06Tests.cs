using Aoc2020.Lib.Day06;
using Aoc2020.Lib.Util;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day06
{
    public class Day06Tests
    {
        [Theory]
        [InlineData("Day06/Example1.txt", 11)]
        [InlineData("Day06/Input.txt", 7128)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var answers = parser.Parse(new AnswerFactory()).Where(c => c != null);
            var actual = answers.Aggregate(0, (acc, next) =>
            {
                acc += next.Count;
                return acc;
            });
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day06/Example1.txt", 6)]
        [InlineData("Day06/Input.txt", 3640)]
        public void Part2(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var answers = parser.Parse(new SameAnswerFactory()).Where(c => c != null);
            var actual = answers.Aggregate(0, (acc, next) =>
            {
                acc += next.Count;
                return acc;
            });
            Assert.Equal(expected, actual);
        }
    }
}
