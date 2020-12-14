using Aoc2020.Lib.Day14;
using Aoc2020.Lib.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day14
{
    public class Day14Tests
    {
        [Theory]
        [InlineData("Day14/Example1.txt", 165)]
        [InlineData("Day14/Input.txt", 5055782549997)]
        public void Part1(string filepath, long expected)
        {
            var parser = new Parser(filepath);
            var masks = parser.Parse(new BitmaskFactory())
                .Where(m => m != null);
            var sut = new PortComputer(masks);
            var actual = sut.Decode();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day14/Example2.txt", 208)]
        [InlineData("Day14/Input.txt", 0)]
        public void Part2(string filepath, long expected)
        {
            var parser = new Parser(filepath);
            var masks = parser.Parse(new BitmaskFactory())
                .Where(m => m != null);
            var sut = new PortComputer(masks);
            var actual = sut.MemoryDecode();
            Assert.Equal(expected, actual);
        }
    }

    internal class BitmaskFactory : IParseFactory<Mask>
    {
        private IEnumerable<MaskValues> mask;
        private List<KeyValuePair<long, BitArray>> memory;
        private const int SIZE = 36;

        public Mask Create(Line line)
        {
            if (this.memory == null)
            {
                memory = new();
            }

            // handle mask
            if (this.mask == null && line.Raw.StartsWith("mask"))
            {
                this.ParseMask(line);
                return null;
            }
            else if (line.Raw.StartsWith("mask"))
            {
                var m = new Mask(this.mask.ToArray(), this.memory);
                this.memory = null;
                this.ParseMask(line);
                return m;
            }

            // handle memory
            var segments = line.Raw.Split(new char[] { '[', ']', '=' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var val = Convert.ToInt64(segments[2]);
            var result = BitUtil.ConvertLongToBitArray(val);

            this.memory.Add(new(Convert.ToInt64(segments[1]), result));

            if (line.LastLine)
            {
                return new Mask(this.mask.ToArray(), this.memory);
            }

            return null;
        }

        private void ParseMask(Line line)
        {
            var stringMask = line.Raw.Split("=", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
            var result = new MaskValues[SIZE];
            for (int pos = 0; pos < stringMask.Length; pos++)
            {
                result[SIZE - 1 - pos] = stringMask[pos] == 'X' ? MaskValues.Floating : (stringMask[pos] == '1' ? MaskValues.One : MaskValues.Zero);
            }
            this.mask = result;
        }
    }
}
