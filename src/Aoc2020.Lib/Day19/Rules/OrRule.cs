using Aoc2020.Lib.Day19.Contracts;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day19.Rules
{
    public record OrRule : Validatable
    {
        private readonly IEnumerable<Validatable> left;
        private readonly IEnumerable<Validatable> right;

        public OrRule(IEnumerable<Validatable> left, IEnumerable<Validatable> right)
        {
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
                Valid = false,
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
