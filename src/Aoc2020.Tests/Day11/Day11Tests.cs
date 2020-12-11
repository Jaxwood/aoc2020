using Aoc2020.Lib.Day11;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day11
{
    public class Day11Tests
    {
        [Theory]
        [InlineData("Day11/Example1.txt", 37)]
        [InlineData("Day11/Input.txt", 2178)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var seats = parser.Parse(new SeatingFactory());
            var sut = new SeatingLayout(
                new Dictionary<(int, int), SeatType>(seats.SelectMany(c => c)));
            var actual = sut.OccupiedSeats();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day11/Example1.txt", 26)]
        [InlineData("Day11/Input.txt", 1978)]
        public void Part2(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var seats = parser.Parse(new SeatingFactory());
            var sut = new SeatingLayout(
                new Dictionary<(int, int), SeatType>(seats.SelectMany(c => c)), false);
            var actual = sut.OccupiedSeats();
            Assert.Equal(expected, actual);
        }
    }

    internal class SeatingFactory : IParseFactory<IEnumerable<KeyValuePair<(int,int), SeatType>>>
    {
        public IEnumerable<KeyValuePair<(int,int), SeatType>> Create(Line line)
        {
            var seats = new List<KeyValuePair<(int, int), SeatType>>();
            for (int x = 0; x < line.Raw.Length; x++)
            {
                var seat = line.Raw[x];
                switch (seat)
                {
                    case 'L':
                        seats.Add(new KeyValuePair<(int, int), SeatType>((x, line.LineNum), SeatType.Empty));
                        break;
                    case '#':
                        seats.Add(new KeyValuePair<(int, int), SeatType>((x, line.LineNum), SeatType.Occupied));
                        break;
                    case '.':
                        seats.Add(new KeyValuePair<(int, int), SeatType>((x, line.LineNum), SeatType.Floor));
                        break;
                    default:
                        throw new Exception($"unknown seat type: {seat}");
                }
            }
            return seats;
        }
    }
}
