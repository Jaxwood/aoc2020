using System;

namespace Aoc2020.Lib.Day04.Rules
{
    public class PredicateRule : Rule
    {
        private readonly Predicate<Passport> condition;
        private readonly Rule left;
        private Rule right;

        public PredicateRule(Predicate<Passport> condition, Rule left, Rule right)
        {
            this.condition = condition;
            this.left = left;
            this.right = right;
        }
        public bool IsValid(Passport passport)
        {
            if (this.condition(passport))
            {
                return this.left.IsValid(passport);
            }
            else
            {
                return this.right.IsValid(passport);
            }
        }
    }
}
