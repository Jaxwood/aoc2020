using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day04
{
    public class PassportValidator
    {
        private IEnumerable<Passport> passports;

        public PassportValidator(IEnumerable<Passport> passports)
        {
            this.passports = passports;
        }

        public int Validate()
        {
            var valid = 0;
            foreach(var passport in this.passports)
            {
                valid += passport.IsValid() ? 1 : 0;
            }

            return valid;
        }
    }
}