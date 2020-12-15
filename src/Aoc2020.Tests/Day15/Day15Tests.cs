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

        [Theory]
        [InlineData("0,3,6", 175594)]
        [InlineData("1,3,2", 2578)]
        [InlineData("2,1,3", 3544142)]
        [InlineData("1,2,3", 261214)]
        [InlineData("2,3,1", 6895259)]
        [InlineData("3,2,1", 18)]
        [InlineData("3,1,2", 362)]
        [InlineData("13,16,0,12,15,1", 2424)]
        public void Part2(string numbers, long expected)
        {
            var sut = new MemoryGame(numbers, 30_000_000);
            var actual = sut.Play();
            Assert.Equal(expected, actual);
        }
    }
}
