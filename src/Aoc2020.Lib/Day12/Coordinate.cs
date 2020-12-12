using System;

namespace Aoc2020.Lib.Day12
{
    public record Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public void Update(Move move)
        {
            switch (move.Direction)
            {
                case Direction.East:
                    this.X += move.Units;
                    break;
                case Direction.West:
                    this.X -= move.Units;
                    break;
                case Direction.North:
                    this.Y += move.Units;
                    break;
                case Direction.South:
                    this.Y -= move.Units;
                    break;
                default:
                    throw new Exception($"unsupported direction {move.Direction}");
            }
        }

        public void Rotate(Move move)
        {
            for (int i = 0; i < move.Units; i += 90)
            {
                var oldX = this.X;
                var oldY = this.Y;
                this.X = move.Direction == Direction.Left ? -1 * oldY : oldY;
                this.Y = move.Direction == Direction.Left ? oldX : -1 * oldX;
            }
        }

        public Coordinate Multiply(Move move)
        {
            return new Coordinate(this.X * move.Units, this.Y * move.Units);
        }

        public int Distance()
        {
            return Math.Abs(this.X) + Math.Abs(this.Y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = this.X;
            y = this.Y;
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
            => new Coordinate(a.X + b.X, a.Y + b.Y);
    }
}
