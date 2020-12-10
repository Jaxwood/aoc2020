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
            // var tree = Build(new Tree(0));
            var sum = Sum(0);
            return sum;
        }
        private BigInteger Sum(int current)
        {
            var result = BigInteger.Zero;
            var nodes = this.Connections(current);
            if (nodes.Count() == 0)
            {
                return 1;
            }
            foreach (var node in nodes)
            {
                result = BigInteger.Add(result, Sum(node));
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
