using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day07
{
    public class LuggageProcessor
    {
        private IDictionary<string, Bag[]> bags;

        public LuggageProcessor(IDictionary<string, Bag[]> bags)
        {
            this.bags = bags;
        }

        public int Process(string name, HashSet<string> seen, int total = 0)
        {
            var containingBags = GetContainingBags(name);

            if (!containingBags.Any()) return 0;

            foreach (var bag in containingBags)
            {
                if (!seen.Contains(bag))
                {
                    seen.Add(bag);
                    total += 1 + Process(bag, seen);
                }
            }

            return total;
        }

        public int Pack(string name, int total = 0)
        {
            if (name == "no other")
            {
                return 0;
            }

            foreach (var bag in this.bags[name])
            {
                total += bag.Amount + bag.Amount * Pack(bag.Name);
            }

            return total;
        }

        private IEnumerable<string> GetContainingBags(string name)
        {
            foreach (var bag in this.bags)
            {
                if (bag.Value.Any(b => b.Name == name))
                {
                    yield return bag.Key;
                }
            }
        }
    }
}
