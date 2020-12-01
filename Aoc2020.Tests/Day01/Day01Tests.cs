using Aoc2020.Lib.Util;
using System;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day01
{
    public class Day01Tests
    {
        [Theory]
        [InlineData("Day01/Example1.txt", 514579)]
        [InlineData("Day01/Input.txt", 980499)]
        public void Part1(string filepath, int expected)
        {
            var sut = new Aoc2020.Lib.Day01.ExpenseReport();
            var parser = new Parser(filepath);
            var lines = parser.Parse(new IdentityFactory());
            var actual = sut.Part1(lines.ToArray());
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day01/Example1.txt", 241861950)]
        [InlineData("Day01/Input.txt", 200637446)]
        public void Part2(string filepath, int expected)
        {
            var sut = new Aoc2020.Lib.Day01.ExpenseReport();
            var parser = new Parser(filepath);
            var lines = parser.Parse(new IdentityFactory());
            var actual = sut.Part2(lines.ToArray());
            Assert.Equal(expected, actual);
        }
    }

    public class IdentityFactory : IParseFactory<int>
    {
        public int Create(string raw)
        {
            return Convert.ToInt32(raw);
        }
    }
}
