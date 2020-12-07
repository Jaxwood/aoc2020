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

        public int Pack(string name)
        {
            var cost = new Dictionary<string, int>();
            var queue = new Queue<string>();
            var leafs = GetContainingBags("no other");
            var visited = new HashSet<string>();
            foreach (var leaf in leafs)
            {
                queue.Enqueue(leaf);
                cost.Add(leaf, 0);
                visited.Add(leaf);
            }

            while (queue.Count > 0)
            {
                var next = GetContainingBags(queue.Dequeue());
                foreach (var n in next)
                {
                    var bag = this.bags[n];
                    if (HasRequired(bag, cost) && !visited.Contains(n))
                    {
                        cost.Add(n, bag.Sum(b => b.Amount + b.Amount * cost[b.Name]));
                        visited.Add(n);
                        queue.Enqueue(n);
                    }
                }
            }

            return cost[name];
        }

        private bool HasRequired(Bag[] bag, Dictionary<string, int> cost)
        {
            return bag.All(b => cost.ContainsKey(b.Name));
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
