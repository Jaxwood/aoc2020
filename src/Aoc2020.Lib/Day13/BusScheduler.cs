using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aoc2020.Lib.Day13
{
    public class BusScheduler
    {
        private readonly IEnumerable<(int, int)> schedules;

        public BusScheduler(string schedules)
        {
            var segments = schedules.Split(',');
            var result = new List<(int, int)>();
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] != "x")
                {
                    result.Add((i, Convert.ToInt32(segments[i])));
                }
            }
            this.schedules = result;
        }

        public int Schedule(int departureTime)
        {
            var bestTimeSlot = Int32.MaxValue;
            var bestBus = 0;
            foreach (var (_, schedule) in this.schedules)
            {
                var timeslot = this.Generate(schedule).SkipWhile(s => s < departureTime).First();
                if (timeslot < bestTimeSlot)
                {
                    bestBus = schedule;
                    bestTimeSlot = timeslot;
                }
            }

            return (bestTimeSlot - departureTime) * bestBus;
        }

        public BigInteger ScheduleByOffset()
        {
            var nums = this.schedules.Select(s => new BigInteger(s.Item2));
            var remainders = this.schedules.Select(s => new BigInteger(s.Item2 - s.Item1));

            return this.ChineseRemainder(nums, remainders);
        }

        private BigInteger ChineseRemainder(IEnumerable<BigInteger> numbers, IEnumerable<BigInteger> remainder)
        {
            var product = numbers.Aggregate(BigInteger.One, (acc, n) => acc * n);

            return numbers
                .Zip(remainder, (num, rem) => (num, rem))
                .Aggregate(BigInteger.Zero, (acc, next) => 
                    acc + next.rem * BigInteger.ModPow(BigInteger.Divide(product, next.num), next.num - 2, next.num) * BigInteger.Divide(product, next.num)
                ) % product;
        }

        public IEnumerable<int> Generate(int interval)
        {
            var num = interval;
            while (true)
            {
                yield return num;
                num += interval;
            }
        }
    }
}
