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
            var sut = new SeatingLayout(seats.ToArray());
            var actual = sut.OccupiedSeats();
            Assert.Equal(expected, actual);
        }
    }

    internal class SeatingFactory : IParseFactory<Seat[]>
    {
        public Seat[] Create(Line line)
        {
            var seats = new List<Seat>();
            foreach (var seat in line.Raw)
            {
                switch (seat)
                {
                    case 'L':
                        seats.Add(new Seat(SeatType.Empty));
                        break;
                    case '#':
                        seats.Add(new Seat(SeatType.Occupied));
                        break;
                    case '.':
                        seats.Add(new Seat(SeatType.Floor));
                        break;
                    default:
                        throw new Exception($"unknown seat type: {seat}");
                }
            }
            return seats.ToArray();
        }
    }
}
