using Aoc2020.Lib.Day01;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
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
            var lines = Parse(filepath);
            var sut = new ExpenseReport(lines);
            var actual = sut.Part1(0, lines.Length - 1);
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("Day01/Example1.txt", 241861950)]
        [InlineData("Day01/Input.txt", 200637446)]
        public void Part2(string filepath, int expected)
        {
            var lines = Parse(filepath);
            var sut = new ExpenseReport(lines);
            var actual = sut.Part2(0, lines.Length - 1, 1);
            Assert.Equal(expected, actual);
        }

        private static int[] Parse(string filepath)
        {
            var parser = new Parser(filepath);
            var lines = parser.Parse(new IdentityFactory()).ToList();
            lines.Sort();
            return lines.ToArray();
        }
    }

    public class IdentityFactory : IParseFactory<int>
    {
        public int Create(Line line)
        {
            return Convert.ToInt32(line.Raw);
        }
    }
}
