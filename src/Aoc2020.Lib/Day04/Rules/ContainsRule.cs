using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Lib.Day04.Rules
{
    public class ContainsRule : Rule
    {
        private Func<Passport, string> selector;
        private readonly string[] acceptedValues;

        public ContainsRule(Func<Passport, string> selector, string[] acceptedValues)
        {
            this.selector = selector;
            this.acceptedValues = acceptedValues;
        }
        public bool IsValid(Passport passport)
        {
            var value = this.selector(passport);
            if (string.IsNullOrEmpty(value)) return false;
            return this.acceptedValues.Contains(value);
        }
    }
}
