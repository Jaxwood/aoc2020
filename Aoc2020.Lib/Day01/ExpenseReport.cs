using System;
using System.Linq;

namespace Aoc2020.Lib.Day01
{
    public class ExpenseReport
    {
        public long Part1(int[] lines, int target = 2020)
        {
            var start = 0; var end = lines.Length - 1;
            while (start < end)
            {
                var sum = lines[start] + lines[end];
                if (sum == target)
                {
                    return lines[start] * lines[end];
                }
                if (sum > target)
                {
                    end--;
                }
                else
                {
                    start++;
                }
            }

            throw new Exception($"could not find target: {target}");
        }
        public long Part2(int[] lines, int start, int end, int target = 2020)
        {
            if (start >= end)
            {
                throw new Exception("Start is greater than end");
            }
            var next = start + 1;
            var sum = lines[start] + lines[end];
            while (next < end)
            {
                var total = sum + lines[next];
                if (total == target)
                {
                    return lines[start] * lines[end] * lines[next];
                }
                if (total < target)
                {
                    next++;
                }
                else
                {
                    return Part2(lines, start, end - 1);
                }
            }
            return Part2(lines, start + 1, end);
        }
    }
}
