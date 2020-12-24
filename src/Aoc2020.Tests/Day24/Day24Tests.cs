using Aoc2020.Lib.Day24;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using Xunit;

namespace Aoc2020.Tests.Day24
{
    public class Day24Tests
    {
        [Theory]
        [InlineData("Day24/Example1.txt", 10)]
        [InlineData("Day24/Input.txt", 391)]
        public void Part1(string filename, long expected)
        {
            var parser = new Parser(filename);
            var directions = parser.Parse(new DirectionFactory());
            var sut = new HexagonalTileFloorBuilder(directions);
            var actual = sut.Flip();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day24/Example1.txt", 100, 2208)]
        [InlineData("Day24/Input.txt", 100, 3876)]
        public void Part2(string filename, int turns, long expected)
        {
            var parser = new Parser(filename);
            var directions = parser.Parse(new DirectionFactory());
            var sut = new HexagonalTileFloorBuilder(directions);
            var actual = sut.Tick(turns);
            Assert.Equal(expected, actual);
        }
    }

    internal class DirectionFactory : IParseFactory<IEnumerable<Direction>>
    {
        public IEnumerable<Direction> Create(Line line)
        {
            List<Direction> directions = new List<Direction>();

            int index = 0;
            int len = line.Raw.Length;
            while (true)
            {
                switch (line.Raw)
                {
                    case string _ when index == len:
                        return directions;
                    case string dir when dir[index] == 'w':
                        directions.Add(Direction.West);
                        index++;
                        break;
                    case string dir when dir[index] == 'e':
                        directions.Add(Direction.East);
                        index++;
                        break;
                    case string dir when dir.Substring(index, 2) == "nw":
                        directions.Add(Direction.NorthWest);
                        index += 2;
                        break;
                    case string dir when dir.Substring(index, 2) == "ne":
                        directions.Add(Direction.NorthEast);
                        index += 2;
                        break;
                    case string dir when dir.Substring(index, 2) == "sw":
                        directions.Add(Direction.SouthWest);
                        index += 2;
                        break;
                    case string dir when dir.Substring(index, 2) == "se":
                        directions.Add(Direction.SouthEast);
                        index += 2;
                        break;
                    default:
                        throw new Exception("Unhandled case");
                }
            }
        }

        private bool Inbounds(int index)
        {
            throw new NotImplementedException();
        }
    }
}
