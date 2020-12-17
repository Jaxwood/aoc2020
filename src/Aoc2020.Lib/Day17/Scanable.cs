using System.Collections.Generic;

namespace Aoc2020.Lib.Day17
{
    public interface Scanable
    {
        int Scan(IDictionary<Cube, State> cubes, Cube cube);
    }
}
