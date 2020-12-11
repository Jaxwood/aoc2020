using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day11
{
    public class SeatingLayout
    {
        private IDictionary<(int, int), SeatType> seats;
        private int rows;
        private int columns;

        public SeatingLayout(IDictionary<(int, int), SeatType> seats)
        {
            this.seats = seats;
            this.columns = seats.Max(kv => kv.Key.Item1);
            this.rows = seats.Max(kv => kv.Key.Item2);
        }

        public int OccupiedSeats()
        {
            while (true)
            {
                var nextState = new Dictionary<(int, int), SeatType>();
                foreach (var (x,y) in this.seats.Keys)
                {
                    nextState[(x,y)] = this.CalculateState(x, y);
                }
                if (IsSame(nextState)) break;
                this.seats = nextState;
            }
            return this.seats.Values
                .Count(v => v == SeatType.Occupied);
        }

        private bool IsSame(Dictionary<(int, int), SeatType> other)
        {
            return this.seats.SequenceEqual(other);
        }

        private SeatType CalculateState(int x, int y)
        {
            var seat = this.seats[(x,y)];
            switch (seat)
            {
                case SeatType.Floor:
                    return SeatType.Floor;
                case SeatType.Empty:
                    return this.OccupiedCount(x, y) == 0
                        ? SeatType.Occupied
                        : SeatType.Empty;
                case SeatType.Occupied:
                    return this.OccupiedCount(x, y) >= 4
                        ? SeatType.Empty
                        : SeatType.Occupied;
                default:
                    throw new Exception($"Unhandled seat type {seat}");
            }
        }

        private int OccupiedCount(int x, int y)
        {
            return Enumerable.Range(-1, 3)
                .Select(yy =>
                    Enumerable.Range(-1, 3)
                              .Select(xx => (xx + x, yy + y))
            ).SelectMany(t => t)
            .Where(c => c != (x, y))
            .Where(c => c.Item1 >= 0 && c.Item1 <= this.columns)
            .Where(c => c.Item2 >= 0 && c.Item2 <= this.rows)
            .Select(c => this.seats[(c.Item1, c.Item2)])
            .Count(s => s == SeatType.Occupied);
        }
    }
}
