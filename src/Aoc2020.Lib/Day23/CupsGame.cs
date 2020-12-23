using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public int[] Play(int rounds)
        {
            int round = 1;
            while (round <= rounds)
            {
                var current = this.cups[0];
                var pickedUpCups = this.cups[1..4];
                var cupsLeft = this.cups[4..];
                this.cups[this.cups.Length - 1] = current;
                var destination = this.FindDestinationCupIndex(current, cupsLeft);
                Array.Copy(cupsLeft, 0, this.cups, 0, destination);
                Array.Copy(pickedUpCups, 0, this.cups, destination, pickedUpCups.Length);
                Array.Copy(cupsLeft, destination, this.cups, pickedUpCups.Length + destination, cupsLeft.Length - destination);
                round++;
            }

            var idxOne = Array.FindIndex(this.cups, c => c == 1);
            return this.Generate(idxOne).Take(this.cups.Length - 1).ToArray();
        }

        private int FindDestinationCupIndex(int current, int[] cupsLeft)
        {
            while (true)
            {
                current--;
                if (current <= 0)
                {
                    var max = cupsLeft.Max();
                    return this.GuardAgainstOverflow(Array.FindIndex(cupsLeft, c => c == max) + 1);
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
