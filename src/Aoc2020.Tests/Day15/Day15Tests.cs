using Aoc2020.Lib.Day15;
using Xunit;

namespace Aoc2020.Tests.Day15
{
    public class Day15Tests
    {
        [Theory]
        [InlineData("0,3,6", 436)]
        [InlineData("1,3,2", 1)]
        [InlineData("2,1,3", 10)]
        [InlineData("1,2,3", 27)]
        [InlineData("2,3,1", 78)]
        [InlineData("3,2,1", 438)]
        [InlineData("3,1,2", 1836)]
        [InlineData("13,16,0,12,15,1", 319)]
        public void Part1(string numbers, long expected)
        {
            var sut = new MemoryGame(numbers, 2020);
            var actual = sut.Play();
            Assert.Equal(expected, actual);
        }
    }
}
