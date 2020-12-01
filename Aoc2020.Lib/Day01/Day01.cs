using System.Linq;

namespace Aoc2020.Lib.Day01
{
    public class Day01
    {
        public long Part1(int[] lines)
        {
            var length = lines.Count();
            for (var i = 0; i < length; i++)
            {
                for (var j = i + 1; j < length; j++)
                {
                    if (lines[i] + lines[j] == 2020)
                    {
                        return lines[i] * lines[j];
                    }
                }
            }
            return 0;
        }
        public long Part2(int[] lines)
        {
            var length = lines.Count();
            for (var i = 0; i < length; i++)
            {
                for (var j = i + 1; j < length; j++)
                {
                    for (var k = j + 1; k < length; k++)
                    {
                        if (lines[i] + lines[j] + lines[k] == 2020)
                        {
                            return lines[i] * lines[j] * lines[k];
                        }
                    }
                }
            }
            return 0;
        }
    }
}
