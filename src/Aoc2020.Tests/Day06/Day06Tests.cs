using Aoc2020.Lib.Util;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day06
{
    public class Day06Tests
    {
        [Theory]
        [InlineData("Day06/Example1.txt", 11)]
        [InlineData("Day06/Input.txt", 11)]
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
    }

    internal class AnswerFactory : IParseFactory<HashSet<char>>
    {
        private HashSet<char> set;

        public AnswerFactory()
        {
        }

        public HashSet<char> Create(Line line)
        {
            if (set == null)
            {
                set = new HashSet<char>();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                var copy = this.set;
                this.set = null;
                return copy;
            }

            foreach (var l in line.Raw)
            {
                set.Add(l);
            }
            
            if (line.LastLine)
            {
                return this.set;
            }

            return null;
        }
    }
}
