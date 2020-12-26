using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aoc2020.Lib.Day23
{
    public class CupsGame
    {
        private int[] cups;
        private int max;

        public CupsGame(int[] cups)
        {
            this.cups = cups;
            this.max = cups.Max();
        }

        public int[] Play(int rounds)
        {
            int round = 0;
            int current = this.cups[0];
            this.Setup();

            while (round < rounds)
            {
                var cupsToMove = this.GetNextThreeCups(current);
                var next = this.GetDestinationCup(current, cupsToMove);
                this.cups[current] = this.cups[cupsToMove[2]];
                this.cups[cupsToMove[2]] = this.cups[next];
                this.cups[next] = cupsToMove[0];
                current = this.cups[current];
                round++;
            }

            return this.cups;
        }

        private int GetDestinationCup(int current, int[] threeCups)
        {
            var candidate = current - 1 == 0 ? this.max : current - 1;
            while (threeCups.Contains(candidate))
            {
                candidate--;
                if (candidate == 0) candidate = this.max;
            }
            return candidate;

        }

        private int[] GetNextThreeCups(int current)
        {
            var one = this.cups[current];
            var two = this.cups[one];
            var three = this.cups[two];
            return new int[] { one, two, three };
        }

        private void Setup()
        {
            var result = new int[this.cups.Length + 1];
            for (int i = 0; i < this.cups.Length; i++)
            {
                result[this.cups[i]] = i + 1 == this.cups.Length ? this.cups[0] : this.cups[i + 1];
            }
            this.cups = result;
        }
    }
}
