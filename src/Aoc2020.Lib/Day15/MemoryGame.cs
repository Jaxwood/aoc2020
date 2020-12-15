using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day15
{
    public class MemoryGame
    {
        private readonly IDictionary<int, int> lastSeen;
        private readonly int turns;
        private int turn;
        private int lastSpoken;

        public MemoryGame(string numbers, int turns)
        {
            var nums = numbers.Split(",", StringSplitOptions.RemoveEmptyEntries)
                              .Select((c, i) => new KeyValuePair<int, int>(
                                  Convert.ToInt32(c),  i + 1));
            this.lastSeen = new Dictionary<int, int>(nums.ToArray()[..^1]);
            
            this.turns = turns;
            this.turn = this.lastSeen.Count + 1;
            this.lastSpoken = nums.Last().Key;
        }

        public long Play()
        {
            while (turn != turns)
            {
                var next = this.lastSeen.TryGetValue(this.lastSpoken, out int val) ? this.turn - val : 0;
                this.lastSeen[this.lastSpoken] = this.turn;
                this.lastSpoken = next;
                this.turn++;
            }
            return this.lastSpoken;
        }
    }
}
