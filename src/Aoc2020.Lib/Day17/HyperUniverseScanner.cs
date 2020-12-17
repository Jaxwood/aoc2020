using System.Collections.Generic;

namespace Aoc2020.Lib.Day17
{
    public class HyperUniverseScanner : Scanable
    {
        public int Scan(IDictionary<Cube, State> cubes, Cube cube)
        {
            var (xx, yy, zz, ww) = cube;
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
                            if (candidate == cube) continue;

                            if (cubes.TryGetValue(candidate, out State state))
                            {
                                cnt += state == State.Active ? 1 : 0;
                            }
                        }
                    }
                }
            }
            return cnt;
        }
    }
}
