using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day12
{
    public class WaypointShip : Ship
    {
        private IEnumerable<Move> moves;
        private Compass[] compasses;
        private Coordinate coord;
        private (int, int) waypoint;
        private const int EASTWEST = 0;
        private const int NORTHSOUTH = 1;

        public WaypointShip(IEnumerable<Move> moves, IEnumerable<Compass> compasses)
        {
            this.moves = moves;
            this.coord = new Coordinate(0, 0);
            this.compasses = compasses.ToArray();
            this.waypoint = (10, -1);
        }

        public int Sail()
        {
            foreach (var move in this.moves)
            {
                var (x, y) = this.waypoint;
                switch (move.Direction)
                {
                    case Direction.East:
                        this.waypoint = (x + move.Units, y);
                        break;
                    case Direction.West:
                        this.waypoint = (x - move.Units, y);
                        break;
                    case Direction.North:
                        this.waypoint = (x, y - move.Units);
                        break;
                    case Direction.South:
                        this.waypoint = (x, y + move.Units);
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        this.compasses[EASTWEST].Turn(move);
                        this.compasses[NORTHSOUTH].Turn(move);
                        break;
                    case Direction.Forward:
                        var (X, Y) = this.coord;
                        this.coord = new (move.Units * x + X, move.Units * y + Y);
                        break;
                    default:
                        throw new Exception($"unknown direction {move.Direction}");
                }
            }

            return Math.Abs(this.coord.X) + Math.Abs(this.coord.Y);
        }
    }
}
