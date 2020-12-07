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
                    if (string.Compare(bag, "no other", StringComparison.CurrentCultureIgnoreCase) != 0)
                    {
                        queue.Enqueue(bag);
                        result.Add(bag);
                    }
                }

            }
            return result.Count;
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

    public record Bag
    {
        public int Amount { get; set; }
        public string Name { get; set; }
    }
}
