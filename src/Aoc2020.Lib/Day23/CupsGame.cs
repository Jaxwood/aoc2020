using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day23
{
    public class CupsGame
    {
        private int[] cups;

        public CupsGame(int[] cups)
        {
            this.cups = cups;
        }

        public string Play(int rounds)
        {
            int round = 1;
            int index = 0;
            while (round <= rounds)
            {
                var current = this.cups[index];
                var pickedUpCups = this.Generate(index).Take(3).ToArray();
                var cupsLeft = this.Generate(index).Skip(3).Take(this.cups.Length - 3).ToArray();
                var destination = this.FindDestinationCup(current, cupsLeft);
                this.cups = cupsLeft.Take(destination).Concat(pickedUpCups).Concat(cupsLeft.Skip(destination)).ToArray();
                round++;

            }

            var idxOne = Array.FindIndex(this.cups, c => c == 1);
            return this.Generate(idxOne).Take(8).Aggregate("", (acc, n) => acc + n);
        }

        private int FindDestinationCup(int current, int[] cupsLeft)
        {
            while (true)
            {
                current--;
                if (current <= 0)
                {
                    return this.GuardAgainstOverflow(Array.FindIndex(cupsLeft, c => c == cupsLeft.Max()) + 1);
                }
                var cupIdx = Array.FindIndex(cupsLeft, c => c == current);
                if (cupIdx != -1)
                {
                    return this.GuardAgainstOverflow(cupIdx + 1);
                }
            }
        }

        private IEnumerable<int> Generate(int from)
        {
            while (true)
            {
                from = this.GuardAgainstOverflow(from + 1);
                yield return this.cups[from];
            }
        }

        private int GuardAgainstOverflow(int idx)
        {
            return idx == this.cups.Length ? 0 : idx;
        }
    }
}
