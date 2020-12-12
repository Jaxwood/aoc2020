using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day12
{
    public class Ship
    {
        private IEnumerable<Move> moves;
        private Direction facing;
        private (int, int) coord;

        public Ship(IEnumerable<Move> moves)
        {
            this.moves = moves;
            this.facing = Direction.East;
            this.coord = (0, 0);
        }

        public int Navigate()
        {
            foreach (var move in this.moves)
            {
                var (x, y) = this.coord;
                switch (move.Direction)
                {
                    case Direction.East:
                        this.coord = (x + move.Units, y);
                        break;
                    case Direction.West:
                        this.coord = (x - move.Units, y);
                        break;
                    case Direction.North:
                        this.coord = (x, y - move.Units);
                        break;
                    case Direction.South:
                        this.coord = (x, y + move.Units);
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        this.facing = this.FaceDirection(move);
                        break;
                    case Direction.Forward:
                        this.coord = this.Move(move);
                        break;
                    default:
                        throw new Exception($"unknown direction {move.Direction}");
                }
            }

            return Math.Abs(this.coord.Item1) + Math.Abs(this.coord.Item2);
        }

        private (int, int) Move(Move move)
        {
            var (x, y) = this.coord;
            switch (this.facing)
            {
                case Direction.North:
                    return (x, y - move.Units);
                case Direction.South:
                    return (x, y + move.Units);
                case Direction.East:
                    return (x + move.Units, y);
                case Direction.West:
                    return (x - move.Units, y);
                default:
                    throw new Exception($"unsupported direction {this.facing}");
            }
        }

        private Direction FaceDirection(Move move)
        {
            var clock = this.GenerateClock(move);
            var ticks = move.Units / 90;
            return clock[ticks];
        }

        private Direction[] GenerateClock(Move move)
        {
            if (move.Direction != Direction.Left && move.Direction != Direction.Right)
            {
                throw new Exception($"{nameof(GenerateClock)} only takes directions left and right");
            }

            var directions = move.Direction == Direction.Left ?
                new Direction[] { Direction.East, Direction.North, Direction.West, Direction.South } :
                new Direction[] { Direction.East, Direction.South, Direction.West, Direction.North };
            var idx = Array.FindIndex(directions, d => d == this.facing);
            return directions.Skip(idx).Concat(directions.Take(idx)).ToArray();
        }
    }
}
