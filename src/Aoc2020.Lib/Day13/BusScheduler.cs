﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public long ScheduleByOffset()
        {
            var highest = this.schedules.Max(tp => tp.Item2);
            var max = this.schedules.First(tp => tp.Item2 == highest);
            for (int i = highest; ; i += highest)
            {
                var match = true;
                foreach (var (j,s) in this.schedules)
                {
                    if ((i - (max.Item1 - j)) % s != 0)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    return i - max.Item1;
                }
            }
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
