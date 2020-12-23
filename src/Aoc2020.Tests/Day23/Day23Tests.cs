using Aoc2020.Lib.Day23;
using System;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day23
{
    public class Day23Tests
    {
        [Theory]
        [InlineData("389125467", 10, "92658374")]
        [InlineData("389125467", 100, "67384529")]
        [InlineData("538914762", 100, "54327968")]
        public void Part1(string cups, int rounds, string expected)
        {
            var sut = new CupsGame(cups.Select(c => Convert.ToInt32(c.ToString())).ToArray());
            var actual = sut.Play(rounds);
            Assert.Equal(expected, actual);
        }
    }
}
