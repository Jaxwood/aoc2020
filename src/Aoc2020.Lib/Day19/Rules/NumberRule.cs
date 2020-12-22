using Aoc2020.Lib.Day19.Contracts;

namespace Aoc2020.Lib.Day19.Rules
{
    public record NumberRule : Validatable
    {
        private readonly int ruleNumber;

        public NumberRule(int ruleNumber)
        {
            this.ruleNumber = ruleNumber;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            var result = new ValidationResult()
            {
                Valid = false,
                Position = context.Position,
            };

            foreach (var rule in context.Rules[this.ruleNumber])
            {
                result = rule.Validate(context with { Position = result.Position });
                if (!result.Valid)
                {
                    return new ValidationResult
                    {
                        Valid = false,
                        Position = result.Position,
                    };
                }
            }

            return new ValidationResult
            {
                Valid = true,
                Position = result.Position,
            };
        }
    }
}
