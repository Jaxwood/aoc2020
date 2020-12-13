using System;
using System.Linq;

namespace Aoc2020.Lib.Day12
{
    public class Compass
    {
        private Direction[] counterClockwise;
        private Direction[] clockwise;
        private Direction needle;

        public Compass(Direction orientation)
        {
            this.counterClockwise = new Direction[] { Direction.East, Direction.North, Direction.West, Direction.South };
            this.clockwise = new Direction[] { Direction.East, Direction.South, Direction.West, Direction.North };
            this.needle = orientation;
        }

        public void Turn(Move move) =>
            this.needle = this.GenerateClock(move.Direction)[move.Units / 90];

        public Direction Read() => this.needle;

        private Direction[] GenerateClock(Direction direction)
        {
            if (direction != Direction.Left && direction != Direction.Right)
            {
                throw new Exception($"{nameof(GenerateClock)} only takes directions left and right");
            }

            var directions = direction == Direction.Left ? counterClockwise : clockwise;
            var idx = Array.FindIndex(directions, d => d == this.needle);
            return directions.Skip(idx).Concat(directions.Take(idx)).ToArray();
        }
    }
}
