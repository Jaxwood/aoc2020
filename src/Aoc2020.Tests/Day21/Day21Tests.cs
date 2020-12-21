using Aoc2020.Lib.Day21;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aoc2020.Tests.Day21
{
    public class Day21Tests
    {
        [Theory]
        [InlineData("Day21/Example1.txt", 5)]
        [InlineData("Day21/Input.txt", 2380)]
        public void Part1(string filename, long expected)
        {
            var parser = new Parser(filename);
            var ingredients = parser.Parse(new IngredientFactory());
            var sut = new IngredientMixer(ingredients);
            var actual = sut.CountAllergens();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day21/Example1.txt", "mxmxvkd,sqjhc,fvjkl")]
        [InlineData("Day21/Input.txt", "ktpbgdn,pnpfjb,ndfb,rdhljms,xzfj,bfgcms,fkcmf,hdqkqhh")]
        public void Part2(string filename, string expected)
        {
            var parser = new Parser(filename);
            var ingredients = parser.Parse(new IngredientFactory());
            var sut = new IngredientMixer(ingredients);
            var actual = sut.Allergens();
            Assert.Equal(expected, actual);
        }
    }

    internal class IngredientFactory : IParseFactory<IngredientList>
    {
        public IngredientList Create(Line line)
        {
            var reciepe = line.Raw.Split('(', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var ingredients = reciepe[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var allergens = reciepe[1].Split(new char[] { ')', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return new IngredientList(ingredients, allergens[1..]);
        }
    }
}
