using System;
using System.Text.RegularExpressions;

namespace Aoc2020.Lib.Day04.Rules
{
    public class RegexRule : Rule
    {
        private readonly Func<Passport, string> selector;
        private readonly string pattern;

        public RegexRule(Func<Passport, string> selector, string pattern)
        {
            this.selector = selector;
            this.pattern = pattern;
        }

        public bool IsValid(Passport passport)
        {
            var value = this.selector(passport);
            if (string.IsNullOrEmpty(value)) return false;
            return Regex.IsMatch(value, this.pattern);
        }
    }
}
