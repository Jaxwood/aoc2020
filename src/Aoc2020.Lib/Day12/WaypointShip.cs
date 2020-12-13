using System;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day12
{
    public class WaypointShip
    {
        private IEnumerable<Move> moves;
        private Coordinate coord;
        private Coordinate waypoint;

        public WaypointShip(IEnumerable<Move> moves)
        {
            this.moves = moves;
            this.coord = new (0, 0);
            this.waypoint = new (10, 1);
        }

        public int Sail()
        {
            foreach (var move in this.moves)
            {
                switch (move.Direction)
                {
                    case Direction.East:
                    case Direction.West:
                    case Direction.North:
                    case Direction.South:
                        this.waypoint.Update(move);
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        this.waypoint.Rotate(move);
                        break;
                    case Direction.Forward:
                        this.coord += this.waypoint.Multiply(move);
                        break;
                    default:
                        throw new Exception($"unknown direction {move.Direction}");
                }
            }

            return this.coord.Distance();
        }
    }
}
