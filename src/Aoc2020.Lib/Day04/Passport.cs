namespace Aoc2020.Lib.Day04
{
    public record Passport
    {
        public string PassportId { get; set; }
        public int CountryId { get; set; }
        public int? BirthYear { get; set; }
        public int? IssueYear { get; set; }
        public int? ExperationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.PassportId) &&
                this.BirthYear != null &&
                this.IssueYear != null &&
                this.ExperationYear != null &&
                !string.IsNullOrEmpty(this.Height) &&
                !string.IsNullOrEmpty(this.HairColor) &&
                !string.IsNullOrEmpty(this.EyeColor);
        }
    }
}
