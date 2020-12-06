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

    internal class AnswerFactory : IParseFactory<HashSet<char>>
    {
        private HashSet<char> set;

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
    internal class SameAnswerFactory : IParseFactory<HashSet<char>>
    {
        private List<string> answers;

        public HashSet<char> Create(Line line)
        {
            if (answers == null)
            {
                answers = new List<string>();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                var same = FindSameAnswers();
                this.answers = null;
                return same;
            }

            answers.Add(line.Raw);
            
            if (line.LastLine)
            {
                return FindSameAnswers();
            }

            return null;
        }

        private HashSet<char> FindSameAnswers()
        {
            if (this.answers.Count() == 1)
            {
                return new HashSet<char>(this.answers[0]);
            }

            var answerGroup = new List<HashSet<char>>();
            this.answers.ForEach(c => {
                answerGroup.Add(new HashSet<char>(c));
            });

            var groups = answerGroup.Aggregate((acc, next) =>
            {
                acc.IntersectWith(next);
                return acc;
            });

            return groups;
        }
    }
}
