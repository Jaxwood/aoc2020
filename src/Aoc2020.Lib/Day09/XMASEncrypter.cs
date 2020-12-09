using Aoc2020.Lib.Util;
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
                if (this.CannotSumTo(i, target))
                {
                    return new Success<long>(target);
                }
            }

            return new Failure<long>();
        }

        public Result<long> Weakness()
        {
            var cypher = this.Cypher();
            for (int i = 0; i < this.lines.Length; i++)
            {
                var result = this.SumTo(i, cypher.Value);
                if (result is Success<int>)
                {
                    var nums = this.lines.Skip(i).Take(result.Value);
                    return new Success<long>(nums.Min() + nums.Max());
                }
            }

            return new Failure<long>();
        }

        private Result<int> SumTo(int index, long target)
        {
            long sum = lines[index];
            for (int i = index + 1; i < this.lines.Length; i++)
            {
                sum += this.lines[i];

                if (sum == target)
                {
                    return new Success<int>(i - index);
                }
                if (sum > target)
                {
                    return new Failure<int>();
                }
            }
            return new Failure<int>();
        }

        private bool CannotSumTo(int index, long target)
        {
            var sum = new HashSet<long>();
            for (int i = 0; i < this.preamble; i++)
            {
                if (sum.Contains(target - this.lines[index + i]))
                {
                    return false;
                }
                sum.Add(this.lines[index + i]);
            }
            return true;
        }
    }
}
