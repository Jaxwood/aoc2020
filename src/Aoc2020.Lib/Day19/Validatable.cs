namespace Aoc2020.Lib.Day19
{
    public interface Validatable
    {
        ValidationResult Validate(ValidationContext context);
    }
}
