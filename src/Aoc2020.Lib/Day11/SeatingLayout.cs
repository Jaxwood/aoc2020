using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day11
{
    public class SeatingLayout
    {
        private Seat[][] seats;

        public SeatingLayout(Seat[][] seats)
        {
            this.seats = seats;
        }

        public int OccupiedSeats()
        {
            var nextState = new Seat[seats.Length][];
            for (int i = 0; i < nextState.Length; i++)
            {
                nextState[i] = new Seat[this.seats[i].Length];
            }
            while (true)
            {
                for (int y = 0; y < this.seats.Length; y++)
                {
                    for (int x = 0; x < this.seats[y].Length; x++)
                    {
                        nextState[y][x] = this.CalculateState(x, y);
                    }
                }
                if (IsSameState(nextState)) break;
                this.seats = this.CopyArray(nextState);
            }
            return this.seats
                .SelectMany(s => s)
                .Count(s => s.Type == SeatType.Occupied);
        }

        private bool IsSameState(Seat[][] nextState)
        {
            for (int i = 0; i < nextState.Length; i++)
            {
                if (!nextState[i].SequenceEqual(this.seats[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private Seat[][] CopyArray(Seat[][] source)
        {
            return source.Select(s => s.ToArray()).ToArray();
        }

        private Seat CalculateState(int x, int y)
        {
            var seat = this.seats[y][x];
            switch (seat.Type)
            {
                case SeatType.Floor:
                    return new Seat(SeatType.Floor);
                case SeatType.Empty:
                    return this.OccupiedCount(x, y) == 0
                        ? new Seat(SeatType.Occupied)
                        : new Seat(SeatType.Empty);
                case SeatType.Occupied:
                    return this.OccupiedCount(x, y) >= 4
                        ? new Seat(SeatType.Empty)
                        : new Seat(SeatType.Occupied);
                default:
                    throw new Exception($"Unhandled seat type {seat.Type}");
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
            .Where(c => c.Item1 >= 0 && c.Item1 < seats[y].Length)
            .Where(c => c.Item2 >= 0 && c.Item2 < seats.Length)
            .Select(c => this.seats[c.Item2][c.Item1])
            .Count(s => s.Type == SeatType.Occupied);
        }
    }
}
