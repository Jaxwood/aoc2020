using Aoc2020.Lib.Day12;
using Aoc2020.Lib.Util;
using System;
using Xunit;

namespace Aoc2020.Tests.Day12
{
    public class Day12Tests
    {
        [Theory]
        [InlineData("Day12/Example1.txt", 25)]
        [InlineData("Day12/Input.txt", 508)]
        public void Part1(string filename, int expected)
        {
            var parser = new Parser(filename);
            var moves = parser.Parse(new NavigationFactory());
            Ship sut = new RegularShip(moves, new Compass(Direction.East));
            var actual = sut.Sail();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day12/Example1.txt", 286)]
        [InlineData("Day12/Input.txt", 0)]
        public void Part2(string filename, int expected)
        {
            var parser = new Parser(filename);
            var moves = parser.Parse(new NavigationFactory());
            Ship sut = new WaypointShip(moves, new Compass[] { new Compass(Direction.East), new Compass(Direction.North) });
            var actual = sut.Sail();
            Assert.Equal(expected, actual);
        }
    }

    internal class NavigationFactory : IParseFactory<Move>
    {
        public Move Create(Line line)
        {
            switch (line.Raw[0])
            {
                case 'L':
                    return new Move(Direction.Left, Convert.ToInt32(line.Raw[1..]));
                case 'R':
                    return new Move(Direction.Right, Convert.ToInt32(line.Raw[1..]));
                case 'N':
                    return new Move(Direction.North, Convert.ToInt32(line.Raw[1..]));
                case 'E':
                    return new Move(Direction.East, Convert.ToInt32(line.Raw[1..]));
                case 'W':
                    return new Move(Direction.West, Convert.ToInt32(line.Raw[1..]));
                case 'S':
                    return new Move(Direction.South, Convert.ToInt32(line.Raw[1..]));
                case 'F':
                    return new Move(Direction.Forward, Convert.ToInt32(line.Raw[1..]));
                default:
                    throw new Exception($"unknown direction {line.Raw[0]}");
            }
        }
    }
}
