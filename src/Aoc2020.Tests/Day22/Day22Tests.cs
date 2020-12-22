using Aoc2020.Lib.Day22;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aoc2020.Tests.Day22
{
    public class Day22Tests
    {
        [Theory]
        [InlineData("Day22/Example1.txt", 306)]
        [InlineData("Day22/Input.txt", 32162)]
        public void Part1(string filename, long expected)
        {
            var parser = new Parser(filename);
            var decks = parser.Parse(new DeckFactory()).Where(c => c != null);
            var sut = new CombatGame(decks.ToArray());
            var actual = sut.Play();
            Assert.Equal(expected, actual);
        }
    }

    internal class DeckFactory : IParseFactory<Deck>
    {
        private List<int> cards;

        public Deck Create(Line line)
        {
            if (line.Raw.StartsWith("Player")) this.cards = new List<int>();

            if (Int32.TryParse(line.Raw, out int card))
            {
                this.cards.Add(card);
            }

            if (string.IsNullOrEmpty(line.Raw) || line.LastLine) return new Deck(cards.ToArray());

            return null;
        }
    }
}
