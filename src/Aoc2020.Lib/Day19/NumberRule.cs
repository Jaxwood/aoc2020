namespace Aoc2020.Lib.Day19
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
                Valid = true,
                Position = context.Position,
            };

            if (context.Position == context.Candidate.Length) return result;

            foreach (var rule in context.Rules[this.ruleNumber])
            {
                result = rule.Validate(context with { Position = result.Position });
                if (!result.Valid)
                {
                    return new ValidationResult
                    {
                        Valid = false
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
