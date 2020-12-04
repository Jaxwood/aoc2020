namespace Aoc2020.Lib.Day04.Rules
{
    public class NotNull : Rule
    {
        public bool IsValid(Passport passport)
        {
            return !string.IsNullOrEmpty(passport.PassportId) &&
                passport.BirthYear != null &&
                passport.IssueYear != null &&
                passport.ExperationYear != null &&
                !string.IsNullOrEmpty(passport.Height) &&
                !string.IsNullOrEmpty(passport.HairColor) &&
                !string.IsNullOrEmpty(passport.EyeColor);
        }
    }
}
