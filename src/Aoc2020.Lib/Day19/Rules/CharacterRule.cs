using Aoc2020.Lib.Day19.Contracts;

namespace Aoc2020.Lib.Day19.Rules
{
    public record CharacterRule : Validatable
    {
        private readonly char character;

        public CharacterRule(char character)
        {
            this.character = character;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            if (context.OutOfBounds())
            {
                return new ValidationResult
                {
                    Valid = false,
                    Position = context.Position,
                };
            }

            return new ValidationResult
            {
                Valid = context.Candidate[context.Position] == this.character,
                Position = context.Position + 1,
            };
        }
    }
}
