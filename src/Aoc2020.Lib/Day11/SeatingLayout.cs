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
        private bool adjecentOnly;
        private int occupiedAmount;

        public SeatingLayout(IDictionary<(int, int), SeatType> seats, bool adjecentOnly = true)
        {
            this.seats = seats;
            this.columns = seats.Max(kv => kv.Key.Item1);
            this.rows = seats.Max(kv => kv.Key.Item2);
            this.adjecentOnly = adjecentOnly;
            this.occupiedAmount = adjecentOnly ? 4 : 5;
        }

        public int OccupiedSeats()
        {
            while (true)
            {
                var nextState = new Dictionary<(int, int), SeatType>();
                foreach (var (x, y) in this.seats.Keys)
                {
                    nextState[(x, y)] = this.CalculateState(x, y);
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
            var seat = this.seats[(x, y)];
            switch (seat)
            {
                case SeatType.Floor:
                    return SeatType.Floor;
                case SeatType.Empty:
                    return this.OccupiedCount(x, y) == 0
                        ? SeatType.Occupied
                        : SeatType.Empty;
                case SeatType.Occupied:
                    return this.OccupiedCount(x, y) >= this.occupiedAmount
                        ? SeatType.Empty
                        : SeatType.Occupied;
                default:
                    throw new Exception($"Unhandled seat type {seat}");
            }
        }

        public int OccupiedCount(int x, int y)
        {
            return new SeatType[]
            {
                North(x,y),
                South(x,y),
                East(x,y),
                West(x,y),
                NorthEast(x,y),
                SouthEast(x,y),
                SouthWest(x,y),
                NorthWest(x,y),
            }.Count(s => s == SeatType.Occupied);
        }

        private SeatType North(int x, int y)
        {
            var up = -1;
            while (y + up >= 0)
            {
                if (this.adjecentOnly) return this.seats[(x, y + up)];
 
                if (this.seats[(x, y + up)] != SeatType.Floor)
                {
                    return this.seats[(x, y + up)];
                }
                up -= 1;
            }
            return SeatType.Floor;
        }
        private SeatType South(int x, int y)
        {
            var down = 1;
            while (y + down <= this.rows)
            {
                if (this.adjecentOnly) return this.seats[(x, y + down)];

                if (this.seats[(x, y + down)] != SeatType.Floor)
                {
                    return this.seats[(x, y + down)];
                }
                down += 1;
            }
            return SeatType.Floor;
        }
        private SeatType West(int x, int y)
        {
            var left = -1;
            while (x + left >= 0)
            {
                if (this.adjecentOnly) return this.seats[(x + left, y)];

                if (this.seats[(x + left, y)] != SeatType.Floor)
                {
                    return this.seats[(x + left, y)];
                }
                left -= 1;
            }
            return SeatType.Floor;
        }
        private SeatType East(int x, int y)
        {
            var right = 1;
            while (x + right <= this.columns)
            {
                if (this.adjecentOnly) return this.seats[(x + right, y)];
                if (this.seats[(x + right, y)] != SeatType.Floor)
                {
                    return this.seats[(x + right, y)];
                }
                right += 1;
            }
            return SeatType.Floor;
        }
        private SeatType NorthEast(int x, int y)
        {
            var up = -1;
            var right = 1;
            while (y + up >= 0 && x + right <= this.columns)
            {
                if (this.adjecentOnly) return this.seats[(x + right, y + up)];
                if (this.seats[(x + right, y + up)] != SeatType.Floor)
                {
                    return this.seats[(x + right, y + up)];
                }
                up -= 1;
                right += 1;
            }
            return SeatType.Floor;
        }
        private SeatType NorthWest(int x, int y)
        {
            var up = -1;
            var left = -1;
            while (y + up >= 0 && x + left >= 0)
            {
                if (this.adjecentOnly) return this.seats[(x + left, y + up)];
                if (this.seats[(x + left, y + up)] != SeatType.Floor)
                {
                    return this.seats[(x + left, y + up)];
                }
                up -= 1;
                left -= 1;
            }
            return SeatType.Floor;
        }
        private SeatType SouthEast(int x, int y)
        {
            var down = 1;
            var right = 1;
            while (y + down <= this.rows && x + right <= this.columns)
            {
                if (this.adjecentOnly) return this.seats[(x + right, y + down)];
                if (this.seats[(x + right, y + down)] != SeatType.Floor)
                {
                    return this.seats[(x + right, y + down)];
                }
                down += 1;
                right += 1;
            }
            return SeatType.Floor;
        }
        private SeatType SouthWest(int x, int y)
        {
            var down = 1;
            var left = -1;
            while (y + down <= this.rows && x + left >= 0)
            {
                if (this.adjecentOnly) return this.seats[(x + left, y + down)];
                if (this.seats[(x + left, y + down)] != SeatType.Floor)
                {
                    return this.seats[(x + left, y + down)];
                }
                down += 1;
                left -= 1;
            }
            return SeatType.Floor;
        }

    }
}
