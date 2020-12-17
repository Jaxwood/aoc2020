namespace Aoc2020.Lib.Day17
{
    public record Cube
    {
        public Cube(int x, int y, int z, int w = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int W { get; }

        public void Deconstruct(out int x, out int y, out int z, out int w)
        {
            x = this.X;
            y = this.Y;
            z = this.Z;
            w = this.W;
        }
    }
}
