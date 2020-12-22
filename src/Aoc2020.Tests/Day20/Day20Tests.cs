using Aoc2020.Lib.Day20;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day20
{
    public class Day20Tests
    {
        [Theory]
        [InlineData("Day20/Example1.txt", 20899048083289)]
        [InlineData("Day20/Input.txt", 51214443014783)]
        public void Part1(string filename, long expected)
        {
            var parser = new Parser(filename);
            var tiles = parser.Parse(new TileFactory())
                               .Where(img => img != null);
            var sut = new MonochromeImage(tiles);
            var actual = sut.CornerTiles().Aggregate(1L, (acc, tile) => acc * tile.Id);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day20/Example1.txt", 10)]
        [InlineData("Day20/Input.txt", 10)]
        public void Part2(string filename, long expected)
        {
            var parser = new Parser(filename);
            var tiles = parser.Parse(new TileFactory())
                               .Where(img => img != null);
            var sut = new MonochromeImage(tiles);
            var actual = sut.BuildImageFromTiles();
            Assert.Equal(expected, actual.Count());
        }
    }

    internal class TileFactory : IParseFactory<Tile>
    {
        private int id;
        private List<string> pixels;

        public Tile Create(Line line)
        {
            if (line.Raw.StartsWith("Tile"))
            {
                this.id = Convert.ToInt32(line.Raw.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1][..^1]);
                this.pixels = new List<string>();
                return null;
            }

            if (!string.IsNullOrEmpty(line.Raw))
            {
                this.pixels.Add(line.Raw);
            }

            if (string.IsNullOrEmpty(line.Raw) || line.LastLine)
            {
                return new Tile(this.id, this.pixels.ToArray());
            }

            return null;
        }
    }
}
