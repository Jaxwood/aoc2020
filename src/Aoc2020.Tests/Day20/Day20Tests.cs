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
            var images = parser.Parse(new ImageFactory())
                               .Where(img => img != null);
            var sut = new ImageReassembler(images);
            var actual = sut.Corners();
            Assert.Equal(expected, actual);
        }
    }

    internal class ImageFactory : IParseFactory<MonochromeImage>
    {
        private int tile;
        private List<string> data;

        public MonochromeImage Create(Line line)
        {
            if (line.Raw.StartsWith("Tile"))
            {
                this.tile = Convert.ToInt32(line.Raw.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1][..^1]);
                this.data = new List<string>();
                return null;
            }

            if (!string.IsNullOrEmpty(line.Raw))
            {
                this.data.Add(line.Raw);
            }

            if (string.IsNullOrEmpty(line.Raw) || line.LastLine)
            {
                return new MonochromeImage(this.tile, this.data.ToArray());
            }

            return null;
        }
    }
}
