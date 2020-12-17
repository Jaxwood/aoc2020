using System.Collections.Generic;

namespace Aoc2020.Lib.Day17
{
    public class Universe : Expandable
    {
        public IDictionary<Cube, State> Expand(IDictionary<Cube, State> universe)
        {
            var result = new Dictionary<Cube, State>();
            foreach (var coord in universe.Keys)
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

                            if (universe.TryGetValue(candidate, out State state))
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
    }
}
