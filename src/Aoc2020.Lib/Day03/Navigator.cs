using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Lib.Day03
{
    public enum Terrain
    {
        Open = 0,
        Tree = 1,
    }

    public class Navigator
    {
        private readonly IDictionary<(int, int), Terrain> map;

        public Navigator(IDictionary<(int, int), Terrain> map)
        {
            this.map = map;
        }

        public int Drive(int down = 1, int right = 3)
        {
            var endRow = this.map.Keys.Max(k => k.Item1);
            var yMax = this.map.Keys.Max(k => k.Item2) + 1;
            var currentCoord = (0, 0);
            var trees = 0;

            while (endRow >= currentCoord.Item1)
            {
                var (x, y) = currentCoord;
                trees += this.map[(x, y % yMax)] == Terrain.Tree ? 1 : 0;
                currentCoord = (x + down, y + right);
            }

            return trees;
        }
    }
}
