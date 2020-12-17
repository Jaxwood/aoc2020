using Aoc2020.Lib.Day17;
using Aoc2020.Lib.Util;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day17
{
    public class Day17Tests
    {
        [Theory]
        [InlineData("Day17/Example1.txt", 6, 112)]
        [InlineData("Day17/Input.txt", 6, 346)]
        public void Part1(string filename, int cycles, long expected)
        {
            var parser = new Parser(filename);
            var cubes = parser.Parse(new CubeParser());
            var sut = new ConwayCubeSimulator(cubes.SelectMany(c => c), new Universe(), new UniverseScanner());
            var actual = sut.Simulate(cycles);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day17/Example1.txt", 6, 848)]
        [InlineData("Day17/Input.txt", 6, 1632)]
        public void Part2(string filename, int cycles, long expected)
        {
            var parser = new Parser(filename);
            var cubes = parser.Parse(new CubeParser());
            var sut = new ConwayCubeSimulator(cubes.SelectMany(c => c), new HyperUniverse(), new HyperUniverseScanner());
            var actual = sut.Simulate(cycles);
            Assert.Equal(expected, actual);
        }
    }

    internal class CubeParser : IParseFactory<IEnumerable<KeyValuePair<Cube, State>>>
    {
        public IEnumerable<KeyValuePair<Cube, State>> Create(Line line)
        {
            for (int x = 0; x < line.Raw.Length; x++)
            {
                yield return KeyValuePair.Create(new Cube(x, line.LineNum, 0), ParseField(line.Raw[x]));
            }
        }

        private State ParseField(char field)
        {
            return field == '#' ? State.Active : State.Inactive;
        }
    }
}
