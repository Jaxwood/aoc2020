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
            return this.policies.Where(p => p.IsValid()).Count();
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

        public bool IsValid()
        {
            var found = 0;
            foreach (var c in this.Password)
            {
                if (c == this.Letter)
                {
                    found++;
                }
            }
            return found >= this.Min && found <= this.Max;
        }
    }
}
