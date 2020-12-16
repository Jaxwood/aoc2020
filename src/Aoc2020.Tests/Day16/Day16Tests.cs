using Aoc2020.Lib.Day16;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Aoc2020.Tests.Day16
{
    public class Day16Tests
    {
        [Theory]
        [InlineData("Day16/Example1.txt", 71)]
        [InlineData("Day16/Input.txt", 28882)]
        public void Part1(string filename, long expected)
        {
            var parser = new Parser(filename);
            var book = parser.Parse(new TicketParser())
                              .Where(c => c != null)
                              .First();
            var sut = new TicketProcessor(book);
            var actual = sut.InvalidTickets();
            Assert.Equal(expected, actual);
        }
    }

    internal class TicketParser : IParseFactory<TicketBook>
    {
        private const string RULEREGEX = @"([\w\s]+):\s(\d+)-(\d+)\sor\s(\d+)-(\d+)";
        private const string MYTICKETREGEX = @"your ticket:";
        private const string OTHERTICKETREGEX = @"nearby tickets:";

        private readonly Dictionary<string, Range[]> rules;
        private readonly List<IEnumerable<int>> others;
        private IEnumerable<int> my;

        private bool myticket = false;

        public TicketParser()
        {
            this.rules = new Dictionary<string, Range[]>();
            this.others = new List<IEnumerable<int>>();
        }

        public TicketBook Create(Line line)
        {
            if (string.IsNullOrWhiteSpace(line.Raw)) return null;

            if (Regex.IsMatch(line.Raw, RULEREGEX))
            {
                foreach (Match match in Regex.Matches(line.Raw, RULEREGEX))
                {
                    this.rules.Add(match.Groups[1].Value, new Range[]
                    {
                        new Range(ToInt(match, 2), ToInt(match, 3)),
                        new Range(ToInt(match, 4), ToInt(match, 5)),
                    });
                }

                return null;
            }

            if (Regex.IsMatch(line.Raw, MYTICKETREGEX))
            {
                myticket = true;
                return null;
            }

            if (Regex.IsMatch(line.Raw, OTHERTICKETREGEX)) return null;

            if (myticket)
            {
                my = this.GetTickets(line.Raw);
                myticket = false;
            }
            else
            {
                this.others.Add(this.GetTickets(line.Raw));
            }

            if (line.LastLine) return new TicketBook(this.rules, this.my, this.others);

            return null;
        }

        private IEnumerable<int> GetTickets(string line)
        {
            return line.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                       .Select(c => Convert.ToInt32(c));
        }

        private int ToInt(Match match, int idx)
        {
            return Convert.ToInt32(match.Groups[idx].Value);
        }
    }
}
