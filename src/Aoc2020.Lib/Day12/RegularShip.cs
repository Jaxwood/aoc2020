using System;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day12
{
    public class RegularShip
    {
        private readonly IEnumerable<Move> moves;
        private readonly Compass compass;
        private readonly Coordinate coord;

        public RegularShip(IEnumerable<Move> moves, Compass compass)
        {
            this.moves = moves;
            this.coord = new (0, 0);
            this.compass = compass;
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
                        this.coord.Update(move);
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        this.compass.Turn(move);
                        break;
                    case Direction.Forward:
                        this.coord.Update(move with { Direction = this.compass.Read() });
                        break;
                    default:
                        throw new Exception($"unknown {move}");
                }
            }

            return this.coord.Distance();
        }
    }
}
