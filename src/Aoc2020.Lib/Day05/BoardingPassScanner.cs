using System;
using System.Linq;

namespace Aoc2020.Lib.Day05
{
    public class BoardingPassScanner
    {
        public Seat Scan(string seat)
        {
            var rows = seat[0..^3];
            var lowerBound = 0;
            var upperBound = 127;
            foreach (var row in rows)
            {
                switch (row)
                {
                    case 'F':
                        var range = upperBound - lowerBound + 1;
                        upperBound -= range / 2;
                        break;
                    case 'B':
                        var range2 = upperBound - lowerBound + 1;
                        lowerBound += range2 / 2;
                        break;
                    default:
                        throw new Exception($"Unknown character {row}");
                }
            }

            var selectedRow = upperBound;
            lowerBound = 0;
            upperBound = 7;

            var columns = seat[^3..];
            foreach (var column in columns)
            {
                switch (column)
                {
                    case 'L':
                        var range = upperBound - lowerBound + 1;
                        upperBound -= range / 2;
                        break;
                    case 'R':
                        var range2 = upperBound - lowerBound + 1;
                        lowerBound += range2 / 2;
                        break;
                    default:
                        throw new Exception($"Unknown character {column}");
                }
            }

            return new Seat()
            {
                Row = selectedRow,
                Column = upperBound,
            };
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
