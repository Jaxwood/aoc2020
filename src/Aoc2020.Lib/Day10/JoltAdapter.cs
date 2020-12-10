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
            tmp.Add(0);
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

        public long Pathways()
        {
            var dict = new Dictionary<int, long>();
            foreach (var adapter in this.adapters)
            {
                var pathways = this.Connections(adapter);
                dict.Add(adapter, pathways.Count());
            }

            var result = BigInteger.One;
            foreach (var adapter in this.adapters.Reverse())
            {
                if (dict[adapter] > 1)
                {
                    long count = 0;
                    foreach (var con in this.Connections(adapter))
                    {
                        count += FindPathways(con, dict);
                    }
                    dict[adapter] = count;
                }
            }

            return dict.First(kv => kv.Value > 1).Value;
        }

        private long FindPathways(int con, IDictionary<int, long> dict)
        {
            var seq = this.adapters.SkipWhile(c => c != con);
            foreach (var s in seq)
            {
                if (dict[s] > 1)
                {
                    return dict[s];
                }
            }

            return 1;
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
