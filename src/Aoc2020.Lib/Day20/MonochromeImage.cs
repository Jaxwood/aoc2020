using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day20
{
    public class MonochromeImage
    {
        private readonly IEnumerable<Tile> tiles;

        public MonochromeImage(IEnumerable<Tile> tiles)
        {
            this.tiles = tiles;
        }

        public IEnumerable<Tile> Corners()
        {
            var result = new Dictionary<int, HashSet<int>>();
            var size = Math.Sqrt(this.tiles.Count());

            foreach (var tile in this.tiles)
            {
                var set = new HashSet<int>();
                var matches = this.MatchTiles(tile);
                foreach (var match in matches)
                {
                    set.Add(match.Id);
                }
                result[tile.Id] = set;
            }

            return result.Where(c => c.Value.Count == 2)
                         .Select(c => this.tiles.First(t => t.Id == c.Key));
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
    }
}
