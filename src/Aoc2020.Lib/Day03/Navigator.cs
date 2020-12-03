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

        public int Drive()
        {
            return 0;
        }
    }
}
