using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day10
{
    public class JoltAdapter
    {
        private IEnumerable<int> adapters;

        public JoltAdapter(IEnumerable<int> adapters)
        {
            var tmp = adapters.ToList();
            tmp.Add(tmp.Max() + 3);
            tmp.Sort();
            this.adapters = tmp;
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
    }
}
