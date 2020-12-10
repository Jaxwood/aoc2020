using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aoc2020.Lib.Day10
{
    public class JoltAdapter
    {
        private IEnumerable<int> adapters;
        private HashSet<int> adapterSet;

        public JoltAdapter(IEnumerable<int> adapters)
        {
            var tmp = adapters.ToList();
            tmp.Add(tmp.Max() + 3);
            tmp.Sort();
            this.adapters = tmp;
            this.adapterSet = new HashSet<int>(this.adapters);
        }

        public int Chain()
        {
            var oneDiff = 0;
            var threeDiff = 0;
            var current = 0;
            foreach (var adapter in this.adapters)
            {
                var diff = adapter - current;
                if (diff == 1)
                {
                    oneDiff++;
                }
                if (diff == 3)
                {
                    threeDiff++;
                }
                
                current = adapter;

            }
            return oneDiff * threeDiff;
        }

        public BigInteger Pathways()
        {
            var queue = new Queue<int>();
            queue.Enqueue(0);
            var result = BigInteger.Zero;
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var pathways = this.Connections(current);
                if (pathways.Count() == 0)
                {
                    result += 1;
                }

                foreach (var pathway in pathways)
                {
                    queue.Enqueue(pathway);
                }
            }

            return result;
        }

        private IEnumerable<int> Connections(int current)
        {
            if (this.adapterSet.Contains(current + 1))
            {
                yield return current + 1;
            }
            if (this.adapterSet.Contains(current + 2))
            {
                yield return current + 2;
            }
            if (this.adapterSet.Contains(current + 3))
            {
                yield return current + 3;
            }
        }
    }
}
