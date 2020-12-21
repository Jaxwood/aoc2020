using Aoc2020.Lib.Day19;
using Aoc2020.Lib.Day19.Rules;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Aoc2020.Tests.Day19
{
    public class Day19Tests
    {
        [Theory]
        [InlineData("Day19/Example1.txt", 0, 2L)]
        [InlineData("Day19/Input.txt", 0, 200L)]
        public void Part1(string filename, int rule, long expected)
        {
            var parser = new Parser(filename);
            var lines = parser.Parse(new MessageFactory())
                              .Where(c => c != null);
            var sut = new MessageValidator(lines.FirstOrDefault());
            var actual = sut.Validate(rule);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day19/Example2.txt", 0, 12L)]
        [InlineData("Day19/Input2.txt", 0, 0L)] // > 404 && < 414 and !410
        public void Part2(string filename, int rule, long expected)
        {
            var parser = new Parser(filename);
            var lines = parser.Parse(new MessageFactory())
                              .Where(c => c != null);
            var sut = new MessageValidator(lines.FirstOrDefault());
            var actual = sut.Validate(rule);
            Assert.Equal(expected, actual);
        }
    }

    internal class MessageFactory : IParseFactory<Envelope>
    {
        private Dictionary<int, Validatable[]> rules;
        private List<string> messages;

        public MessageFactory()
        {
            this.rules = new Dictionary<int, Validatable[]>();
            this.messages = new List<string>();
        }

        public Envelope Create(Line line)
        {
            if (string.IsNullOrEmpty(line.Raw)) return null;

            // handle rules
            if (line.Raw.Contains(":"))
            {
                var segements = line.Raw.Split(":", System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);
                var num = Convert.ToInt32(segements[0]);
                if (segements[1].Contains("|"))
                {
                    var pipes = segements[1].Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var left = pipes[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(c => new NumberRule(Convert.ToInt32(c)));
                    var right = pipes[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(c => new NumberRule(Convert.ToInt32(c)));
                    this.rules.Add(num, new [] { new OrRule(left, right) });
                }
                else if (Regex.IsMatch(segements[1], "\"(\\w)\""))
                {
                    var charRule = Regex.Match(segements[1], "\"(\\w)\"").Groups[1].Value;
                    this.rules.Add(num, new[] { new CharacterRule(charRule[0]) });
                }
                else
                {
                    this.rules.Add(num, segements[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(c => new NumberRule(Convert.ToInt32(c))).ToArray());
                }
            }
            else // handle messages
            {
                this.messages.Add(line.Raw);
            }

            if (line.LastLine)
            {
                return new Envelope(this.rules, this.messages);
            }

            return null;
        }
    }
}
