using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day24
{
    public class HexagonalTileFloorBuilder
    {
        private IEnumerable<IEnumerable<Direction>> directions;
        private Dictionary<Cube, bool> cubeState;

        public HexagonalTileFloorBuilder(IEnumerable<IEnumerable<Direction>> directions)
        {
            this.directions = directions;
            this.cubeState = new Dictionary<Cube, bool>();
        }

        public long Flip()
        {
            foreach (var tiles in this.directions)
            {
                var current = new Cube(0, 0, 0);
                foreach (var direction in tiles)
                {
                    var (x, y, z) = current;
                    switch (direction)
                    {
                        case Direction.East:
                            current = new Cube(x + 1, y - 1 , z);
                            break;
                        case Direction.West:
                            current = new Cube(x - 1, y + 1, z);
                            break;
                        case Direction.NorthEast:
                            current = new Cube(x + 1, y, z - 1);
                            break;
                        case Direction.NorthWest:
                            current = new Cube(x, y + 1, z - 1);
                            break;
                        case Direction.SouthEast:
                            current = new Cube(x, y - 1, z + 1);
                            break;
                        case Direction.SouthWest:
                            current = new Cube(x - 1, y, z + 1);
                            break;
                        default:
                            break;
                    }
                }
                this.cubeState[current] = this.Toggle(current);
            }

            return cubeState.Values.Count(c => c == true);
        }

        private bool Toggle(Cube cube)
        {
            if (this.cubeState.ContainsKey(cube))
            {
                var state = this.cubeState[cube];
                return !state;
            }
            else
            {
                this.cubeState.Add(cube, false);
                return true;
            }
        }
    }
}
