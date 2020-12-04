using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day04
{
    public class PassportValidator
    {
        private IEnumerable<Passport> passports;
        private readonly IEnumerable<Rule> rules;

        public PassportValidator(IEnumerable<Passport> passports, IEnumerable<Rule> rules)
        {
            this.passports = passports;
            this.rules = rules;
        }

        public int Validate()
        {
            var valid = 0;
            foreach(var passport in this.passports)
            {
                valid += rules.All(r => r.IsValid(passport)) ? 1 : 0;
            }

            return valid;
        }
    }
}