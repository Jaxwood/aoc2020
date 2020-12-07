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

        public int Process(string name)
        {
            var result = new HashSet<string>();
            var queue = new Queue<string>();
            foreach(var bag in GetContainingBags(name))
            {
                result.Add(bag);
                queue.Enqueue(bag);
            }

            while (queue.Count > 0)
            {
                var bagName = queue.Dequeue();
                foreach (var bag in GetContainingBags(bagName))
                {
                    if (HasBags(bag))
                    {
                        queue.Enqueue(bag);
                        result.Add(bag);
                    }
                }

            }
            return result.Count;
        }

        public int Pack(string name, int total = 0)
        {
            if(!HasBags(name))
            {
                return 0;
            }

            foreach (var bag in this.bags[name])
            {
                total += bag.Amount + bag.Amount * Pack(bag.Name);
            }

            return total;
        }

        private bool HasBags(string bag)
        {
            return string.Compare(bag, "no other", StringComparison.CurrentCultureIgnoreCase) != 0;
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
