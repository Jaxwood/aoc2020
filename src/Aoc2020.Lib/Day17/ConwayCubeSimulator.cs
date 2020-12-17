using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day17
{
    public class ConwayCubeSimulator
    {
        private IDictionary<Cube, State> cubes;
        private readonly Expandable universe;
        private readonly Scanable scanner;
        private IDictionary<Cube, State> nextState;

        public ConwayCubeSimulator(
            IEnumerable<KeyValuePair<Cube, State>> cubes,
            Expandable universe,
            Scanable scanner)
        {
            this.cubes = new Dictionary<Cube, State>(cubes);
            this.universe = universe;
            this.scanner = scanner;
        }

        public long Simulate(int cycles)
        {
            var cycle = 0;
            while (cycle < cycles)
            {
                this.nextState = new Dictionary<Cube, State>();
                this.cubes = this.universe.Expand(this.cubes);
                foreach (var kv in this.cubes)
                {
                    var active = this.scanner.Scan(this.cubes, kv.Key);
                    this.nextState[kv.Key] = this.GetNextState(kv.Value, active);
                }
                this.cubes = nextState;
                cycle++;
            }

            return this.cubes.Values.Count(c => c == State.Active);
        }

        private State GetNextState(State state, int active)
        {
            switch (state)
            {
                case State.Inactive:
                    return active == 3 ? State.Active : State.Inactive;
                case State.Active:
                    return (active == 2 || active == 3) ? State.Active : State.Inactive;
                default:
                    throw new Exception($"unhandled state {state}");
            }
        }
    }
}
