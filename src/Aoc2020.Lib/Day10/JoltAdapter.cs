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
            var tmp = adapters.Concat(new int[] { 0, adapters.Max() + 3 }).ToList();
            tmp.Sort();
            this.adapters = tmp;
            this.adapterSet = new HashSet<int>(tmp);
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

        public long Pathways()
        {
            var branches = new Dictionary<int, long>();
            foreach (var adapter in this.adapters)
            {
                var pathways = this.Connections(adapter);
                branches.Add(adapter, pathways.Count());
            }

            foreach (var adapter in this.adapters.Reverse())
            {
                if (branches[adapter] > 1)
                {
                    branches[adapter] = this.Connections(adapter)
                        .Aggregate(0L, (acc, n) => acc + NextBranchCount(n, branches));
                }
            }

            return branches.First(kv => kv.Value > 1).Value;
        }

        private long NextBranchCount(int con, IDictionary<int, long> branches)
        {
            return this.adapters
                .SkipWhile(c => c != con)
                .Where(c => branches[c] > 1)
                .Select(c => branches[c])
                .DefaultIfEmpty(1L)
                .First();
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
