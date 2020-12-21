using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day20
{
    public record MonochromeImage
    {
        private readonly int tile;
        private string[] data;
        private Dictionary<Alignment, string[]> cache = new();

        public MonochromeImage(int tile, string[] data)
        {
            this.tile = tile;
            this.data = data;

            this.Initialize(data);
        }

        private void Initialize(string[] data)
        {
            this.cache[Alignment.Zero | Alignment.Front] = this.data;

            this.Rotate();
            this.cache[Alignment.Nineteen | Alignment.Front] = this.data;

            this.Rotate();
            this.cache[Alignment.OneHundredEighty | Alignment.Front] = this.data;

            this.Rotate();
            this.cache[Alignment.TwoHundredSeventy | Alignment.Front] = this.data;

            this.Rotate();
            if (!this.data.SequenceEqual(data)) throw new Exception("image not correctly aligned");

            this.Flip();
            this.cache[Alignment.Zero | Alignment.Back] = this.data;

            this.Rotate();
            this.cache[Alignment.Nineteen | Alignment.Back] = this.data;

            this.Rotate();
            this.cache[Alignment.OneHundredEighty | Alignment.Back] = this.data;

            this.Rotate();
            this.cache[Alignment.TwoHundredSeventy | Alignment.Back] = this.data;

            this.Rotate();
            this.Flip();

            if (!this.data.SequenceEqual(data)) throw new Exception("image not correctly aligned");
        }

        public int Tile => this.tile;

        public bool Compare(MonochromeImage other)
        {
            var sides = this.Sides();
            var otherSides = other.Sides();
            var sidesSet = new HashSet<string>(sides);
            var otherSet = new HashSet<string>(otherSides);
            sidesSet.IntersectWith(otherSet);
            return sidesSet.Count > 0;
        }

        private string[] Sides()
        {
            return this.cache.Values.SelectMany(v => new string[] { Top(v), Right(v), Bottom(v), Left(v) }).ToArray();
        }

        private string Top(string[] data)
        {
            return data.First();
        }

        private string Bottom(string[] data)
        {
            return data.Last();
        }

        private string Left(string[] data)
        {
            return data.Aggregate(string.Empty, (acc, n) => acc + n.First());
        }

        private string Right(string[] data)
        {
            return data.Aggregate(string.Empty, (acc, n) => acc + n.Last());
        }

        private void Rotate()
        {
            var columns = new List<string>();
            for (int i = 0; i < this.data[0].Length; i++)
            {
                var column = string.Empty;
                foreach (var row in this.data)
                {
                    column = row[i] + column;
                }
                columns.Add(column);
            }

            this.data = columns.ToArray();
        }

        private void Flip()
        {
            var result = new List<string>();

            foreach (var row in this.data)
            {
                result.Add(String.Join("", row.Reverse()));
            }

            this.data = result.ToArray();
        }
    }
}
