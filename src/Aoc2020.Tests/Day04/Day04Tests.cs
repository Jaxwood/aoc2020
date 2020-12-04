using Aoc2020.Lib.Day04;
using Aoc2020.Lib.Day04.Rules;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
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
            parser.Parse(factory);
            var sut = new PassportValidator(factory.Passports, new[] { new NotNull() });
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
            parser.Parse(factory);
            var sut = new PassportValidator(factory.Passports, new Rule[] {
                new InRangeRule(p => p.BirthYear, p => 1920, p => 2020),
                new InRangeRule(p => p.ExperationYear, p => 2020, p => 2030),
                new ContainsRule(p => p.EyeColor, new [] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }),
                new RegexRule(p => p.HairColor, "^#[0-9a-f]{6}$"),
                new InRangeRule(
                    p => !string.IsNullOrEmpty(p.Height) && (p.Height.Contains("in") || p.Height.Contains("cm")) ?
                        Convert.ToInt32(p.Height[0..^2]) :
                        (int?) null, 
                    p => p.Height.EndsWith("cm") ? 150 : 59,
                    p => p.Height.EndsWith("cm") ? 193 : 76),
                new InRangeRule(p => p.IssueYear, p => 2010, p => 2020),
                new RegexRule(p => p.PassportId, "^[0-9]{9}$"),
            });
            var actual = sut.Validate();
            Assert.Equal(expected, actual);
        }
    }

    internal class PassportFactory : IParseFactory<Passport>
    {
        public List<Passport> Passports { get; private set; }
        private Passport passport;

        public PassportFactory()
        {
            this.Passports = new List<Passport>();
        }

        public Passport Create(Line line)
        {
            if (this.passport == null)
            {
                this.passport = new Passport();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                this.Passports.Add(this.passport);
                this.passport = new Passport();
                return null;
            }

            var segments = line.Raw.Split(' ');
            foreach(var segment in segments)
            {
                UpdatePassport(segment.Split(':'));
            }

            return null;
        }

        private void UpdatePassport(string[] segments)
        {
            var identifier = segments[0];
            var value = segments[1];

            switch (segments[0])
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
                    this.passport.Height = value;
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
