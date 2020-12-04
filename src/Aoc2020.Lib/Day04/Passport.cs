namespace Aoc2020.Lib.Day04
{
    public record Passport
    {
        public string PassportId { get; set; }
        public int CountryId { get; set; }
        public int? BirthYear { get; set; }
        public int? IssueYear { get; set; }
        public int? ExperationYear { get; set; }
        public int? Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public Unit Unit { get; set; }

        public bool IsValid(Rule rule)
        {
            return rule.IsValid(this);
        }
    }

    public enum Unit
    {
        Unknown = 0,
        Imperial = 1,
        Metric = 2
    }
}
