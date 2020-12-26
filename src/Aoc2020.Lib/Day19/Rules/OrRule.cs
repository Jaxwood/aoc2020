using Aoc2020.Lib.Day19.Contracts;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day19.Rules
{
    public record OrRule : Validatable
    {
        private readonly int ruleNumber;
        private readonly IEnumerable<Validatable> left;
        private readonly IEnumerable<Validatable> right;

        public IEnumerable<Validatable> Left => this.left;

        public IEnumerable<Validatable> Right => this.right;

        public int RuleNumber => this.ruleNumber;


        public OrRule(int ruleNumber, IEnumerable<Validatable> left, IEnumerable<Validatable> right)
        {
            this.ruleNumber = ruleNumber;
            this.left = left;
            this.right = right;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            var leftResult = Check(context, this.left);
            var rightResult = this.Check(context, this.right);

            return new ValidationResult
            {
                Valid = leftResult.Valid || rightResult.Valid,
                Position = leftResult.Valid ? leftResult.Position : (rightResult.Valid ? rightResult.Position : context.Candidate.Length),
            };
        }

        private ValidationResult Check(ValidationContext context, IEnumerable<Validatable> rules)
        {
            var result = new ValidationResult()
            {
                Position = context.Position,
                Valid = true,
            };

            foreach (var r in rules)
            {
                result = r.Validate(context with { Position = result.Position });
                if (!result.Valid)
                {
                    return result;
                }
            }

            return result;
        }
    }
}
