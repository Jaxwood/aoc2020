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
        private (int, int) waypoint;

        public Ship(IEnumerable<Move> moves)
        {
            this.moves = moves;
            this.facing = Direction.East;
            this.coord = (0, 0);
            this.waypoint = (10, -1);
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
                        this.facing = this.GenerateClock(move.Direction, this.facing)[move.Units / 90];
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

        public int NavigateByWaypoint()
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
                        var ticks = move.Units / 90;
                        var eastwest = x > 0 ?
                            this.GenerateClock(move.Direction, Direction.East)[ticks] :
                            this.GenerateClock(move.Direction, Direction.West)[ticks];
                        var northsouth = y > 0 ?
                            this.GenerateClock(move.Direction, Direction.South)[ticks] :
                            this.GenerateClock(move.Direction, Direction.North)[ticks];
                        if (eastwest == Direction.South || eastwest == Direction.North)
                        {
                            this.waypoint = (
                                eastwest == Direction.South ? Math.Abs(y) : y * -1,
                                northsouth == Direction.East ? Math.Abs(x) : x * -1);
                        }
                        else
                        {
                            this.waypoint = (
                                eastwest == Direction.East ? Math.Abs(x) : -1 * x,
                                northsouth == Direction.South ? Math.Abs(y) : -1 * y);
                        }
                        break;
                    case Direction.Forward:
                        var (cx, cy) = this.coord;
                        this.coord = (move.Units * x + cx, move.Units * y + cy);
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

        private Direction[] GenerateClock(Direction direction, Direction current)
        {
            if (direction != Direction.Left && direction != Direction.Right)
            {
                throw new Exception($"{nameof(GenerateClock)} only takes directions left and right");
            }

            var directions = direction == Direction.Left ?
                new Direction[] { Direction.East, Direction.North, Direction.West, Direction.South } :
                new Direction[] { Direction.East, Direction.South, Direction.West, Direction.North };
            var idx = Array.FindIndex(directions, d => d == current);
            return directions.Skip(idx).Concat(directions.Take(idx)).ToArray();
        }
    }
}
