using System.Linq;

namespace Aoc2020.Lib.Day22
{
    public record Deck
    {
        private int[] cards;

        public Deck(int[] cards)
        {
            this.cards = cards;
        }

        public int Draw()
        {
            var card = this.cards[0];
            this.cards = this.cards[1..];
            return card;
        }

        public bool HasCards()
        {
            return this.cards.Length > 0;
        }

        public void Win(int highCard, int lowCard)
        {
            this.cards = this.cards.Concat(new int[] { highCard, lowCard }).ToArray();
        }

        public long Score()
        {
            return this.cards.Reverse()
                             .Select((c, i) => c * (i + 1))
                             .Aggregate(0L, (acc, n) => acc + n);
        }
    }
}
