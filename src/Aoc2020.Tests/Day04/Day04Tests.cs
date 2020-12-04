using Aoc2020.Lib.Day04;
using Aoc2020.Lib.Day04.Rules;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aoc2020.Tests.Day04
{
    public class Day04Tests
    {
        [Theory]
        [InlineData("Day04/Example1.txt", 2)]
        [InlineData("Day04/Input.txt", 264)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var factory = new PassportFactory();
            var passports = parser.Parse(factory).Where(p => p != null);
            var sut = new PassportValidator(passports, new[] { new NotNull() });
            var actual = sut.Validate();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Day04/Invalid.txt", 0)]
        [InlineData("Day04/Valid.txt", 4)]
        [InlineData("Day04/Input.txt", 224)]
        public void Part2(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var factory = new PassportFactory();
            var passports = parser.Parse(factory).Where(p => p != null);
            var sut = new PassportValidator(passports, new Rule[] {
                new InRangeRule(p => p.BirthYear, 1920, 2020),
                new InRangeRule(p => p.ExperationYear, 2020, 2030),
                new ContainsRule(p => p.EyeColor, new [] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }),
                new RegexRule(p => p.HairColor, "^#[0-9a-f]{6}$"),
                new PredicateRule(
                    p => p.Unit == Unit.Metric,
                    new InRangeRule(p => p.Height, 150, 193),
                    new InRangeRule(p => p.Height, 59, 76)
                ),
                new InRangeRule(p => p.IssueYear, 2010, 2020),
                new RegexRule(p => p.PassportId, "^[0-9]{9}$"),
            });
            var actual = sut.Validate();
            Assert.Equal(expected, actual);
        }
    }

    internal class PassportFactory : IParseFactory<Passport>
    {
        private Passport passport;

        public Passport Create(Line line)
        {
            if (this.passport == null)
            {
                this.passport = new Passport();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                var copy = this.passport;
                this.passport = null;
                return copy;
            }

            var segments = line.Raw.Split(' ');
            foreach(var segment in segments)
            {
                UpdatePassport(segment.Split(':'));
            }

            if (line.LastLine)
            {
                return this.passport;
            }

            return null;
        }

        private void UpdatePassport(string[] segments)
        {
            var identifier = segments[0];
            var value = segments[1];

            switch (identifier)
            {
                case "byr":
                    this.passport.BirthYear = Convert.ToInt32(value);
                    break;
                case "iyr":
                    this.passport.IssueYear = Convert.ToInt32(value);
                    break;
                case "eyr":
                    this.passport.ExperationYear = Convert.ToInt32(value);
                    break;
                case "hgt":
                    if (value.Contains("cm") || value.Contains("in"))
                    {
                        this.passport.Unit = value.Contains("cm") ? Unit.Metric : Unit.Imperial;
                        this.passport.Height = Convert.ToInt32(value[0..^2]);
                    }
                    else
                    {
                        this.passport.Height = Convert.ToInt32(value);
                    }
                    break;
                case "hcl":
                    this.passport.HairColor = value;
                    break;
                case "ecl":
                    this.passport.EyeColor = value;
                    break;
                case "pid":
                    this.passport.PassportId = value;
                    break;
                case "cid":
                    this.passport.CountryId = Convert.ToInt32(value);
                    break;
                default:
                    throw new Exception($"unknown identifier {identifier}");
            }
        }
    }
}
