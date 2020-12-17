using System.Collections.Generic;

namespace Aoc2020.Lib.Day17
{
    public interface Expandable
    {
        IDictionary<Cube, State> Expand(IDictionary<Cube, State> universe);
    }
}
