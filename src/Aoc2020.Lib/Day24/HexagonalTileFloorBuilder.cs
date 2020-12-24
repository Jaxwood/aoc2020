using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day24
{
    public class HexagonalTileFloorBuilder
    {
        private const bool WHITE = false;
        private const bool BLACK = true;
        private IEnumerable<IEnumerable<Direction>> directions;
        private IEnumerable<Cube> cubeDirections = new Cube[] {
            new Cube(1, -1, 0), new Cube(1, 0, -1), new Cube(0, +1, -1),
            new Cube(-1, 1, 0), new Cube(-1, 0, 1), new Cube(0, -1, 1)
        };
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
                            current = new Cube(x + 1, y - 1, z);
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

            return cubeState.Values.Count(c => c);
        }

        public long Tick(int turns)
        {
            this.Flip();
            var turn = 0;

            while (turn != turns)
            {
                var neighbors = this.GrowGrid();
                this.cubeState = new Dictionary<Cube, bool>(neighbors);
                var nextState = new List<KeyValuePair<Cube, bool>>();
                foreach (var kv in this.cubeState)
                {
                    nextState.Add(CalculateNextState(kv));
                }
                this.cubeState = new Dictionary<Cube, bool>(nextState);
                turn++;
            }
            return this.cubeState.Values.Count(c => c);
        }

        private KeyValuePair<Cube, bool> CalculateNextState(KeyValuePair<Cube, bool> kv)
        {
            var blackTiles = this.CubeNeighbors(kv.Key).Sum(cube => this.cubeState.ContainsKey(cube) && this.cubeState[cube] ? 1 : 0);
            switch (kv.Value)
            {
                case BLACK:
                    if (blackTiles == 0 || blackTiles > 2)
                    {
                        return KeyValuePair.Create(kv.Key, this.Toggle(kv.Key));
                    }
                    else
                    {
                        return KeyValuePair.Create(kv.Key, kv.Value);
                    }
                case WHITE:
                    if (blackTiles == 2)
                    {
                        return KeyValuePair.Create(kv.Key, this.Toggle(kv.Key));
                    }
                    else
                    {
                        return KeyValuePair.Create(kv.Key, kv.Value);
                    }
                default:
                    throw new Exception("unknown state");
            }
        }

        private IEnumerable<KeyValuePair<Cube, bool>> GrowGrid()
        {
            var listOfStates = new Dictionary<Cube, bool>(this.cubeState);
            foreach (var kv in this.cubeState)
            {
                foreach (var neighbor in this.CubeNeighbors(kv.Key))
                {
                    if (!listOfStates.ContainsKey(neighbor))
                    {
                        listOfStates.Add(neighbor, WHITE);
                    }
                }
            }
            return listOfStates;
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
                this.cubeState.Add(cube, WHITE);
                return BLACK;
            }
        }

        private IEnumerable<Cube> CubeNeighbors(Cube cube)
        {
            return this.cubeDirections.Select(other => other + cube);
        }

    }
}
