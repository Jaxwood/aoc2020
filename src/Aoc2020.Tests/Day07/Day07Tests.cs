using Aoc2020.Lib.Day07;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Aoc2020.Tests.Day07
{
    public class Day07Tests
    {
        [Theory]
        [InlineData("Day07/Example1.txt", 4)]
        [InlineData("Day07/Input.txt", 211)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var bags = parser.Parse(new BagFactory());
            var sut = new LuggageProcessor(new Dictionary<string, Bag[]>(bags));
            var actual = sut.Process("shiny gold");
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day07/Example1.txt", 32)]
        [InlineData("Day07/Example2.txt", 126)]
        [InlineData("Day07/Input.txt", 12414)]
        public void Part2(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var bags = parser.Parse(new BagFactory());
            var sut = new LuggageProcessor(new Dictionary<string, Bag[]>(bags));
            var actual = sut.Pack("shiny gold");
            Assert.Equal(expected, actual);
        }
    }

    internal class BagFactory : IParseFactory<KeyValuePair<string, Bag[]>>
    {
        public KeyValuePair<string, Bag[]> Create(Line line)
        {
            var segments = line.Raw.Split(new[] { "bags", "bag", "contain", ".", "," }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var bags = new List<Bag>();
            foreach (var seg in segments.Skip(1))
            {
                var matches = Regex.Matches(seg, @"(\d+)?([a-zA-Z\s]+)");
                foreach (Match match in matches)
                {
                    if (int.TryParse(match.Groups[1].Value, out int amount))
                    {
                        bags.Add(new Bag
                        {
                            Amount = amount,
                            Name = match.Groups[2].Value.Trim(),
                        });
                    }
                    else
                    {
                        bags.Add(new Bag
                        {
                            Amount = 0,
                            Name = match.Groups[2].Value.Trim(),
                        });

                    }
                }
            }

            return KeyValuePair.Create(segments[0], bags.ToArray());
        }
    }
}
