using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day20
{
    public record Tile
    {
        private readonly int id;
        public string[] pixels;
        private Dictionary<Alignment, string[]> cache = new();
        private bool locked;

        public Tile(int id, string[] pixels)
        {
            this.id = id;
            this.pixels = pixels;
            this.Initialize(pixels);
        }

        private void Initialize(string[] data)
        {
            this.cache[Alignment.Zero | Alignment.Front] = this.pixels;

            this.Rotate();
            this.cache[Alignment.Nineteen | Alignment.Front] = this.pixels;

            this.Rotate();
            this.cache[Alignment.OneHundredEighty | Alignment.Front] = this.pixels;

            this.Rotate();
            this.cache[Alignment.TwoHundredSeventy | Alignment.Front] = this.pixels;

            this.Rotate();
            if (!this.pixels.SequenceEqual(data)) throw new Exception("image not correctly aligned");

            this.Flip();
            this.cache[Alignment.Zero | Alignment.Back] = this.pixels;

            this.Rotate();
            this.cache[Alignment.Nineteen | Alignment.Back] = this.pixels;

            this.Rotate();
            this.cache[Alignment.OneHundredEighty | Alignment.Back] = this.pixels;

            this.Rotate();
            this.cache[Alignment.TwoHundredSeventy | Alignment.Back] = this.pixels;

            this.Rotate();
            this.Flip();

            if (!this.pixels.SequenceEqual(data)) throw new Exception("image not correctly aligned");
        }

        public int Id => this.id;

        public bool Compare(Tile other)
        {
            var borders = this.Borders();
            var otherBorders = other.Borders();
            var borderSet = new HashSet<string>(borders);
            var otherBorderSet = new HashSet<string>(otherBorders);
            borderSet.IntersectWith(otherBorderSet);
            return borderSet.Count > 0;
        }

        public void RemoveBorder()
        {
            var result = new List<string>();

            for (var i = 0; i < this.pixels.Length; i++)
            {
                if (i == 0 || i == this.pixels.Length - 1) continue;
                result.Add(this.pixels[i][1..^1]);
            }

            this.pixels = result.ToArray();
        }

        private (int, int)[] seaMonsterPattern = new (int, int)[]
        {
            (0, 1), (1, 2), (4, 2), (5, 1), (6, 1), (7, 2), (10, 2), (11, 1),
            (12, 1), (13, 2), (16, 2), (17, 1), (18, 0), (18, 1), (19, 1),
        };

        public long Scan()
        {
            for (int i = 0; i < 8; i++)
            {
                var set = this.FindSeaMonsters();
                if (set.Count > 0)
                {
                    return this.HazardCount(set);
                }
                if (i == 3)
                {
                    this.Flip();
                }

                this.Rotate();
            }

            return 0L;
        }

        private long HazardCount(HashSet<(int, int)> set)
        {
            var result = 0L;

            for (int y = 0; y < this.pixels.Length; y++)
            {
                for (int x = 0; x < this.pixels[0].Length; x++)
                {
                    if (this.pixels[y][x] == '#' && !set.Contains((x, y)))
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private HashSet<(int, int)> FindSeaMonsters()
        {
            var result = new HashSet<(int, int)>();
            for (int y = 0; y < this.pixels.Length - 2; y++)
            {
                for (int x = 0; x < this.pixels.Length - 13; x++)
                {
                    var coords = this.seaMonsterPattern.Select(tp => (tp.Item1 + x, tp.Item2 + y));
                    var fields = coords.Select(c => pixels[c.Item2][c.Item1]);
                    if (fields.All(c => c == '#'))
                    {
                        foreach (var coord in coords)
                        {
                            result.Add(coord);
                        }
                    }
                }
            }

            return result;
        }

        private string[] Borders()
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

        public Direction Align(Tile other)
        {
            this.Lock();
            var direction = Direction.NotSet;
            var alignment = Array.Empty<string>();
            foreach (var kv in other.cache)
            {
                if (Top(this.pixels) == Bottom(kv.Value))
                {
                    direction = Direction.Top;
                    alignment = kv.Value;
                }
                else if (Bottom(this.pixels) == Top(kv.Value))
                {
                    direction = Direction.Bottom;
                    alignment = kv.Value;
                }
                else if (Left(this.pixels) == Right(kv.Value))
                {
                    direction = Direction.Left;
                    alignment = kv.Value;
                }
                else if (Right(this.pixels) == Left(kv.Value))
                {
                    direction = Direction.Right;
                    alignment = kv.Value;
                }
            }

            if (!other.Locked())
            {
                other.pixels = alignment;
            }
            return direction;
        }

        private void Lock()
        {
            this.locked = true;
        }

        private bool Locked()
        {
            return this.locked;
        }

        private void Rotate()
        {
            var columns = new List<string>();
            for (int i = 0; i < this.pixels[0].Length; i++)
            {
                var column = string.Empty;
                foreach (var row in this.pixels)
                {
                    column = row[i] + column;
                }
                columns.Add(column);
            }

            this.pixels = columns.ToArray();
        }

        private void Flip()
        {
            var result = new List<string>();

            foreach (var row in this.pixels)
            {
                result.Add(String.Join("", row.Reverse()));
            }

            this.pixels = result.ToArray();
        }
    }
}
