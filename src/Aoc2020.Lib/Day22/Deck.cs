using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day22
{
    public record Deck
    {
        private int[] cards;
        private int game;
        private HashSet<Deck> memory;

        public Deck(int[] cards, int game = 0)
        {
            this.cards = cards;
            this.game = game;
            this.memory = new HashSet<Deck>(new DeckComparer());
        }

        public int[] Cards => this.cards;

        public int Game => this.game;

        public int Draw()
        {
            this.memory.Add(new Deck(this.cards, this.game));
            var card = this.cards[0];
            this.cards = this.cards[1..];
            return card;
        }

        public bool PreviousHand()
        {
            return this.memory.Contains(this);
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
            return this.cards.Select((c, i) => c * (this.cards.Length - i))
                             .Aggregate(0L, (acc, n) => acc + n);
        }

        public bool RecursiveMode(int card)
        {
            return card <= this.cards.Length;
        }

        public Deck CreateRecursiveDeck(int size, int game)
        {
            return new Deck(this.cards[..size], game);
        }
    }
}
