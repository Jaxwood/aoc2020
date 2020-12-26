using Aoc2020.Lib.Day19.Contracts;

namespace Aoc2020.Lib.Day19.Rules
{
    public record CharacterRule : Validatable
    {
        private readonly int ruleNumber;
        private readonly char character;

        public char Character => this.character;
        public int RuleNumber => this.ruleNumber;

        public CharacterRule(int ruleNumber, char character)
        {
            this.ruleNumber = ruleNumber;
            this.character = character;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            return new ValidationResult
            {
                Valid = context.Candidate[context.Position] == this.character,
                Position = context.Position + 1,
            };
        }
    }
}
