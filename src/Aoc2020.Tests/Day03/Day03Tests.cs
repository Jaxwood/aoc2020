﻿using Aoc2020.Lib.Day03;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aoc2020.Tests.Day03
{
    public class Day03Tests
    {
        [Theory]
        [InlineData("Day03/Example1.txt", 7)]
        [InlineData("Day03/Input.txt", 156)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var geology = parser.Parse(new GeologyFactory());
            var sut = new Navigator(new Dictionary<(int, int), Terrain>(geology.SelectMany(g => g)));
            Assert.Equal(expected, sut.Drive());
        }

        [Theory]
        [InlineData("Day03/Example1.txt", "336")]
        [InlineData("Day03/Input.txt", "3521829480")]
        public void Part2(string filepath, string expected)
        {
            var parser = new Parser(filepath);
            var geology = parser.Parse(new GeologyFactory());
            var sut = new Navigator(new Dictionary<(int, int), Terrain>(geology.SelectMany(g => g)));
            Assert.Equal(BigInteger.Parse(expected),
                BigInteger.Multiply(
                    BigInteger.Multiply(
                        BigInteger.Multiply(
                            sut.Drive(1, 1), sut.Drive(1, 3)),
                        BigInteger.Multiply(
                            sut.Drive(1, 5), sut.Drive(1, 7))),
                    sut.Drive(2, 1)));
        }
    }

    internal class GeologyFactory : IParseFactory<IEnumerable<KeyValuePair<(int, int), Terrain>>>
    {
        private readonly Dictionary<(int, int), Terrain> map;

        public GeologyFactory()
        {
            this.map = new Dictionary<(int, int), Terrain>();
        }
        public IEnumerable<KeyValuePair<(int, int), Terrain>> Create(Line line)
        {
            int idx = 0;
            foreach (var l in line.Raw)
            {
                yield return KeyValuePair.Create((line.LineNum, idx++), ParseTerrain(l));
            }
        }

        private Terrain ParseTerrain(char l)
        {
            switch (l)
            {
                case '#':
                    return Terrain.Tree;
                default:
                    return Terrain.Open;
            }
        }
    }
}
