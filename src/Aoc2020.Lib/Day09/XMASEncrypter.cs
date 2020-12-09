using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day09
{
    public class XMASEncrypter
    {
        private readonly long[] lines;

        public readonly int preamble;

        public XMASEncrypter(long[] lines, int preamble)
        {
            this.lines = lines;
            this.preamble = preamble;
        }

        public Result<long> Cypher()
        {
            for (int i = 0; i < this.lines.Length - this.preamble; i++)
            {
                long target = this.lines[i + this.preamble];
                if (!CanSumTo(i, target))
                {
                    return new Success<long>(target);
                }
            }

            return new Failure<long>();
        }

        private bool CanSumTo(int index, long target)
        {
            var sum = new HashSet<long>();
            for (int i = 0; i < this.preamble; i++)
            {
                if (sum.Contains(target - this.lines[index + i]))
                {
                    return true;
                }
                sum.Add(this.lines[index + i]);
            }
            return false;
        }
    }
}
