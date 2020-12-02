using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day02
{
    public class PasswordValidator
    {
        private readonly IEnumerable<Policy> policies;

        public PasswordValidator(IEnumerable<Policy> policies)
        {
            this.policies = policies;
        }

        public int Validate()
        {
            return this.policies
                       .Where(p => p.IsValid())
                       .Count();
        }
    }
}
