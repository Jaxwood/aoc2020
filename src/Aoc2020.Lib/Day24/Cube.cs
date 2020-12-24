namespace Aoc2020.Lib.Day24
{
    public record Cube
    {
        public Cube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
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

        public static Cube operator +(Cube a, Cube b)
        {
            var (x, y, z) = a;
            var (xx, yy, zz) = b;
            return new Cube(x + xx, y + yy, z + zz);
        }
    }
}
