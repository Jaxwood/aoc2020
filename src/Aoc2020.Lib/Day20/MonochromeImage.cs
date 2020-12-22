using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day20
{
    public class MonochromeImage
    {
        private readonly IEnumerable<Tile> tiles;
        private readonly int size;
        private readonly Dictionary<int, HashSet<int>> memory;

        public MonochromeImage(IEnumerable<Tile> tiles)
        {
            this.tiles = tiles;
            this.size = (int)Math.Sqrt(tiles.Count());
            this.memory = new Dictionary<int, HashSet<int>>();
            this.Initialize();
        }

        private void Initialize()
        {
            foreach (var tile in this.tiles)
            {
                var set = new HashSet<int>();
                foreach (var match in this.MatchTiles(tile))
                {
                    set.Add(match.Id);
                }
                this.memory[tile.Id] = set;
            }
        }

        private IEnumerable<Tile> MatchTiles(Tile tile)
        {
            var others = this.tiles.Where(i => i.Id != tile.Id);

            foreach (var other in others)
            {
                if (tile.Compare(other))
                {
                    yield return other;
                }
            }
        }

        public IEnumerable<Tile> CornerTiles()
        {
            return this.memory.Where(c => c.Value.Count == 2)
                              .Select(c => this.tiles.First(t => t.Id == c.Key));
        }

        public long FindSeaMosters()
        {
            var matchedTiles = this.MatchTiles();
            var grid = this.ArrangeTiles(matchedTiles);
            var image = this.BuildMonochromeImage(grid);

            return image.Scan();
        }

        private Tile BuildMonochromeImage(IDictionary<(int, int), int> grid)
        {
            // remove outer border
            foreach (var kv in grid)
            {
                var tile = this.tiles.First(t => t.Id == kv.Value);
                tile.RemoveBorder();
            }
            // merge all tiles
            var result = new List<string>();
            for (int y = 0; y < this.size; y++)
            {
                var yTiles = new List<Tile>();
                for (int x = 0; x < this.size; x++)
                {
                    var tileId = grid[(x, y)];
                    var tile = this.tiles.First(t => t.Id == tileId);
                    yTiles.Add(tile);
                }
                for (int i = 0; i < 8; i++)
                {
                    var line = yTiles.Aggregate("", (acc, n) => acc + n.pixels[i]);
                    result.Add(line);
                }
            }
            // return new tile
            return new Tile(0, result.ToArray());
        }

        private IDictionary<int, (int, Direction)[]> MatchTiles()
        {
            var arrangement = new Dictionary<int, (int, Direction)[]>();
            var start = this.CornerTiles().First();
            var queue = new Queue<int>();
            var visited = new HashSet<int>();
            queue.Enqueue(start.Id);
            visited.Add(start.Id);

            while (queue.Count > 0)
            {
                var tileId = queue.Dequeue();
                var tile = this.tiles.First(t => t.Id == tileId);
                var matches = this.MatchTiles(tile);
                arrangement[tile.Id] = Array.Empty<(int, Direction)>();
                foreach (var match in matches)
                {
                    var direction = tile.Align(match);
                    var current = arrangement[tile.Id];
                    arrangement[tile.Id] = current.Concat(new[] { (match.Id, direction) }).ToArray();
                    if (!visited.Contains(match.Id))
                    {
                        visited.Add(match.Id);
                        queue.Enqueue(match.Id);
                    }
                }
            }

            return arrangement;
        }

        public IDictionary<(int, int), int> ArrangeTiles(IDictionary<int, (int, Direction)[]> arrangement)
        {
            var grid = new Dictionary<(int, int), int>();
            var topleft = arrangement.First(a => a.Value.All(t => t.Item2 == Direction.Bottom || t.Item2 == Direction.Right));
            var queue = new Queue<int>();
            queue.Enqueue(topleft.Key);
            var x = 0; var y = 0;

            while (queue.Count > 0)
            {
                var tile = queue.Dequeue();
                grid.Add((x, y), tile);
                var next = arrangement[tile];
                if (next.Any(t => t.Item2 == Direction.Right))
                {
                    x++;
                    queue.Enqueue(next.First(t => t.Item2 == Direction.Right).Item1);
                }
                else
                {
                    x = 0;
                    var leftTile = grid[(x, y)];
                    y++;
                    if (y != this.size)
                    {
                        next = arrangement[leftTile];
                        queue.Enqueue(next.First(t => t.Item2 == Direction.Bottom).Item1);
                    }
                }
            }

            return grid;
        }
    }
}
