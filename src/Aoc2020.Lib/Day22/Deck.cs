using Aoc2020.Lib.Util;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day22
{
    public class Deck
    {
        private int[] cards;
        private HashSet<Deck> memory;

        public Deck(int[] cards)
        {
            this.cards = cards;
            this.memory = new HashSet<Deck>();
        }

        public int[] Cards => this.cards;

        public Result<int> Draw()
        {
            if (this.PreviousHand())
            {
                return new Failure<int>();
            }
            this.memory.Add(new Deck(this.cards));
            var card = this.cards[0];
            this.cards = this.cards[1..];

            return new Success<int>(card);
        }

        private bool PreviousHand()
        {
            return this.memory.Contains(this);
        }

        public bool HasCards()
        {
            return this.cards.Length > 0;
        }

        public void Win(Result<int> highCard, Result<int> lowCard)
        {
            this.cards = this.cards.Concat(new int[] { highCard.Value, lowCard.Value }).ToArray();
        }

        public long Score()
        {
            return this.cards.Select((c, i) => c * (this.cards.Length - i))
                             .Aggregate(0L, (acc, n) => acc + n);
        }

        public bool RecursiveMode(Result<int> card)
        {
            return card.Value <= this.cards.Length;
        }

        public Deck CreateRecursiveDeck(Result<int> size)
        {
            return new Deck(this.cards[..size.Value]);
        }

        public override bool Equals(object other)
        {
            if (other is Deck d) return this.cards.SequenceEqual(d.cards);
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.cards.Aggregate((acc, n) => acc ^ n);
        }
    }
}
