namespace Aoc2020.Lib.Day19.Contracts
{
    public interface Validatable
    {
        int RuleNumber { get; }
        ValidationResult Validate(ValidationContext context);
    }
}
