using System;

namespace Aoc2020.Lib.Day17
{
    public record Cube
    {
        public Cube(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = this.X;
            y = this.Y;
            z = this.Z;
        }
    }
}
