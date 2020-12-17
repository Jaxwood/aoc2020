using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day17
{
    public class ConwayCubeSimulator
    {
        private IDictionary<Cube, State> cubes;
        private IDictionary<Cube, State> nextState;

        public ConwayCubeSimulator(IEnumerable<KeyValuePair<Cube, State>> cubes)
        {
            this.cubes = new Dictionary<Cube, State>(cubes);
        }

        public long Simulate(int cycles)
        {
            var cycle = 0;
            while (cycle < cycles)
            {
                this.nextState = new Dictionary<Cube, State>();
                this.cubes = this.Expand();
                foreach (var kv in this.cubes)
                {
                    int cnt = this.GetActiveNeighbors(kv.Key);
                    this.nextState[kv.Key] = this.GetNextState(kv.Value, cnt);
                }
                this.cubes = nextState;
                cycle++;
            }

            return this.cubes.Values.Count(c => c == State.Active);
        }
        public long SimulateHyperCubes(int cycles)
        {
            var cycle = 0;
            while (cycle < cycles)
            {
                this.nextState = new Dictionary<Cube, State>();
                this.cubes = this.ExpandHyperCube();
                foreach (var kv in this.cubes)
                {
                    int cnt = this.GetActiveHyperCubeNeighbors(kv.Key);
                    this.nextState[kv.Key] = this.GetNextState(kv.Value, cnt);
                }
                this.cubes = nextState;
                cycle++;
            }

            return this.cubes.Values.Count(c => c == State.Active);
        }

        private State GetNextState(State state, int cnt)
        {
            switch (state)
            {
                case State.Inactive:
                    return cnt == 3 ? State.Active : State.Inactive;
                case State.Active:
                    return (cnt == 2 || cnt == 3) ? State.Active : State.Inactive;
                default:
                    throw new Exception($"unhandled state {state}");
            }
        }
        private int GetActiveNeighbors(Cube coord)
        {
            var (xx, yy, zz, _) = coord;
            int cnt = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        var candidate = new Cube(x + xx, y + yy, z + zz);
                        if (candidate == coord) continue;

                        if (this.cubes.TryGetValue(candidate, out State state))
                        {
                            cnt += state == State.Active ? 1 : 0;
                        }
                    }
                }
            }
            return cnt;
        }
        private int GetActiveHyperCubeNeighbors(Cube coord)
        {
            var (xx, yy, zz, ww) = coord;
            int cnt = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = -1; w <= 1; w++)
                        {
                            var candidate = new Cube(x + xx, y + yy, z + zz, w + ww);
                            if (candidate == coord) continue;

                            if (this.cubes.TryGetValue(candidate, out State state))
                            {
                                cnt += state == State.Active ? 1 : 0;
                            }
                        }
                    }
                }
            }
            return cnt;
        }

        private IDictionary<Cube, State> Expand()
        {
            var result = new Dictionary<Cube, State>();
            foreach (var coord in this.cubes.Keys)
            {
                var (xx, yy, zz, _) = coord;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            var candidate = new Cube(x + xx, y + yy, z + zz);
                            if (candidate == coord) continue;

                            if (this.cubes.TryGetValue(candidate, out State state))
                            {
                                result[candidate] = state;
                            }
                            else
                            {
                                result[candidate] = State.Inactive;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private IDictionary<Cube, State> ExpandHyperCube()
        {
            var result = new Dictionary<Cube, State>();
            foreach (var coord in this.cubes.Keys)
            {
                var (xx, yy, zz, ww) = coord;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            for (int w = -1; w <= 1; w++)
                            {
                                var candidate = new Cube(x + xx, y + yy, z + zz, w + ww);
                                if (candidate == coord) continue;

                                if (this.cubes.TryGetValue(candidate, out State state))
                                {
                                    result[candidate] = state;
                                }
                                else
                                {
                                    result[candidate] = State.Inactive;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
