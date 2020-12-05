using System;

namespace Aoc2020.Lib.Day05
{
    public class BoardingPassScanner
    {
        public Seat Scan(string seat)
        {
            var selectedRow = Find(seat[0..^3], 'F', 'B', 0, 127);
            var selectedColumn = Find(seat[^3..], 'L', 'R', 0, 7);

            return new Seat()
            {
                Row = selectedRow,
                Column = selectedColumn,
            };
        }

        private static int Find(string sequence, char high, char low, int lowerBound, int upperBound)
        {
            foreach (var character in sequence)
            {
                switch (character)
                {
                    case char f when f == high:
                        var upperRange = upperBound - lowerBound + 1;
                        upperBound -= upperRange / 2;
                        break;
                    case char b when b == low:
                        var lowerRange = upperBound - lowerBound + 1;
                        lowerBound += lowerRange / 2;
                        break;
                    default:
                        throw new Exception($"Unknown character {character}");
                }
            }
            return upperBound;
        }

        public record Seat
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int SeatId()
            {
                return this.Row * 8 + this.Column;
            }
        }
    }
}
