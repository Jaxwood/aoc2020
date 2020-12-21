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
            var leftResult = new ValidationResult()
            {
                Position = context.Position,
                Valid = true,
            };

            foreach (var l in this.left)
            {
                leftResult = l.Validate(context with { Position = leftResult.Position });
                if (!leftResult.Valid)
                {
                    break;
                }
            }

            var rightResult = new ValidationResult()
            {
                Position = context.Position,
                Valid = true,
            };

            foreach (var r in this.right)
            {
                rightResult = r.Validate(context with { Position = rightResult.Position });
                if (!rightResult.Valid)
                {
                    break;
                }
            }

            return new ValidationResult
            {
                Valid = leftResult.Valid || rightResult.Valid,
                Position = leftResult.Valid ? leftResult.Position : rightResult.Position,
            };
        }
    }
}
