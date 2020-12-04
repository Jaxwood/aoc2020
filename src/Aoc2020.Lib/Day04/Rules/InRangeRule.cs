using System;

namespace Aoc2020.Lib.Day04.Rules
{
    public class InRangeRule : Rule
    {
        private readonly Func<Passport, int?> selector;
        private Func<Passport, int> min;
        private readonly Func<Passport, int> max;

        public InRangeRule(Func<Passport, int?> selector, Func<Passport, int> min, Func<Passport, int> max)
        {
            this.selector = selector;
            this.min = min;
            this.max = max;
        }

        public bool IsValid(Passport passport)
        {
            var value = this.selector(passport);
            if (!value.HasValue) return false;
            return value >= this.min(passport) && value <= this.max(passport);
        }
    }
}
