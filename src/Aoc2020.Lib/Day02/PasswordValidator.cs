using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day02
{
    public class PasswordValidator
    {
        private readonly IEnumerable<Policy> policies;
        private readonly IValidator validator;

        public PasswordValidator(IEnumerable<Policy> policies, IValidator validator)
        {
            this.policies = policies;
            this.validator = validator;
        }

        public int Validate()
        {
            return this.policies.Where(p => this.validator.IsValid(p)).Count();
        }
    }

    public interface IValidator
    {
        bool IsValid(Policy policy);
    }

    public class PartOneValidator : IValidator
    {
        public bool IsValid(Policy policy)
        {
            var found = 0;
            foreach (var c in policy.Password)
            {
                if (c == policy.Letter)
                {
                    found++;
                }
            }
            return found >= policy.Min && found <= policy.Max;
        }
    }

    public class PartTwoValidator : IValidator
    {
        public bool IsValid(Policy policy)
        {
            var first = policy.Password[policy.Min - 1];
            var second = policy.Password[policy.Max - 1];

            return (first == policy.Letter || second == policy.Letter) && first != second;
        }
    }

    public class Policy
    {
        public Policy(int min, int max, char letter, string password)
        {
            Min = min;
            Max = max;
            Letter = letter;
            Password = password;
        }

        public int Min { get; }
        public int Max { get; }
        public char Letter { get; }
        public string Password { get; }
    }
}
