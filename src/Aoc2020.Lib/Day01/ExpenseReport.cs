namespace Aoc2020.Lib.Day01
{
    public class ExpenseReport
    {
        private readonly int[] lines;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lines">The sorted list of integers</param>
        public ExpenseReport(int[] lines)
        {
            this.lines = lines;
        }

        /// <summary>
        /// Find 2 values that sum to a target
        /// </summary>
        /// <param name="start">The start index</param>
        /// <param name="end">The end index</param>
        /// <param name="target">The target value</param>
        /// <returns>The product of the 2 values</returns>
        public long Part1(int start, int end, int target = 2020)
        {
            switch (lines[start] + lines[end])
            {
                case int sum when sum == target:
                    return lines[start] * lines[end];
                case int sum when sum > target:
                    return Part1(start, decrement(end));
                default:
                    return Part1(increment(start), end);
            }
        }

        /// <summary>
        /// Find 3 values that sum to a target.
        /// </summary>
        /// <param name="start">The start index</param>
        /// <param name="end">The end index</param>
        /// <param name="next">The next index</param>
        /// <param name="target">The target value to match</param>
        /// <returns>The product of the 3 values</returns>
        public long Part2(int start, int end, int next, int target = 2020)
        {
            switch (lines[start] + lines[end] + lines[next])
            {
                case int total when total == target:
                    return lines[start] * lines[end] * lines[next];
                case int total when total > target:
                    return Part2(start, decrement(end), increment(start));
                case int _ when next == end:
                    return Part2(increment(start), end, increment(increment(start)));
                default:
                    return Part2(start, end, increment(next));
            }
        }

        /// <summary>
        /// Increment by 1
        /// </summary>
        /// <param name="num">The value to be incremented</param>
        /// <returns>The incremented value</returns>
        private static int increment(int num)
        {
            return num + 1;
        }

        /// <summary>
        /// Decrement by 1
        /// </summary>
        /// <param name="num">The value to be decremented</param>
        /// <returns>The decremented value</returns>
        private static int decrement(int num)
        {
            return num - 1;
        }
    }
}
