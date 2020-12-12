namespace Aoc2020.Lib.Day12
{
    public record Move
    {
        public Move(Direction direction, int units)
        {
            this.Direction = direction;
            this.Units = units;
        }

        public Direction Direction { get; private set; }

        public int Units { get; private set; }
    }

    public enum Direction
    {
        North = 0,
        South = 1,
        East = 2,
        West = 3,
        Left = 4,
        Right = 5,
        Forward = 6,
    }
}
