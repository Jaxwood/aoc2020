using System;

namespace Aoc2020.Lib.Day04.Rules
{
    public class InRangeRule : Rule
    {
        private readonly Func<Passport, int?> selector;
        private int min;
        private readonly int max;

        public InRangeRule(Func<Passport, int?> selector, int min, int max)
        {
            this.selector = selector;
            this.min = min;
            this.max = max;
        }

        public bool IsValid(Passport passport)
        {
            var value = this.selector(passport);
            if (!value.HasValue) return false;
            return value >= this.min && value <= this.max;
        }
    }
}
