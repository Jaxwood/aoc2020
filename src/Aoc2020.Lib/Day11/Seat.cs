namespace Aoc2020.Lib.Day11
{
    public record Seat
    {

        public Seat(SeatType type)
        {
            this.Type = type;
        }

        public SeatType Type { get; private set; }
    }

    public enum SeatType
    {
        Empty = 0,
        Occupied = 1,
        Floor = 2
    }
}
