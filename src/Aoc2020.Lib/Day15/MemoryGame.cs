using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day15
{
    public class MemoryGame
    {
        private readonly HashSet<int> spokenNumbers;
        private readonly Dictionary<int, int> lastSeen;
        private readonly int turns;
        private int turn;
        private int lastSpoken;

        public MemoryGame(string numbers, int turns)
        {
            var nums = numbers.Split(",", StringSplitOptions.RemoveEmptyEntries)
                              .Select((c, i) => new KeyValuePair<int, int>(
                                  Convert.ToInt32(c),  i + 1));
            this.spokenNumbers = new HashSet<int>(nums.Select(c => c.Key).ToArray()[..^1]);
            this.lastSeen = new Dictionary<int, int>(nums.ToArray()[..^1]);
            
            this.turns = turns;
            this.turn = spokenNumbers.Count + 1;
            this.lastSpoken = nums.Last().Key;
        }

        public long Play()
        {
            while (turn != turns)
            {
                var next = !spokenNumbers.Contains(this.lastSpoken)
                    ? 0
                    : turn - lastSeen[this.lastSpoken];
                this.spokenNumbers.Add(this.lastSpoken);
                this.lastSeen[this.lastSpoken] = this.turn;
                this.lastSpoken = next;
                this.turn++;
            }
            return this.lastSpoken;
        }
    }
}
