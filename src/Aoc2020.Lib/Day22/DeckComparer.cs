using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Aoc2020.Lib.Day22
{
    public class DeckComparer : IEqualityComparer<Deck>
    {
        public bool Equals(Deck x, Deck y)
        {
            return x.Cards.SequenceEqual(y.Cards);
        }

        public int GetHashCode([DisallowNull] Deck obj)
        {
            return obj.Cards.Aggregate((acc, n) => acc ^ n);
        }
    }
}
