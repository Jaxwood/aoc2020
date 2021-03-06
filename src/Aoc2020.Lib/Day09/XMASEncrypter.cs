﻿using Aoc2020.Lib.Util;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day09
{
    public class XMASEncrypter
    {
        private readonly long[] numbers;

        public readonly int preamble;

        public XMASEncrypter(long[] numbers, int preamble)
        {
            this.numbers = numbers;
            this.preamble = preamble;
        }

        public Result<long> Cypher()
        {
            for (int i = 0; i < this.numbers.Length - this.preamble; i++)
            {
                long target = this.numbers[i + this.preamble];
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
            for (int i = 0; i < this.numbers.Length; i++)
            {
                var result = this.SumTo(i, cypher.Value);
                if (result is Success<int>)
                {
                    var nums = this.numbers.Skip(i).Take(result.Value);
                    return new Success<long>(nums.Min() + nums.Max());
                }
            }

            return new Failure<long>();
        }

        private Result<int> SumTo(int index, long target)
        {
            long sum = numbers[index];
            for (int i = index + 1; i < this.numbers.Length; i++)
            {
                sum += this.numbers[i];

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
                if (sum.Contains(target - this.numbers[index + i]))
                {
                    return false;
                }
                sum.Add(this.numbers[index + i]);
            }
            return true;
        }
    }
}
