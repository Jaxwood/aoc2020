namespace Aoc2020.Lib.Day19.Contracts
{
    public interface Validatable
    {
        ValidationResult Validate(ValidationContext context);
    }
}
