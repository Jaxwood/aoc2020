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
            return passports.Aggregate(0, (acc, next) => {
                return (rules.All(r => r.IsValid(next)) ? 1 : 0) + acc;
            });
        }
    }
}