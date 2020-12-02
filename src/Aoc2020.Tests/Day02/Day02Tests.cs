using Aoc2020.Lib.Day02;
using Aoc2020.Lib.Util;
using System;
using Xunit;

namespace Aoc2020.Tests.Day02
{
    public class Day02Tests
    {
        [Theory]
        [InlineData("Day02/Example1.txt", 2)]
        [InlineData("Day02/Input.txt", 556)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var policies = parser.Parse<Policy>(new PolicyFactory());
            var sut = new PasswordValidator(policies, new PartOneValidator());
            Assert.Equal(expected, sut.Validate());
        }

        [Theory]
        [InlineData("Day02/Example1.txt", 1)]
        [InlineData("Day02/Input.txt", 605)]
        public void Part2(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var policies = parser.Parse<Policy>(new PolicyFactory());
            var sut = new PasswordValidator(policies, new PartTwoValidator());
            Assert.Equal(expected, sut.Validate());
        }
    }

    internal class PolicyFactory : IParseFactory<Policy>
    {
        public Policy Create(string raw)
        {
            var segements = raw.Split(' ');
            var amount = segements[0].Split('-');
            var min = Convert.ToInt32(amount[0]);
            var max = Convert.ToInt32(amount[1]);
            var letter = Convert.ToChar(segements[1][0]);
            var password = segements[2];
            return new Policy(min, max, letter, password);
        }
    }
}
