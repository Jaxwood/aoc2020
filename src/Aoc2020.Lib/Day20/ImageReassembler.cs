using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day20
{
    public class ImageReassembler
    {
        private readonly IEnumerable<MonochromeImage> images;

        public ImageReassembler(IEnumerable<MonochromeImage> images)
        {
            this.images = images;
        }

        public long Corners()
        {
            var result = new Dictionary<int, HashSet<int>>();
            var size = Math.Sqrt(this.images.Count());

            foreach (var img in this.images)
            {
                var set = new HashSet<int>();
                var matches = this.GetMatchingImages(img);
                foreach (var match in matches)
                {
                    set.Add(match.Tile);
                }
                result[img.Tile] = set;
            }

            return result.Where(c => c.Value.Count == 2)
                         .Aggregate(1L, (acc, n) => acc * n.Key);
        }

        private IEnumerable<MonochromeImage> GetMatchingImages(MonochromeImage img)
        {
            var others = this.images.Where(i => i.Tile != img.Tile);

            foreach (var other in others)
            {
                if (img.Compare(other))
                {
                    yield return other;
                }
            }
        }
    }
}
