using Aoc2020.Lib.Util;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day06
{
    public class AnswerFactory : IParseFactory<HashSet<char>>
    {
        private HashSet<char> set;

        public HashSet<char> Create(Line line)
        {
            if (set == null)
            {
                set = new HashSet<char>();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                var copy = this.set;
                this.set = null;
                return copy;
            }

            foreach (var l in line.Raw)
            {
                set.Add(l);
            }
            
            if (line.LastLine)
            {
                return this.set;
            }

            return null;
        }
    }
}
