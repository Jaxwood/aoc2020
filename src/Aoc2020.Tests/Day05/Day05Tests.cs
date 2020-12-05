using Aoc2020.Lib.Day05;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day05
{
    public class Day05Tests
    {
        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void Scan(string seat, int expected)
        {
            var sut = new BoardingPassScanner();
            var actual = sut.Scan(seat);
            Assert.Equal(expected, actual.SeatId());
        }

        [Fact]
        public void Part1()
        {
            var parser = new Parser("Day05/Input.txt");
            var lines = parser.Parse(new BoardingPassFactory());
            var sut = new BoardingPassScanner();
            var actual = lines.Aggregate(0, (acc, next) =>
            {
                var seat = sut.Scan(next);
                return Math.Max(acc, seat.SeatId());
            });

            Assert.Equal(880, actual);
        }

        [Fact]
        public void Part2()
        {
            var parser = new Parser("Day05/Input.txt");
            var lines = parser.Parse(new BoardingPassFactory());
            var sut = new BoardingPassScanner();
            var seats = new HashSet<int>();
            foreach( var line in lines)
            {
                seats.Add(sut.Scan(line).SeatId());
            }
            var actual = Enumerable.Range(seats.Min(), seats.Max())
                                   .SkipWhile(s => seats.Contains(s))
                                   .First();
            Assert.Equal(731, actual);
        }
    }

    internal class BoardingPassFactory : IParseFactory<string>
    {
        public string Create(Line line)
        {
            return line.Raw;
        }
    }
}
